using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerApi.Models.Abstruction;
using TaskManagerApi.Models.Date;
using TaskManagerPP.Common.Models;

namespace TaskManagerApi.Models.Services
{
    public class ProjectsService : AbsractionService, ICommonService<ProjectModel>
    {
        private readonly ApplicationContext _db;
        

        public ProjectsService(ApplicationContext db)
        {
            _db = db;
         
        }      
        public bool Create(ProjectModel model)
        {
           bool result = DoAction(delegate ()
           {
                
               Project newProject = new Project(model);
               _db.Projects.Add(newProject);
               _db.SaveChanges();
           });
            return result;
           
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                Project newProject = _db.Projects.FirstOrDefault(n=>n.Id == id);
                _db.Projects.Remove(newProject);
                _db.SaveChanges();
            });
            return result;
        }
        public bool Update(int id, ProjectModel model)
        {
            bool result = DoAction(delegate ()
            {
                Project newProject = _db.Projects.FirstOrDefault(n => n.Id == id);
                newProject.Name = model.Name;
                newProject.Discription = model.Discription;
                newProject.Photo = model.Photo;
                newProject.Status = model.Status;
                newProject.AdminId = model.AdminId;
                _db.Projects.Update(newProject);
                _db.SaveChanges();
            });
            return result;

        }

        public ProjectModel Get(int id)
        {
            Project project = _db.Projects.FirstOrDefault(n => n.Id == id);
            return project?.ToDto();
        }

        public async Task<IEnumerable<ProjectModel>>  GetByUserId(int userId)
        {
            List<ProjectModel> result = new List<ProjectModel>();
            var admin = _db.ProjectAdmins.FirstOrDefault(a => a.UserId == userId);
            if (admin!=null)
            {
                var projectForAdmin =await  _db.Projects.Where(n => n.AdminId == admin.Id).Select(b => b.ToDto()).ToListAsync();
                result.AddRange(projectForAdmin);
            }
            var projectForUser = await _db.Projects.Include(p => p.AllUsers).Where(b => b.AllUsers.Any(b => b.Id == userId)).Select(b => b.ToDto()).ToListAsync();
            result.AddRange(projectForUser); 
            return result;
        }

        public IQueryable<ProjectModel> GetAll()
        {
            return  _db.Projects.Select(n => n.ToDto());
        }

        public void AddUsersToProject(int id, List<int> userIds)
        {
            Project project = _db.Projects.FirstOrDefault(p => p.Id == id);
            foreach (int userId in userIds)
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                if (project.AllUsers.Contains(user) == false)
                    project.AllUsers.Add(user);
            }
            _db.SaveChanges();
        }

        public void RemoveUsersFromProject(int id, List<int> userIds)
        {
            Project project = _db.Projects.Include(p => p.AllUsers).FirstOrDefault(p => p.Id == id);
            foreach (int userId in userIds)
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                if (project.AllUsers.Contains(user))
                    project.AllUsers.Remove(user);
            }
            _db.SaveChanges();
        }


    }
}
