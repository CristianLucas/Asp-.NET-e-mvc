using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //Declarando um dependencia para o seller service e chamar o método FindALL do sellerservice
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }



        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {

            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) //Cria o novo vendedor
        {
            _sellerService.Insert(seller); //Insere através do método no banco
            return RedirectToAction(nameof(Index)); //Após isso redireciona para a view para mostrar o resultado novamente
        }


        public IActionResult Delete(int? id)
        {
            if(id == null) //Valida se de fato o vendedor com o Id selecionado existe
            {
                return NotFound(); //Mensagem de não encontrado
            }

            var obj = _sellerService.FindbyId(id.Value);

            if (obj== null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost] //Indica que este método é POST
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) //Método de remoção com método de envio POST
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index)); //Após a remoção é enviado para a index novamente
        }


        public IActionResult Details(int? id)
        {
            if (id == null) //Valida se de fato o vendedor com o Id selecionado existe
            {
                return NotFound(); //Mensagem de não encontrado
            }

            var obj = _sellerService.FindbyId(id.Value);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindbyId(id.Value);

            if(obj == null)
            {
                return NotFound();
            }
            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
        if(id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }catch(NotFoundException e)
            {
                return NotFound();
            }  catch(DbConcurrencyException e )
            {
                return BadRequest();
            }
        }


    }



}
