using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fusariose.Models
{
    public class DataModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Campo obrigatório")]
        public int Temperature { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Rain { get; set; }
        public string Humidity { get; set; }
    }
}
