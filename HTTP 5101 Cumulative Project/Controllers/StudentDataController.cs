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
    public class StudentDataController : ApiController
    {
        private SchoolDbContext Student = new SchoolDbContext();

        /// <summary>
        /// Returns a list of students from the database
        /// <example> GET api/StudentData/ListStudents </example>
        /// </summary>
        /// <returns>
        /// A list of student names, student names, and the enrolment dates
        /// </returns>
        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        public IEnumerable<Student> ListStudents()
        {
            MySqlConnection Conn = Student.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //select query from the student table
            cmd.CommandText = "SELECT studentid, studentfname, studentlname, studentnumber, enroldate from students";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Student> Students = new List<Student> { };


            while (ResultSet.Read())
            {
                int StudentId = (int)ResultSet["studentid"];
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNum = (string)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];

                Student newStudent = new Student();
                newStudent.StudentId = StudentId;
                newStudent.StudentFname = StudentFname;
                newStudent.StudentLname = StudentLname;
                newStudent.StudentNum = StudentNum;
                newStudent.EnrolDate = EnrolDate;


                Students.Add(newStudent);

            }
            Conn.Close();

            return Students;

        }


        /// <summary>
        /// Returns a list of classes from the database
        /// <example> GET api/ClassData/FindClass/{id} </example>
        /// </summary>
        /// <returns>
        /// A student name, id, student number, and enrolment date,
        /// </returns>
        [HttpGet]
        [Route("api/StudentData/FindStudent/{id}")]
        public Student FindStudent(int id)
        {
            Student newStudent = new Student();

            MySqlConnection Conn = Student.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "SELECT studentid, studentfname, studentlname, studentnumber, enroldate from students where studentid = " + id;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int StudentId = (int)ResultSet["studentid"];
                string StudentFname = (string)ResultSet["studentfname"];
                string StudentLname = (string)ResultSet["studentlname"];
                string StudentNum = (string)ResultSet["studentnumber"];
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];

                newStudent.StudentId = StudentId;
                newStudent.StudentFname = StudentFname;
                newStudent.StudentLname = StudentLname;
                newStudent.StudentNum = StudentNum;
                newStudent.EnrolDate = EnrolDate;


            }
            return newStudent;
        }

    }

}
