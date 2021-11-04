using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fusariose_api.Models
{
    public class DataModel
    {
        public Guid Id { get; set; }
        public int Temperature { get; set; }
        public int Rain { get; set; }
        public int Humidity { get; set; }
        public DateTime Date { get; set; }
    }
}
