using fusariose.Application.DTO;
using fusariose.Domain.Entidades;

namespace fusariose.Application.Adapter
{
    public class MonthDataAdapter
    {
        public static MonthDataDTO ToDataDTO(MonthData data)
        {
            return new MonthDataDTO()
            {
                Id = data.Id,
                Temperature = data.Temperature,
                Rain = data.Rain,
                Humidity = data.Humidity,
                Month = data.Month,
                Risk = data.Risk
            };
        }

        public static MonthData ToDataDomain(MonthDataDTO data)
        {
            return new MonthData()
            {
                Id = data.Id,
                Temperature = data.Temperature,
                Rain = data.Rain,
                Humidity = data.Humidity,
                Month = data.Month,
                Risk = data.Risk
            };
        }
    }
}
