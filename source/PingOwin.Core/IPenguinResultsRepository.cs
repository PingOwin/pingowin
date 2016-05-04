using System.Collections.Generic;
using System.Threading.Tasks;

namespace PingOwin.Core
{
    public interface IPenguinResultsRepository
    {
        Task<IEnumerable<PenguinResult>> GetAll();
        Task<bool> Insert(PenguinResult target);
    }
}