using Acudir.Test.Application.Handlers;
using MediatR;
using Acudir.Test.Application.Mappers;
using Acudir.Test.Application.Queries;
using Acudir.Test.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Acudir.Test.Infrastructure.Data;
using Acudir.Test.Infrastructure.Interfaces;
using Acudir.Test.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Configuración de MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllPersonsHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetPersonByIdQuery).Assembly));

//Configuración de DBbaseMock.
builder.Services.AddDbContext<PersonContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

// Registro de PersonContext con _filePath
string filePath = "./Test.json";

builder.Services.AddSingleton(filePath);

builder.Services.AddScoped<IPersonContext>(provider =>
{
    var options = provider.GetRequiredService<DbContextOptions<PersonContext>>();
    var filePath = provider.GetRequiredService<string>();
    return new PersonContext(options, filePath);
});

builder.Services.AddScoped<IPersonRepository, PersonRepository>();

var app = builder.Build();

var personContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<IPersonContext>();
personContext.LoadData(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Person.API - v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();