using System.ComponentModel.DataAnnotations;

namespace Grading_System_Backend.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(14,MinimumLength =14)]
        
        public string NationalId { get; set; }
        [Required]
        public string Name { get; set; }

        public  char TotalGrade { get; set; }
        
       

        public ICollection<StudentSubjects> StudentSubjects { get; set; }
        public AcademicYear AcademicYear { get; set; }
    }
}
