using System;
using System.ComponentModel.DataAnnotations;

namespace fusariose.Models
{
    public class DataModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Campo obrigatório")]
        public int Temperature { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Rain { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Humidity { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime Date { get; set; }
        public string Risk { get; set; }
    }
}
