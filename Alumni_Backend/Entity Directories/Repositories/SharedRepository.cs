using Alumni_Portal.Infrastrcuture.Data_Models;
using Alumni_Portal.Infrastructure.Persistance;
using Alumni_Portal.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shared.Custom_Exceptions.ExceptionClasses;

namespace Alumni_Portal.Entity_Directories.Repositories
{
    public class SharedRepository
    {
        private IndividualDbContext _individualContext;
        private ProjectDbContext _projectContext;

        public SharedRepository(IndividualDbContext individual_context, ProjectDbContext project_context)
        {
            _individualContext = individual_context;
            _projectContext = project_context;
        }

        public async Task<int>  Individual_Exists_Async(string individualInstitutionID)
        {
           int Individual_ID =await _individualContext.Individuals.
                Where(i => i.Individual_Institution_ID == individualInstitutionID)
                .Select(i => i.Individual_ID).FirstOrDefaultAsync();

            if (Individual_ID == 0)
            {
                throw new ValidationException($"Individual with Institution ID {individualInstitutionID} does not exist.");
            }
 
           

            return Individual_ID;
        }
        
        public async Task<int> Project_Exists_Async(string projectAcademicID)
        {
            int projectId =
                await _projectContext.Projects.
                Where(p=>p.Project_Academic_ID==projectAcademicID)
                .Select(p=>p.Project_ID).FirstOrDefaultAsync();
            if (projectId==0)
            {
                throw new ValidationException($"Project with Academic ID {projectAcademicID} does not exists.");
            }

            return projectId;
        }

        public async Task Individual_Has_ProjectAsync(int individualID)
        {
            bool hasProject=
                await _projectContext.Project_Individuals
                .AnyAsync(pi => pi.Individual_ID == individualID);
                
            if (hasProject)
            
            {
                throw new ValidationException($"Individual with ID {individualID} is already associated with a project.");
            }
           
        }
        
        public async Task AddProjectMembersAsync(List<Project_Individuals> members)
        {
            
                _projectContext.Project_Individuals.AddRange(members);

             
                await _projectContext.SaveChangesAsync();
            

            
        }

        public async Task RollBackAsync(DbContext context)
        {
            await context.DisposeAsync();
        }


    }
}
