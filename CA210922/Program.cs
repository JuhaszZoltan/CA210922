using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA210922
{
    class Palinka
    {
        private int _alkoholFoka;
        private string _gyumolcs;
        private int _mennyiseg;
        private int _keszitesiEv;
        private int _ar;

        public int AlkoholFoka
        {
            get => _alkoholFoka;
            set
            {
                if (value < 30) throw new Exception("hiba: túl alacsony alkoholfok");
                if (value > 87) throw new Exception("hiba: túl magas alkoholfok");
                _alkoholFoka = value;
            }
        }
        public string Gyumolcs
        {
            get => _gyumolcs;
            set
            {
                if (value is null) throw new Exception("hiba: a gyümölcs nem lehet null");
                if (value.Length < 3) throw new Exception("hiba: a gyümölcs neve minimum 3 krakter kell, hogy legyen");
                if (value.Length > 20) throw new Exception("hiba:_ a gyüm9ölcs neve maximum 30 karakter lehet");

                _gyumolcs = value;
            }
        }
        public int Mennyiseg
        {
            get => _mennyiseg;
            set
            {
                if (value < 0) throw new Exception("hiba: a mennyiség nem lehet negatív");
                if (value > 50) throw new Exception("hiba: a mennyiség nem lehet több 50-nél");
                _mennyiseg = value;
            }
        }
        public int KeszitesiEv
        {
            get => _keszitesiEv;
            set
            {
                if (value < 2000) throw new Exception("hiba: túl öreg pálinka");
                if (value > DateTime.Now.Year) throw new Exception("hiba: pálinka a jövőből");
                _keszitesiEv = value;
            }
        }
        public int Ar
        {
            get => _ar;
            set
            {
                if (value < 50) throw new Exception("hiba: túl olcsó pálinka");
                if (value > 1000) throw new Exception("hiba: túl drága pálinka");
                _ar = value;
            }
        }
    }

    class Program
    {
        static Random rnd = new Random();
        static string[] gyumolcsok =
        {
            "szilva",
            "körte",
            "cseresznye",
            "homoktövis",
            "kaktusz",
            "eper",
            "alma",
            "dió",
        };

        static void Main()
        {
            var palinkak = new List<Palinka>();

            for (int i = 0; i < 20; i++)
            {
                palinkak.Add(new Palinka()
                {
                    AlkoholFoka = rnd.Next(30, 88),
                    Gyumolcs = gyumolcsok[rnd.Next(gyumolcsok.Length)],
                    Mennyiseg = rnd.Next(51),
                    Ar = rnd.Next(5, 101) * 10,
                    KeszitesiEv = rnd.Next(2000, DateTime.Now.Year + 1),
                });
            }
            Console.WriteLine("------------------");
            Kiir(palinkak);
            Console.WriteLine("------------------");
            Osszpalinka(palinkak);


            int bevetel = 0;
            for (int i = 0; i < 50; i++)
            {
                int rndIndex = rnd.Next(palinkak.Count);
                bevetel += palinkak[rndIndex].Mennyiseg / 2 * palinkak[rndIndex].Ar;
                palinkak[rndIndex].Mennyiseg /= 2;
            }

            Kiir(palinkak);
            Osszpalinka(palinkak);
            Console.WriteLine($"bevétel: {bevetel} Ft");

            Console.ReadKey(true);
        }

        private static void Osszpalinka(List<Palinka> palinkak)
        {
            int sum = 0;
            foreach (var p in palinkak)
            {
                sum += p.Mennyiseg;
            }
            Console.WriteLine($"Összesen {sum/10F} liter pálinka van");
        }

        private static void Kiir(List<Palinka> palinkak)
        {
            foreach (var p in palinkak)
            {
                Console.WriteLine("{0, -10} ({1}%): {2, 4} Ft/dl {3,2}dl {4}",
                    p.Gyumolcs, p.AlkoholFoka, p.Ar, p.Mennyiseg, p.KeszitesiEv);
            }
        }
    }
}
