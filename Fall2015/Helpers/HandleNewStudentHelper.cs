using System.Collections.Generic;
using System.Web;
using System.Web.Hosting;
using Fall2015.Models;

namespace Fall2015.Helpers
{
    public class HandleNewStudentHelper
    {
        private Student _student;
        private IEnumerable<int> _compIds;

        public HandleNewStudentHelper(Student student, IEnumerable<int> compIds)
        {
            this._student = student;
            this._compIds = compIds;
        }

        public void HandleNewStudent()
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            if (_compIds != null)
            {
                _student.Competencies = new List<Competency>();
                foreach (var competencyId in _compIds)
                {
                    var competencyToAdd = unitOfWork.CompetenciesRepository.Find(competencyId);
                    _student.Competencies.Add(competencyToAdd);
                }
            }

            unitOfWork.StudentsRepository.InsertOrUpdate(_student);
            unitOfWork.Save();

        }
    }
}