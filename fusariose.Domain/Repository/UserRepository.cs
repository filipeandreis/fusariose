using fusariose.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusariose.Domain.Repository
{
    public interface IUserRepository
    {
        public List<User> GetAll();
        public User Get(string username);
        public Guid Add(User user);
        public void Change(User user);
        public void Delete(Guid username);
    }
}
