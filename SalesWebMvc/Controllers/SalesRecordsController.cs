using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models.Services;
using System;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _Service;

        public SalesRecordsController(SalesRecordService service)
        {
            _Service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? fromDate,
                                                      DateTime? toDate)
        {
            fromDate = fromDate.HasValue == true ? fromDate : new DateTime(2018, 01, 01);
            toDate = toDate.HasValue == true ? toDate : new DateTime(2018, 12, 31);

            var result = await _Service.FindByDateAsync(fromDate, toDate);

            ViewData["fromDate"] = fromDate.Value.ToString("yyyy-MM-dd");
            ViewData["toDate"] = toDate.Value.ToString("yyyy-MM-dd");
            return View(result);
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}