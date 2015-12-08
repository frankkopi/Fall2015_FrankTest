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

        public StudentsController(IStudentsRepository studentsRepository, IEducationsRepository  educationsRepository,
            ICompetencyHeadersRepository competencyHeadersRepository, IEmailer emailer)
        {
            this._studentsRepository = studentsRepository;
            this._educationsRepository = educationsRepository;
            this._competencyHeadersRepository = competencyHeadersRepository;
            _emailer = emailer;
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
            CreateEditStudentViewModel viewModel = new CreateEditStudentViewModel()
            {
                Student = student,
                Educations = _educationsRepository.All.ToList(),
                CompetencyHeaders = _competencyHeadersRepository.AllIncluding(a => a.Competencies).ToList()
            };
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Edit(Student student, HttpPostedFileBase image, IEnumerable<int> compIds)
        {
            if (ModelState.IsValid)
            {
                _studentsRepository.InsertOrUpdate(student);

                //student.SaveImage(image, Server.MapPath("~"), "/ProfileImages/");
                string path = Server != null ? Server.MapPath("~") : "";
  
                student.SaveImage(image, path, "/ProfileImages/");
                _studentsRepository.Save();
 
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

                HandleNewStudentHelper handleNewStudentHelper = new HandleNewStudentHelper(student, compIds);
                handleNewStudentHelper.HandleNewStudent();

                _emailer.Send("Welcome to our website...");

                return View("Thanks");
            }
            else
            {
                return View();
            }
        }



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




