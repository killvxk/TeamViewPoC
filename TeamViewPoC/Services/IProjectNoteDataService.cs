using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamViewPoC.Models;
using TeamViewPoC.Services;

namespace TeamViewPoC.Services
{
    public interface IProjectNoteDataService
    {
        Task AddProjectNoteAsync(ProjectNote note);
    }
}
