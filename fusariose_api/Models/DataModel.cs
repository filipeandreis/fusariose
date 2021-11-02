using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fusariose_api.Models
{
    public class DataModel
    {
        public Guid Id { get; set; }
        public int Temperature { get; set; }
        public string Rain { get; set; }
        public string Humidity { get; set; }
        public string Month { get; set; }
    }
}
