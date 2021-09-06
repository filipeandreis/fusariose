using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fusariose.Models
{
    public class ProdutoModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Campo obrigatório")]
        public string Nome { get; set; }
        public decimal Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public Guid CategoriaId { get; set; }

    }
}
