using fusariose.Application.DTO;
using fusariose.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusariose.Application.Adapter
{
    public class UserAdapter
    {
        public static UserDTO ToUserDTO(User user)
        {
            return new UserDTO()
            {
                Username = user.Username,
                Password = user.Password
            };
        }

        public static User ToUserDomain(UserDTO user)
        {
            return new User()
            {
                Username = user.Username,
                Password = user.Password
            };
        }
    }
}
