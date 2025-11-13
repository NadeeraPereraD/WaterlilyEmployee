using Microsoft.EntityFrameworkCore;
using WaterlilyEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WaterlilyEmployee.Repositories
{
    public class WorkingDaysRepository
    {
        private readonly WaterlilyEmployeeDbContext _context;
        
        public WorkingDaysRepository(WaterlilyEmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<List<DateTime>> GetPublicHolidaysAsync()
        {
            return await _context.PublicHolidays.FromSqlRaw("EXEC sp_GetPublicHolidays").Select(h => h.HolidayDate.ToDateTime(TimeOnly.MinValue)).ToListAsync();
        }
    }
}
