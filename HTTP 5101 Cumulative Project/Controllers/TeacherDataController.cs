using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTP_5101_Cumulative_Project.Models;
using MySql.Data.MySqlClient;

namespace HTTP_5101_Cumulative_Project.Controllers
{
    public class TeacherDataController : ApiController
    {

        private SchoolDbContext Teacher = new SchoolDbContext();


        /// <summary>
        /// Returns a list of Teachers from the database
        /// <example> GET api/TeacherData/ListTeachers </example>
        /// </summary>
        /// <returns>
        /// A list of teachers' first & last names
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        public IEnumerable<Teacher> ListTeachers() 
        {
            //connects
            MySqlConnection Conn = Teacher.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "SELECT teachers.teacherid, teachers.teacherfname, teachers.teacherlname, teachers.employeenumber, teachers.hiredate, teachers.salary FROM teachers";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Teachers = new List<Teacher>{};

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

                Teachers.Add(newTeacher);

            }
            Conn.Close();

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

            cmd.CommandText = "SELECT *, DATE_FORMAT(hiredate,'%y-%m-%d') as HireDate FROM teachers where teacherid = " + id;

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
            return newTeacher;

        }

    }

}
