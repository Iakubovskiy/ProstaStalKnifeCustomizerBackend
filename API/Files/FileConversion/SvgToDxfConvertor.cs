using Application.Files.FileConversion;
using Application.Files.FileConversion.SvgToDxfConvertor;
using Microsoft.AspNetCore.Mvc;

namespace API.Files.FileConversion;

[Route("api/files/convert/svg-to-dxf")]
[ApiController]
public class SvgToDxfConvertor : ControllerBase
{
    private readonly IFileConversionService _svgToDxfConvertorService;
    public SvgToDxfConvertor(IFileConversionService svgToDxfConvertorService)
    {
        this._svgToDxfConvertorService = svgToDxfConvertorService;
    }

    [HttpPost]
    public async Task<IActionResult> Convert(IFormFile file)
    {
        if (!file.FileName.EndsWith(".svg"))
        {
            return BadRequest("Only .svg files are supported");
        }

        byte[] dxfFile = await this._svgToDxfConvertorService.ConvertFile(file, "svg", "dxf");
        string outputFileName = Path.ChangeExtension(file.FileName, ".dxf");
        return File(dxfFile, "application/octet-stream", outputFileName);
    }
}