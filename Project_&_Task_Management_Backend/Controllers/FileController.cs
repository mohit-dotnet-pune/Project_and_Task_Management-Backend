using Microsoft.AspNetCore.Mvc;

using global::Project___Task_Management_Backend.Data;
using global::Project___Task_Management_Backend.DTO.FileDtos;
using global::Project___Task_Management_Backend.Models;

namespace Project___Task_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FileController(AppDbContext context)
        {
            _context = context;
        }

       

        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
           List<Doc> list = _context.docs.ToList();
            return Ok(list);
        }


    }
}

