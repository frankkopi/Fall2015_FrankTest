using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fall2015.Models;
using Fall2015.Repositories;

namespace Fall2015.Helpers
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        private IStudentsRepository _studentsRepository;
        private ICompetenciesRepository _competenciesRepository;

        public IStudentsRepository StudentsRepository
        {
            get
            {
                if (this._studentsRepository == null)
                {
                    this._studentsRepository = new StudentsRepository(_context);
                }
                return _studentsRepository;
            }
        }

        public ICompetenciesRepository CompetenciesRepository
        {
            get
            {
                if (this._competenciesRepository == null)
                {
                    this._competenciesRepository = new CompetenciesRepository(_context);
                }
                return _competenciesRepository;
            }
        }



        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

