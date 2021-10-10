using System;
using System.ComponentModel.DataAnnotations;

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
