using Microsoft.EntityFrameworkCore;
using WaterlilyEmployee.Models;

namespace WaterlilyEmployee.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly WaterlilyEmployeeDbContext _context;

        public EmployeeRepository(WaterlilyEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync() =>
            await _context.Employees.ToListAsync();

        public async Task<Employee?> GetByIdAsync(int id) =>
            await _context.Employees.FindAsync(id);

        public async Task AddAsync(Employee entity)
        {
            _context.Employees.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var e = await _context.Employees.FindAsync(id);
            if (e != null)
            {
                _context.Employees.Remove(e);
                await _context.SaveChangesAsync();
            }
        }
    }
}
