
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace ThreadPool
{
    class Program
    {
        static string s;
        static List<string> Nomi = new List<string>();
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("Nomi.txt");
            string file = "Nomiomi.txt";
            if (File.Exists(file))
            {

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Nomi.Add(line);
                }

                foreach (string nome in Nomi)
                {
                    Console.WriteLine(nome);
                }

                Console.Write("\n Immetti nome e cognome perfavore: ");
                s = Console.ReadLine();
                Stopwatch cronometro = new Stopwatch();

                cronometro.Start();
                ThreadPoolUtilizzo("Morgan Izzo", Nomi);
                cronometro.Stop();

                Console.WriteLine("Tempo " + cronometro.ElapsedTicks.ToString());
                cronometro.Reset();

                cronometro.Start();
                ThreadUtilizzo("Morgan Izzo", Nomi);
                cronometro.Stop();

                Console.WriteLine("Tempo impiegato Thread: " + cronometro.ElapsedTicks.ToString());

                Console.ReadLine();
            }
        }


        private static void ThreadUtilizzo(string s, List<string> nomi)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread t1 = new Thread(Ricerca);
                t1.Start();
            }

        }


        static void ThreadPoolUtilizzo(string s, List<string> nomi)
        {
            for (int i = 0; i <= 10; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Ricerca)); 
            }
        }

        static void Ricerca(object callback)
        {
            bool trovato = false;
            int i;
            for (i = 0; i < 100; i++)
            {
                if (s.ToLower().Trim() == Nomi[i].ToLower().Trim())
                    trovato = true;

            }

            if (trovato == false)
            {
                Console.WriteLine($"{s} non è stato trovato");
            }
            else
            {
                Console.WriteLine($"{s} è stato trovato ed è in posizione {i}");
            }

        }

    }
}