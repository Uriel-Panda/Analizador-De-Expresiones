using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evaluador_De_Expresiones
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Operar();
        }


        public void Operar()
        {
           textBox2.Text = new Evaluador().ConversionDePreAPost(textBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void escribir_en_text_box(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            textBox1.Text += button.Text;
        }

        private void Eliminar_Caracter(object sender, EventArgs e)
        {
            if(textBox1.Text.Length >= 1)
                textBox1.Text = textBox1.Text.Substring(0, (textBox1.Text.Length-1));
        }
    }
}
