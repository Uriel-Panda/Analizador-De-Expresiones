using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Evaluador_De_Expresiones
{
    public class Evaluador
    {

        //Converción de preorden a postorde

        Stack<string> Postorden = new Stack<string>();
        Stack<string> Operadores = new Stack<string>();
        Stack<string> Resultados = new Stack<string>();

        //esta variable se utiliza por si se ingresan numeros de mas de un digito
        string cadenaPostorden = "";
        string numero = "";

        public string ConversionDePreAPost(string cadena)
        {
            foreach(char caracter in cadena)
            {
                EvaluacionNumeroOperador(caracter);
            }
            //Descaragar lo que contega numero
            Postorden.Push(numero);
            cadenaPostorden += numero + ",";
            //Descargar lo que contenga la pila de operaciones
            while (Operadores.Count != 0)
            {
                if (Operadores.Count != 1)
                {
                    Postorden.Push(Operadores.Pop());
                    cadenaPostorden += Postorden.Peek() + ",";
                }
                else
                {
                    Postorden.Push(Operadores.Pop());
                    cadenaPostorden += Postorden.Peek();
                }
            }

            return RealizarOperciones();

        }

        private void EvaluacionNumeroOperador(char caracter)
        {
            if (isOperador(caracter))
            {
                if (caracter != '(' && caracter != ')')
                {
                    Postorden.Push(numero);
                    cadenaPostorden += numero + ",";
                    numero = "";
                }

                if (Operadores.Count == 0)
                {
                    Operadores.Push(caracter + "");
                }
                else if (caracter == ')')
                {
                    while (!Operadores.Peek().Equals("("))
                    {
                        Postorden.Push(Operadores.Pop());
                        cadenaPostorden += Postorden.Peek() + ",";
                    }
                    Operadores.Pop();
                }
                else if (EnPila(Operadores.Peek()) < EnFila(caracter + ""))
                {
                    Operadores.Push(caracter + "");
                }
                else
                {
                    while (Operadores.Count != 0)
                    {
                        if (EnPila(Operadores.Peek()) >= EnFila(caracter + ""))
                        {
                            Postorden.Push(Operadores.Pop());
                            cadenaPostorden += Postorden.Peek() + ",";

                        }
                        else
                        {
                            break;
                        }
                    }
                    Operadores.Push(caracter + "");
                }
                
               
            }
            else
            {
                numero += caracter + "";
            }
        }





        #region Evaluacion de operadores
        public int EnFila(string operador)
        {
            if (operador.Equals("+") || operador.Equals("-")) return 1;
            if (operador.Equals("*") || operador.Equals("/")) return 2;
            if (operador.Equals("^")) return 3;
            if (operador.Equals("(")) return 5;
            return -1;
        }

        public int EnPila(String operador)
        {
            if (operador.Equals("+") || operador.Equals("-")) return 1;
            if (operador.Equals("*") || operador.Equals("/")) return 2;
            if (operador.Equals("^")) return 3;
            if (operador.Equals("(")) return 0;
            return -1;
        }

        private bool isOperador(char caracter)
        {
            if (caracter == '+') return true;
            if (caracter == '-') return true;
            if (caracter == '*') return true;
            if (caracter == '/') return true;
            if (caracter == '^') return true;
            if (caracter == '(') return true;
            if (caracter == ')') return true;
            return false;
        }
        #endregion

        #region Operaciones

        public string RealizarOperciones()
        {
            string[] post = cadenaPostorden.Split(',');
            foreach(string numero in post)
            {
                if (numero.Length < 2) {
                    if (!isOperador(Convert.ToChar(numero)))
                    {
                        Resultados.Push(numero);
                    }
                    else
                    {
                        Resultados.Push(Operacion(Resultados.Pop(), Resultados.Pop(), numero));
                    }
                }
                else
                {
                    Resultados.Push(numero);
                }
            }

            return Resultados.Peek();
        }



        public string Operacion(string numero2, string numero1, string operador)
        {
            double valor1 = Convert.ToDouble(numero1);
            double valor2 = Convert.ToDouble(numero2);
            double resultado = 0;

            switch (operador)
            {
                case "+":
                    resultado = valor1 + valor2;
                    break;
                case "-":
                    resultado = valor1 - valor2;
                    break;
                case "*":
                    resultado = valor1 * valor2;
                    break;
                case "/":
                    resultado = valor1 / valor2;
                    break;
                case "^":
                    resultado = Math.Pow(valor1,valor2);
                    break;
            }

            return Convert.ToString(resultado);

        }



        #endregion

    }
}
