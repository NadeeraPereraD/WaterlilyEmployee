using Microsoft.EntityFrameworkCore;
using WaterlilyEmployee.Helpers;
using WaterlilyEmployee.Models;
using WaterlilyEmployee.Repositories;
using WaterlilyEmployee.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WaterlilyEmployeeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IWorkingDaysService, WorkingDaysService>();
builder.Services.AddScoped<WorkingDaysRepository>();
builder.Services.AddSingleton<CacheHelper>();
builder.Services.AddMemoryCache();

builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");

app.Run();
