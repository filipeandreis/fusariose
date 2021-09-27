using fusariose.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using fusariose.Filtros;

namespace fusariose.Controllers
{
    public class ProdutoController : Controller
    {
        [HttpGet]
        //[CustomActionFilter]
        public IActionResult Index(int id)
        {
            List<ProdutoModel> produtos = new List<ProdutoModel>();
            string listaProdutos = HttpContext.Session.GetString("listaprodutos");
            if (string.IsNullOrEmpty(listaProdutos))
            {
                produtos = MockFactory.MockFactory.GerarListaProdutos(10);
            }
            else
            {
                produtos = JsonConvert.DeserializeObject<List<ProdutoModel>>(listaProdutos);
            }
            
            //ViewBag.ListaProdutos = produtos;
            listaProdutos = JsonConvert.SerializeObject(produtos);
            HttpContext.Session.SetString("listaprodutos", listaProdutos);
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
                //Recuperar a string que representa a lista de produtos
                //Convertar a string na lista de produtos
                //Adicionar o novo produto na lista
                //Salvar novamente na variavel de sessao a lista de produtos
                string listaProdutos = HttpContext.Session.GetString("listaprodutos");
                List<ProdutoModel> produtos = JsonConvert.DeserializeObject<List<ProdutoModel>>(listaProdutos);
                produtos.Add(produto);
                listaProdutos = JsonConvert.SerializeObject(produtos);
                HttpContext.Session.SetString("listaprodutos", listaProdutos);

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

        [HttpPost]
        public IActionResult DecrementarQuantidade(Guid idProduto)
        {
            string listaProdutos = HttpContext.Session.GetString("listaprodutos");
            List<ProdutoModel> produtos = JsonConvert.DeserializeObject<List<ProdutoModel>>(listaProdutos);
            var produto = produtos.Where(p => p.Id == idProduto).FirstOrDefault();
            if (produto != null)
            {
                produto.Quantidade--;
                listaProdutos = JsonConvert.SerializeObject(produtos);
                HttpContext.Session.SetString("listaprodutos", listaProdutos);
                return Json(produto);
            }
            return Json(produtos);
        }

    }
}
