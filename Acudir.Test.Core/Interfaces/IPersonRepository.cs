using Acudir.Test.Core.Entities;

namespace Acudir.Test.Core.Interfaces;

public interface IPersonRepository
{
    Task<Person> Get(int id);
    Task<IEnumerable<Person>> GetAll();
    Task<Person> Create(Person person);
    Task<bool> Update(Person person);
    Task<bool> Delete(int id);
}