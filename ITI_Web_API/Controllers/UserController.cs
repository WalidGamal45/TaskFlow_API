using Application.DTOs;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userServices.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _userServices.GetByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

      
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            await _userServices.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto user)
        {
            if (id != user.Id)
                return BadRequest("Id mismatch.");

            try
            {
                await _userServices.UpdateAsync(user);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userServices.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
