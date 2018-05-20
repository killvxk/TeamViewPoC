using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamViewPoC.Models;

namespace TeamViewPoC.Services
{
    public interface IProjectDataService
    {
        Task AddProject(Project project);
        Task<Project> GetProjectById(int? id);
        Task<Project> GetProjectById(int? id, string option);
    }
}
