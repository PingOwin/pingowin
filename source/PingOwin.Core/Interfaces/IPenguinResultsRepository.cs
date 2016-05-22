using System.Collections.Generic;
using System.Threading.Tasks;
using PingOwin.Core.Processing;

namespace PingOwin.Core.Interfaces
{
    public interface IPenguinResultsRepository
    {
        Task<IEnumerable<PenguinResult>> GetAll();
        Task<IEnumerable<PenguinResult>> GetAll(int skip, int take);
        Task<bool> Insert(PenguinResult target);
    }
}