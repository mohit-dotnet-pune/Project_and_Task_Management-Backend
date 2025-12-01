using global::Project___Task_Management_Backend.Data;
using global::Project___Task_Management_Backend.DTO.FileDtos;
using global::Project___Task_Management_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project___Task_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FileController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/File
        //[HttpPost]
        //public async Task<IActionResult> CreateFile(CreateFileDto dto)
        //{
        //    var file = new Doc
        //    {
        //        fileName = dto.fileName,
        //        fileURL = dto.fileURL
        //    };

        //    _context.docs.Add(file);
        //    await _context.SaveChangesAsync();

        //    return Ok(file); // returns fileId also
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
           List<Doc> list = _context.docs.ToList();
            return Ok(list);
        }


    }
}

