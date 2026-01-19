using Entity_Directories.Services.DTO;


namespace Entity_Directories.Services.Abstractions
{

    
        public interface IProjectRepository
    {
            public IQueryable<projectDTO> getProjects(ProjectFilters filters);
            public Task<projectDTO?> getProjectsByAcademicID(string projectAcademicID);
        }
    }

