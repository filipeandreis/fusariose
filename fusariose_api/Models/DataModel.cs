using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fusariose_api.Models
{
    public class DataModel
    {
        public Guid Id { get; set; }
        public int Temperature { get; set; }
        public bool Rain { get; set; }
        public bool Humidity { get; set; }
        public DateTime Date { get; set; }
    }
}
