using System.ComponentModel.DataAnnotations;

namespace Grading_System_Backend.Models
{
    public class StudentSubjects
    {
        [Key]
        public int id { get; set; }
        public float Term1 { get; set; }    
        public float Term2 { get; set; }    
        public float Total { get; set; }

        public char Grade {  get; set; }


        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
