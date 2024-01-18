using ApiDevBP.Entities;
using ApiDevBP.Models.User.DTO;
using AutoMapper;

namespace ApiDevBP.Models.User
{
    public class UserProfile: Profile
    {
        public  UserProfile()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap();

        }
    }
}
