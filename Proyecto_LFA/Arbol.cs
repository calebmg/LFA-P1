using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_LFA
{
    public class Arbol
    {
        public  PictureBox ptb;        
        public  Bitmap bit;
        public  Graphics g;
        public  Pen lapiz;
        public  int despX = 250;
        public  int despY = 150;
        public  int raizX;
        public  int raizY;

        public Arbol(PictureBox ptb) 
        {
            this.ptb = ptb;
            lapiz = new Pen(Color.Red,5);
            bit = new Bitmap(ptb.Width, ptb.Height);
            raizX = (ptb.Width *4)/5;
            raizY = 20;
        }

        public void DibujarArbol(Nodo_Generico nodo, int rama)
        {
            if (nodo != null)
            {
                ptb.Image = (Image)bit;
                g = Graphics.FromImage(bit);
                if (rama == 0)
                {
                    nodo.posX = raizX;
                    nodo.posY = raizY;
                    g.DrawString(nodo.id, new Font("Microsoft Sans Serif", 16, FontStyle.Bold), new SolidBrush(Color.Red), nodo.posX, nodo.posY);
                    g.DrawEllipse(new Pen(Color.Black, 1), nodo.posX - 5, nodo.posY, 30, 20);
                }
                else if (rama == 1)
                {
                    if (Helpers.Verificar_esHoja(nodo) == true)
                    {
                        nodo.posX = nodo.padre.posX - 80;
                        nodo.posY = nodo.padre.posY + 40;
                    }
                    else
                    {
                        if (nodo.id == "|" || nodo.padre.id == "|")
                        {
                            nodo.posX = nodo.padre.posX - despX;
                            nodo.posY = nodo.padre.posY + despY;
                        }
                        else
                        {
                            nodo.posX = nodo.padre.posX - 100;
                            nodo.posY = nodo.padre.posY + 50;
                        }
                    }
                    
                    if (SintacticoA.SetsList.Contains(nodo.id))
                    {
                        g.DrawString(nodo.id, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), new SolidBrush(Color.Red), nodo.posX, nodo.posY);
                        g.DrawEllipse(new Pen(Color.Black, 1), nodo.posX - 5, nodo.posY, 70, 20);
                    }
                    else
                    {
                        g.DrawString(nodo.id, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), new SolidBrush(Color.Black), nodo.posX, nodo.posY);
                        g.DrawEllipse(new Pen(Color.Black, 1), nodo.posX - 5, nodo.posY, 40, 20);
                    }
                    g.DrawLine(new Pen(Color.Black, 1), nodo.padre.posX, nodo.padre.posY, nodo.posX, nodo.posY);

                }
                else if (rama == 2)
                {
                    if (Helpers.Verificar_esHoja(nodo) == true)
                    {
                        nodo.posX = nodo.padre.posX + 80;
                        nodo.posY = nodo.padre.posY + 40;
                    }
                    else
                    {
                        nodo.posX = nodo.padre.posX + 125;
                        nodo.posY = nodo.padre.posY + 75;
                    }
                    
                    if (SintacticoA.SetsList.Contains(nodo.id))
                    {
                        g.DrawString(nodo.id, new Font("Microsoft Sans Serif", 12, FontStyle.Regular), new SolidBrush(Color.Red), nodo.posX + 15, nodo.posY);
                        g.DrawEllipse(new Pen(Color.Black, 1), nodo.posX + 5, nodo.posY, 70, 20);
                    }
                    else
                    {
                        g.DrawString(nodo.id, new Font("Microsoft Sans Serif", 12, FontStyle.Regular), new SolidBrush(Color.Black), nodo.posX + 15, nodo.posY);
                        g.DrawEllipse(new Pen(Color.Black, 1), nodo.posX + 5, nodo.posY, 40, 20);
                    }
                    g.DrawLine(new Pen(Color.Black, 1), nodo.padre.posX, nodo.padre.posY, nodo.posX, nodo.posY);

                }
                DibujarArbol(nodo.hijo_izquierdo, 1);
                DibujarArbol(nodo.hijo_derecho, 2);
            }
        }
    }
}
