using ApiDevBP.Models.User.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ApiDevBP.Models.User
{
    public interface IUserRepository
    {
        int SaveUser(UserDTO user);
        ICollection<UserModel> GetUsers();

        UserModel EditUser(int UserId, string name, string lastname);
        bool DeleteUser(int UserId);
    }
}
