using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingOwin.Core
{
    public interface IPenguinRepository
    {
        Task<IEnumerable<Penguin>> GetAll();
        Task<bool> Insert(Penguin target);
    }
}
