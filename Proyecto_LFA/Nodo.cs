using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_LFA
{
    public class Nodo
    {
        public Nodo raiz_aux { get; set; }//Para formar el arbol
        public Nodo hijo_derecho { get; set; }
        public Nodo hijo_izquierdo { get; set; }
        public Nodo padre { get; set; } //Sirve para el metodo de eliminar
        public bool recorrido { get; set; }

        public char  id { get; set; }//Nombre del farmaco

        public Nodo(char value) //Metodo constructor
        {
            this.hijo_derecho = null;
            this.hijo_izquierdo = null;
            this.padre = null;
            this.recorrido = false;
            id = value;
        }
    }
}
