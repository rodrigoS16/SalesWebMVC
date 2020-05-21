using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Services;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _SellerService;
        private readonly DepartmentService _DepartmentService;

        public SellersController(SellerService service, DepartmentService departmentService)
        {
            _SellerService = service;
            _DepartmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _SellerService.FindAll();

            return View(list);
        }

        public IActionResult Create()
        {
            var departmets = _DepartmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departmets };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _SellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            IActionResult result = NotFound();

            if (id != null)
            {
                var obj = _SellerService.FindById(id.Value);

                if (obj != null)
                {
                    result = View(obj);
                }
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _SellerService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}