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
        
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {
            
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
            await File.WriteAllTextAsync("./Data/SeedData/Test.json", json);
            return await base.SaveChangesAsync(cancellationToken);
        }

        public void LoadData()
        {
            if (File.Exists("./Data/SeedData/Test.json"))
            {
                var json = File.ReadAllText("./Data/SeedData/Test.json");
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
        }
    }
}