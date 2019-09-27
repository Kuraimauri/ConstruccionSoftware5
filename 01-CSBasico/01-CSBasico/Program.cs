using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_CSBasico
{
    class Program
    {
        /// <summary>
        /// Descripción de lo que hace la función
        /// </summary>
        /// <param name="args">Describir que hace cada parámetro</param>
        static void Main(string[] args)
        {
            // Comentario de una linea
            /*
             * Comentario de varias lineas
             * Segunda linea
             */

            // Tipos de datos básicos de C#

            int entero = 0; // 32 bit -2billones - +2billones -2e16 - +2e16-1
            short enteroCorto; //16 bit -32768 - +32767
            long enteroLargo; //64 bit -2e32 - +2e32-1
            byte enteroByte; //1 byte, 8bit 0 - 255

            uint enteroSinSigno; //32 bit 0-2e32-1
            ushort enteroCortoSinSigno; // 16 bit
            ulong enteroLargoSinSigno; // 64 bit
            sbyte enteroByteConSigno; //8 bit -128 - 127

            float real; //precisión: 7 digitos
            double realDoble; //precisión: 15 dígitos
            decimal realDecimal; //precisión: 128 bits

            bool logico; //true, false

            char caracter = 'A'; //Caracter Unicode

            string texto = "Texto"; //Arreglo de caracteres
            texto = null;

            int? enteroAnulable = 0;
            enteroAnulable = null;
            bool? logicoAnulable = false;
            logicoAnulable = null;

            //Operadores en C#
            //Operadores aritméticos

            // + - * /
            int valor1 = 0, valor2 = 0;
            valor1 = valor1 + valor2;
            // % módulo
            // incremento y decremento
            // ++ --
            valor1++;

            //Operadores relacionales
            // ==, !=, >, <, >=, <=
            bool resultadoLogico = true;
            resultadoLogico = (valor1 == valor2);

            //Operadores lógicos
            // y = &&, o = ||, no = !
            bool expresionA = true, expresionB = false;
            expresionA = (expresionA && expresionB); //false
            expresionA = !expresionB;

            //Operadores de asignación
            // =, +=, -=, *=, /=, %=
            resultadoLogico = true;
            entero += 3; // entero = entero + 3
            entero -= 2; // entero = entero - 2

            //Priorización de operadores
            //()
            //++, --
            //*, / %
            //+, -

            //Estructuras de datos en C#
            //Arreglos
            int[] arregloEnteros = new int[4];
            int[] arregloEnterosValores = { 10, 20, 30 };

            //Base 0
            entero = arregloEnterosValores[0];
            arregloEnteros[0] = 0;
            arregloEnteros[1] = 2;

            texto = "Clase";
            char caracterDeTexto = texto[2];
            //NO SE PUEDE: texto[0] = 'g';

            int[,] matrizEnteros = new int[3,3];
            matrizEnteros[0, 0] = 0;

            //Listas
            //using System.Collections.Generic;

            List<string> listaTextos = new List<string>();
            List<int> listaEnteros = new List<int> { 50, 100, 200 };

            listaTextos.Add("texto");
            string textoEnLista = listaTextos[0];
            listaEnteros.RemoveAll(x => x == 50);
            listaEnteros.RemoveAt(1);

            //Stack, queue, dictionary, struct

            //Estructuras de control
            //Condicionales y ciclos

            //Condicionales: if y switch
            int ejemploCondicional = 30;
            if(ejemploCondicional == 40)
            {
                //No ejecuta esto
            }
            else if(ejemploCondicional == 30)
            {
                //Ejecuta esto
            }
            else
            {
                //No se ejecuta esto
            }

            string esVerdad = ejemploCondicional == 30 ? "Verdad" : "Mentira";

            int mes = 9;
            string nombreMes;
            switch(mes)
            {
                case 1:
                    nombreMes = "Enero";
                    break;
                case 9:
                    nombreMes = "Septiembre";
                    break;
                default:
                    nombreMes = "Mes inválido";
                    break;
            }

            //Ciclos
            //While, For, Foreach
            int valorWhile = 0;
            while(valorWhile < 100)
            {
                //valorWhile sea 99, es decir va a ejecutar 100 ciclos
                valorWhile++;
            }

            for (int i = 10; i > 0; i--)
            {
                //Se van a ejecutar 10 ciclos
            }

            List<int> listaForEach = new List<int> { 1, 2, 3, 4, 5 };
            foreach(int numeroLista in listaForEach)
            {
                //Se ejecuta 5 veces, numeroLista = 1, 2, 3
            }

        }
    }
}
