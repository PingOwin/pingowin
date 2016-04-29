using System.Collections.Generic;
using System.Threading.Tasks;
using PingIt.Lib;

namespace PingOwin
{
    public interface IPenguinResultsRepository
    {
        Task<IEnumerable<PenguinResult>> GetAll();
        Task<bool> Insert(PenguinResult target);
    }
}