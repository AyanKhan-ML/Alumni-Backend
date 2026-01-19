using Entity_Directories.Services.DTO;
using Entity_Directories.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Shared.Custom_Exceptions.ExceptionClasses;

namespace Entity_Directories.Services
{

    public class ProjectService
    {
        private IProjectRepository _projectRepo;

        public ProjectService(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task<projectDTO?> GetProject(string projectAcademicID)
        {
            return await _projectRepo.getProjectsByAcademicID(projectAcademicID);
        }

        public async Task<List<projectDTO>> GetProjectsPaginated(ProjectFilters filters, int _page, int _limit)
        {
            
            if (_page > 0 || _limit > 0)
            {

                var users = await _projectRepo.getProjects(filters)
                        .Skip((_page - 1) * _limit)
                        .Take(_limit)
                        .ToListAsync();
                return users;
            }

           

            throw new ValidationException("Page and Limit must be greater than 0");

        }
    }
}
