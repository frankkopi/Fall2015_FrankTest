using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fall2015.Repositories;
using Fall2015.ViewModels;

namespace Fall2015.Helpers
{
    public class GetListOfCompetenciesHelper
    {
        // get all competencies and make a ViewBag with a list of AssignedCompetencyData
        public List<AssignedCompetencyData> GetAllCompetencyData(ICompetenciesRepository _competenciesRepository)
        {
            var allCompetencies = _competenciesRepository.All;
            var viewModel = new List<AssignedCompetencyData>();

            foreach (var competency in allCompetencies)
            {
                viewModel.Add(new AssignedCompetencyData()
                {
                    CompetencyId = competency.CompetencyId,
                    Name = competency.Name,
                    Assigned = false
                });
            }

            return viewModel;
        }
    }
}