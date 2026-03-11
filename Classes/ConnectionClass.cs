using kpk_avalonia.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpk_avalonia.Classes
{
    internal class ConnectionClass
    {
        public static readonly AppDBContext connect = new AppDBContext();
    }
}
