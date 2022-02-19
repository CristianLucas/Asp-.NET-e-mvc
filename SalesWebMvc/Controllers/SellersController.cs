using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //Declarando um dependencia para o seller service e chamar o método FindALL do sellerservice
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }



        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) //Cria o novo vendedor
        {
            _sellerService.Insert(seller); //Insere através do método no banco
            return RedirectToAction(nameof(Index)); //Após isso redireciona para a view para mostrar o resultado novamente
        }
    }
}
