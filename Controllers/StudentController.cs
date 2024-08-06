using Grading_System_Backend.Dtos;
using Grading_System_Backend.Models;
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
        private UnitOfWork _unitOfWork;
        public StudentController(UnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult getAll() {
            var students = _unitOfWork.StudentRepo.getAsQuery()
                .AsSplitQuery()
                .Include(s=>s.AcademicYear)
                .Include(s=>s.StudentSubjects);
            List<StudentDto> result = new List<StudentDto>();
            foreach (var item in students)
            {
                result.Add(createStudentsDto(item));
            }
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public IActionResult getById(int id) { 
            var student = _unitOfWork.StudentRepo
                .getAsQuery()
                .AsSplitQuery()
                .Include(s => s.AcademicYear)
                .Include(s => s.StudentSubjects)
                .FirstOrDefault(s => s.Id == id);
        if(student == null)  return NotFound();
        return Ok(createStudentsDto(student));
        }
        [HttpPost]
        public IActionResult add(AddUpdateStudentDto dto)
        {
            if(!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToList();

                return BadRequest(new { Errors = errors });
            }
            Student student = new Student();
            student.Name = dto.Name;
            student.NationalId = dto.NationalId;

            var academicYear = _unitOfWork.AcademicYearRepo.getById(dto.AcademicYearId);
            if (academicYear==null) return BadRequest("invalid id");

            student.AcademicYear = academicYear;

            _unitOfWork.StudentRepo.Add(student);
            _unitOfWork.saveChanges();
            return Created("done ya m3lm",dto);
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
            var student = _unitOfWork.StudentRepo.getAsQuery()
                .Include(s => s.AcademicYear)
                .Include(s => s.StudentSubjects)
                .FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();
            student.Name = dto.Name;
            student.NationalId = dto.NationalId;
            var academicYear = _unitOfWork.AcademicYearRepo.getById(dto.AcademicYearId);
            if (academicYear == null) return BadRequest("invalid id");
            student.AcademicYear = academicYear;
            _unitOfWork.saveChanges();

            return Ok(createStudentsDto(student));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var student = _unitOfWork.StudentRepo
                .getAsQuery()
                .FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();
            _unitOfWork.StudentRepo.delete(student);
            _unitOfWork.saveChanges();
            return StatusCode(204,"Student is deleted");
        }


        private StudentDto createStudentsDto(Student student) { 
            StudentDto studentDto = new StudentDto();
            studentDto.Name = student.Name;
            studentDto.NationalId = student.NationalId;
            studentDto.TotalGrade = student.TotalGrade;
            studentDto.AcademicYear = student?.AcademicYear.Name??"";
            studentDto.Id = student.Id;
            studentDto.grades = new List<StudentGradeDto>();
            foreach (var item in student.StudentSubjects)
            {
                StudentGradeDto studentGradeDto = new StudentGradeDto();
                studentGradeDto.Term1 = item.Term1;
                studentGradeDto.Term2 = item.Term2;
                studentGradeDto.Total = item.Total;
                studentGradeDto.Grade = item.Grade;
                //studentGradeDto.SubjectName = item?.Subject.Name??"";

                studentDto.grades.Add(studentGradeDto);
            }
            return studentDto;
        }
    }
}
