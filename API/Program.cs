
using API.Helpers;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//se injecta el repositorio
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//se injecta el repositorio Generico
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//se injecta el servicio de Automapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Se hace la injeccion de dependencias del DbContext


builder.Services.AddDbContext<StoreContext>(options =>
                         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

//se crea este metodo para poder insertar los datos en la base de datos
//cuando arranca el programa, se manda llamar el metodo static SeedAsync de la clase StoreContextSeed

using(var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var context = services.GetRequiredService<StoreContext>();
    await StoreContextSeed.SeedAsync(context,loggerFactory);


}

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();

//se pone el middleware para poder leer las imagenes
app.UseStaticFiles();

//se coloca este middleware para controlar el controller de errores,
//para cuando no pone un endpoint que no existe
app.UseStatusCodePagesWithReExecute("/errors/{0}");


app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.Run();
