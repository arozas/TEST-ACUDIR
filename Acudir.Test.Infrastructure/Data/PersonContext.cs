using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Acudir.Test.Core.Entities;

namespace Acudir.Test.Infrastructure.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryDb");
        }

        public override int SaveChanges()
        {
            var entities = Persons.ToList();
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(entities, jsonOptions);
            File.WriteAllText("./Data/SeedData/Test.json", json);
            return base.SaveChanges();
        }

        public void LoadData()
        {
            if (File.Exists("./Data/SeedData/Test.json"))
            {
                var json = File.ReadAllText("./Data/SeedData/Test.json");
                var entities = JsonSerializer.Deserialize<List<Person>>(json);
                Persons.AddRange(entities);
                base.SaveChanges();
            }
        }
    }
}