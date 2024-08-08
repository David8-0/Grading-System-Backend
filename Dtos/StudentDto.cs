using System.ComponentModel.DataAnnotations;

namespace Grading_System_Backend.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(14, MinimumLength = 14)]
        public string NationalId { get; set; }
        [Required]
        public string Name { get; set; }
        public int AcademicYearId { get; set; }
        public string AcademicYear { get; set; }

        public char TotalGrade { get; set; }

        public List<StudentGradeDto> grades { get; set; }
    }
}
