using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP_5101_Cumulative_Project.Models;
using System.Diagnostics;

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

    }

}