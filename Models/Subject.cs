using System.ComponentModel.DataAnnotations;

namespace Grading_System_Backend.Models
{
    public class Subject
    {
        [Key]
        public int id {  get; set; }
        [Required]
        public string Name { set; get; }
        [Required]

        public int MaximumDegree { set; get; }




        public ICollection<StudentSubjects> StudentSubjects { get; set; }

    }
}
