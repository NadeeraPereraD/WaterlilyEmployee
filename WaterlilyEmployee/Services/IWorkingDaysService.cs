namespace WaterlilyEmployee.Services
{
    public interface IWorkingDaysService
    {
        Task<int> GetWorkingDaysAsync(DateTime start, DateTime end);
    }
}
