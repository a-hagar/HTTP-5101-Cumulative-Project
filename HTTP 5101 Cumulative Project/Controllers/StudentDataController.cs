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
    public class StudentDataController : ApiController
    {
        private SchoolDbContext Student = new SchoolDbContext();

        /// <summary>
        /// Returns a list of students from the database
        /// </summary>
        /// <example> GET api/StudentData/ListStudents </example>
        /// <returns> A list of student names, student names, and the enrolment dates </returns>
        /// <example> GET api/StudentData/ListStudents/List?SearchKey=name</example>
        /// <returns> A result of student(s) that partially match the search term </returns> 

        [HttpGet]
        [Route("api/StudentData/ListStudents/{SearchKey?}")]
        public IEnumerable<Student> ListStudents(string SearchKey = null)
        {
            //connects to database and establishes connection between server & database
            MySqlConnection Conn = Student.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //select query from the student table with search cababilities
            cmd.CommandText = "SELECT studentid, studentfname, studentlname, studentnumber, enroldate from students WHERE studentfname LIKE @key OR studentlname LIKE @key OR (concat(studentfname, ' ', studentlname)) LIKE @key";

            //parameters for the searchkey
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Creating a new list for classes
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

                //Adds a new student to the list
                Students.Add(newStudent);

            }
            //Closes connection to MySql database
            Conn.Close();

            //Returns list of all Classes
            return Students;

        }


        /// <summary>
        /// Returns a list of classes from the database
        /// <param name="id"/> Student Id number </param>
        /// </summary>
        /// <example> GET api/ClassData/FindClass/{id} </example
        /// <returns> A student name, id, student number, and enrolment date </returns>
        [HttpGet]
        [Route("api/StudentData/FindStudent/{id}")]
        public Student FindStudent(int id)
        {
            Student newStudent = new Student();

            //connects to database and establishes connection between server & database
            MySqlConnection Conn = Student.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //select query from the student table with where statement 
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
            //Returns list of all Classes
            return newStudent;
        }

        //Delete a student's info
        /// <summary>
        /// Deletes a  student's info from the database
        /// </summary>
        /// example api/StudentData/DeleteStudent/200
        ///  
        [Route("api/StudentData/DeleteStudent/{studentid}")]
        [HttpPost]

        public void DeleteStudent(int studentid)
        {

            MySqlConnection Conn = Student.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            string query = "delete from students where studentid=@id";
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@id", studentid);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        [HttpPost]
        public void AddStudent(Student newStudent)
        {
            MySqlConnection Conn = Student.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            string query = "insert into students (studentfname, studentlname, studentnumber, enroldate) values(@fname, @lname, @studentnum, NOW())";
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@fname", newStudent.StudentFname);
            cmd.Parameters.AddWithValue("@lname", newStudent.StudentLname);
            cmd.Parameters.AddWithValue("@studentnum", newStudent.StudentNum);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }

}
