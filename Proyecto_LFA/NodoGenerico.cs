using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_LFA
{
    public class Nodo_Generico
    {
        public Nodo_Generico raiz_aux { get; set; }//Para formar el arbol
        public Nodo_Generico hijo_derecho { get; set; }
        public Nodo_Generico hijo_izquierdo { get; set; }
        public Nodo_Generico padre { get; set; } //Sirve para el metodo de eliminar
        public bool recorrido { get; set; }
        public string id { get; set; }//Valor del nodo
        public int numero_hoja { get; set; }//Numero de la hoja
        public bool nullable { get; set; }//Saber si es nullable
        public List<int> first { get; set; }
        public List<int> last { get; set; }

        public int posX { get; set; }
        public int posY { get; set; }

        //First, Last, Annulable

        public Nodo_Generico(string value) //Metodo constructor
        {
            this.hijo_derecho = null;
            this.hijo_izquierdo = null;
            this.padre = null;
            this.recorrido = false;
            this.numero_hoja = 0;
            this.nullable = false;
            this.first = new List<int>();
            this.last = new List<int>();
            this.posX = 0;
            this.posY = 0;
            id = value;
        }
    }
}
