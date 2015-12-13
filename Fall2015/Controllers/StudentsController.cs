using Fall2015.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fall2015.Helpers;
using Fall2015.Repositories;
using Fall2015.ViewModels;

// franks project
namespace Fall2015.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IEmailer _emailer;
        private readonly ICompetencyHeadersRepository _competencyHeadersRepository;
        private readonly IEducationsRepository _educationsRepository;
        private readonly ICompetenciesRepository _competenciesRepository;

        public StudentsController(IStudentsRepository studentsRepository, IEducationsRepository  educationsRepository,
            ICompetencyHeadersRepository competencyHeadersRepository, IEmailer emailer, ICompetenciesRepository competenciesRepository)
        {
            this._studentsRepository = studentsRepository;
            this._educationsRepository = educationsRepository;
            this._competencyHeadersRepository = competencyHeadersRepository;
            this._emailer = emailer;
            this._competenciesRepository = competenciesRepository;
        }

        public ActionResult GridExample()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult Index(string searchString = "")
        {
            IEnumerable<Student> students = _studentsRepository.All;

            if (!string.IsNullOrEmpty(searchString) && searchString.Contains(" "))
            {
                int spaceIndex = searchString.IndexOf(" ");
                int toLastIndex = searchString.Length - spaceIndex;
                var substring1 = searchString.Substring(0, spaceIndex);
                var substring2 = searchString.Substring(spaceIndex + 1, toLastIndex - 1);
                students = _studentsRepository.All.Where(s => s.Firstname == (substring1) && s.Lastname == (substring2));
            }
            else if (!string.IsNullOrEmpty(searchString))
            {
                students = _studentsRepository.All.Where(s => s.Firstname.Contains(searchString) || s.Lastname.Contains(searchString)).OrderBy(x => x.Lastname);
            }

            StudentIndexViewModel viewModel = new StudentIndexViewModel
            {
                Students = students.ToList(),
                CompetencyHeaders = _competencyHeadersRepository.All.ToList()
            };

            return View(viewModel);

        }


        [Authorize]
        [HttpGet]
        public ActionResult Edit(int studentId)
        {
            //look up a Student in the db
            Student student = _studentsRepository.Find(studentId);

            PopulateAssignedCompetencyData(student);

            CreateEditStudentViewModel viewModel = new CreateEditStudentViewModel()
            {
                Student = student,
                Educations = _educationsRepository.All.ToList(),
                CompetencyHeaders = _competencyHeadersRepository.AllIncluding(a => a.Competencies).ToList()
            };
            return View(viewModel);
        }

        // Creates a ViewBag with a list of AssignedCompetencyData (all competencies with flags if they are currently assigned to the student or not)  
        private void PopulateAssignedCompetencyData(Student student)
        {
            var allCompetencies = _competenciesRepository.All;
            var studentCompetencies = new HashSet<int>(student.Competencies.Select(c => c.CompetencyId));
            var viewModel = new List<AssignedCompetencyData>();
        
            foreach (var competency in allCompetencies)
            {
                viewModel.Add(new AssignedCompetencyData()
                {
                    CompetencyId = competency.CompetencyId,
                    Name = competency.Name,
                    Assigned = studentCompetencies.Contains(competency.CompetencyId)
                });
            }

            ViewBag.Competencies = viewModel;
        }


        [HttpPost]
        public ActionResult Edit(Student student, HttpPostedFileBase image, IEnumerable<int> compIds)
        {
            if (ModelState.IsValid)
            {
                //student.SaveImage(image, Server.MapPath("~"), "/ProfileImages/");
                string path = Server != null ? Server.MapPath("~") : "";  
                student.SaveImage(image, path, "/ProfileImages/");

                HandleStudentHelper handleStudentHelper = new HandleStudentHelper(student, compIds);
                handleStudentHelper.HandleStudent(true);

                return RedirectToAction("Index");
            }

            CreateEditStudentViewModel viewModel = new CreateEditStudentViewModel()
            {
                Student = student,
                Educations = _educationsRepository.All.ToList(),
                CompetencyHeaders = _competencyHeadersRepository.AllIncluding(a => a.Competencies).ToList()
            };
            return View(viewModel);
        }


        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            CreateEditStudentViewModel viewModel = new CreateEditStudentViewModel
                {
                    Student = new Student(),
                    Educations = new List<Education>(_educationsRepository.All),
                    CompetencyHeaders = _competencyHeadersRepository.AllIncluding(x => x.Competencies).ToList()
            };

            GetListOfCompetenciesHelper getListOfCompetenciesHelper = new GetListOfCompetenciesHelper();
            ViewBag.Competencies = getListOfCompetenciesHelper.GetAllCompetencyData(_competenciesRepository); 

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Email,MobilePhone,EducationId")]Student student,
        HttpPostedFileBase image, IEnumerable<int> compIds)
        {
            if (ModelState.IsValid)
            {
                //student.SaveImage(image, Server.MapPath("~"), "/ProfileImages/");
                string path = Server != null ? Server.MapPath("~") : "";
                student.SaveImage(image, path, "/ProfileImages/");

                // Creates a UnitOfWork object (for saving different DbSet in one session to db) and creates a list of all 
                // competencies to the student and save student to db. This code is also used in the AccountController 
                // in the Register method
                HandleStudentHelper handleStudentHelper = new HandleStudentHelper(student, compIds);
                handleStudentHelper.HandleStudent(null);

                _emailer.Send("Welcome to our website...");

                return View("Thanks");
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Details(int? studentId)
        {
            if (studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = _studentsRepository.Find(studentId);

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? studentId)
        {
            if (studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = _studentsRepository.Find(studentId);

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        // POST: /Students/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int studentId)
        {
            Student student = _studentsRepository.Find(studentId);
            _studentsRepository.Delete(studentId);

            //return View("DeleteConfirmed", student);

            _studentsRepository.Delete(studentId);
            _studentsRepository.Save();

            return RedirectToAction("Index");
        }

    }
}



// Example of saving a list of competencies to student. part of the many-to-many relationsship between Student and Competency
// Here is the navigation proterty Competencies for student
// public virtual ICollection<Competency> Competencies { get; set; }

//[HttpPost]
//public ActionResult Create([Bind(Include = "FirstName,LastName,Email,MobilePhone,EducationId")]Student student,
//        HttpPostedFileBase image, IEnumerable<int> compIds)
//{
//    if (ModelState.IsValid)
//    {
//        UnitOfWork unitOfWork = new UnitOfWork();

//        if (compIds != null)
//        {
//            student.Competencies = new List<Competency>();
//            foreach (var competencyId in compIds)
//            {
//                var competencyToAdd = unitOfWork.CompetenciesRepository.Find(competencyId);
//                student.Competencies.Add(competencyToAdd);
//            }

//        }

//        //student.SaveImage(image, Server.MapPath("~"), "/ProfileImages/");
//        string path = Server != null ? Server.MapPath("~") : "";
//        student.SaveImage(image, path, "/ProfileImages/");

//        unitOfWork.StudentsRepository.InsertOrUpdate(student);
//        unitOfWork.Save();

//        _emailer.Send("Welcome to our website...");

//        return View("Thanks");
//    }
//    else
//    {
//        return View();
//    }
//}
