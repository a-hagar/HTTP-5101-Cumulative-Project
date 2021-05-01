using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTP_5101_Cumulative_Project.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace HTTP_5101_Cumulative_Project.Controllers
{
    public class CourseDataController : ApiController
    {
        private SchoolDbContext Course = new SchoolDbContext();

        /// <summary>
        /// Returns a list of classes from the database
        /// <example> GET api/CourseData/ListCourses</example>
        /// </summary>
        /// <returns> A list of classes names, course codes, and the ids and names of the teachers of the course </returns>
        /// <example> GET api/CourseData/ListCourses/List?SearchKey=name</example>
        /// <returns> A result of class(es) that partially match the search term </returns>
        [HttpGet]
        [Route("api/CourseData/ListCourses/{SearchKey?}")]
        public IEnumerable<Course> ListCourses(string SearchKey = null)
        {
            //connects to database and establishes connection between server & database
            MySqlConnection Conn = Course.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //select query with a join with the teachers table 
            cmd.CommandText = "SELECT classid, classname, classcode, startdate, finishdate FROM classes WHERE classname LIKE @key OR classcode LIKE @key";

            //sql parameters to avoid injections
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Creating a new list for classes
            List<Course> Courses = new List<Course> { };

  
            while (ResultSet.Read())
            {
                int CourseId = (int)ResultSet["classid"];
                string CourseName = (string)ResultSet["classname"];
                string CourseCode = (string)ResultSet["classcode"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];


                Course newCourse = new Course();
                newCourse.CourseId = CourseId;
                newCourse.CourseName = CourseName;
                newCourse.CourseCode = CourseCode;
                newCourse.StartDate = StartDate;
                newCourse.FinishDate = FinishDate;

                //Adds a new class to the list
                Courses.Add(newCourse);

            }
            //Closes connection to MySql database
            Conn.Close();

            //Returns list of all Classes
            return Courses;

        }

        /// <summary>
        /// Returns a list of classes from the database
        /// <example> GET api/CourseData/FindCourse/{id} </example>
        /// </summary>
        /// <returns>
        /// A class with an id, name, start and finish dates
        /// </returns>
        [HttpGet]
        [Route("api/CourseData/FindCourse/{id}")]
        public Course FindCourse(int id)
        {
            Course newCourse = new Course();

            MySqlConnection Conn = Course.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "SELECT classid, classname, classcode, startdate, finishdate FROM classes where classid =@id";

            //sql parameters to avoid injections
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int CourseId = (int)ResultSet["classid"];
                string CourseName = (string)ResultSet["classname"];
                string CourseCode = (string)ResultSet["classcode"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];


                newCourse.CourseId = CourseId;
                newCourse.CourseName = CourseName;
                newCourse.CourseCode = CourseCode;
                newCourse.StartDate = StartDate;
                newCourse.FinishDate = FinishDate;

            }
            return newCourse;
        }

        //Delete a course
        /// <summary>
        /// Deletes a class from the database
        /// </summary>
        /// example api/CourseData/DeleteCourse/2
        /// 
        [Route("api/CourseData/DeleteCourse/{courseid}")]
        [HttpPost]
        public void DeleteCourse(int courseid)
        {

            MySqlConnection Conn = Course.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            string query = "delete from classes where classid=@id";
            cmd.CommandText = query;

            //sql parameters to avoid injections
            cmd.Parameters.AddWithValue("@id", courseid);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Adds a new course to the database
        /// </summary>
        /// <param name="NewCourse"> Maps the new inputs from view to the database </param>
        /// example api/Course/AddCourse/        
        /// 
        [HttpPost]
        public void AddCourse(Course newCourse)
        {
            MySqlConnection Conn = Course.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            string query = "insert into classes (classcode, startdate, finishdate, classname) values(@code, @startdate, @finishdate, @name)";
            cmd.CommandText = query;

            //sql parameters to avoid injections
            cmd.Parameters.AddWithValue("@code", newCourse.CourseCode);
            cmd.Parameters.AddWithValue("@name", newCourse.CourseName);
            cmd.Parameters.AddWithValue("@startdate", newCourse.StartDate);
            cmd.Parameters.AddWithValue("@finishdate", newCourse.FinishDate);
            cmd.Parameters.AddWithValue("@name", newCourse.CourseName);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Changes existing course data from the database
        /// </summary>
        /// <param name="CourseInfo"> Maps the changed inputs from view to the database </param>
        /// example api/Course/UpdateCourse/1      
        /// 
        [HttpPost]
        public void UpdateCourse(Course CourseInfo)
        {
            MySqlConnection Conn = Course.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "update classes set classcode=@code, classname=@name, startdate=@startdate, finishdate=@finishdate where classid=@id";


            cmd.Parameters.AddWithValue("@id", CourseInfo.CourseId);
            cmd.Parameters.AddWithValue("@code", CourseInfo.CourseCode);
            cmd.Parameters.AddWithValue("@name", CourseInfo.CourseName);
            cmd.Parameters.AddWithValue("@startdate", CourseInfo.StartDate);
            cmd.Parameters.AddWithValue("@finishdate", CourseInfo.FinishDate);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}
