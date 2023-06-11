using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using StudentClassLibrary;

namespace NewStudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        /// <summary>
        /// Get students records
        /// </summary>
        /// <param name="searchText">Seach text</param>
        /// <returns>Satus Code</returns>
        [HttpGet]
        public ActionResult GetStudents(string searchText) 
        {
            try
            {
                return Ok(new TestApp.StudentBusinessLogic().GetStudents(searchText));//List of Students http Satus code ok 200
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retieving data from the database");//sending 500 status http code
            }
        }


        /// <summary>
        /// Get student record based on studentID
        /// </summary>
        /// <param name="studentID">student unique id</param>
        /// <returns>status code </returns>
        
        [HttpGet("{studentID:int}")]
        public ActionResult<StudentModel> GetStudent(int studentID)
        {
            try
            {
                return new TestApp.StudentBusinessLogic().GetStudent(studentID);//automatically serialize object to json  along with http status code 200 is return to the client
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retieving data from the database");//sending 500 status http code
            }
        }

        /// <summary>
        /// Save Student record
        /// </summary>
        /// <param name="student">student object to be saved</param>
        /// <returns>Status Code</returns>

        [HttpPost]
        public ActionResult SaveStudent(StudentModel student) 
        {
            try
            {
                new TestApp.StudentBusinessLogic().SaveStudent(student);
                //in response include the  location header
                return CreatedAtAction(nameof(GetStudent), new { studentID = student.StudentID }, student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new student record");//sending 500 status http code
            }
        }

        /// <summary>
        /// Delete student record based studentID
        /// </summary>
        /// <param name="studentID">student unique id</param>
        /// <returns>Status Code</returns>

        [HttpDelete("{studentID:int}")]
        public ActionResult DeleteStudent(int studentID)
        {
            try
            {
                new TestApp.StudentBusinessLogic().DeleteStudent(studentID);
                return Ok($"Student with id={studentID} deleted");

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting student record");//sending 500 status http code
            }
        }

        /// <summary>
        /// Get Genders 
        /// </summary>
        /// <returns>Status Code</returns>

        [Route("~/api/Students/genders")]
        [HttpGet]
        public ActionResult GetGenders()
        {
            try
            {
                return Ok(new TestApp.GenderBusinessLogic().GetGenders());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retieving data from the database");
            }
        }

    }
}
