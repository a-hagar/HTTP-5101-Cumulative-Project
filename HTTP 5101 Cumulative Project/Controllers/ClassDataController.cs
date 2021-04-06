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
    public class ClassDataController : ApiController
    {
        private SchoolDbContext Class = new SchoolDbContext();

        /// <summary>
        /// Returns a list of classes from the database
        /// <example> GET api/ClassData/ListClasses</example>
        /// </summary>
        /// <returns> A list of classes names, course codes, and the ids and names of the teachers of the course </returns>
        /// <example> GET api/ClassData/ListClasses/List?SearchKey=name</example>
        /// <returns> A result of class(es) that partially match the search term </returns>


        [HttpGet]
        [Route("api/ClassData/ListClasses/{SearchKey?}")]
        public IEnumerable<Class> ListClasses(string SearchKey = null)
        {
            //connects to database and establishes connection between server & database
            MySqlConnection Conn = Class.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //select query with a join with the teachers table 
            cmd.CommandText = "SELECT classes.classid, classes.classname, classes.classcode, classes.startdate, classes.finishdate, teachers.teacherid, teachers.teacherfname, teachers.teacherlname FROM classes join teachers on classes.teacherid = teachers.teacherid WHERE classes.classname LIKE @key OR classes.classcode LIKE @key";

            //parameters for the searchkey
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Creating a new list for classes
            List<Class> Classes = new List<Class> { };

            //
            while (ResultSet.Read())
            {
                int ClassId = (int)ResultSet["classid"];
                string ClassName = (string)ResultSet["classname"];
                string ClassCode = (string)ResultSet["classcode"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];


                Class newClass = new Class();
                newClass.ClassId = ClassId;
                newClass.ClassName = ClassName;
                newClass.ClassCode = ClassCode;
                newClass.StartDate = StartDate;
                newClass.FinishDate = FinishDate;
                newClass.TeacherId = TeacherId;
                newClass.TeacherFname = TeacherFname;
                newClass.TeacherLname = TeacherLname;

                //Adds a new class to the list
                Classes.Add(newClass);

            }
            //Closes connection to MySql database
            Conn.Close();

            //Returns list of all Classes
            return Classes;

        }

        /// <summary>
        /// Returns a list of classes from the database
        /// <example> GET api/ClassData/FindClass/{id} </example>
        /// </summary>
        /// <returns>
        /// A class with an id, name, start and finish dates
        /// </returns>
        [HttpGet]
        [Route("api/ClassData/FindClass/{id}")]
        public Class FindClass(int id)
        {
            Class newClass = new Class();

            MySqlConnection Conn = Class.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "SELECT classes.classid, classes.classname, classes.classcode, classes.startdate, classes.finishdate, teachers.teacherid, teachers.teacherfname, teachers.teacherlname FROM classes join teachers on classes.teacherid = teachers.teacherid where classid = " + id;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int ClassId = (int)ResultSet["classid"];
                string ClassName = (string)ResultSet["classname"];
                string ClassCode = (string)ResultSet["classcode"];
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];


                newClass.ClassId = ClassId;
                newClass.ClassName = ClassName;
                newClass.ClassCode = ClassCode;
                newClass.StartDate = StartDate;
                newClass.FinishDate = FinishDate;
                newClass.TeacherId = TeacherId;
                newClass.TeacherFname = TeacherFname;
                newClass.TeacherLname = TeacherLname;

            }
            return newClass;
        }

    }

}
