using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqPéldák
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Adatok
            int[] számok = { 5, 4, 5, -3, 7, 11, -6, 7, -3, 0 };
            string[] szavak = { "tégla", "virág", "kapa", "télapó", "kendő", "rózsa" };
            Iskola iskola = new Iskola();
            #endregion

            #region Tömbkezelés
            Tömb_Where(számok);
            Tömb_OrderBy(számok);
            Tömb_Statisztika(számok);
            Tömb_Szavak(szavak);
            #endregion

            #region Táblák kezelése
            Iskola_Where(iskola.személyek);
            Iskola_Névsor(iskola.személyek, iskola.osztályok);
            Iskola_OsztályLétszámok(iskola.személyek, iskola.osztályok);
            #endregion


            Console.ReadLine();
        }

 

        #region Tömb kezelés példák függvényei

        private static void Tömb_Szavak(string[] szavak)
        {
            double átlagosSzóhossz = 0;
            átlagosSzóhossz = szavak.Average(x => x.Length);
            Console.WriteLine("\nSzavak:");
            Console.WriteLine("Átlagos szóhossz: {0}", átlagosSzóhossz);

            var szótár =
                from szó in szavak
                group szó by szó[0] into g
                orderby g.Key
                select new { Kezdőbetű = g.Key, Szavak = g, Darab = g.Count() };

            Console.WriteLine("Szólista:");
            foreach (var g in szótár)
            {
                Console.WriteLine("{0}: ({1})", g.Kezdőbetű, g.Darab);
                foreach (var szó in g.Szavak)
                {
                    Console.WriteLine("{0}", szó);
                }
            }

        }

        private static void Tömb_Statisztika(int[] számok)
        {
            //Pozitív számok
            var pozitívSzámok =
                from szám in számok
                where szám > 0
                select szám;
            int pozitívakSzáma = pozitívSzámok.Count();
            int pozitívakÖsszege = pozitívSzámok.Sum();
            double pozitívakÁtlaga = pozitívSzámok.Average();


            Console.WriteLine("\nStatisztika:");
            Console.WriteLine("Pozitívak: száma: {0}, összege: {1}, átlaga: {2}", pozitívakSzáma, pozitívakÖsszege, pozitívakÁtlaga);

            //Páros számok
            var párosSzámok =
               from szám in számok
               where Páros(szám)
               select szám;
            int párosakSzáma = párosSzámok.Count();
            int párosakÖsszege = párosSzámok.Sum();
            double párosakÁtlaga = párosSzámok.Average();
            Console.WriteLine("Párosak: száma: {0}, összege: {1}, átlaga: {2}", párosakSzáma, párosakÖsszege, párosakÁtlaga);

            //Páros-páratlan eloszlás
            var párosSzámokEloszlás =
                from szám in számok
                group szám by PárosSzöveg(szám) into g
                orderby g.Key
                select new { Párosak = g.Key, Száma = g.Count(), Összege = g.Sum(), Átlaga = g.Average() };
            Console.WriteLine("Páros, páratlan eloszlás:");
            string maszk = "{0,10}{1,8}{2,8}{1,8:#.00}";

            Console.WriteLine(maszk, "Milyen", "Mennyi", "Összeg", "Átlag");
            foreach (var item in párosSzámokEloszlás)
            {
                Console.WriteLine(maszk, item.Párosak, item.Száma, item.Összege, item.Átlaga);
            }

            //Számok eloszlása
            var számokEloszlás =
                from szám in számok
                group szám by szám into g
                orderby g.Key
                select new { szám = g.Key, mennyi = g.Count() };
            Console.WriteLine("Számok eloszlása:");
            maszk = "{0,5}{1,5}";
            Console.WriteLine(maszk, "Szám", "db");
            foreach (var item in számokEloszlás)
            {
                Console.WriteLine(maszk, item.szám, item.mennyi);
            }

        }


        static bool Páros(int szám)
        {
            return szám % 2 == 0;
        }

        static string PárosSzöveg(int szám)
        {
            string vissza = "";
            if (szám % 2 == 0)
            {
                vissza = "páros";
            }
            else
            {
                vissza = "páratlan";
            }
            return vissza;
        }


        static void Tömb_Where(int[] számok)
        {
            var kisSzámok =
                from n in számok
                where n < 5
                select n;
            Console.WriteLine("\nSzámok < 5:");

            //LinQ gyűjtemény listává alakítása
            List<int> ListkisSzámok = kisSzámok.ToList<int>();

            Listáz(ListkisSzámok);
            //foreach (var x in kisSzámok)
            //{
            //    Console.WriteLine(x);
            //}
        }


        static void Tömb_OrderBy(int[] számok)
        {
            var kisSzámok =
                from n in számok
                orderby n
                where n < 5
                select n;

            Console.WriteLine("\nSzámok < 5 sorrendben:");
            List<int> ListkisSzámok = kisSzámok.ToList<int>();
            Listáz(ListkisSzámok);
            //foreach (var szám in kisSzámok)
            //{
            //    Console.WriteLine(szám);
            //}
        }


        static void Listáz(List<int> intLista)
        {
            foreach (int item in intLista)
            {
                Console.WriteLine(item);
            }
        }
        #endregion

        #region Táblakezelés példák függvényei

        private static void Iskola_OsztályLétszámok(List<Személy> személyek, List<Osztály> osztályok)
        {
            var osztályLétszámok =
                from osztály in személyek
                group osztály by osztály.osztályAz into g
                orderby g.Count()
                select new { osztály = g.Key, létszám = g.Count() };

            Console.WriteLine("\nOsztálylétszámok: ");
            foreach (var osztály in osztályLétszámok)
            {
                Console.WriteLine("{0}, létszám: {1}", osztály.osztály, osztály.létszám);
            }
        }

        private static void Iskola_Névsor(List<Személy> személyek, List<Osztály> osztályok)
        {
            var nevsor =
                from személy in személyek
                orderby személy.osztályAz, személy.név
                select személy;
            Console.WriteLine("\nIskolanévsor Osztály azonosítóval:");
            string maszk = "{0,-8}{1,5}{2,8}";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(maszk, "Név", "OAz", "Átlag");
            Console.ResetColor();
            foreach (var tanulo in nevsor)
            {
                Console.WriteLine(maszk, tanulo.név, tanulo.osztályAz, tanulo.átlag);
            }


            var nevsor2 =
                from személy in személyek
                join osztály in osztályok on személy.osztályAz equals osztály.osztályAz
                orderby személy.osztályAz, személy.név
                select new { név=személy.név, osztály=osztály.osztály, átlag=személy.név};
            Console.WriteLine("\nIskolanévsor Osztállyal:");
            maszk = "{0,-8}{1,5}{2,8}";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(maszk, "Név", "OAz", "Átlag");
            Console.ResetColor();
            foreach (var tanulo in nevsor2)
            {
                Console.WriteLine(maszk, tanulo.név, tanulo.osztály, tanulo.átlag);
            }


        }


        static void Iskola_Where(List<Személy> személyek)
        {
            var jótanulók =
                from tanuló in személyek
                where tanuló.átlag > 4
                select tanuló;
            Console.WriteLine("\nJó tanulók");
            foreach (var jotanulo in jótanulók)
            {
                Console.WriteLine("{0} átlag:{1}", jotanulo.név, jotanulo.átlag);
            }
        }
        #endregion

    }
}
