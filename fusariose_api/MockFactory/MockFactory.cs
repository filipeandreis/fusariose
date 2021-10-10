using fusariose_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fusariose_api.MockFactory
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
                Humidity = "nao"
            });

            data.Add(new DataModel()
            {
                Id = Guid.NewGuid(),
                Temperature = 22,
                Rain = "sim",
                Humidity = "nao"
            });

            data.Add(new DataModel()
            {
                Id = Guid.NewGuid(),
                Temperature = 21,
                Rain = "nao",
                Humidity = "nao"
            });

            return data;
        }
    }
}
