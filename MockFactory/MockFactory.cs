using fusariose.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fusariose.MockFactory
{
    public class MockFactory
    {
        public static List<ProdutoModel> GerarListaProdutos(int quantidadeElementos)
        {
            List<ProdutoModel> produtos = new List<ProdutoModel>();

            for(int i = 0; i < quantidadeElementos; i++)
            {
                produtos.Add(new ProdutoModel()
                {
                    Id = Guid.NewGuid(),
                    CategoriaId = Guid.NewGuid(),
                    Nome = "Nome do produto",
                    Quantidade = 10,
                    UnidadeMedida = "PC"
                });
            }

            return produtos;
        }

        public static List<CategoriaProdutoModel> GerarListaCategoriaProduto(int quantidadeElementos)
        {
            List<CategoriaProdutoModel> categorias = new List<CategoriaProdutoModel>();

            for(int i = 0; i < quantidadeElementos; i++)
            {
                categorias.Add(new CategoriaProdutoModel()
                {
                    Descricao = "Descricao categoria",
                    Id = Guid.NewGuid()
                });
            }

            return categorias;
        }
    }
}
