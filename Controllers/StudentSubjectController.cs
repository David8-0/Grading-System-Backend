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
            calculateTotalGrade(student.Id);
            return GetSubjectsWithStudents();
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
            calculateTotalGrade(dto.StudentId);
            return GetSubjectsWithStudents();
        }

        [HttpGet]
        public IActionResult GetSubjectsWithStudents()
        {
            var subjects = _unitOfWork.SubjectRepo
                .getAsQuery()
                .AsSplitQuery()
                .Include(s => s.StudentSubjects)
                .ThenInclude(s => s.Student)
                .ThenInclude(s => s.AcademicYear)
                .Select(subject => new
                {
                    subject = subject,
                    OrderedStudentSubjects = subject.StudentSubjects
                        .OrderBy(ss => ss.Student.Name)
                        .ToList()
                })
                .ToList();
            List<SubjectStudentsDto> subjectStudentsList = new List<SubjectStudentsDto>();
            foreach (var item in subjects)
            {
                var subjectStudentDto = new SubjectStudentsDto();
                subjectStudentDto.SubjectId = item.subject.id;
                subjectStudentDto.SubjectName = item.subject.Name;
                subjectStudentDto.studentGrades = new List<StudentGradeDto>();
                subjectStudentDto.MaximumDegree = item.subject.MaximumDegree;

                subjectStudentDto.succeededStudentsNumber = item.subject.StudentSubjects.Where(s => s.Total >= item.subject.MaximumDegree).Count();
                subjectStudentDto.failedStudentsNumber = item.subject.StudentSubjects.Where(s => s.Total < item.subject.MaximumDegree).Count();

                foreach (var studentSubject in item.OrderedStudentSubjects)
                {
                    StudentGradeDto studentGradeDto = new StudentGradeDto();
                    studentGradeDto.Term1 = studentSubject.Term1;
                    studentGradeDto.Term2 = studentSubject.Term2;
                    studentGradeDto.Total = studentSubject.Total;
                    studentGradeDto.Grade = studentSubject.Grade;
                    studentGradeDto.StudentId = studentSubject.Student.Id;
                    studentGradeDto.StudentName = studentSubject.Student.Name;
                    studentGradeDto.AcademicYear = studentSubject.Student.AcademicYear.Name;

                    subjectStudentDto.studentGrades.Add(studentGradeDto);
                }
                subjectStudentsList.Add(subjectStudentDto);
            }
            return Ok(subjectStudentsList);

        }


        private void calculateTotalGrade(int studentId) {
            var student = _unitOfWork.StudentRepo
                .getAsQuery()
                .Include(s => s.StudentSubjects)
                .ThenInclude(s => s.Subject)
                .FirstOrDefault(s=>s.Id == studentId);
            float totalDegrees = 0;
            float maximumDegrees = 0;
            foreach (var item in student.StudentSubjects) {
                totalDegrees += item.Total;
                maximumDegrees += item.Subject.MaximumDegree * 2 ;
            }
            float totalPercentage = (totalDegrees / maximumDegrees) * 100;
            switch (totalPercentage)
            {
                case >= 90:
                    student.TotalGrade = 'A';
                    break;
                case < 90 and >= 75:
                    student.TotalGrade = 'B';
                    break;
                case < 75 and >= 65:
                    student.TotalGrade = 'C';
                    break;
                case < 65 and >= 50:
                    student.TotalGrade = 'D';
                    break;
                case < 50:
                    student.TotalGrade = 'F';
                    break;
            }
            _unitOfWork.saveChanges();
        }
    }
}
