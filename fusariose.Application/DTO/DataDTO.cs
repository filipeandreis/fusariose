using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusariose.Application.DTO
{
    public class DataDTO
    {
        public Guid Id { get; set; }
        public int Temperature { get; set; }
        public bool Rain { get; set; }
        public bool Humidity { get; set; }
        public DateTime Date { get; set; }
    }
}
