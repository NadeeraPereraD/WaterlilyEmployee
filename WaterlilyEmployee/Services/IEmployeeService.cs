using WaterlilyEmployee.Models;

namespace WaterlilyEmployee.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync (Func<Employee, bool>? filter = null);
        Task<Employee?> GetEmployeeByIdAsync (int id);
        Task AddEmployeeAsync (Employee e);
        Task UpdateEmployeeAsync (Employee e);
        Task DeleteEmployeeAsync (int id);
    }
}
