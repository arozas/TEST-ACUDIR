using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Acudir.Test.Core.Entities;
using Acudir.Test.Infrastructure.Interfaces;

namespace Acudir.Test.Infrastructure.Data
{
    public class PersonContext : DbContext, IPersonContext
    {
        public DbSet<Person> Persons { get; set; }
        private readonly string _filePath;

        public PersonContext(DbContextOptions<PersonContext> options, string filePath) : base(options)
        {
            //_filePath = "Test.json";
            _filePath = filePath;
            //"C:/Users/aleja/OneDrive/Escritorio/Test Acudir/TEST-ACUDIR/Acudir.Test.Apis/Test.json";
                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryDb");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = Persons.ToList();
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(entities, jsonOptions);
            await File.WriteAllTextAsync(_filePath, json);
            return await base.SaveChangesAsync(cancellationToken);
        }

        public void LoadData()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                var entities = JsonSerializer.Deserialize<List<Person>>(json);
                if (entities != null)
                {
                    var existingIds = Persons.Select(p => p.id).ToHashSet();
                    var newEntities = entities.Where(e => !existingIds.Contains(e.id)).ToList();

                    if (newEntities.Any())
                    {
                        Persons.AddRange(newEntities);
                        base.SaveChanges();
                    }
                }
            }
            else
            {
                var initialData = new List<Person>
                {
                    new Person { id = 1, NombreCompleto = "Ramon Perez", Edad = 45, Domicilio = "Av Segurola 1445", Telefono = "4533542", Profesion = "Escritor", Active = true },
                    new Person { id = 2, NombreCompleto = "Rita Pavone", Edad = 78, Domicilio = "Mercedes 4112", Telefono = "45333442", Profesion = "Doctora", Active = true },
                    new Person { id = 3, NombreCompleto = "Pedro Franco", Edad = 34, Domicilio = "Camarones 2343", Telefono = "44565432", Profesion = "Programador", Active = true }
                };

                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(initialData, jsonOptions);
                File.WriteAllText(_filePath, json);

                Persons.AddRange(initialData);
                base.SaveChanges();
            }
        }

    }
}