using Acudir.Test.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Acudir.Test.Infrastructure.Data;
using Acudir.Test.Infrastructure.Interfaces;
using Acudir.Test.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PersonContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddScoped<IPersonContext>(provider => provider.GetService<PersonContext>());

builder.Services.AddScoped<IPersonRepository, PersonRepository>();

var app = builder.Build();

var personContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<IPersonContext>();
personContext.LoadData(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();