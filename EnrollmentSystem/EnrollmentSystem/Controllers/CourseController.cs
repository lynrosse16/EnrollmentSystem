using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EnrollmentSystem.Controllers
{
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CourseController(IConfiguration config)
        {
            _config = config;
        }

        private static async Task<IEnumerable<Course>> SelectAllCourse(SqlConnection connection)
        {
            return await connection.QueryAsync<Course>("SELECT * FROM Course");
        }

        [HttpGet]
        [Route("All[controller]")]
        // GET: /AllCourse
        public async Task<ActionResult<List<Course>>> GetAllCourse()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            IEnumerable<Course> courses = await SelectAllCourse(connection);
            return Ok(courses);
        }

        [HttpPost]
        [Route("Create[controller]")]
        // POST: /CreateCourse
        public async Task<ActionResult<List<Course>>> CreateCourse(Course course)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("INSERT INTO Course VALUES (@course_id, @courseName, @description)", course);
            return Ok(await SelectAllCourse(connection));
        }

        [HttpPut]
        [Route("Update[controller]")]
        // PUT: /UpdateCourse
        public async Task<ActionResult<List<Course>>> UpdateCourse(Course course)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("UPDATE Course SET courseName = @courseName, description = @description WHERE course_id = @course_id", course);
            return Ok(await SelectAllCourse(connection));
        }

        [HttpDelete]
        [Route("Delete[controller]/UsingId")]
        // DELETE: /DeleteCourse/UsingId
        public async Task<ActionResult<Course>> DeleteCourse(int coursetId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("DELETE FROM Course WHERE course_id = @Id", new { Id = coursetId });
            return Ok(await SelectAllCourse(connection));
        }
    }
}
