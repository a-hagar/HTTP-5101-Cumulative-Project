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
    public class TeacherDataController : ApiController
    {

        private readonly SchoolDbContext Teacher = new SchoolDbContext();

        /// <summary>
        /// Returns a list of Teachers from the database
        /// <example> GET api/TeacherData/ListTeachers </example>
        /// </summary>
        /// <returns> A list of teachers' names </returns>
        /// <example> GET api/TeacherData/ListTeachers/List?SearchKey?=name</example>
        /// <returns> A result of teacher(s) that partially match the search term </returns>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            //debug comments for searching the teacher table
            Debug.WriteLine("I am looking for  ");
            Debug.WriteLine(SearchKey);

            //connects to database and establishes connection between server & database
            MySqlConnection Conn = Teacher.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();


            //select query from teachers table with search function
            cmd.CommandText = "SELECT teachers.teacherid, teachers.teacherfname, teachers.teacherlname, teachers.employeenumber, teachers.hiredate, teachers.salary FROM teachers WHERE teachers.teacherfname LIKE @key OR teachers.teacherlname LIKE @key OR (concat(teacherfname, ' ', teacherlname)) LIKE @key";

            //parameters for the searchkey
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Creating a new list for classes
            List<Teacher> Teachers = new List<Teacher> { };

            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNum = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];

                Teacher newTeacher = new Teacher();
                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = TeacherLname;
                newTeacher.EmployeeNum = EmployeeNum;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;

                //Adds a new teacher to the list
                Teachers.Add(newTeacher);

            }
            //Closes connection to MySql database
            Conn.Close();

            //Returns list of all teachers
            return Teachers;

        }


        /// <summary>
        /// Returns a list of Teachers from the database
        /// <example> GET api/TeacherData/FindTeacher/{id} </example>
        /// </summary>
        /// <returns>
        /// A teacher's name, id, employee id, salary
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher newTeacher = new Teacher();

            MySqlConnection Conn = Teacher.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //select query from teachers table with search function
            cmd.CommandText = "SELECT teachers.teacherid, teachers.teacherfname, teachers.teacherlname, teachers.employeenumber, teachers.hiredate, teachers.salary FROM teachers WHERE teachers.teacherid=" + id;

            MySqlDataReader ResultSet = cmd.ExecuteReader();


            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNum = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];


                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = TeacherLname;
                newTeacher.EmployeeNum = EmployeeNum;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;


            }
            //Returns the teacher that matches the id
            return newTeacher;

        }

        //Delete a teacher
        /// <summary>
        /// Deletes a teacher from the database
        /// </summary>
        /// example api/TeacherData/DeleteArticle/200
        ///  
        [Route("api/TeacherData/DeleteTeacher/{teacherid}")]
        [HttpPost]

        public void DeleteTeacher(int teacherid)
        {

            MySqlConnection Conn = Teacher.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            string query = "delete from teachers where teacherid=@id";
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@id", teacherid);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        [HttpPost]
        public void AddTeacher(Teacher newTeacher)
        {
            MySqlConnection Conn = Teacher.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            string query = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values(@fname, @lname, @employeenum, NOW(), @salary)";
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@fname", newTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@lname", newTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@employeenum", newTeacher.EmployeeNum);
            cmd.Parameters.AddWithValue("@salary", newTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }

}
