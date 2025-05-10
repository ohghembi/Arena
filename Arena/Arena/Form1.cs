using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arena
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            List<Combattente> lista = new List<Combattente>
            {
                new Combattente("Massimo", 25, progressBar1, pictureBox1),
                new Combattente("Spartaco", 22, progressBar2, pictureBox1),
                /*new Combattente("Tito", 20),
                new Combattente("Bruto", 23),
                new Combattente("Cassio", 21)*/
            };

            Arena arena = new Arena(lista);
            arena.IniziaTorneo();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }

    
}