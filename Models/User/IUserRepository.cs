using ApiDevBP.Models.User.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ApiDevBP.Models.User
{
    public interface IUserRepository
    {
        Task<IActionResult> SaveUser(UserModel user);
        Task<IActionResult> GetUsers();
    }
}
