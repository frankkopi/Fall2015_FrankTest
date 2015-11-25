using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Fall2015.Models;

namespace Fall2015.Repositories
{
    public interface IStudentsRepository
    {
        IQueryable<Student> All { get; }

        IQueryable<Student> AllIncluding(
            params Expression<Func<Student, object>>[] includeProperties);

        Student Find(int? id);
        void InsertOrUpdate(Student student);
        void Delete(int id);
        void Save();
    }

    public class StudentsRepository : IStudentsRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IQueryable<Student> All
        {
            get { return context.Students; }
        }

        public IQueryable<Student> AllIncluding(
            params Expression<Func<Student, object>>[] includeProperties)
        {
            IQueryable<Student> query = context.Students;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
        
        public Student Find(int? id)
        {
            return context.Students.Find(id);
        }

        public void InsertOrUpdate(Student student)
        {
            if (student.StudentId == 0) //new
            {
                context.Students.Add(student);
            }
            else //edit
            {
                context.Entry(student).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            Student s = Find(id);
            context.Students.Remove(s);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        
    }
}