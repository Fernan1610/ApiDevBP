using ApiDevBP.Entities;
using ApiDevBP.Models.User.DTO;
using Microsoft.AspNetCore.Mvc;
using SQLite;
using System.Data.Common;
using System.Xml.Linq;

namespace ApiDevBP.Models.User
{
    public class UserRepository : IUserRepository
    {
        private readonly SQLiteConnection _db;

        public UserRepository(SQLiteConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db)); ;

        }

        public bool DeleteUser(int UserId)
        {
            var users = _db.Query<UserEntity>($"Select * from Users");
            var user = users.FirstOrDefault(X => X.Id == UserId);
            if (user != null)
            {
              
                // Realiza la eliminacion del usuario  en la base de datos
                _db.Delete(user);

                // Exito al eliminar
                return true;
            }

            // El usuario no fue encontrado
            return false;
        }

        public UserModel EditUser(int UserId, string name, string lastname)
        {
            var users = _db.Query<UserEntity>($"Select * from Users");
            var user = users.FirstOrDefault(X => X.Id ==UserId);
            if (user != null)
            {
                // Actualiza solo si se proporciona un nuevo valor
                if (!string.IsNullOrEmpty(name))
                {
                    user.Name = name;
                }

                if (!string.IsNullOrEmpty(lastname))
                {
                    user.Lastname = lastname;
                }

                // Realiza la actualización en la base de datos
                _db.Update(user);

                // Puedes devolver el usuario actualizado o alguna otra información relevante
                return new UserModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Lastname = user.Lastname
                };
            }

            // Retorna null o algún valor que indique que el usuario no fue encontrado
            return null;
        }

        public ICollection< UserModel> GetUsers()
        {
            var users = _db.Query<UserEntity>($"Select * from Users");
            if (users != null)
            {
                return users.Select(x => new UserModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Lastname = x.Lastname
                }).ToList();
            }
            return null;
        }

        public int SaveUser(UserModel user)
        {

            var result =_db.Insert(new UserEntity()
            {
                Name = user.Name,
                Lastname = user.Lastname
            });

            //pregunto si la insercion fue exitosa
            // devuelve 1 si se pudo insertar el usuario
            //devuelve 0 si no se pudo insertar el usuario
            if(result != 0 )
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
