using fusariose.Application.Adapter;
using fusariose.Application.DTO;
using fusariose.Domain.Entidades;
using fusariose.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusariose.Application
{
    public class UserApplication
    {
        private IUserRepository userRepository;

        public UserApplication(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserDTO Get(string username)
        {
            var user = userRepository.Get(username);

            return UserAdapter.ToUserDTO(user);
        }

        public List<UserDTO> GetAll()
        {
            List<User> users = userRepository.GetAll();

            List<UserDTO> userDTO = new();

            foreach (var elem in users)
            {
                userDTO.Add(UserAdapter.ToUserDTO(elem));
            }

            return userDTO;
        }

        public Guid Add(UserDTO user)
        {
            var userModel = new User()
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password
            };

            var userId = userRepository.Add(userModel);

            return userId;
        }
        public Guid Change(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid idUser)
        {
            throw new NotImplementedException();
        }
    }
}
