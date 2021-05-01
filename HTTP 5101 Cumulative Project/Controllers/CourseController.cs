using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP_5101_Cumulative_Project.Models;
using System.Diagnostics;

namespace HTTP_5101_Cumulative_Project.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Course/List
        public ActionResult List(string SearchKey = null)
        {
            //debug comments for searching the class table
            Debug.WriteLine("The input search key is " + SearchKey);
            Debug.WriteLine(SearchKey);

            CourseDataController controller = new CourseDataController();
            IEnumerable<Course> Courses = controller.ListCourses(SearchKey);

            return View(Courses);
        }

        //GET: /Course/Show/{id}
        public ActionResult Show(int id)
        {
            CourseDataController controller = new CourseDataController();

            Course newCourse = controller.FindCourse(id);

            return View(newCourse);
        }

        //POST: /Course/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            CourseDataController controller = new CourseDataController();

            controller.DeleteCourse(id);

            return RedirectToAction("List");
        }

        //GET: /Course/DeleteConfirm/{id}
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            CourseDataController controller = new CourseDataController();

            Course newCourse = controller.FindCourse(id);

            return View(newCourse);
        }

        //GET /Course/AddNew/
        public ActionResult New()
        {
            return View();
        }

        //POST: /Course/Create
        [HttpPost]
        public ActionResult Create(string CourseCode, string CourseName, DateTime StartDate, DateTime FinishDate)
        {
            Course newCourse = new Course();
            newCourse.CourseName = CourseName;
            newCourse.CourseCode = CourseCode;
            newCourse.StartDate = StartDate;
            newCourse.FinishDate = FinishDate;

            CourseDataController controller = new CourseDataController();

            controller.AddCourse(newCourse);

            return RedirectToAction("List");
        }


        //GET /Course/Update/{CourseId}
        public ActionResult Update(int id)
        {
            CourseDataController controller = new CourseDataController();

            Course SelectedCourse = controller.FindCourse(id);

            return View(SelectedCourse);
        }

        [HttpPost]
        public ActionResult Update(int id, string CourseName, string CourseCode, DateTime StartDate, DateTime FinishDate)
        {
            Debug.WriteLine("The updated info received is " + CourseName + " " + CourseCode);

            Course CourseInfo = new Course();
            CourseInfo.CourseId = id;
            CourseInfo.CourseName = CourseName;
            CourseInfo.CourseCode = CourseCode;
            CourseInfo.StartDate = StartDate;
            CourseInfo.FinishDate = FinishDate;

            CourseDataController controller = new CourseDataController();

            controller.UpdateCourse(CourseInfo);

            return RedirectToAction("Show/" + id);
        }

    }
}