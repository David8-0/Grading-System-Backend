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
    public class StudentSubjectController : ControllerBase
    {
        
        private  SubjectService _subjectService;

        public StudentSubjectController(SubjectService subjectService)
        {
            
            _subjectService = subjectService;
        }

       
        [HttpPost]
        public IActionResult add(AddEditStudentSubjectDto dto) {
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToList();

                return BadRequest(new { Errors = errors });
            }
            StudentSubjects studentSubject;
            try
            {
                studentSubject = _subjectService.Add(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var subjectWithStudents = _subjectService.GetSubjectsWithStudents();


            return StatusCode(201, subjectWithStudents);
        }

        [HttpPut]
        public IActionResult update(AddEditStudentSubjectDto dto) {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToList();

                return BadRequest(new { Errors = errors });
            }
            StudentSubjects studentSubject;
            try
            {
                studentSubject = _subjectService.Update(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var subjectWithStudents = _subjectService.GetSubjectsWithStudents();
            return Ok(subjectWithStudents);
        }

        [HttpGet]
        public IActionResult GetSubjectsWithStudents()
        {
            var subjects = _subjectService.GetSubjectsWithStudents();
            return Ok(subjects);

        }


        
    }
}
