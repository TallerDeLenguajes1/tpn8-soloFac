using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraHistorial
{
    public class Calculadora
    {
        private float numero1;
        private float numero2;

        public float Numero1 { get => numero1; set => numero1 = value; }
        public float Numero2 { get => numero2; set => numero2 = value; }

        public float Suma(float Numero1, float Numero2)
        {
            float result = Numero1 + Numero2; return result;
        }

        public float Resta(float Numero1, float Numero2)
        {
            float result = Numero1 - Numero2; return result;
        }

        public float Multiplicacion(float Numero1, float Numero2)
        {
            float result = Numero1 * Numero2; return result;
        }


        public float Division(float Numero1, float Numero2)
        {
            if (Numero2 != 0)
            {
                float result = Numero1 / Numero2; return result;
            }
            else
            {
                return 999999999999;
            }
        }

        //CONSULTA: Si modifico la Clase Calculadora, donde deberia generar la fecha? en un Metodo? ó deberia generarla en Form1.cs en un 
        //metodo aparte o solamente suelta? y guardarla en las variables del nodo LinkedList

        //Al eliminar un elemento de historial del listBox deberia eliminarlo tambien de la LinkedList?

        //Como creo un evento para que al hacer doble click en un elemento de listBox pueda saber cual fue el seleccionado. 

        public string Fecha()
        {
            DateTime FechaActual = DateTime.Now;

            string fecha = FechaActual.ToShortDateString() + " " + FechaActual.ToShortTimeString();

            Console.WriteLine(fecha);

            return fecha;
        }
        
    }
}
