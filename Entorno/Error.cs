using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodo_Thompson.Entorno
{
    class Error
    {

        private int number, fila, columnal;
        private String errorL;

        public Error(int number, string error ,int fila, int columnal)
        {
            this.Number = number;
            this.Fila = fila;
            this.Columnal = columnal;
            this.Errorl = error;
        }

        public int Number { get => number; set => number = value; }
        public int Fila { get => fila; set => fila = value; }
        public int Columnal { get => columnal; set => columnal = value; }
        public string Errorl { get => errorL; set => errorL = value; }
    }
}
