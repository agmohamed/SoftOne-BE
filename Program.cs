using SOTaskManagement.Interfaces;
using SOTaskManagement.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SOTaskManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'Application db context' not found.")));
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();



app.MapControllers();

await app.RunAsync();
