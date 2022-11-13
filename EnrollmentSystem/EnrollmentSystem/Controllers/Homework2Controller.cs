using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EnrollmentSystem.Controllers
{
    [ApiController]
    public class Homework2Controller : ControllerBase
    {
        private readonly IConfiguration _config;

        public Homework2Controller(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [Route("Student/UsingStudentId")]
        public async Task<ActionResult<Student>> GetStudent(int studentId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var student = await connection.QueryFirstAsync<Student>("SELECT * FROM Student WHERE student_id = @Id", new { Id = studentId });
            return Ok(student);
        }

        [HttpGet]
        [Route("StudentList/UsingCourseId")]
        public async Task<ActionResult<List<StudentList>>> GetStudentList(int courseId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var students = await connection.QueryAsync<StudentList>("SELECT Student.FirstName, Student.LastName, Course.description FROM StudentCourse JOIN Student ON StudentCourse.student_id = Student.student_id JOIN Course ON StudentCourse.course_id = Course.course_id WHERE Course.course_id = @Id", new { Id = courseId });
            return Ok(students);
        }

        [HttpGet]
        [Route("StudentSchedule/UsingStudentId")]
        public async Task<ActionResult<List<StudentSchedule>>> GetStudentSchedule(int studentId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var student = await connection.QueryAsync<StudentSchedule>("SELECT Student.FirstName, Student.LastName, Course.description, ClassSchedule.room, ClassSchedule.from_time, ClassSchedule.to_time, ClassSchedule.days, Professor.ProfFirstName, Professor.ProfLastName, StudentCourse.enrollment_date, StudentCourse.units FROM StudentCourse JOIN Student ON StudentCourse.student_id = Student.student_id JOIN Course ON StudentCourse.course_id = Course.course_id JOIN ClassSchedule ON ClassSchedule.course_id = Course.course_id JOIN Professor ON ClassSchedule.professor_id = Professor.professor_id WHERE Student.student_id = @Id", new { Id = studentId });
            return Ok(student);
        }
    }
}
