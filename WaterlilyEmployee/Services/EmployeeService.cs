using WaterlilyEmployee.Helpers;
using WaterlilyEmployee.Models;
using WaterlilyEmployee.Repositories;

namespace WaterlilyEmployee.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repo;
        private readonly CacheHelper _cacheHelper;

        public EmployeeService(IRepository<Employee> repo, CacheHelper cacheHelper)
        {
            _repo = repo;
            _cacheHelper = cacheHelper;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(Func<Employee, bool>? filter = null)
        {
            var all = _cacheHelper.CachedLong("employees_all", () => _repo.GetAllAsync().Result.ToList());
            return filter != null ? all.Where(filter) : all;
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task AddEmployeeAsync(Employee e)
        {
            await _repo.AddAsync(e);
            _cacheHelper.CachedLong("employees_all", () => _repo.GetAllAsync().Result.ToList());
        }

        public async Task UpdateEmployeeAsync(Employee e)
        {
            await _repo.UpdateAsync(e);
            _cacheHelper.CachedLong("employees_all", () => _repo.GetAllAsync().Result.ToList());
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _repo.DeleteAsync(id);
            _cacheHelper.CachedLong("employees_all", () => _repo.GetAllAsync().Result.ToList());
        }
    }
}
