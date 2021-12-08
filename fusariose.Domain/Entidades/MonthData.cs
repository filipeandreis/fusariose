using System;

namespace fusariose.Domain.Entidades
{
    public class MonthData
    {
        public Guid Id { get; set; }
        public int Temperature { get; set; }
        public int Rain { get; set; }
        public int Humidity { get; set; }
        public string Month { get; set; }
        public string Risk { get; set; }

        public MonthData()
        {
            Id = new Guid();
            Temperature = 24;
            Rain = 0;
            Humidity = 15;
            Month = "";
            Risk = "";
        }
    }
}
