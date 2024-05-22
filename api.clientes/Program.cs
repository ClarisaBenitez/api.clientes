using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Services.Logica;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"),
    b => b.MigrationsAssembly("Repository")));

// Injeccion de dependencias

builder.Services.AddTransient<IFactura, FacturaRepository>();
builder.Services.AddTransient<FacturaService>();

// Añadir el de cliente
builder.Services.AddTransient<ICliente, ClienteRepository>();
builder.Services.AddTransient<ClienteService>();


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
