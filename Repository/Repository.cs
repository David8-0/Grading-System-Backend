using Grading_System_Backend.Models;

namespace Grading_System_Backend.Repository
{
    public class Repository<T>  where T : class
    {
        Context _db;
        public Repository(Context db)
        {
            this._db = db;
        }

        //public List<T> getAll()
        //{
        //    return _db.Set<T>().ToList();
        //}

        public IQueryable<T> getAsQuery()
        {
            return _db.Set<T>();
        }
        public T getById(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public void Add(T item)
        {
            _db.Set<T>().Add(item);
        }

        public void update(T item)
        {
            _db.Set<T>().Update(item);
        }

        public void delete(T item)
        {
            _db.Set<T>().Remove(item);
        }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
