using fusariose.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fusariose.Controllers
{
    public class ProdutoController : Controller
    {
        [HttpGet]
        public IActionResult Index(int id)
        {
            List<ProdutoModel> produtos = MockFactory.MockFactory.GerarListaProdutos(10);
            //ViewBag.ListaProdutos = produtos;
            return View(produtos);
        }

        [Route("/NovoProduto")]
        public IActionResult Formulario()
        {
            List<CategoriaProdutoModel> categorias = MockFactory.MockFactory.GerarListaCategoriaProduto(5);
            ViewBag.ListaCategorias = categorias;
            ViewBag.Produto = new ProdutoModel();

            HttpContext.Session.SetString("CodigoProduto", "123.ABCD");

            return View();
        }

        [Route("/produto/cadastro/{id:int}")]
        public IActionResult Cadastro(int id)
        {
            List<CategoriaProdutoModel> categorias = MockFactory.MockFactory.GerarListaCategoriaProduto(5);
            ViewBag.ListaCategorias = categorias;


            return View("formulario");
        }

        [Route("/produto/cadastro/{id:alpha}")]
        public IActionResult Cadastro(string id)
        {
            List<CategoriaProdutoModel> categorias = MockFactory.MockFactory.GerarListaCategoriaProduto(5);
            ViewBag.ListaCategorias = categorias;
            return View("formulario");
        }



        [HttpPost]
        public IActionResult Cadastrar(ProdutoModel produto)
        {
            if (produto.Quantidade < 100)
            {
                ModelState.AddModelError("produto.quantidadeincorreta", "A quantidade deve ser maior que 100!");
            }
            
            
            if (ModelState.IsValid)
            {
                //Inclusao destes dados no banco de dados
                return RedirectToAction("Index");
            }
            else
            {
                List<CategoriaProdutoModel> categorias = MockFactory.MockFactory.GerarListaCategoriaProduto(5);
                ViewBag.ListaCategorias = categorias;
                ViewBag.Produto = produto;
                return View("Formulario");
            }
        }
    }
}
