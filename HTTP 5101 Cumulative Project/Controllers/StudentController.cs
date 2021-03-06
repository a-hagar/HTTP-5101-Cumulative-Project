using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP_5101_Cumulative_Project.Models;
using System.Diagnostics;

namespace HTTP_5101_Cumulative_Project.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Student/List
        public ActionResult List(string SearchKey = null)
        {
            //debug comments for searching the student table
            Debug.WriteLine("The input search key is ");
            Debug.WriteLine(SearchKey);

            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents(SearchKey);

            return View(Students);
        }

        //GET: /Student/Show/{id}
        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();

            Student newStudent = controller.FindStudent(id);

            return View(newStudent);
        }

        //POST: /Course/Student/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            StudentDataController controller = new StudentDataController();

            controller.DeleteStudent(id);

            return RedirectToAction("List");
        }

        //GET: /Student/DeleteConfirm/{id}
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            StudentDataController controller = new StudentDataController();

            Student newStudent = controller.FindStudent(id);

            return View(newStudent);
        }

        //GET /Student/New/
        public ActionResult New()
        {
            return View();
        }

        //POST: /Student/Create
        [HttpPost]
        public ActionResult Create(string StudentFname, string StudentLname, string StudentNum)
        {
            Student newStudent = new Student();
            newStudent.StudentFname = StudentFname;
            newStudent.StudentLname = StudentLname;
            newStudent.StudentNum = StudentNum;

            StudentDataController controller = new StudentDataController();

            controller.AddStudent(newStudent);

            return RedirectToAction("List");
        }

        //GET /Student/Update/
        public ActionResult Update(int id)
        {
            StudentDataController controller = new StudentDataController();

            Student SelectedStudent = controller.FindStudent(id);

            return View(SelectedStudent);
        }

        [HttpPost]
        public ActionResult Update(int id, string StudentFname, string StudentLname, string StudentNum)
        {
            Debug.WriteLine("The updated info received is " + StudentFname +" "+ StudentLname +" "+ StudentNum);

            Student StudentInfo = new Student();
            StudentInfo.StudentId = id;
            StudentInfo.StudentFname = StudentFname;
            StudentInfo.StudentLname = StudentLname;
            StudentInfo.StudentNum = StudentNum;

            StudentDataController controller = new StudentDataController();

            controller.UpdateStudent(StudentInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}