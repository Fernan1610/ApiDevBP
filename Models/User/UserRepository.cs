using ApiDevBP.Models.User.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ApiDevBP.Models.User
{
    public class UserRepository : IUserRepository
    {
        public Task<IActionResult> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> SaveUser(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
