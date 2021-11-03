using fusariose.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fusariose.MockFactory
{
    public class MockFactory
    {
        public static List<DataModel> GerarListaDados()
        {
            List<DataModel> data = new();

            data.Add(new DataModel()
            {
                Id = Guid.NewGuid(),
                Temperature = 25,
                Rain = false,
                Humidity = false,
                Date = new DateTime()
            });

            data.Add(new DataModel()
            {
                Id = Guid.NewGuid(),
                Temperature = 22,
                Rain = false,
                Humidity = false,
                Date = new DateTime()
            });

            data.Add(new DataModel()
            {
                Id = Guid.NewGuid(),
                Temperature = 21,
                Rain = true,
                Humidity = false,
                Date = new DateTime()
            });

            return data;
        }

        public static List<UserModel> CreateAdminUser()
        {
            List<UserModel> data = new();

            data.Add(new UserModel()
            {
                Username = "admin",
                Password = "admin"
            });

            return data;
        }
    }
}
