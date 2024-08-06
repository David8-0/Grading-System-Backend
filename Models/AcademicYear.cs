using System.ComponentModel.DataAnnotations;

namespace Grading_System_Backend.Models
{
    public class AcademicYear
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Student> students { get; set; }
    }
}
