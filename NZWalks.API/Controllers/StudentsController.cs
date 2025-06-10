using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.API.Controllers
{

    //https://localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents() {
            string[] studentNames = new string[] { "John Doe", "Jane Smith", "Alice Johnson" };

            return Ok(studentNames);
        }
    }
}
