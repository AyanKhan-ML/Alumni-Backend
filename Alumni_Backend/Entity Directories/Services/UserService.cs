using Entity_Directories.Services.DTO;
using Entity_Directories.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Alumni_Portal.Infrastructure.Data_Models;
namespace Entity_Directories.Services
{
    public class UserService
    {

        private IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<userDirectoryDTO?> GetUser(string individualInstitutionID)
        {
            return await _userRepo.GetUserByInstitutionID(individualInstitutionID);
            
        }

        public async Task<List<userDirectoryDTO>> GetUsersPaginated(string type, int _page, int _limit)
        {


            var users = await _userRepo.GetUsers(type)
             .Skip((_page - 1) * _limit)
             .Take(_limit)
             .ToListAsync();


            return users;



        }

        public async Task<int> CreateUser(Individuals newUser)
        {

            int id = await _userRepo.Create(newUser);

            return id;
        }








    }
}
