using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPéldák
{
    class Iskola
    {
        public List<Személy> személyek = new List<Személy>();
        public List<Osztály> osztályok=new List<Osztály>();

        public Iskola()
        {
            osztályok.Add(new Osztály("1a", 1));
            osztályok.Add(new Osztály("1b", 2));
            osztályok.Add(new Osztály("1c", 3));

            személyek.Add(new Személy("endre", 1, 3.5));
            személyek.Add(new Személy("ágota", 2, 4.5));
            személyek.Add(new Személy("ida", 2, 3.2));
            személyek.Add(new Személy("béla", 3, 3.7));
            személyek.Add(new Személy("timi", 1, 3.8));
            személyek.Add(new Személy("robi", 2, 2.5));
            személyek.Add(new Személy("zsolt", 3, 3.9));
            személyek.Add(new Személy("anna", 1, 4.5));
            személyek.Add(new Személy("péter", 2, 4.2));
            személyek.Add(new Személy("jános", 1, 3.3));
        }

    }
}
