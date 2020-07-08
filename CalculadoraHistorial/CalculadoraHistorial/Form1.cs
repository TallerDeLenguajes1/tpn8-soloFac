using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CalculadoraHistorial
{
    enum EstadoOperador
    {
        OperadorAsignado,
        OperadorNoAsignado,
        OperadorIgual
    }

    enum EstadoPunto
    {
        PuntoNoAsignado,
        PuntoAsignado
    }

    enum TeclaPresionada
    {
        TeclaNumerica,
        TeclaBack,
        TeclaOperador,
        TeclaNoValida
    }

    public partial class Form1 : Form
    {
        Calculadora MiCalculadora;


        LinkedList<string> HistorialList = new LinkedList<string>();

        //string estado = EstadoOperador.OperadorNoAsignado;
        string operador;
        TeclaPresionada MiTeclaPresionada;
        EstadoOperador estado = EstadoOperador.OperadorNoAsignado;
        EstadoPunto estadopunto = EstadoPunto.PuntoNoAsignado;

        float Num1 = 0, Num2 = 0, resultado = 0;

        public Form1()
        {
            InitializeComponent();
            MiCalculadora = new Calculadora();

            button1.Click += button_Click;
            button2.Click += button_Click;
            button3.Click += button_Click;
            button4.Click += button_Click;
            button5.Click += button_Click;
            button6.Click += button_Click;
            button7.Click += button_Click;
            button8.Click += button_Click;
            button9.Click += button_Click;
            button11.Click += button_Click;

            buttonDivision.Click += buttonOperador_Click;
            buttonMultiplicacion.Click += buttonOperador_Click;
            buttonSuma.Click += buttonOperador_Click;
            buttonResta.Click += buttonOperador_Click;

            textBoxInf.KeyDown += textBoxInf_KeyDown;
            textBoxInf.KeyPress += textBoxInf_KeyPress;

        }



        private void button_Click(object sender, EventArgs e)
        {
            string TextoBoton = ((Button)sender).Text;

            if (estado == EstadoOperador.OperadorNoAsignado)
            {
                textBoxInf.Text += TextoBoton;
                Num1 = Convert.ToSingle(textBoxInf.Text.Trim());
            }
            else if (estado == EstadoOperador.OperadorAsignado)
            {
                textBoxInf.Text += TextoBoton;
            }
            else if (estado == EstadoOperador.OperadorIgual)
            {
                textBoxInf.Text = "";
                textBoxSup.Text = "";
                textBoxInf.Text += TextoBoton;
                Num1 = Convert.ToSingle(textBoxInf.Text.Trim());
                estado = EstadoOperador.OperadorNoAsignado;
            }
        }

        private void buttonOperador_Click(object sender, EventArgs e)
        {
            if (Num1.ToString() != "" && estado == EstadoOperador.OperadorNoAsignado)
            {
                operador = ((Button)sender).Text;
                textBoxSup.Text = Num1.ToString() + operador;
                textBoxInf.Text = "";
                estado = EstadoOperador.OperadorAsignado;
                estadopunto = EstadoPunto.PuntoNoAsignado;
            }
        }

        private void buttonPunto_Click(object sender, EventArgs e)
        {
            if(estadopunto == EstadoPunto.PuntoNoAsignado)
            {
                textBoxInf.Text += ".";
                estadopunto = EstadoPunto.PuntoAsignado;
            }
        }


        private void buttonBorrarTodo_Click(object sender, EventArgs e)
        {
            estadopunto = EstadoPunto.PuntoNoAsignado;
            estado = EstadoOperador.OperadorNoAsignado;
            textBoxInf.Text = "";
            textBoxSup.Text = "";
        }


        private void buttonBorrar_Click(object sender, EventArgs e)
        {
            int lng = textBoxInf.Text.Length;

            if (lng > 0)
            {
                textBoxInf.Text = textBoxInf.Text.Remove(lng - 1);
            }
        }



        private void buttonIgual_Click(object sender, EventArgs e)
        {
            if (textBoxInf.Text != "")
            {
                Num2 = Convert.ToSingle(textBoxInf.Text);
            }

            if (estado == EstadoOperador.OperadorAsignado && estado != EstadoOperador.OperadorIgual
                && Num1.ToString() != "" && Num2.ToString() != "")
            {

                resultado = RealizarOperacion(Num1, Num2, operador);

                textBoxSup.Text += textBoxInf.Text;         //Expreso la operacion arriba
                textBoxInf.Text = resultado.ToString();         //Muestro el resultado debajo

                string historial = MiCalculadora.Fecha() + " ";

                historial = historial + "----->" + Num1.ToString() + operador.ToString() + Num2.ToString() + "=" + resultado.ToString();

                HistorialList.AddFirst(historial);              //Agrego la fecha (variable aux historial) a la LinkedList

                HistorialOperaciones.Items.Add(HistorialList.First.Value);      //Muestro en el listBox el valor de PrimerNodo (ultimo historial creado)

                estado = EstadoOperador.OperadorIgual;

            }
            else if (estado == EstadoOperador.OperadorAsignado && Num1.ToString() != "" && Num2.ToString() == "")
            {
                textBoxSup.Text = Num1.ToString();
                textBoxInf.Text = "";

                estado = EstadoOperador.OperadorIgual;
            }
        }



        public float RealizarOperacion(float Num1, float Num2, string operador)
        {
            float resultado = 0;

            MiCalculadora.Numero1 = Num1;
            MiCalculadora.Numero2 = Num2;

            char c = Convert.ToChar(operador);

            int AsciiCode = (int) c;
            
            switch (AsciiCode)
            {
                case 43:    //mas
                    resultado = MiCalculadora.Suma(MiCalculadora.Numero1, MiCalculadora.Numero2);
                    break;

                case 45:    //menos
                    resultado = MiCalculadora.Resta(MiCalculadora.Numero1, MiCalculadora.Numero2);
                    break;

                case 42:    //asterisco de multiplicacion
                    resultado = MiCalculadora.Multiplicacion(MiCalculadora.Numero1, MiCalculadora.Numero2);
                    break; 

                case 47:    //barra de division
                    resultado = MiCalculadora.Division(MiCalculadora.Numero1, MiCalculadora.Numero2);
                    break;
            }

            return resultado;
        }

        private void HistorialOperaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void HistorialDoubleClick(object sender, MouseEventArgs e)
        {
            int indSelec = HistorialOperaciones.SelectedIndex;

            HistorialOperaciones.Items.RemoveAt(HistorialOperaciones.SelectedIndex);

            string historialPBorrar = (string)HistorialOperaciones.Items[indSelec];

            HistorialList.Remove(historialPBorrar);
            
        }

        //------------------------------- Eventos de Teclado ------------------------------------


        private void textBoxInf_KeyDown(object sender, KeyEventArgs e)
        {
            MiTeclaPresionada = TeclaPresionada.TeclaNoValida;

            if ((e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || 
                (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) ||
                (e.KeyCode == Keys.Decimal))
            {
                MiTeclaPresionada = TeclaPresionada.TeclaNumerica;
            }

            if (e.KeyCode == Keys.Add)
            {
                MiTeclaPresionada = TeclaPresionada.TeclaOperador;
            }

            if (e.KeyCode == Keys.Back)
            {
                MiTeclaPresionada = TeclaPresionada.TeclaBack;
            }

            if (e.KeyCode == Keys.Shift)
            {
                MiTeclaPresionada = TeclaPresionada.TeclaNoValida;
            }
        }

        
        private void textBoxInf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (MiTeclaPresionada == TeclaPresionada.TeclaNoValida)
            {
                e.Handled = true;
            }
        }
        
    }
}