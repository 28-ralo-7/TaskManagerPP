using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerApi.Models;
using TaskManagerApi.Models.Date;
using TaskManagerApi.Models.Services;
using TaskManagerPP.Common.Models;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UserService _userService;
        private readonly ProjectsService _projectService;
        public ProjectsController(ApplicationContext db)
        {
            _db = db;
            _userService = new UserService(db);
            _projectService = new ProjectsService(db);
        }



        [HttpPost]
        public IActionResult Create([FromBody] ProjectModel projectModel)
        {
            if (projectModel != null)
            {
                var user = _userService.GetUser(HttpContext.User.Identity.Name);
                if (user!=null)
                {
                    if (user.Status == UserStatus.Admin || user.Status == UserStatus.Editor)
                    {
                        var admin = _db.ProjectAdmins.FirstOrDefault(n => n.UserId == user.Id);
                        if (admin == null)
                        {
                            admin = new ProjectAdmin(user);
                            _db.ProjectAdmins.Add(admin);
                        }
                        projectModel.AdminId = admin.Id;

                        bool result = _projectService.Create(projectModel);
                        if (result)
                        {
                            return Ok();
                        }
                        else return NotFound();
                    }
                   
                }
                return Unauthorized();
            }
            return BadRequest();
        }


        [HttpGet("{id}")]
        
        public IActionResult Get(int id)
        {
            var project = _projectService.Get(id);
            if (project == null)
            {
                return NoContent();
            }
            else return Ok(project);
        }
        [HttpGet]
        public async Task<IEnumerable<ProjectModel>> Get()
        {
            var user = _userService.GetUser(HttpContext.User.Identity.Name);
            if (user.Status==UserStatus.Admin)
            {
                 return await _projectService.GetAll().ToListAsync();
            }
            else
            {
                return await _projectService.GetByUserId(user.Id);
            }
           
        }




        [HttpPatch]
        public IActionResult Update(int id, [FromBody] ProjectModel projectModel)
        {
            
            if (projectModel != null)
            {
                var user = _userService.GetUser(HttpContext.User.Identity.Name);
                if (user != null)
                {
                    if (user.Status == UserStatus.Admin || user.Status == UserStatus.Editor)
                    {
                        bool result = _projectService.Update(id, projectModel);
                        if (result)
                        {
                            return Ok();
                        }
                        else return NotFound();
                    }
                    return Unauthorized();
                }
            }            
            return BadRequest();
        }




        [HttpDelete]
        public IActionResult Delete(int id)
        {
           
                bool result = _projectService.Delete(id); 
                if (result)
                {
                    return Ok();
                } 
                else return NotFound();
            
        }


        [HttpPatch("{id}/users")]
        public IActionResult AddUsersToProject(int id,[FromBody] List<int> usersId)
        {
           
            if (usersId !=null)
            {
                var user = _userService.GetUser(HttpContext.User.Identity.Name);
                if (user != null)
                {
                    if (user.Status == UserStatus.Admin || user.Status == UserStatus.Editor)
                    {
                        _projectService.AddUsersToProject(id, usersId);
                        return Ok();
                    }
                    return Unauthorized();
                }
                
            }
            return BadRequest();
        }


        [HttpPatch("{id}/users/remove")]
        public IActionResult RemoveUsersToProject(int id, [FromBody] List<int> usersId)
        {

            if (usersId != null)
            {
                var user = _userService.GetUser(HttpContext.User.Identity.Name);
                if (user != null)
                {
                    if (user.Status == UserStatus.Admin || user.Status == UserStatus.Editor)
                    {
                        _projectService.RemoveUsersFromProject(id, usersId);
                        return Ok();
                    }
                    return Unauthorized();
                }

            }
            return BadRequest();
        }
    }
}