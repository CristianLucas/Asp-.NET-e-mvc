using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

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



        public async Task< IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
         
            var departments = await _departmentService.FindAllAsynk();
            var viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller) //Cria o novo vendedor
        {
            if (!ModelState.IsValid) //Verifica se o formulário do seller está válidado (previne quando o javascript do clinte está desabilitado
            {
                var departments = await _departmentService.FindAllAsynk();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
           await _sellerService.InsertAsync(seller); //Insere através do método no banco
            return RedirectToAction(nameof(Index)); //Após isso redireciona para a view para mostrar o resultado novamente
        }


        public async Task<IActionResult> Delete(int? id)
        {
            
            if(id == null) //Valida se de fato o vendedor com o Id selecionado existe
            {
                return NotFound();//Mensagem de não encontrado
            }

            var obj = await _sellerService.FindbyIdAsync(id.Value);

            if (obj== null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost] //Indica que este método é POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) //Método de remoção com método de envio POST
        {
           await  _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index)); //Após a remoção é enviado para a index novamente
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) //Valida se de fato o vendedor com o Id selecionado existe
            {
                return NotFound(); //Mensagem de não encontrado
            }

            var obj = await _sellerService.FindbyIdAsync(id.Value);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var obj = await _sellerService.FindbyIdAsync(id.Value);

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
        public async  Task<IActionResult> Edit(int id, Seller seller)
        {

            if (!ModelState.IsValid) //Verifica se o formulário do seller está válidado (previne quando o javascript do clinte está desabilitado
            {
                var departments =await _departmentService.FindAllAsynk();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                await _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }catch(NotFoundException e)
            {
                return NotFound();
            }  catch(DbConcurrencyException e )
            {
                return BadRequest();
            }

             IActionResult Error(string message)
            {
                var viewModel = new ErrorViewModel()
                {
                    Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier//Massete para pegar o ID interno da requisição
                };
                return View(viewModel);
            }
        }


    }



}
