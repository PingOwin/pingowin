using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingIt.Lib
{
    public interface ITargetRepository
    {
        Task<IEnumerable<Target>> GetAll();
        Task<bool> Insert(Target target);
    }
}
