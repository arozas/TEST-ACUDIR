using Acudir.Test.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Acudir.Test.Infrastructure.Interfaces
{
    public interface IPersonContext
    {
        DbSet<Person> Persons { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void LoadData();
    }
}