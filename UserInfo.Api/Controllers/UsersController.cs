using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using UserInfo.Api.Models;
using UserInfo.Models;

namespace UserInfo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = _userRepository.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while fetching users: " + ex.Message);
            }
        }

        [HttpPost("Create")]
        public ActionResult CreateUser([FromBody] User newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.Add(newUser);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating user: " + ex.Message);
            }
        }

        [HttpPost("Delete/{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userRepository.GetUser(id);
                    
                    if (user == null)
                    {
                        return new NotFoundResult();
                    }
                    _userRepository.Delete(id);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating user: " + ex.Message);
            }

        }

    }
}
