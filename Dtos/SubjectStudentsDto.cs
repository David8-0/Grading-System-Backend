namespace Grading_System_Backend.Dtos
{
    public class SubjectStudentsDto
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        
        public List<StudentGradeDto> studentGrades { get; set; }

        public int succeededStudentsNumber {  get; set; }
        public int failedStudentsNumber { get; set; }

    }
}
