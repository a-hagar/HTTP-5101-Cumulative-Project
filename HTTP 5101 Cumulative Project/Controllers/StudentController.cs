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
    }
}