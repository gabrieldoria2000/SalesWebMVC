using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // public IActionResult Index()
        // {
        //    var List = _sellerService.FindAll();
        //    return View(List);
        // }

        //adaptando para Assincrono
        public async Task<IActionResult> Index()
        {
            var List = await _sellerService.FindAllAsync();
            return View(List);
        }

        // public IActionResult Create()
        //{
        //    var departments = _departmentService.FindAll();
        //    var viewmodel = new SellerFormViewModel { Departments = departments };
        //    return View(viewmodel);
        // }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewmodel = new SellerFormViewModel { Departments = departments };
            return View(viewmodel);
        }

        //Esse ANOTATION validate é pra previnir que alguem use codigos maliciosos aproveitando a sua autenticação
        //e esse ANOTATION httppost é pra indicar que isso é uma ação de post e não de GET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            // ATENÇÃO!!! Validação no Servidor (caso o usuario esteja com o javascript desabilitado!!!!
            if (!ModelState.IsValid)
            {
                //carrega novamente o ViewModel de vendedor
                var departments = await _departmentService.FindAllAsync();
                var viewmodel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewmodel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new {message = "Id not provided!"});
            }

            var obj = await _sellerService.FindByIdAsync(Id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not Found!" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            await _sellerService.RemoveAsync(Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                // return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });
            }

            var obj = await _sellerService.FindByIdAsync(Id.Value);
            if (obj == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not Found!" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });
            }
            var obj = await _sellerService.FindByIdAsync(Id.Value);
            if (obj == null)
            {
                // return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not Found!" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewmodel = new SellerFormViewModel { Seller = obj,Departments = departments };

            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Seller seller)
        {
            //validação no servidor
            if (!ModelState.IsValid)
            {
                //carrega novamente o ViewModel de vendedor
                var departments = await _departmentService.FindAllAsync();
                var viewmodel = new SellerFormViewModel { Seller=seller,Departments = departments };
                return View(viewmodel);
            }

            if (Id != seller.Id)
            {
                // return BadRequest();
                return RedirectToAction(nameof(Error), new { message = "Id mismatch!" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                //return RedirectToAction("Index");
                //dessa forma é mais dificil de ter erros pq tem a ajuda do Intelisense
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                //return NotFound();
                return RedirectToAction(nameof(Error), ex.Message);
            }
            

        }

        public IActionResult Error(string message)
        {
            var viewmodel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier

            };

            return View(viewmodel);

        }
    }
}

