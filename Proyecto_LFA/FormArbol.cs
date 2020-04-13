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

namespace Proyecto_LFA
{
    public partial class FormArbol : Form
    {
        public FormArbol()
        {
            InitializeComponent();
        }
        
        public static Thread th;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            var tempX = pictureBox1.Width*2;
            var tempY = pictureBox1.Height;
            ObtenerTamañoPicture(Form1.arbol_Sintactico, ref tempX, ref tempY);
            pictureBox1.Width = tempX;
            pictureBox1.Height = tempY;
            var arbol = new Arbol(pictureBox1);
            arbol.DibujarArbol(Form1.arbol_Sintactico, 0);
        }

       public void ObtenerTamañoPicture(Nodo_Generico nodo, ref int x, ref int y) 
       {
            if (nodo != null)
            {
                x += 275;
                y += 150;
                ObtenerTamañoPicture(nodo.hijo_izquierdo, ref x, ref y);
            }
       }

    }
}
