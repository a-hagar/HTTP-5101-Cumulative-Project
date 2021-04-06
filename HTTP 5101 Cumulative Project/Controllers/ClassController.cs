using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP_5101_Cumulative_Project.Models;
using System.Diagnostics;

namespace HTTP_5101_Cumulative_Project.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Class/List
        public ActionResult List(string SearchKey = null)
        {
            //debug comments for searching the class table
            Debug.WriteLine("The input search key is ");
            Debug.WriteLine(SearchKey);

            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> Classes = controller.ListClasses(SearchKey);

            return View(Classes);
        }

        //GET: /Class/Show/{id}
        public ActionResult Show(int id)
        {
            ClassDataController controller = new ClassDataController();

            Class newClass = controller.FindClass(id);

            return View(newClass);
        }

    }
}