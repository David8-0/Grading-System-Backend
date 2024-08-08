using Grading_System_Backend.Dtos;
using Grading_System_Backend.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grading_System_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private UnitOfWork _unitOfWork;
        public SubjectController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetSubjectsWithStudents()
        {
            var subjects = _unitOfWork.SubjectRepo
                .getAsQuery()
                .AsSplitQuery()
                .Include(s => s.StudentSubjects)
                .ThenInclude(s => s.Student)
                .ThenInclude(s => s.AcademicYear);
            List<SubjectStudentsDto> subjectStudentsList = new List<SubjectStudentsDto>();
            foreach (var subject in subjects)
            {
                var subjectStudentDto = new SubjectStudentsDto();
                subjectStudentDto.SubjectId = subject.id;
                subjectStudentDto.SubjectName = subject.Name;
                subjectStudentDto.studentGrades = new List<StudentGradeDto>();
                subjectStudentDto.MaximumDegree = subject.MaximumDegree;

                subjectStudentDto.succeededStudentsNumber = subject.StudentSubjects.Where(s=>s.Total>=subject.MaximumDegree).Count();
                subjectStudentDto.failedStudentsNumber = subject.StudentSubjects.Where(s=>s.Total<subject.MaximumDegree).Count();

                foreach (var item in subject.StudentSubjects)
                {
                    StudentGradeDto studentGradeDto = new StudentGradeDto();
                    studentGradeDto.Term1 = item.Term1;
                    studentGradeDto.Term2 = item.Term2;
                    studentGradeDto.Total = item.Total;
                    studentGradeDto.Grade = item.Grade;
                    studentGradeDto.StudentId = item.Student.Id;
                    studentGradeDto.StudentName = item.Student.Name;
                    studentGradeDto.AcademicYear = item.Student.AcademicYear.Name;

                    subjectStudentDto.studentGrades.Add(studentGradeDto);
                }
                subjectStudentsList.Add(subjectStudentDto);
            }
            return Ok(subjectStudentsList);

        }



    }
}
