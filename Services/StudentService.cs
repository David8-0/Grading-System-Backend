using Grading_System_Backend.Dtos;
using Grading_System_Backend.Models;
using Grading_System_Backend.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Grading_System_Backend.Services
{
    public class StudentService
    {
        private UnitOfWork _unitOfWork;
        public StudentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<AcademicYear> GetAcademicYears()
        {
            List<AcademicYear> academicYears = _unitOfWork.AcademicYearRepo.getAsQuery().ToList();
            return academicYears;
        }

        public List<StudentDto> GetAll()
        {
            var students = _unitOfWork.StudentRepo.getAsQuery()
                .AsSplitQuery()
                .Include(s => s.AcademicYear)
                .Include(s => s.StudentSubjects);
            List<StudentDto> result = new List<StudentDto>();
            foreach (var item in students)
            {
                result.Add(createStudentsDto(item));
            }
            return result;
        }

        public StudentDto GetById(int id)
        {
            var student = _unitOfWork.StudentRepo
                .getAsQuery()
                .AsSplitQuery()
                .Include(s => s.AcademicYear)
                .Include(s => s.StudentSubjects)
                .ThenInclude(s => s.Subject)
                .FirstOrDefault(s => s.Id == id);
            return createStudentsDto(student);
        }

        public Student Add(AddUpdateStudentDto dto)
        {
            
            var existedStudent = _unitOfWork.StudentRepo.getAsQuery().FirstOrDefault(s => s.NationalId == dto.NationalId);
            if (existedStudent != null) throw new Exception("this national ID already exists!");

            var academicYear = _unitOfWork.AcademicYearRepo.getById(dto.AcademicYearId);
            if (academicYear == null) throw new Exception("invalid Academic Year id");

            Student student = new Student();
            student.Name = dto.Name;
            student.NationalId = dto.NationalId;
            student.AcademicYear = academicYear;
            student.TotalGrade = 'z';

            _unitOfWork.StudentRepo.Add(student);
            _unitOfWork.saveChanges();
            return student;
        }

        public Student Update(int id, AddUpdateStudentDto dto)
        {
            
            var existedStudent = _unitOfWork.StudentRepo.getAsQuery().FirstOrDefault(s => s.NationalId == dto.NationalId);
            if (existedStudent != null && existedStudent.Id != id) throw new Exception("this national ID already exists!");

            var academicYear = _unitOfWork.AcademicYearRepo.getById(dto.AcademicYearId);
            if (academicYear == null) throw new Exception("invalid academic year id");

            var student = _unitOfWork.StudentRepo.getAsQuery()
                .Include(s => s.AcademicYear)
                .Include(s => s.StudentSubjects)
                .FirstOrDefault(s => s.Id == id);

            if (student == null) throw new Exception("Student is not found!");

            student.Name = dto.Name;
            student.NationalId = dto.NationalId;
            
            student.AcademicYear = academicYear;
            _unitOfWork.saveChanges();

            return student;
        }

        public Student Delete(int id)
        {
            var student = _unitOfWork.StudentRepo
                .getAsQuery()
                .FirstOrDefault(s => s.Id == id);
            if (student == null) throw new Exception("Student doesn't exists");
            _unitOfWork.StudentRepo.delete(student);
            _unitOfWork.saveChanges();
            return student;
        }

        private StudentDto createStudentsDto(Student student)
        {
            StudentDto studentDto = new StudentDto();
            studentDto.Name = student.Name;
            studentDto.NationalId = student.NationalId;
            studentDto.TotalGrade = student.TotalGrade;
            studentDto.AcademicYearId = student?.AcademicYear.Id ?? 0;
            studentDto.AcademicYear = student?.AcademicYear.Name ?? "";

            studentDto.Id = student.Id;
            studentDto.grades = new List<StudentGradeDto>();
            foreach (var item in student.StudentSubjects)
            {
                StudentGradeDto studentGradeDto = new StudentGradeDto();
                studentGradeDto.SubjectName = item?.Subject?.Name ?? "";
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
