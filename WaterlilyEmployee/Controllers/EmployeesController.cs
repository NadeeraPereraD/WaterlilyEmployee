using Microsoft.AspNetCore.Mvc;
using WaterlilyEmployee.Models;
using WaterlilyEmployee.Services;

namespace WaterlilyEmployee.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllEmployeesAsync();
            return View(list);
        }

        public IActionResult Create() => View(new Employee());

        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {
            if (!ModelState.IsValid) return View(model);
            await _service.AddEmployeeAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var emp = await _service.GetEmployeeByIdAsync(id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee model)
        {
            if (!ModelState.IsValid) return View(model);
            await _service.UpdateEmployeeAsync(model);
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _service.DeleteEmployeeAsync(id);
        //    return RedirectToAction("Index");
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
