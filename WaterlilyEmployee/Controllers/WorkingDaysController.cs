using Microsoft.AspNetCore.Mvc;
using WaterlilyEmployee.Services;

namespace WaterlilyEmployee.Controllers
{
    public class WorkingDaysController : Controller
    {
        private readonly IWorkingDaysService _service;
        public WorkingDaysController(IWorkingDaysService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(DateTime startDate, DateTime endDate)
        //{
        //    try
        //    {
        //        var days = await _service.GetWorkingDaysAsync(startDate, endDate);
        //        ViewBag.Result = days;
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //    }
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Index(DateTime startDate, DateTime endDate)
        {
            try
            {
                var days = await _service.GetWorkingDaysAsync(startDate, endDate);
                ViewBag.Result = days;

                // Pass values back to the view to keep them in the input fields
                ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
                ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                // Keep values even on error
                ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
                ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");
            }
            return View();
        }



    }
}
