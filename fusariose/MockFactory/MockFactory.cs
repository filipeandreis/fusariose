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
                Rain = "sim",
                Humidity = "nao",
                Month = "Janeiro"
            });

            data.Add(new DataModel()
            {
                Id = Guid.NewGuid(),
                Temperature = 22,
                Rain = "sim",
                Humidity = "nao",
                Month = "Fevereiro"
            });

            data.Add(new DataModel()
            {
                Id = Guid.NewGuid(),
                Temperature = 21,
                Rain = "nao",
                Humidity = "nao",
                Month = "Março"
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
