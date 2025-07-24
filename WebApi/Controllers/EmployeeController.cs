using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // In-memory storage
        private static List<Employee> _employees = new List<Employee>();

        // GET: api/employee
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_employees);
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return NotFound($"Employee with ID {id} not found.");
            return Ok(emp);
        }

        // POST: api/employee
        [HttpPost]
        public IActionResult Post([FromBody] Employee emp)
        {
            if (emp == null)
                return BadRequest("Employee is null.");

            _employees.Add(emp);
            return Ok($"Added employee: {emp.Name} with ID: {emp.Id}");
        }

        // PUT: api/employee/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee emp)
        {
            var existing = _employees.FirstOrDefault(e => e.Id == id);
            if (existing == null)
                return NotFound($"Employee with ID {id} not found.");

            existing.Name = emp.Name;
            existing.Department = emp.Department;
            existing.Salary = emp.Salary;
            existing.Email = emp.Email;

            return Ok($"Updated employee with ID {id}");
        }

        // DELETE: api/employee/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == id);
            if (emp == null)
                return NotFound($"Employee with ID {id} not found.");

            _employees.Remove(emp);
            return Ok($"Deleted employee with ID {id}");
        }
    }
}
