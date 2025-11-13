using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            //await _context.Employees.ToListAsync();
            return await _context.Employees.FromSqlRaw("EXEC sp_GetAllEmployees").ToListAsync();         
        }
            
        public async Task<Employee?> GetByIdAsync(int id)
        {
            //await _context.Employees.FindAsync(id);
            var param = new SqlParameter("@Id", id);
            var list = await _context.Employees.FromSqlRaw("EXEC sp_GetEmployeeById @Id", param).ToListAsync();
            return list.FirstOrDefault();
        }           

        public async Task AddAsync(Employee entity)
        {
            //_context.Employees.Add(entity);
            //await _context.SaveChangesAsync();
            var parameters = new[]
            {
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@JobPosition", entity.JobPosition),
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_InsertEmployee @Name, @Email, @JobPosition", parameters);
        }

        public async Task UpdateAsync(Employee entity)
        {
            //_context.Entry(entity).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            var parameters = new[]
            {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@JobPosition", entity.JobPosition),
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateEmployee @Id, @Name, @Email, @JobPosition", parameters);
        }

        public async Task DeleteAsync(int id)
        {
            //var e = await _context.Employees.FindAsync(id);
            //if (e != null)
            //{
            //    _context.Employees.Remove(e);
            //    await _context.SaveChangesAsync();
            //}
            var param = new SqlParameter("@Id", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteEmployee @Id", param);
        }
    }
}
