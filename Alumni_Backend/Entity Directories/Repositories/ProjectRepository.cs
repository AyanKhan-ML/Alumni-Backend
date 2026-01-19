
using System;
using System.Linq.Expressions;
using Entity_Directories.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Entity_Directories.Services.DTO;
using Alumni_Portal.Infrastructure.Data_Models;
using Alumni_Portal.Infrastructure.Persistence;
using Entity_Directories.Repositories.MappingExpressions;
namespace Entity_Directories.Repositories
{
    
        internal class ProjectRepository : IProjectRepository
    {
            private ProjectDbContext _context;
            public ProjectRepository(ProjectDbContext context)
            {
                _context = context;
            }

            private static Expression<Func<Projects, projectDTO>> ProjectToDTO()
            {
                return p => new projectDTO
                {
                    Project_Academic_ID = p.Project_Academic_ID,
                    Project_Name = p.Project_Name,
                    //Project_Category = p.Project_Category ?? "N/A",
                    Project_Type = p.Project_Type_Value,
                    Project_Year = p.Project_Year,
                    Is_Mentored = p.Is_Mentored,
                    Is_Sponsored = p.Is_Sponsored
                };
            }


            public IQueryable<projectDTO> getProjects(ProjectFilters filters)
            {

                return _context.Projects
                              .Where(p => filters.Types == null ||filters.Types.Count==0|| filters.Types.Contains(p.Project_Type_ID))
                              .Select(ProjectToDTO())
                              .OrderByDescending(p => p.Project_Year);
            }

            public async Task<projectDTO?> getProjectsByAcademicID(string projectAcademicID)
            {
                var project = await _context.Projects
                              .Where(p => p.Project_Academic_ID == projectAcademicID)
                              .Select(ProjectToDTO())
                              .FirstOrDefaultAsync();



                return project;
            }

            //public async Task<int> createProject(Projects newProject)
            //{
            //    using var transaction = await _context.Database.BeginTransactionAsync();
            //    try
            //    {
            //        _context.Projects.Add(newProject);



            //    }
            //}



        }
    }
