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
        public string Rain { get; set; }
        public string Humidity { get; set; }
        public string Month { get; set; }
    }
}
