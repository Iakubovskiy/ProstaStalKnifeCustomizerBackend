using Application.Files;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Domain.Files;

namespace API.Files;

[Route("api/files")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IRepository<FileEntity> _fileRepository;
    public FileController(
        IFileService fileService,
        IRepository<FileEntity> fileRepository
    )
    {
        this._fileService = fileService;
        this._fileRepository = fileRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFile(IFormFile file)
    {
        Guid id = Guid.NewGuid();
        string fileUrl = await this._fileService.SaveFile(file, file.FileName);
        FileEntity newFile = new FileEntity(id,fileUrl);
        return Created("/", await this._fileRepository.Create(newFile));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllFiles()
    {
        return Ok(await this._fileRepository.GetAll());
    }
}