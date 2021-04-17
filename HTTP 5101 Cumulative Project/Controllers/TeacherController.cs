using HTTP_5101_Cumulative_Project.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

namespace HTTP_5101_Cumulative_Project.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Teachers/List
        public ActionResult List(string SearchKey = null)
        {
            //debug comments for searching the teacher table
            Debug.WriteLine("The input search key is ");
            Debug.WriteLine(SearchKey);


            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);

            return View(Teachers);
        }

        //GET: /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();

            Teacher newTeacher = controller.FindTeacher(id);
           
            return View(newTeacher);
        }

        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();

            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }

        //GET: /Teacher/DeleteConfirm/{id}
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();

            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //GET /Teacher/AddNew/
        public ActionResult New()
        {
            return View();
        }

        //POST: /Teacher/create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNum, int Salary)
        {
            Teacher newTeacher = new Teacher();
            newTeacher.TeacherFname = TeacherFname;
            newTeacher.TeacherLname = TeacherLname;
            newTeacher.EmployeeNum = EmployeeNum;
            newTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();

            controller.AddTeacher(newTeacher);

            return RedirectToAction("List");
        }
    }

}