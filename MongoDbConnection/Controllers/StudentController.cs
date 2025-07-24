using Microsoft.AspNetCore.Mvc;
using MongoDbConnection.model;
using MongoDbConnection.service;

namespace MongoDbConnection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var students = _studentService.Get();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var student = _studentService.Get(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Student student)
        {
            if (student == null)
                return BadRequest("Student is null");

            var createdStudent = _studentService.Create(student);
            return CreatedAtAction(nameof(Get), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPost("sample")]
        public IActionResult CreateSample()
        {
            var sampleStudent = new Student("anagha", 15);
            var createdStudent = _studentService.Create(sampleStudent);
            return CreatedAtAction(nameof(Get), new { id = createdStudent.Id }, createdStudent);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Student studentIn)
        {
            var existingStudent = _studentService.Get(id);
            if (existingStudent == null)
                return NotFound();

            _studentService.Update(id, studentIn);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var student = _studentService.Get(id);
            if (student == null)
                return NotFound();

            _studentService.Remove(id);
            return NoContent();
        }
    }
}
