using System.Collections.Generic;
using System.Threading.Tasks;
using PingOwin.Core.Processing;

namespace PingOwin.Core.Interfaces
{
    public interface IPenguinRepository
    {
        Task<IEnumerable<Penguin>> GetAll();
        Task<bool> Insert(Penguin target);
    }
}
