using Microsoft.EntityFrameworkCore;
using G2_Proyecto.Server.Datos;
using G2_Proyecto.Server.Repositorios;
using G2_Proyecto.Server.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ContextoBaseDatos>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Server=(localdb)\\mssqllocaldb;Database=SaborExpressDB;Trusted_Connection=True;"));

builder.Services.AddScoped<ClienteRepositorio>();
builder.Services.AddScoped<ProductoRepositorio>();
builder.Services.AddScoped<ReservaRepositorio>();
builder.Services.AddScoped<PedidoRepositorio>();

builder.Services.AddScoped<ClienteServicio>();
builder.Services.AddScoped<ProductoServicio>();
builder.Services.AddScoped<ReservaServicio>();
builder.Services.AddScoped<PedidoServicio>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
