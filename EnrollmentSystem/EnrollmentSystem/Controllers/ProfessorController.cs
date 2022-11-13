using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EnrollmentSystem.Controllers
{
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProfessorController(IConfiguration config)
        {
            _config = config;
        }

        private static async Task<IEnumerable<Professor>> SelectAllProfessor(SqlConnection connection)
        {
            return await connection.QueryAsync<Professor>("SELECT * FROM Professor");
        }

        [HttpGet]
        [Route("All[controller]")]
        // GET: /AllProfessor
        public async Task<ActionResult<List<Professor>>> GetAllProfessor()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            IEnumerable<Professor> professor = await SelectAllProfessor(connection);
            return Ok(professor);
        }

        [HttpPost]
        [Route("Create[controller]")]
        // POST: /CreateProfessor
        public async Task<ActionResult<List<Professor>>> CreateProfessor(Professor professor)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("INSERT INTO Professor VALUES (@professor_id, @ProfFirstName, @ProfLastName, @ProfAddress)", professor);
            return Ok(await SelectAllProfessor(connection));
        }

        [HttpPut]
        [Route("Update[controller]")]
        // PUT: /UpdateProfessor
        public async Task<ActionResult<List<Professor>>> UpdateProfessor(Professor professor)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("UPDATE Professor SET ProfFirstName = @ProfFirstName, ProfLastName = @ProfLastName, ProfAddress = @ProfAddress WHERE professor_id = @professor_id", professor);
            return Ok(await SelectAllProfessor(connection));
        }

        [HttpDelete]
        [Route("Delete[controller]/UsingId")]
        // DELETE: /DeleteProfessor/UsingId
        public async Task<ActionResult<Professor>> DeleteProfessor(int professorId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("DELETE FROM Professor WHERE professor_id = @Id", new { Id = professorId });
            return Ok(await SelectAllProfessor(connection));
        }
    }
}