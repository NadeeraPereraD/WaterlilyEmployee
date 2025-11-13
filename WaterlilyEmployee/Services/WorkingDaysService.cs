using WaterlilyEmployee.Helpers;
using WaterlilyEmployee.Repositories;

namespace WaterlilyEmployee.Services
{
    public class WorkingDaysService : IWorkingDaysService
    {
        private readonly WorkingDaysRepository _repo;
        private readonly CacheHelper _cache;

        public WorkingDaysService(WorkingDaysRepository repo, CacheHelper cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<int> GetWorkingDaysAsync(DateTime start, DateTime end)
        {
            if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
                throw new ArgumentException("Start date cannot be on Saturday or Sunday.");

            if (end < start)
                throw new ArgumentException("End date must be same or after start date.");

            var holidays = _cache.CachedLong("public_holidays", () => _repo.GetPublicHolidaysAsync().Result);

            int workingDays = 0;
            for (var date = start.Date; date <= end.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                if (holidays.Contains(date.Date))
                    continue;

                workingDays++;
            }

            return workingDays;
        }
    }
}
