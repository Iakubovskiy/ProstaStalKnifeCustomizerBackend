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
        
        Stream stream = fileFrom.OpenReadStream();
        SvgDocument svg = SvgDocument.Open<SvgDocument>(stream);
        
        DxfDocument dxf = new DxfDocument();
        foreach (SvgElement svgElement in svg.Children)
        {
            if (svgElement is SvgText svgText)
            {
                string text = svgText.Text.Trim();
                float fontSize = svgText.FontSize.Value;
                string rawSvgFontFamily = svgText.FontFamily;
                string[] rawSvgFontFamilyArray = rawSvgFontFamily.Split(',');
                string svgFontFamily = rawSvgFontFamilyArray[0].Replace("\"", "");
                
                string svgFontFamilyBase64 = FontOptions.GetFontOptions()[svgFontFamily];
                byte[] fontBytes = Convert.FromBase64String(svgFontFamilyBase64);

                double textXLocation = svgText.X.FirstOrDefault().Value;
                double textYLocation = svgText.Y.FirstOrDefault().Value;

                using MemoryStream fontStream = new MemoryStream(fontBytes);
                using SKTypeface typeface = SKTypeface.FromStream(fontStream);
                using SKPaint paint = new SKPaint();
                paint.Typeface = typeface;
                paint.TextSize = fontSize;

                using SKPath path = paint.GetTextPath(text, (float)textXLocation, (float)textYLocation);
                SKMatrix transformMatrix = SKMatrix.CreateScale(1, -1);
                path.Transform(in transformMatrix);
                List<Polyline2D> polylines = ConvertSkiaPathToPolylines(path);
                foreach (Polyline2D polyline in polylines)
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
        List<Polyline2D> polylines = new List<Polyline2D>();
        SKPath.Iterator iterator = path.CreateIterator(false);
        SKPoint[] points = new SKPoint[4];
        Polyline2D currentPolyline = new Polyline2D();
    
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
                    List<SKPoint> quadPoints = ApproximateQuadCurve(points[0], points[1], points[2]);
                    foreach (SKPoint point in quadPoints)
                    {
                        currentPolyline.Vertexes.Add(new Polyline2DVertex(point.X, point.Y));
                    }
                    break;
                    
                case SKPathVerb.Cubic:
                    List<SKPoint> cubicPoints = ApproximateCubicCurve(points[0], points[1], points[2], points[3]);
                    foreach (SKPoint point in cubicPoints)
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
        List<SKPoint> points = new List<SKPoint>();
        
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
        List<SKPoint> points = new List<SKPoint>();
        
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