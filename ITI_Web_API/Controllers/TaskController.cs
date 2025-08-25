using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ITI_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskServices _taskServices;

        public TaskController(TaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        // GET: api/Task
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskServices.GetAllAsync();
            return Ok(tasks);
        }

        // GET: api/Task/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var task = await _taskServices.GetByIdAsync(id);
                return Ok(task);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // POST: api/Task
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Task1 task)
        {
            await _taskServices.AddAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        // PUT: api/Task/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Task1 task)
        {
            if (id != task.Id)
                return BadRequest("Id mismatch.");

            try
            {
                await _taskServices.UpdateAsync(task);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // DELETE: api/Task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _taskServices.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
