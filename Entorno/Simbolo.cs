using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodo_Thompson.Entorno
{
    class Simbolo
    {

        private int simbolNumber, fila,columna;
        private String lexema, token;

        public Simbolo(int simbolNumber, string lexema, string token, int fila, int columna)
        {
            this.SimbolNumber = simbolNumber;
            this.Fila = fila;
            this.Columna = columna;
            this.Lexema = lexema;
            this.Token = token;
        }

        public int SimbolNumber { get => simbolNumber; set => simbolNumber = value; }
        public int Fila { get => fila; set => fila = value; }
        public int Columna { get => columna; set => columna = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public string Token { get => token; set => token = value; }
    }
}
