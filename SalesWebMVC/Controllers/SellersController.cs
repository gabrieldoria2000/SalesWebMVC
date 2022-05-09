using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var List = _sellerService.FindAll();
            return View(List);
        }
        public IActionResult Create()
        {
            return View();
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
    }
}

