using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Metodo_Thompson.Analysis;

namespace Metodo_Thompson
{
    public partial class Form1 : Form
    {
        private Lexico scanner;

        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            scanner = new Lexico();
            if (txtInput.Text.Length != 0)
            {
                scanner.autamataFinitoDeterministico(txtInput.Text);
                if (!scanner.tablaErrores.Any())
                {
                    Console.WriteLine("No hay errores");
                }
                else
                    Console.WriteLine("Exiten errores");



            }
            else
                Console.WriteLine("No hay nada para analizar");
        }
    }
}
