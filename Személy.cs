using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPéldák
{
    class Személy
    {
        public string név;
        public int osztályAz;
        public double átlag;

        public Személy(string név, int osztályAz, double átlag)
        {
            this.név = név;
            this.osztályAz = osztályAz;
            this.átlag = átlag;
        }
    }
}
