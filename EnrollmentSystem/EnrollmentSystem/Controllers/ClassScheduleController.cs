using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EnrollmentSystem.Controllers
{
    [ApiController]
    public class ClassScheduleController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ClassScheduleController(IConfiguration config)
        {
            _config = config;
        }

        private static async Task<IEnumerable<ClassSchedule>> SelectAllClassSchedule(SqlConnection connection)
        {
            return await connection.QueryAsync<ClassSchedule>("SELECT * FROM ClassSchedule");
        }

        [HttpGet]
        [Route("All[controller]")]
        // GET: /AllClassSchedule
        public async Task<ActionResult<List<ClassSchedule>>> GetAllClassSchedule()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            IEnumerable<ClassSchedule> classSchedules = await SelectAllClassSchedule(connection);
            return Ok(classSchedules);
        }

        [HttpPost]
        [Route("Create[controller]")]
        // POST: /CreateClassSchedule
        public async Task<ActionResult<List<ClassSchedule>>> CreateClassSchedule(ClassSchedule classSchedule)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("INSERT INTO ClassSchedule VALUES (@class_schedule_id, @professor_id, @course_id, @room, @from_time, @to_time, @days)", classSchedule);
            return Ok(await SelectAllClassSchedule(connection));
        }

        [HttpPut]
        [Route("Update[controller]")]
        // PUT: /UpdateClassSchedule
        public async Task<ActionResult<List<ClassSchedule>>> UpdateClassSchedule(ClassSchedule classSchedule)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("UPDATE ClassSchedule SET room = @room, from_time = @from_time, to_time = @to_time, days = @days WHERE class_schedule_id = @class_schedule_id", classSchedule);
            return Ok(await SelectAllClassSchedule(connection));
        }

        [HttpDelete]
        [Route("Delete[controller]/UsingId")]
        // DELETE: /DeleteClassSchedule/UsingId
        public async Task<ActionResult<ClassSchedule>> DeleteClassSchedule(int classScheduleId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("DELETE FROM ClassSchedule WHERE class_schedule_id = @Id", new { Id = classScheduleId });
            return Ok(await SelectAllClassSchedule(connection));
        }
    }
}
