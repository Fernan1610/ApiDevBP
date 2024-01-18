using ApiDevBP.Entities;
using ApiDevBP.Models;
using ApiDevBP.Models.User;
using ApiDevBP.Models.User.DTO;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger , IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserDTO user)
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
                Console.WriteLine(ex.Message); 
                return BadRequest("Ocurrió un error al intentar insertar un usuario.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = _userRepository.GetUsers();
                // si lo que devuelve es distinto de null quiere decir que encontro usuarios
                if (users != null)
                {
                    var userModel = _mapper.Map<ICollection<UserModel>>(users);
                    return Ok(userModel);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                return BadRequest("Ocurrió un error al intentar traer los usuario.");
            }
            
        }

        //Este metodo edita un usuario mediante el id de usuario 
        //Si no se ingresa name o lastname se le agrega como valor por defecto en vacio 
        //si users es distinto de null quiere decir que edito el usuario y lo devuelve para mostrarlo
        [HttpPut]
        public async Task<IActionResult> EditUser(int UserId, string name="", string lastname = "")
        {
            
            try
            {
                var users = _userRepository.EditUser(UserId, name, lastname);
                if (users != null)
                {
                    var userModel = _mapper.Map<UserModel>(users);
                    return Ok(userModel);
                }
                return BadRequest("No se pudo actualizar el usuario");

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                return BadRequest("Ocurrió un error al intentar editar el usuario.");
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
                Console.WriteLine(ex.Message);
                return BadRequest("Ocurrió un error al intentar eliminar el usuario.");
            }
        }

    }
}
