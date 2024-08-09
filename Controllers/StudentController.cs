using Grading_System_Backend.Dtos;
using Grading_System_Backend.Models;
using Grading_System_Backend.Services;
using Grading_System_Backend.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grading_System_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService) { 
            
            _studentService = studentService;
        }
        [HttpGet("academicyears")]
        public IActionResult getAcademicYears() { 
            var academicYears = _studentService.GetAcademicYears();
            return Ok(academicYears);
        }

        [HttpGet]
        public IActionResult getAll() {
            var students = _studentService.GetAll();
            return Ok(students);
        }
        [HttpGet("{id:int}",Name ="getByStudentId")]
        public IActionResult getById(int id) { 
            var student = _studentService.GetById(id);
                if(student == null)  return NotFound();
        return Ok(student);
        }
        [HttpPost]
        public IActionResult add(AddUpdateStudentDto dto)
        {
            Student student;
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToList();

                return BadRequest(new { Errors = errors });
            }
            try
            {
                student = _studentService.Add(dto);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
            var newStudentList = _studentService.GetAll();
            string url = Url.Link("getByStudentId", new { id = student.Id })??"";

            return Created(url,newStudentList);
        }
        [HttpPut("{id:int}")]
        public IActionResult update(int id,AddUpdateStudentDto dto) {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToList();

                return BadRequest(new { Errors = errors });
            }
            Student student;
            try
            {
                student = _studentService.Update(id,dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var newStudentList = _studentService.GetAll();

            return Ok(newStudentList);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            Student student;
            try
            {
                student = _studentService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var newStudentList = _studentService.GetAll();
            return Ok(newStudentList);
        }


        
    }
}
