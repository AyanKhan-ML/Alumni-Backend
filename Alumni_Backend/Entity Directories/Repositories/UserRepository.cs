using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using Entity_Directories.Services.DTO;
using Alumni_Portal.Infrastructure.Persistance;
using Alumni_Portal.Infrastructure.Data_Models;
using Entity_Directories.Services.Abstractions;
using Entity_Directories.Repositories.MappingExpressions;

namespace Entity_Directories.Repositories
{


    public class UserRepository : IUserRepository
    {
        private readonly IndividualDbContext _context;

        public UserRepository(IndividualDbContext context)
        {
            _context = context;
        }

        public Task<userDirectoryDTO?> GetUserByInstitutionID(string individualInstitutionID)
        {
            return _context.Individuals
                  .AsNoTracking()
                  .Where(i => i.Individual_Institution_ID == individualInstitutionID)
                  .Select(UserMappings.AlumniMapping())
                  .FirstOrDefaultAsync();
        }

        public IQueryable<userDirectoryDTO> GetUsers(string individualType)
        {
            var query = _context.Individuals.AsNoTracking();

            return individualType.ToLower() switch
            {
                "alumni" => query
                    .Where(i => i.Individual_Type_Value == "Alumni")
                    .Select(UserMappings.AlumniMapping()),
                "student" => query
                    .Where(i => i.Individual_Type_Value == "Student")
                    .Select(UserMappings.StudentMapping()),
                "supervisor" => query
                    .Where(i => i.Individual_Type_Value == "Supervisor")
                    .Select(UserMappings.SupervisorMapping()),
                _ => throw new ValidationException("Invalid individual type. Must be 'alumni', 'student', or 'supervisor'.")
            };
        }

        public async Task<int> Create(Individuals newUser)
        {
            try
            {
                _context.Individuals.Add(newUser);
                await _context.SaveChangesAsync();
                return newUser.Individual_ID;
                
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while creating the individual.", ex);
            }
        }
    }
}
