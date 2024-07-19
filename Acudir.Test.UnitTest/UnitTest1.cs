using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Acudir.Test.Core.Entities;
using Acudir.Test.Infrastructure.Data;
using Acudir.Test.Infrastructure.Interfaces;
using Acudir.Test.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Acudir.Test.Core.Interfaces;

namespace TestProject1
{
    public class PersonRepositoryTests
    {
        private IPersonContext _context;
        private IPersonRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PersonContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new PersonContext(options);
            _repository = new PersonRepository(_context);

            // Cargar datos iniciales si es necesario
            _context.LoadData();
        }

        [Test]
        public async Task CreatePerson_ShouldAddPerson()
        {
            // Arrange
            var newPerson = new Person
            {
                id = 4,
                NombreCompleto = "Nuevo Persona",
                Edad = 30,
                Domicilio = "Nueva Calle 123",
                Telefono = "12345678",
                Profesion = "Ingeniero",
                Active = true
            };

            // Act
            await _repository.Create(newPerson);
            var personFromDb = await _repository.Get(4);

            // Assert
            Assert.IsNotNull(personFromDb);
            Assert.AreEqual("Nuevo Persona", personFromDb.NombreCompleto);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllPersons()
        {
            // Act
            var persons = await _repository.GetAll();

            // Assert
            Assert.IsNotNull(persons);
            Assert.IsTrue(persons.Any());
        }

        [Test]
        public async Task UpdatePerson_ShouldModifyPerson()
        {
            // Arrange
            var person = await _repository.Get(1);
            person.NombreCompleto = "Nombre Actualizado";

            // Act
            var result = await _repository.Update(person);
            var updatedPerson = await _repository.Get(1);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Nombre Actualizado", updatedPerson.NombreCompleto);
        }

        [Test]
        public async Task DeletePerson_ShouldRemovePerson()
        {
            // Act
            var result = await _repository.Delete(1);
            var person = await _repository.Get(1);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(person);
        }
    }
}
