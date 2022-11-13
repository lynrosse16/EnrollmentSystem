using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EnrollmentSystem.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _config;

        public StudentController(IConfiguration config)
        {
            _config = config;
        }

        private static async Task<IEnumerable<Student>> SelectAllStudent(SqlConnection connection)
        {
            return await connection.QueryAsync<Student>("SELECT * FROM Student");
        }

        [HttpGet]
        [Route("All[controller]")]
        // GET: /AllStudent
        public async Task<ActionResult<List<Student>>> GetAllStudent()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            IEnumerable<Student> students = await SelectAllStudent(connection);
            return Ok(students);
        }

        [HttpPost]
        [Route("Create[controller]")]
        // POST: /CreateStudent
        public async Task<ActionResult<List<Student>>> CreateStudent(Student student)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("INSERT INTO Student VALUES (@student_id, @FirstName, @LastName, @Address)", student);
            return Ok(await SelectAllStudent(connection));
        }

        [HttpPut]
        [Route("Update[controller]")]
        // PUT: /UpdateStudent
        public async Task<ActionResult<List<Student>>> UpdateStudent(Student student)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("UPDATE Student SET FirstName = @FirstName, LastName = @LastName, Address = @Address WHERE student_id = @student_id", student);
            return Ok(await SelectAllStudent(connection));
        }

        [HttpDelete]
        [Route("Delete[controller]/UsingId")]
        // DELETE: /DeleteStudent
        public async Task<ActionResult<Student>> DeleteStudent(int studentId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("DELETE FROM Student WHERE student_id = @Id", new { Id = studentId });
            return Ok(await SelectAllStudent(connection));
        }

    }
}