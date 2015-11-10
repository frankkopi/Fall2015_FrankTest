using Fall2015.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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


        public ActionResult Index()
        {
            StudentIndexViewModel viewModel = new StudentIndexViewModel()
            {
                Students = _studentsRepository.All.ToList(),
                CompetencyHeaders = _competencyHeadersRepository.All.ToList()
            };

            //List<Student> students = _studentsRepository.All.ToList();
            //return View(students);
            return View(viewModel);
        }

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

        //[HttpPost]
        //public ActionResult Edit(Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _studentsRepository.InsertOrUpdate(student);
        //        _studentsRepository.Save();
        //        return RedirectToAction("Index");
        //    }
        //    CreateEditStudentViewModel viewModel = new CreateEditStudentViewModel()
        //    {
        //        Student = student,
        //        Educations = _educationsRepository.All.ToList(),
        //        CompetencyHeaders = _competencyHeadersRepository.AllIncluding(a => a.Competencies).ToList()
        //    };
        //    return View(viewModel);
        //}


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
        public ActionResult Create(Student student, HttpPostedFileBase image, IEnumerable<int> compIds)
        {
            if (ModelState.IsValid)
            {
                _studentsRepository.InsertOrUpdate(student);

                //student.SaveImage(image, Server.MapPath("~"), "/ProfileImages/");
                string path = Server != null ? Server.MapPath("~") : "";

                student.SaveImage(image, path , "/ProfileImages/");
                _studentsRepository.Save();
                _emailer.Send("Welcome to our website...");

                // get the studentId
                // send the studentId to each competency from the IEnumerable<int> compIds

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




