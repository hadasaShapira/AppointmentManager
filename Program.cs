using AppointmentManager.Repositories.EFGetStarted.DataBase;
using AppointmentManager.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using AppointmentManager.Repositories.EFGetStarted.DataBase;
using AppointmentManager.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//שליפה של נתונים דרך apointmetRepository
//builder.Services.AddDbContext<AppointmentManagerContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Register the repository
//builder.Services.AddScoped<AppointmentRepository>();
//--------------------------------------------------------------

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
