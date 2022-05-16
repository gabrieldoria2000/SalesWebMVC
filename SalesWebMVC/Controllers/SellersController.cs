using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services.Exceptions;

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

        public IActionResult Index()
        {
            var List = _sellerService.FindAll();
            return View(List);
        }
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewmodel = new SellerFormViewModel { Departments = departments };
            return View(viewmodel);
        }

        //Esse ANOTATION validate é pra previnir que alguem use codigos maliciosos aproveitando a sua autenticação
        //e esse ANOTATION httppost é pra indicar que isso é uma ação de post e não de GET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(Id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int Id)
        {
            _sellerService.Remove(Id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(Id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(Id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewmodel = new SellerFormViewModel { Seller = obj,Departments = departments };

            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, Seller seller)
        {
            if(Id != seller.Id)
            {
                return BadRequest();
            }

            try
            {
                _sellerService.Update(seller);
                //return RedirectToAction("Index");
                //dessa forma é mais dificil de ter erros pq tem a ajuda do Intelisense
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundExceptions ex)
            {
                return NotFound();
            }
            catch (DbConcurrencyException ex)
            {
                return BadRequest();
            }

        }
    }
}

