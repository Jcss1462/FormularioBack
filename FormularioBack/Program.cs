using FormularioBack.Context;
using FormularioBack.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Agregar DbContext con conexión de appsettings.json
builder.Services.AddDbContext<FormularioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FormularioDB")));

builder.Services.AddScoped<IFormularioService, FormularioService>();
builder.Services.AddScoped<IRespuestasService, RespuestasService>();

//Añadimos política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()    //permite cualquier origen
                .AllowAnyMethod()    //permite GET, POST, PUT, DELETE, etc.
                .AllowAnyHeader();   //permite cualquier header
        });
});

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

//Usar CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
