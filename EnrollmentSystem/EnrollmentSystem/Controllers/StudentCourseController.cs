using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EnrollmentSystem.Controllers
{
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        private readonly IConfiguration _config;

        public StudentCourseController(IConfiguration config)
        {
            _config = config;
        }

        private static async Task<IEnumerable<StudentCourse>> SelectAllStudentCourse(SqlConnection connection)
        {
            return await connection.QueryAsync<StudentCourse>("SELECT * FROM StudentCourse");
        }

        [HttpGet]
        [Route("All[controller]")]
        // GET: /AllStudentCourse
        public async Task<ActionResult<List<StudentCourse>>> GetAllStudentCourse()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            IEnumerable<StudentCourse> studentCourses = await SelectAllStudentCourse(connection);
            return Ok(studentCourses);
        }

        [HttpPost]
        [Route("Create[controller]")]
        // POST: /CreateStudentCourse
        public async Task<ActionResult<List<StudentCourse>>> CreateStudentCourse(StudentCourse studentCourse)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("INSERT INTO StudentCourse VALUES (@student_course_id, @student_id, @course_id, @class_schedule_id, @enrollment_date, @units)", studentCourse);
            return Ok(await SelectAllStudentCourse(connection));
        }

        [HttpPut]
        [Route("Update[controller]")]
        // PUT: /UpdateStudentCourse
        public async Task<ActionResult<List<StudentCourse>>> UpdateStudentCourse(StudentCourse studentCourse)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("UPDATE StudentCourse SET class_schedule_id = @class_schedule_id, enrollment_date = @enrollment_date, units = @units WHERE student_course_id = @student_course_id", studentCourse);
            return Ok(await SelectAllStudentCourse(connection));
        }

        [HttpDelete]
        [Route("Delete[controller]/UsingId")]
        // DELETE: /DeleteStudentCourse/UsingId
        public async Task<ActionResult<StudentCourse>> DeleteStudentCourse(int studentCourseId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("DELETE FROM StudentCourse WHERE student_course_id = @Id", new { Id = studentCourseId });
            return Ok(await SelectAllStudentCourse(connection));
        }
    }
}
