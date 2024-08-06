using System.ComponentModel.DataAnnotations;

namespace Grading_System_Backend.Dtos
{
    public class AddUpdateStudentDto
    {
        [StringLength(14, MinimumLength = 14)]
        public string NationalId { get; set; }
        [Required]
        public string Name { get; set; }
        public int AcademicYearId { get; set; }

    }
}
