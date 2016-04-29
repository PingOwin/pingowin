using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingIt.Lib
{
    public interface IPenguinRepository
    {
        Task<IEnumerable<Penguin>> GetAll();
        Task<bool> Insert(Penguin target);
    }
}
