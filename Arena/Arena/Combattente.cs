using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arena
{
    internal class Combattente
    {
        public string Nome { get; private set; }
        public int PuntiFerita { get; private set; }
        public int Attacco { get; private set; }
        public int Difesa { get; private set; }
        public bool Vivo => PuntiFerita > 0;
        public ProgressBar BarraVita { get;  set; }
        public PictureBox Gladiatore { get;  set; }

        private static Random random = new Random();

        public Combattente(string nome, int attacco, ProgressBar barravita, PictureBox gladiatore)
        {
            Nome = nome;
            Attacco = attacco;
            PuntiFerita = 100;
            Difesa = random.Next(0, 81);
            BarraVita = barravita;
            Gladiatore = gladiatore;
        }

        public void SubisciAttacco(int danno)
        {
            bool difende = random.NextDouble() < 0.5;
            if (difende)
            {
                int riduzione = (danno * Difesa) / 100;
                danno -= riduzione;
                Console.WriteLine($"{Nome} difende! Riduce il danno di {riduzione} ({Difesa}%)");
            }
            else
            {
                Console.WriteLine($"{Nome} non riesce a difendersi!");
            }
            if (PuntiFerita - danno > 0) PuntiFerita -= danno;
            else PuntiFerita = 0;
            
            BarraVita.Value = PuntiFerita;
            BarraVita.Refresh();

            Console.WriteLine($"{Nome} subisce {danno} danni. HP rimanenti: {PuntiFerita}");

            

            
        }

        public void Attacca(Combattente avversario)
        {
            Console.WriteLine($"{Nome} attacca {avversario.Nome} con {Attacco} di potenza.");
            avversario.SubisciAttacco(Attacco);
            
        }

        public void Reset()
        {
            PuntiFerita = 100;
        }
    }
}
