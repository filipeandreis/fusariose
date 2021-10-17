using fusariose.Application.DTO;
using fusariose.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusariose.Application.Adapter
{
    public class DataAdapter
    {
        public static DataDTO ToDataDTO(Data data)
        {
            return new DataDTO()
            {
                Id = new Guid(),
                Temperature = 24,
                Rain = "não",
                Humidity = "não"
            };
        }

        public static Data ToDataDomain(DataDTO data)
        {
            return new Data()
            {
                Id = data.Id,
                Temperature = data.Temperature,
                Rain = data.Rain,
                Humidity = data.Humidity
            };
        }
    }
}
