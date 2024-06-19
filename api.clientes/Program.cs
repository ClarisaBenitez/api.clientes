using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Services.Logica;
using FluentValidation;
using api.clientes.Validators;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"),
    b => b.MigrationsAssembly("Repository")));

// Inyección de dependencias
builder.Services.AddTransient<ICliente, ClienteRepository>();
builder.Services.AddTransient<ClienteService>();
builder.Services.AddTransient<IValidator<ClienteModel>, ClienteValidator>();

builder.Services.AddTransient<IFactura, FacturaRepository>();
builder.Services.AddTransient<FacturaService>();
builder.Services.AddTransient<IValidator<FacturaModel>, FacturaValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Habilitar CORS
app.UseCors("AllowAllOrigins");

app.MapControllers();
app.Run();