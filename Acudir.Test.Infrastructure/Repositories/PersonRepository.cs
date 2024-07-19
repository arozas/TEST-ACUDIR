using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acudir.Test.Core.Entities;
using Acudir.Test.Core.Interfaces;
using Acudir.Test.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Acudir.Test.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IPersonContext _personContext;

        public PersonRepository(IPersonContext personContext)
        {
            _personContext = personContext;
        }

        public async Task<Person> Get(int id)
        {
            return await _personContext.Persons.FindAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _personContext.Persons.ToListAsync();
        }

        public async Task<Person> Create(Person person)
        {
            _personContext.Persons.Add(person);
            await _personContext.SaveChangesAsync();
            return person;
        }

        public async Task<bool> Update(Person person)
        {
            var existingPerson = await _personContext.Persons.FindAsync(person.id);
            if (existingPerson == null)
            {
                return false;
            }

            existingPerson.NombreCompleto = person.NombreCompleto;
            existingPerson.Edad = person.Edad;
            existingPerson.Domicilio = person.Domicilio;
            existingPerson.Telefono = person.Telefono;
            existingPerson.Profesion = person.Profesion;

            await _personContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var person = await _personContext.Persons.FindAsync(id);
            if (person == null)
            {
                return false;
            }

            _personContext.Persons.Remove(person);
            await _personContext.SaveChangesAsync();
            return true;
        }
    }
}
