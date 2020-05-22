using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Services;
using SalesWebMvc.Models.Services.Exceptions;
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

        public async Task<IActionResult> Index()
        {
            var list = await _SellerService.FindAllAsync();

            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departmets = await _DepartmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departmets };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            IActionResult result = null;

            if (!ModelState.IsValid) // validate if the JavaScript is enabled and already validated this record
            {
                var departmets = await _DepartmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departmets, Seller = seller };

                result = View(viewModel);
            }
            else
            {
                await _SellerService.InsertAsync(seller);
                result = RedirectToAction(nameof(Index));
            }
            return result;
        }

        public async Task<IActionResult> Delete(int? id)
        {
            IActionResult result = RedirectToAction(nameof(Error), new { message = "Id not found" });

            if (id != null)
            {
                var obj = await _SellerService.FindByIdAsync(id.Value);

                if (obj != null)
                {
                    result = View(obj);
                }
            }
            else
            {
                result = RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _SellerService.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (IntegretyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            IActionResult result = RedirectToAction(nameof(Error), new { message = "Id not found" });

            if (id != null)
            {
                var obj = await _SellerService.FindByIdAsync(id.Value);

                if (obj != null)
                {
                    result = View(obj);
                }
            }
            else
            {
                result = RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            return result;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            IActionResult result = RedirectToAction(nameof(Error), new { message = "Id not founded" });

            if (id != null)
            {
                var obj = await _SellerService.FindByIdAsync(id.Value);

                if (obj != null)
                {
                    var departmets = await _DepartmentService.FindAllAsync();
                    var viewModel = new SellerFormViewModel { Seller = obj, Departments = departmets };

                    result = View(viewModel);
                }
            }
            else
            {
                result = RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            IActionResult result = RedirectToAction(nameof(Error), new { message = "Id mismatch" });

            if (!ModelState.IsValid) // validate if the JavaScript is enabled and already validated this record
            {
                var departmets = await _DepartmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departmets, Seller = seller };

                result = View(viewModel);
            }
            else
            {
                if (id == seller.Id)
                {
                    try
                    {
                        await _SellerService.UpdateAsync(seller);
                        result = RedirectToAction(nameof(Index));
                    }
                    catch (ApplicationException e)
                    {
                        result = RedirectToAction(nameof(Error), new { message = e.Message });
                    }
                }
            }
            return result;
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}