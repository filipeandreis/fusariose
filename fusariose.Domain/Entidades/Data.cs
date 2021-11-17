using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fusariose.Domain.Entidades
{
    public class Data
    {
        public Guid Id { get; set; }
        public int Temperature { get; set; }
        public int Rain { get; set; }
        public int Humidity { get; set; }
        public DateTime Date { get; set; }
        public string Risk { get; set; }

        public Data()
        {
            Id = new Guid();
            Temperature = 24;
            Rain = 0;
            Humidity = 15;
            Date = new DateTime();
            Risk = "null";
        }
    }
}
