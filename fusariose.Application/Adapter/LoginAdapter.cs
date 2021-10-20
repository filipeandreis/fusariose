using fusariose.Application.DTO;
using fusariose.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusariose.Application.Adapter
{
    public class LoginAdapter
    {
        public static LoginDTO ToLoginDTO(Login login)
        {
            return new LoginDTO()
            {
                Username = login.Username,
                Password = login.Password
            };
        }

        public static Login ToLoginDomain(LoginDTO login)
        {
            return new Login()
            {
                Username = login.Username,
                Password = login.Password
            };
        }
    }
}
