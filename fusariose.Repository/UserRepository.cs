using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fusariose.Domain.Entidades;
using fusariose.Domain.Repository;
using Npgsql;

namespace fusariose.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string strConexao;

        public UserRepository(string strConexao)
        {
            this.strConexao = strConexao;
        }

        public Guid Add(User user)
        {
            using NpgsqlConnection conn = new(strConexao);

            conn.Open();

            NpgsqlCommand query = new()
            {
                Connection = conn,

                CommandText = "INSERT INTO users (id, username, password) VALUES (@id, @username, @password);"
            };

            query.Parameters.AddWithValue("id", user.Id.ToString());
            query.Parameters.AddWithValue("username", user.Username.ToString());
            query.Parameters.AddWithValue("password", user.Password.ToString());

            try
            {
                query.ExecuteNonQuery();

                return user.Id;
            }
            catch
            {
                throw new ApplicationException("Usuário já existe");
            }
        }

        public void Change(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid username)
        {
            throw new NotImplementedException();
        }

        public User Get(string username)
        {
            using NpgsqlConnection conn = new(strConexao);

            var user = new User();

            conn.Open();

            NpgsqlCommand query = new()
            {
                Connection = conn,

                CommandText = "SELECT * FROM users WHERE username = @username;"
            };

            query.Parameters.AddWithValue("username", username);

            NpgsqlDataReader reader = query.ExecuteReader();

            while (reader.Read())
            {
                user = new User()
                {
                    Username = reader["username"].ToString(),
                    Password = reader["password"].ToString(),
                };
            }

            return user;
        }

        public List<User> GetAll()
        {
            List<User> listUser = new();

            using (NpgsqlConnection conn = new(strConexao))
            {
                conn.Open();

                NpgsqlCommand query = new()
                {
                    Connection = conn,

                    CommandText = "SELECT * FROM users;"
                };

                NpgsqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    listUser.Add(
                        new User()
                        {
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString(),
                        }); ;
                }
            }
            return listUser;
        }

        public User Set(Guid username)
        {
            throw new NotImplementedException();
        }
    }
}
