global using EmployeeCostPreview.Models;
using EmployeeCostPreview.Data;
using EmployeeCostPreview.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add database connection context 
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDependentService, DependentService>();
builder.Services.AddScoped<ICostProjectionService, CostProjectionService>();
builder.Services.AddScoped<IBenefitCalculatorService, BenefitCalculatorService>();


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Projections API",
        Description = "A simple API for adding employee and dependent records to a repository and generating employee benefit cost projections.",
        Contact = new OpenApiContact
        {
            Name = "Michael Ostrowski",
            Email = "michael.ostrowski.az@gmail.com",
            Url = new Uri("https://github.com/KI8IK?tab=repositories"),
        }
    });
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
