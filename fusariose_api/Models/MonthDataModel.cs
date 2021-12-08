using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fusariose_api.Models
{
    public class MonthDataModel
    {
        public Guid Id { get; set; }
        public int Temperature { get; set; }
        public int Rain { get; set; }
        public int Humidity { get; set; }
        public string Month { get; set; }
        public string Risk { get; set; }
    }
}
