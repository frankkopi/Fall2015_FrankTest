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
    public class HandleNewStudentHelper
    {
        private readonly Student _student;
        private readonly IEnumerable<int> _compIds;

        public HandleNewStudentHelper(Student student, IEnumerable<int> compIds)
        {
            this._student = student;
            this._compIds = compIds;
        }

        public void HandleNewStudent(bool? editStudent)
        {

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

            if (_compIds != null && editStudent == true)
            {
                // Create a EF Context
                using (var ctx = new ApplicationDbContext())
                {
                    // Fetch Student from DB (if its not a NEW entry). Must use Include, else it's not working.
                    var newStudent = ctx.Students
                        .Include(c => c.Competencies)
                        .FirstOrDefault(s => s.StudentId == _student.StudentId) ?? _student;

                    // Clear competencies of the object, else EF will INSERT them.. Silly.
                    newStudent.Competencies.Clear();

                    // update?
                    ctx.Entry(newStudent).State = EntityState.Modified;

                    foreach (var competencyId in _compIds)
                    {
                        var competencyToAdd = ctx.Competencies.Find(competencyId);
                        ctx.Competencies.Attach(competencyToAdd);
                        newStudent.Competencies.Add(competencyToAdd);
                    }

                    // Apply new scalar values (scalar are all the non navigation properties)
                    if (newStudent.StudentId != 0)
                    {
                        _student.StudentId = newStudent.StudentId;
                        ctx.Entry(newStudent).CurrentValues.SetValues(_student);

                    }

                    // update?
                    ctx.Entry(newStudent).State = EntityState.Modified;

                    var objCtx = ((IObjectContextAdapter)ctx).ObjectContext;
                    var objentr = objCtx.ObjectStateManager.GetObjectStateEntries(EntityState.Modified);
                    // Save
                    ctx.SaveChanges();
                }

            }
        }
    }
}



//var compsToAdd = new List<Competency>();

//var currentStudentCompetencies = unitOfWork.StudentsRepository.Find(_student.StudentId).Competencies;
//var selectedCompetencies = new HashSet<Competency>();
//                foreach (var competencyId in _compIds)
//                {
//                    var competency = unitOfWork.CompetenciesRepository.Find(competencyId);
//selectedCompetencies.Add(competency);
//                }

//                foreach (var competency in selectedCompetencies)
//                {
//                    if (!currentStudentCompetencies.Contains(competency))
//                    {
//                        compsToAdd.Add(competency);
//                    }
//                }

//                foreach (var competency in currentStudentCompetencies)
//                {
//                    if (!selectedCompetencies.Contains(competency))
//                    {
//                        unitOfWork.StudentsRepository.Find(_student.StudentId).Competencies.Remove(competency);
//                    }
//                }
//                foreach (var competency in compsToAdd)
//                {
//                    unitOfWork.StudentsRepository.Find(_student.StudentId).Competencies.Add(competency);
//                }

