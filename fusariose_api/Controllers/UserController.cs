using fusariose.Application;
using fusariose.Application.DTO;
using fusariose.Domain.Repository;
using fusariose.Repository;
using fusariose_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace fusariose_api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly UserApplication userApplication;

        public UserController(IConfiguration configuration)
        {
            string strConexao = configuration.GetConnectionString("dbconnection");

            userRepository = new UserRepository(strConexao);
            userApplication = new UserApplication(userRepository);
        }

        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public IActionResult GetAll()
        {
            var allData = userApplication.GetAll();

            List<UserModel> listUser = new();

            foreach (var userDTO in allData)
            {
                listUser.Add(new UserModel()
                {
                    Id = userDTO.Id,
                    Username = userDTO.Username,
                    Password = userDTO.Password
                }); ;
            }

            return Ok(listUser);
        }

        [HttpGet]
        [Route("api/[controller]/get/{idUser}")]
        public IActionResult Get([FromRoute] string idUser)
        {
            var userDTO = userApplication.Get(idUser);

            var user = new UserModel()
            {
                Id = userDTO.Id,
                Username = userDTO.Username,
                Password = userDTO.Password
            }; 

            return Ok(user);
        }

        [HttpPost]
        [Route("api/[controller]/Authenticate")]
        public IActionResult Authenticate([FromBody] UserModel user)
        {
            try
            {
                var userDTO = userApplication.Get(user.Username);

                var userModel = new UserModel()
                {
                    Username = userDTO.Username,
                    Password = userDTO.Password
                };

                if(user.Password.Equals(userModel.Password))
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/[controller]/Store")]
        public IActionResult Store([FromBody] UserModel user)
        {
            var userDTO = new UserDTO()
            {
                Id = Guid.NewGuid(),
                Username = user.Username,
                Password = user.Password
            };

            var userAdd = userApplication.Add(userDTO);

            if (!String.IsNullOrEmpty(userAdd.ToString()))
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}
