using Grading_System_Backend.Models;
using Grading_System_Backend.Repository;

namespace Grading_System_Backend.UnitOfWorks
{
    public class UnitOfWork
    {
        Context _db;
        Repository<Student> studentRepo;
        Repository<Subject> subjectRepo;
        Repository<StudentSubjects> studentSubjectsRepo;
        Repository<AcademicYear> academicYearRepo;

        public UnitOfWork(Context db)
        {
            this._db = db;
        }

        public Repository<AcademicYear> AcademicYearRepo
        {
            get
            {
                if (academicYearRepo == null)
                {
                    academicYearRepo = new Repository<AcademicYear>(_db);
                }
                return academicYearRepo;
            }

        }

        public Repository<StudentSubjects> StudentSubjectRepo
        {
            get
            {
                if (studentSubjectsRepo == null)
                {
                    studentSubjectsRepo = new Repository<StudentSubjects>(_db);
                }
                return studentSubjectsRepo;
            }

        }

        public Repository<Student> StudentRepo
        {
            get
            {
                if (studentRepo == null)
                {
                    studentRepo = new Repository<Student>(_db);
                }
                return studentRepo;
            }

        }

        public Repository<Subject> SubjectRepo
        {
            get
            {
                if (subjectRepo == null)
                {
                    subjectRepo = new Repository<Subject>(_db);
                }
                return subjectRepo;
            }

        }

        public void saveChanges()
        {
            _db.SaveChanges();
        }
    }
}
