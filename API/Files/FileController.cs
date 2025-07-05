using System.Data.Entity.Core;
using Application.Files;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Domain.Files;
using Infrastructure.Files;

namespace API.Files;

[Route("api/files")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;
    public FileController(
        IFileService fileService,
        IFileRepository fileRepository
    )
    {
        this._fileService = fileService;
        this._fileRepository = fileRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFile(IFormFile file)
    {
        Guid id = Guid.NewGuid();
        string fileUrl = await this._fileService.SaveFile(file, id.ToString() + file.FileName);
        FileEntity newFile = new FileEntity(id,fileUrl);
        return Created("/", await this._fileRepository.Create(newFile));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllFiles()
    {
        return Ok(await this._fileRepository.GetAll());
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFileById(Guid id)
    {
        try
        {
            return Ok(await this._fileRepository.GetById(id));
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteFile(Guid id)
    {
        
        return BadRequest();
        
    }

    [HttpGet("is-used/{id:guid}")]
    public async Task<IActionResult> IsUsed(Guid id)
    {
        return Ok(await this._fileRepository.IsRecordReferencedAsync(id));
    }

    [HttpDelete("remove-unused/")]
    public async Task<IActionResult> RemoveUnusedFiles()
    {
        await this._fileService.RemoveUnusedFiles();
        return Ok();
    }
}