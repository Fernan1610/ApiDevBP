using ApiDevBP.Entities;
using ApiDevBP.Models;
using ApiDevBP.Models.User;
using ApiDevBP.Models.User.DTO;
using Microsoft.AspNetCore.Mvc;
using SQLite;
using System.Reflection;

namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly  SQLiteConnection _db;
        
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;

        public UsersController(ILogger<UsersController> logger , IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
          
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserModel user)
        {
            try
            {
                var result =_userRepository.SaveUser(user);

                if (result > 0)
                {
                    return Ok("Usuario guardado correctamente");
                }
                else
                {
                    return BadRequest("No se pudo guardar el usuario");
                }
                
            }catch (Exception ex)
            {
                 throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = _userRepository.GetUsers();
                if (users != null)
                {
                    return Ok(users);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(int UserId, string name="", string lastname = "")
        {
            try
            {
                var users = _userRepository.EditUser(UserId, name, lastname);
                if (users != null)
                {
                    return Ok(users);
                }
                return BadRequest("No se pudo actualizar el usuario");

            }catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int UserId)
        {
            try
            {
                var result = _userRepository.DeleteUser(UserId);

                if (result)
                {
                    return Ok("Usuario eliminado correctamente");
                }

                return NotFound("No se encontró el usuario para eliminar");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
