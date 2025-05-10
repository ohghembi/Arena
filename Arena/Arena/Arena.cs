using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Arena
{
    internal class Arena
    {
        private Semaphore sem = new Semaphore(2, 2);
        private List<Combattente> combattenti;
        private Combattente campione = null;
        private object locker = new object();

        public Arena(List<Combattente> listaCombattenti)
        {
            combattenti = listaCombattenti;
        }

        public void IniziaTorneo()
        {
            while (combattenti.Count > 0)
            {
                Combattente c1 = null;
                Combattente c2 = null;

                if (campione == null && combattenti.Count >= 2)
                {
                    c1 = combattenti[0];
                    combattenti.RemoveAt(0);
                    c2 = combattenti[0];
                    combattenti.RemoveAt(0);
                }
                else if (campione != null && combattenti.Count > 0)
                {
                    c1 = campione;
                    c2 = combattenti[0];
                    combattenti.RemoveAt(0);
                    c1.Reset();
                }
                else break;

                Thread thread = new Thread(() =>
                {
                    sem.WaitOne();
                    
                    Combattente vincitore = Combatti(c1, c2);
                    lock (locker)
                    {
                        campione = vincitore;
                    }
                    sem.Release();
                });

                thread.Start();
                thread.Join();
                
            }

            Console.WriteLine($"\nIl vincitore del torneo è: {campione.Nome}!");
        }

        private Combattente Combatti(Combattente a, Combattente b)
        {
            Console.WriteLine($"\nInizia il combattimento tra {a.Nome} e {b.Nome}!");
            a.Reset();
            b.Reset();
            Thread.Sleep(1000);
            a.BarraVita.Refresh();
            b.BarraVita.Refresh();
            while (a.Vivo && b.Vivo)
            {
                a.Attacca(b);
                if (!b.Vivo) break;

                Thread.Sleep(500);

                b.Attacca(a);
                Thread.Sleep(500);
            }

            Combattente vincitore = a.Vivo ? a : b;
            Combattente perdente = a.Vivo ? b : a;
            Console.WriteLine($"{vincitore.Nome} vince contro {perdente.Nome}!\n");
            return vincitore;
        }
    }
}
