using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project___Task_Management_Backend.Data;
using Project___Task_Management_Backend.DTO.CloudinaryDtos;
using Project___Task_Management_Backend.Models;
using Project___Task_Management_Backend.Services;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class FileController : ControllerBase
{
    private readonly CloudinaryService _cloudinaryService;
    private readonly AppDbContext _appDbContext ;

    public FileController(CloudinaryService cloudinaryService, AppDbContext appDbContext)
    {
        _cloudinaryService = cloudinaryService;
        _appDbContext = appDbContext;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] FileUploadDto fileUpload)
    {
        var result = await _cloudinaryService.UploadFileAsync(fileUpload.File);

        if (result == null)
            return BadRequest("Upload failed");

        var file = new Doc
        {
            fileName = result.FileName,
            fileURL = result.FileUrl
        };

        _appDbContext.docs.Add(file);
        await _appDbContext.SaveChangesAsync();

        return Ok(file); // returns fileId also
    }


    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string publicId, string resourceType = "raw")
    {
        var result = await _cloudinaryService.DeleteFileAsync(publicId, resourceType);

        if (result?.Result == "ok")
            return Ok("File deleted");

        return BadRequest("Delete failed");
    }

}


