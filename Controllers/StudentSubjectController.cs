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
    public class StudentSubjectController : ControllerBase
    {
        private UnitOfWork _unitOfWork;
        public StudentSubjectController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            var student = _unitOfWork.StudentRepo.getAsQuery().FirstOrDefault(s=>s.Id == dto.StudentId);
            if (student == null) return BadRequest("student is invalid");

            var subject = _unitOfWork.SubjectRepo.getAsQuery().FirstOrDefault(s => s.id == dto.SubjectId);
            if (subject == null) return BadRequest("Subject is invalid");

            var studentSubject = _unitOfWork.StudentSubjectRepo
                .getAsQuery()
                .AsSplitQuery()
                .Include(s=>s.Subject)
                .Include(s=>s.Student)
                .FirstOrDefault(s=>s.Student.Id == dto.StudentId && s.Subject.id == dto.SubjectId);

            if (studentSubject != null) return BadRequest("there is already record");

            if (dto.Term1 > subject.MaximumDegree || dto.Term2 > subject.MaximumDegree)
                return BadRequest("Term value exceeded the maximum range");

            StudentSubjects studentSubjects = new StudentSubjects();
            studentSubjects.Student = student;
            studentSubjects.Subject = subject;
            studentSubjects.Term1 = dto.Term1;
            studentSubjects.Term2 = dto.Term2;
            studentSubjects.Total = dto.Term1 + dto.Term2;
            float totalPercentage = (studentSubjects.Total/(subject.MaximumDegree*2))*100;
            switch (totalPercentage)
            {
                case >= 90:
                    studentSubjects.Grade = 'A';
                    break;
                case < 90 and >= 75:
                    studentSubjects.Grade = 'B';
                    break;
                case < 75 and >= 65:
                    studentSubjects.Grade = 'C';
                    break;
                case < 65 and >= 50:
                    studentSubjects.Grade = 'D';
                    break;
                case < 50:
                    studentSubjects.Grade = 'F';
                break;
            }
            _unitOfWork.StudentSubjectRepo.Add(studentSubjects);
            _unitOfWork.saveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult update(AddEditStudentSubjectDto dto) {
            var studentSubjects = _unitOfWork.StudentSubjectRepo
                .getAsQuery()
                .Include(s=>s.Subject)
                .FirstOrDefault(s => s.Subject.id == dto.SubjectId && s.Student.Id == dto.StudentId );
            if (studentSubjects == null) return NotFound();
            studentSubjects.Term1 = dto.Term1;
            studentSubjects.Term2 = dto.Term2;
            studentSubjects.Total = dto.Term1 + dto.Term2;
            float totalPercentage = (studentSubjects.Total / (studentSubjects.Subject.MaximumDegree*2)) * 100;
            switch (totalPercentage)
            {
                case >= 90:
                    studentSubjects.Grade = 'A';
                    break;
                case < 90 and >= 75:
                    studentSubjects.Grade = 'B';
                    break;
                case < 75 and >= 65:
                    studentSubjects.Grade = 'C';
                    break;
                case < 65 and >= 50:
                    studentSubjects.Grade = 'D';
                    break;
                case < 50:
                    studentSubjects.Grade = 'F';
                    break;
            }
            _unitOfWork.StudentSubjectRepo.update(studentSubjects);
            _unitOfWork.saveChanges();
            return Ok();
        }
    }
}
