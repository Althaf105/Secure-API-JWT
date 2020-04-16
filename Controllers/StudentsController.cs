using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_demo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        Student[] students = new Student[]
        {
            new Student { Id = 1, FirstName = "Althaf", LastName = "Edathara", Age = 26 },
            new Student { Id = 2, FirstName = "this", LastName = "is it", Age = 26 },
            new Student { Id = 3, FirstName = "learning", LastName = "api", Age = 40 },
            new Student { Id = 4, FirstName = "test", LastName = "base", Age = 36 },
            new Student { Id = 5, FirstName = "testname", LastName = "testlast", Age = 25 },
            new Student { Id = 6, FirstName = "happy", LastName = "ending", Age = 30 },
            new Student { Id = 7, FirstName = "final", LastName = "touch", Age = 52 },
            
        };

        //Get all students
        
        [HttpGet]
        public IEnumerable<Student> GetAllStudents()
        {
            return students;
        }

        //Get one student by id
        [HttpGet("{id}")]
        public ActionResult<string> GetStudent(int id)
        {
            var student = students.FirstOrDefault((st) => st.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
    }
}