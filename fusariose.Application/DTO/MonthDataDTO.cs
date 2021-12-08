using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusariose.Application.DTO
{
    public class MonthDataDTO
    {
        public Guid Id { get; set; }
        public int Temperature { get; set; }
        public int Rain { get; set; }
        public int Humidity { get; set; }
        public string Month { get; set; }
        public string Risk { get; set; }
    }
}
