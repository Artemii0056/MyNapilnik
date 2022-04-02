using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode6
{
    class Player { }
    
    class Gun { }

    class Target { }

    class Unit
    {
        public IReadOnlyCollection<Unit> Units;
    }
}