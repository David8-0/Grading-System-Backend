using System.ComponentModel.DataAnnotations;

namespace Grading_System_Backend.Dtos
{
    public class AddEditStudentSubjectDto
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public float Term1 { get; set; }
        [Required]
        public float Term2 { get; set; }
    }
}
