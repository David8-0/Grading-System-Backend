namespace Grading_System_Backend.Dtos
{
    public class StudentGradeDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public string SubjectName { get; set; }
        public string AcademicYear { get; set; }
        public float Total { get; set; }
        public float Term1 { get; set; }
        public float Term2 { get; set; }
        public char Grade {  get; set; }
    }
}
