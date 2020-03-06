using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metodo_Thompson.Entorno;

namespace Metodo_Thompson.Analysis
{
    class Lexico
    {
        private int fila, columna, estado, indice, actual, numeroSimbolo, numeroErrores;
        private String lexema, nuevaCadena, cadenaEntrada;
        private char buscador;
        public LinkedList<Simbolo> tablaSimbolos;
        public LinkedList<Error> tablaErrores;


        public Lexico()
        {
            fila = 1;
            columna = 1;
            estado = 0;
            tablaSimbolos = new LinkedList<Simbolo>();
            tablaErrores = new LinkedList<Error>();


        }

        public void autamataFinitoDeterministico(String text)
        {

            cadenaEntrada = text.Trim();
            buscador = ' ';
            lexema = "";
            numeroSimbolo = 0;
            numeroErrores = 0;
            fila = 1;
            columna = 0;

            for (indice = 0; indice <= cadenaEntrada.Length; indice++)
            {
                if (indice != cadenaEntrada.Length)
                {
                    buscador = cadenaEntrada.ElementAt(indice);

                }
                else
                {
                    buscador = ' ';

                }


                switch (estado)
                {
                    case 0:
                        if ((buscador > 96 && buscador < 123) || (buscador > 64 && buscador < 91))
                        {
                            lexema += buscador;
                            estado = 1;
                            columna++;
                        }
                        //Digito
                        else if (buscador > 47 && buscador < 58)
                        {
                            lexema += buscador;
                            estado = 3;
                            columna++;
                        }
                        // Asignacion
                        else if (buscador == 45)
                        {
                            lexema += buscador;
                            estado = 7;
                            columna++;
                        }
                        //Cadena
                        else if (buscador == 34)
                        {
                            estado = 7;
                            columna++;
                        }
                        //Comentario  multi-linea
                        else if (buscador == 60)
                        {
                            lexema += buscador;
                            estado = 6;
                            columna++;
                        }
                        else if (buscador > 32 && buscador <= 126)
                        {
                            lexema += buscador;
                            estado = 4;
                            columna++;
                        }
                        else
                        {

                            if (buscador == ' ' || buscador == '\n' || buscador == '\t')
                            {
                                validarEspacios(buscador);
                                Console.WriteLine("Terminal" + buscador);
                            }
                            else
                            {
                                lexema += buscador;
                                numeroErrores++;
                                tablaErrores.AddLast(new Entorno.Error(numeroErrores, "Simbolo indefinido", fila, columna));
                                columna++;
                                lexema = "";
                                estado = 0;

                            }

                        }

                        break;
                    case 1:
                        if ((buscador > 64 && buscador < 91) || (buscador > 96 && buscador < 123))
                        {
                            lexema += buscador;
                            columna++;
                            estado = 1;
                        }
                        else if (buscador > 47 && buscador < 58)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 1;
                        }
                        else if (buscador == 95)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 1;
                        }
                        else
                        {
                            numeroSimbolo++;
                            tablaSimbolos.AddLast(new Simbolo(numeroSimbolo, lexema, devolverTokenReservada(lexema), fila, columna));
                            lexema = "";
                            columna++;
                            estado = 0;
                            validarEspacios(buscador);
                        }
                        break;
                    case 2:
                        if (buscador > 47 && buscador < 58)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 2;
                        }
                        else if (buscador == 46)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 16;
                        }
                        else
                        {
                            numeroSimbolo++;
                            tablaSimbolos.AddLast(new Simbolo(numeroSimbolo, lexema, "TK_Digito", fila, columna));
                            lexema = "";
                            columna++;
                            estado = 0;
                            validarEspacios(buscador);
                        }
                        break;
                    case 16:
                        if (buscador > 47 && buscador < 58)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 17;
                        }
                        else
                        {
                            validarErrores();
                            tablaErrores.AddLast(new Entorno.Error(numeroErrores, "Simbolo indefinido" + lexema, fila, columna));
                            columna++;
                            lexema = "";
                            estado = 0;

                        }
                        break;
                    case 17:
                        if (buscador > 47 && buscador < 58)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 17;

                        }
                        else
                        {
                            numeroSimbolo++;
                            tablaSimbolos.AddLast(new Simbolo(numeroSimbolo, lexema, "TK_Digito", fila, columna));
                            lexema = "";
                            columna++;
                            estado = 0;
                            validarEspacios(buscador);
                        }
                        break;
                    case 3:
                        if (buscador == 62)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 18;

                        }
                        else
                        {
                            numeroSimbolo++;
                            tablaSimbolos.AddLast(new Simbolo(numeroSimbolo, lexema, "TK_Simbolo", fila, columna));
                            lexema = "";
                            columna++;
                            estado = 0;
                            validarEspacios(buscador);
                        }
                        break;
                    case 18:
                        numeroSimbolo++;
                        tablaSimbolos.AddLast(new Simbolo(numeroSimbolo, lexema, devolverToken(lexema), fila, columna));
                        lexema = "";
                        columna++;
                        estado = 0;
                        validarEspacios(buscador);
                        break;
                    case 4:
                        numeroSimbolo++;
                        tablaSimbolos.AddLast(new Simbolo(numeroSimbolo, lexema, devolverToken(lexema), fila, columna));
                        lexema = "";
                        columna++;
                        estado = 0;
                        validarEspacios(buscador);

                        break;
                    case 6:
                        if (buscador == 33)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 19;

                        }
                        else
                        {
                            validarErrores();
                            tablaErrores.AddLast(new Entorno.Error(numeroErrores, "Error indefinido" + lexema, fila, columna));
                            columna++;
                            lexema = "";
                            estado = 0;
                        }
                        break;
                    case 19:
                        if (buscador == 33)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 22;

                        }
                        else
                        {
                            columna++;
                            estado = 19;

                        }
                        break;
                    case 22:
                        if (buscador == 62)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 0;
                            lexema = "";
                        }
                        else if (buscador == 33)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 22;
                        }
                        else
                        {
                            validarErrores();
                            tablaErrores.AddLast(new Entorno.Error(numeroErrores, "Error indefinido" + lexema, fila, columna));

                        }
                        break;
                    case 5:
                        if (buscador == 47)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 5;

                        }
                        else if (buscador == 10)
                        {
                            lexema += buscador;
                            columna++;
                            estado = 0;
                            lexema = "";

                        }
                        else
                        {
                            lexema += buscador;
                            columna++;
                            estado = 5;
                        }
                        break;
                    case 7:
                        if (buscador == 34)
                        {
                            numeroSimbolo++;
                            tablaSimbolos.AddLast(new Simbolo(numeroSimbolo, lexema, "TK_Cadena", fila, columna));
                            lexema = "";
                            columna++;
                            estado = 0;


                        }
                        else
                        {
                            lexema += buscador;
                            columna++;
                            estado = 7;
                        }
                        break;
                }
            }
            numeroSimbolo++;
            tablaSimbolos.AddLast(new Simbolo(numeroSimbolo, "fin", "TK_Final", -1, -1));


        }

        private String devolverToken(String palabra)
        {
            palabra = palabra.ToLower();

            if (palabra.Equals("}"))
            {
                return "TK_Cerrar";
            }
            else if (palabra.Equals("\""))
            {
                return "TK_Comillas";
            }
            else if (palabra.Equals("+"))
            {
                return "TK_Mas";
            }
            else if (palabra.Equals("*"))
            {
                return "TK_Por";
            }
            else if (palabra.Equals("?"))
            {
                return "TK_Interrogacion";
            }
            else if (palabra.Equals("."))
            {
                return "TK_Concatenacion";
            }
            else if (palabra.Equals("|"))
            {
                return "TK_Disyuncion";
            }
            else if (palabra.Equals(":"))
            {
                return "TK_DosPuntos";
            }
            else if (palabra.Equals(";"))
            {
                return "TK_PuntoComa";
            }
            else if (palabra.Equals(","))
            {
                return "TK_Coma";
            }
            else if (palabra.Equals("~"))
            {
                return "TK_Virgulilla";
            }
            else if (palabra.Equals("{"))
            {
                return "TK_Abrir";
            }
            else if (palabra.Equals("%"))
            {
                return "TK_Porcentaje";
            }
            else if (palabra.Equals("->"))
            {
                return "TK_Asignar";

            }
            else
            {

                return "TK_Simbolo";
            }
        }

        private String devolverTokenReservada(String palabra)


        {
            palabra = palabra.ToLower();
            // Cambiar todo los iguales a matches()
            if (palabra.Equals("conj"))
            {
                return "TK_Conjunto";
            }
            else
            {

                return "TK_Id";
            }

        }

        public void validarEspacios(char c)
        {
            if (c == ' ' || c == 9)
            {
                columna++;
            }
            else if (c == '\n')
            {
                fila++;
                columna = 0;
            }
            else
            {
                indice--;
            }
        }

        private void validarErrores()
        {
            while (indice != cadenaEntrada.Length && cadenaEntrada.ElementAt(indice) != ' '
                    && cadenaEntrada.ElementAt(indice) != '\n' && cadenaEntrada.ElementAt(indice)
                    != '\t')
            {
                lexema += cadenaEntrada.ElementAt(indice);
                indice++;
                columna++;

            }
        }

    }
}
