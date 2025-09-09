using Application.Files.FileConversion.FontOption;
using Microsoft.AspNetCore.Http;
using netDxf;
using netDxf.Entities;
using SkiaSharp;
using Svg;

namespace Application.Files.FileConversion.SvgToDxfConvertor;

public class SvgToDxfConvertorService: IFileConversionService
{
    public async Task<byte[]> ConvertFile(IFormFile fileFrom, string fromFormat, string targetFormat)
    {
        if (fromFormat == targetFormat)
        {
            throw new Exception("File formats are not supported");
        }

        if (!fromFormat.Equals("svg", StringComparison.CurrentCultureIgnoreCase) 
            || !targetFormat.Equals("dxf", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new Exception("You are using svg to dxf convertor, please use different converter");
        }
        
        var stream = fileFrom.OpenReadStream();
        var svg = SvgDocument.Open<SvgDocument>(stream);
        
        var dxf = new DxfDocument();
        foreach (var svgElement in svg.Children)
        {
            if (svgElement is SvgText svgText)
            {
                string text = svgText.Text.Trim();
                float fontSize = svgText.FontSize.Value;
                string rawSvgFontFamily = svgText.FontFamily;
                var rawSvgFontFamilyArray = rawSvgFontFamily.Split(',');
                string svgFontFamily = rawSvgFontFamilyArray[0].Replace("\"", "");
                
                string svgFontFamilyBase64 = FontOptions.GetFontOptions()[svgFontFamily];
                byte[] fontBytes = Convert.FromBase64String(svgFontFamilyBase64);

                double textXLocation = svgText.X.FirstOrDefault().Value;
                double textYLocation = svgText.Y.FirstOrDefault().Value;

                using var fontStream = new MemoryStream(fontBytes);
                using var typeface = SKTypeface.FromStream(fontStream);
                using var paint = new SKPaint();
                paint.Typeface = typeface;
                paint.TextSize = fontSize;

                using var path = paint.GetTextPath(text, (float)textXLocation, (float)textYLocation);
                var transformMatrix = SKMatrix.CreateScale(1, -1);
                path.Transform(in transformMatrix);
                var polylines = ConvertSkiaPathToPolylines(path);
                foreach (var polyline in polylines)
                {
                    dxf.Entities.Add(polyline);
                }
            }
        }
        
        string outputFileName = Path.ChangeExtension(fileFrom.FileName, ".dxf");
        string tempFilePath = Path.Combine(Path.GetTempPath(), outputFileName);
        dxf.Save(tempFilePath);
        byte[] dxfFileBytes = File.ReadAllBytes(tempFilePath);
        
        if (File.Exists(tempFilePath))
        {
            File.Delete(tempFilePath);
        }
        
        return dxfFileBytes;
    }
    
    private List<Polyline2D> ConvertSkiaPathToPolylines(SKPath path)
    {
        var polylines = new List<Polyline2D>();
        var iterator = path.CreateIterator(false);
        var points = new SKPoint[4];
        var currentPolyline = new Polyline2D();
    
        SKPathVerb verb;
        while ((verb = iterator.Next(points)) != SKPathVerb.Done)
        {
            switch (verb)
            {
                case SKPathVerb.Move:
                    if (currentPolyline.Vertexes.Count > 0)
                    {
                        polylines.Add(currentPolyline);
                        currentPolyline = new Polyline2D();
                    }
                    currentPolyline.Vertexes.Add(new Polyline2DVertex(points[0].X, points[0].Y));
                    break;
                
                case SKPathVerb.Line:
                    currentPolyline.Vertexes.Add(new Polyline2DVertex(points[1].X, points[1].Y));
                    break;
                
                case SKPathVerb.Quad:
                    var quadPoints = ApproximateQuadCurve(points[0], points[1], points[2]);
                    foreach (var point in quadPoints)
                    {
                        currentPolyline.Vertexes.Add(new Polyline2DVertex(point.X, point.Y));
                    }
                    break;
                    
                case SKPathVerb.Cubic:
                    var cubicPoints = ApproximateCubicCurve(points[0], points[1], points[2], points[3]);
                    foreach (var point in cubicPoints)
                    {
                        currentPolyline.Vertexes.Add(new Polyline2DVertex(point.X, point.Y));
                    }
                    break;
                
                case SKPathVerb.Close:
                    if (currentPolyline.Vertexes.Count > 0)
                    {
                        currentPolyline.IsClosed = true;
                    }
                    break;
            }
        }
    
        if (currentPolyline.Vertexes.Count > 0)
        {
            polylines.Add(currentPolyline);
        }
    
        return polylines;
    }
    
    private List<SKPoint> ApproximateQuadCurve(SKPoint p0, SKPoint p1, SKPoint p2, int segments = 10)
    {
        var points = new List<SKPoint>();
        
        for (int i = 1; i <= segments; i++)
        {
            float t = (float)i / segments;
            float x = (1 - t) * (1 - t) * p0.X + 2 * (1 - t) * t * p1.X + t * t * p2.X;
            float y = (1 - t) * (1 - t) * p0.Y + 2 * (1 - t) * t * p1.Y + t * t * p2.Y;
            points.Add(new SKPoint(x, y));
        }
        
        return points;
    }
    
    private List<SKPoint> ApproximateCubicCurve(SKPoint p0, SKPoint p1, SKPoint p2, SKPoint p3, int segments = 10)
    {
        var points = new List<SKPoint>();
        
        for (int i = 1; i <= segments; i++)
        {
            float t = (float)i / segments;
            float t2 = t * t;
            float t3 = t2 * t;
            float mt = 1 - t;
            float mt2 = mt * mt;
            float mt3 = mt2 * mt;
            
            float x = mt3 * p0.X + 3 * mt2 * t * p1.X + 3 * mt * t2 * p2.X + t3 * p3.X;
            float y = mt3 * p0.Y + 3 * mt2 * t * p1.Y + 3 * mt * t2 * p2.Y + t3 * p3.Y;
            
            points.Add(new SKPoint(x, y));
        }
        
        return points;
    }
}