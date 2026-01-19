using Microsoft.AspNetCore.Mvc;

using Entity_Directories.Services.DTO;
using Alumni_Portal.Infrastructure.Data_Models;
using Entity_Directories.Services;



namespace Admin.Controllers

{

    //[Authorize(Roles = "Admin")]
    [Route("api/Admin/users")]
    [ApiController]

    public class UserController : Controller

    {

        private UserService _userDirectory;
        public UserController( UserService userDirectory)
        {
            _userDirectory = userDirectory;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var response = await _userDirectory.GetUser(id);
            if (response == null)
            {
                return NotFound(new {message="No user found with this Institution ID"});
            }
            return Ok(response);
        }


       
        [HttpGet]

        public async Task<ActionResult> getUsers([FromQuery] string type, int _page, int _size)
        {
            
                var response = await _userDirectory.GetUsersPaginated(type, _page, _size);
                
                 return Ok(response);
            
            
        }


        [HttpPost("create")]
        [Consumes("application/json")]

        public async Task<IActionResult> create([FromBody] Individuals newUser, [FromQuery] string type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
                int createdId= await _userDirectory.CreateUser(newUser);
                var response = new 
                {
                    Status = "Success",
                    Message = "User successfully created.",
                    UserId = createdId
                };

                return Ok(response);
            

            
        }

        

    }

}

