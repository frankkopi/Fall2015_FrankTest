using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Fall2015.Models;
using Fall2015.ViewModels;

namespace Fall2015.Helpers
{
    public class HandleStudentHelper
    {
        private readonly Student _student;
        private readonly IEnumerable<int> _compIds;

        public HandleStudentHelper(Student student, IEnumerable<int> compIds)
        {
            this._student = student;
            this._compIds = compIds;
        }

        public void HandleStudent(bool? editStudent)
        {
            // Create new student with given competencies
            if (_compIds != null && editStudent == null)
            {
                 UnitOfWork unitOfWork = new UnitOfWork();
                _student.Competencies = new List<Competency>();

                foreach (var competencyId in _compIds)
                {
                    var competencyToAdd = unitOfWork.CompetenciesRepository.Find(competencyId);
                    _student.Competencies.Add(competencyToAdd);
                }
                unitOfWork.StudentsRepository.InsertOrUpdate(_student);
                unitOfWork.Save();
            }

            // Edit existing student with given competencies
            if (_compIds != null && editStudent == true)
            {
                UnitOfWork unitOfWork = new UnitOfWork();

                // Fetch Student from DB. Must use Include, else it's not working.
                var studentFromDb = unitOfWork.StudentsRepository.AllIncluding(c => c.Competencies)
                                            .FirstOrDefault(s => s.StudentId == _student.StudentId) ?? _student;

                // Clear competencies of the object, else EF will INSERT them.. Silly.
                studentFromDb.Competencies.Clear();

                foreach (var competencyId in _compIds)
                {
                    var competencyToAdd = unitOfWork.CompetenciesRepository.Find(competencyId);
                    // when you use Attach you tell the context that the entity is already in the database, SaveChanges will have no effect over attached entities.
                    unitOfWork.GetApplicationDbContext.Competencies.Attach(competencyToAdd);
                    studentFromDb.Competencies.Add(competencyToAdd);
                }

                // Apply new scalar values (scalar values are all the non navigation properties)
                if (studentFromDb.StudentId != 0)
                {
                    _student.StudentId = studentFromDb.StudentId;
                    unitOfWork.GetApplicationDbContext.Entry(studentFromDb).CurrentValues.SetValues(_student);
                }

                // update?
                unitOfWork.GetApplicationDbContext.Entry(studentFromDb).State = EntityState.Modified;
                    
                // Save
                unitOfWork.Save();
            }
        }
    }
}


// Alternative way of editing a student and given competencies
//
//            if (_compIds != null && editStudent == true)
//            {
//                // Create a EF Context
//                using (var ctx = new ApplicationDbContext())
//                {
//                    // Fetch Student from DB (if its not a NEW entry). Must use Include, else it's not working.
//                    var newStudent = ctx.Students
//                        .Include(c => c.Competencies)
//                        .FirstOrDefault(s => s.StudentId == _student.StudentId) ?? _student;

//// Clear competencies of the object, else EF will INSERT them.. Silly.
//newStudent.Competencies.Clear();

//                    // update?
//                    ctx.Entry(newStudent).State = EntityState.Modified;

//                    foreach (var competencyId in _compIds)
//                    {
//                        var competencyToAdd = ctx.Competencies.Find(competencyId);
//ctx.Competencies.Attach(competencyToAdd);
//                        newStudent.Competencies.Add(competencyToAdd);
//                    }

//                    // Apply new scalar values (scalar are all the non navigation properties)
//                    if (newStudent.StudentId != 0)
//                    {
//                        _student.StudentId = newStudent.StudentId;
//                        ctx.Entry(newStudent).CurrentValues.SetValues(_student);

//                    }

//                    // update?
//                    ctx.Entry(newStudent).State = EntityState.Modified;

//                    var objCtx = ((IObjectContextAdapter)ctx).ObjectContext;
//var objentr = objCtx.ObjectStateManager.GetObjectStateEntries(EntityState.Modified);
//// Save
//ctx.SaveChanges();
//                }


