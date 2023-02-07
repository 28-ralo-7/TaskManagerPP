using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
using TaskManagerApi.Models.Date;
using TaskManagerApi.Models.Services;
using TaskManagerPP.Common.Models;

namespace TaskManagerApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UserService _userService;
        public UsersController(ApplicationContext db)
        {
            _db = db;
            _userService = new UserService(db);

        }

        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult TestApi()
        {
            return Ok("Сервер запущен. Время запуска: " + DateTime.Now);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel userModel)
        {

            if (userModel != null)
            {
                bool result = _userService.Create(userModel);
                if (result)
                {
                    return Ok();
                }
                else return NotFound();
               
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateUser(int id,[FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                bool result = _userService.Update(id, userModel);
                if (result)
                {
                    return Ok();
                }
                else return NotFound();
            }
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            bool result = _userService.Delete(id);
            if (result)
            {
                return Ok();
            }
            else return NotFound();
            
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUser()
        {
            return await _db.Users.Select(n => n.ToDto()).ToListAsync();
        }

        [HttpPost("all")]
        public IActionResult CreateMultiUsers([FromBody] List<UserModel> userModels)
        {
            if (userModels != null && userModels.Count > 0)
            {
                bool result = _userService.CreateMultipleUsers(userModels);
                if (result)
                {
                    return Ok();
                }
                else return NotFound();
            }
            return BadRequest();
        }

        [HttpGet("{id}/admin")]
        public ActionResult<int?> GetProjectAdminId(int id)
        {
            var admin = _userService.GetProjectAdmin(id);
            if (admin == null)
            {
                return NotFound(null);
            }
            else return Ok(admin.Id); 
        }
    }
}