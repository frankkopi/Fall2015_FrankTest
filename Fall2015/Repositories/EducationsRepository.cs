using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Fall2015.Models;

namespace Fall2015.Repositories
{
    public interface IEducationsRepository
    {
        IQueryable<Education> All { get; }

        IQueryable<Education> AllIncluding(
            params Expression<Func<Education, object>>[] includeProperties);

        Education Find(int? id);
        void InsertOrUpdate(Education education);
        void Delete(int id);
        void Save();
    }

    public class EducationsRepository : IEducationsRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IQueryable<Education> All
        {
            get { return context.Educations; }
        }

        public IQueryable<Education> AllIncluding(params Expression<Func<Education, object>>[] includeProperties)
        {
            IQueryable<Education> query = context.Educations;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Education Find(int? id)
        {
            return context.Educations.Find(id);
        }

        public void InsertOrUpdate(Education education)
        {
            if (education.EducationId == 0) //new
            {
                context.Educations.Add(education);
            }
            else //edit
            {
                context.Entry(education).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            Education education = Find(id);
            context.Educations.Remove(education);
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}