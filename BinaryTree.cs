using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_LFA
{
    class BinaryTree
    {
        public int count;
        public Nodo root;
        public class Nodo
        {
            public char caracter;
            public Nodo left;
            public Nodo right;

            public Nodo(char info)
            {
                caracter = info;
                left = null;
                right = null;
            }
        }

        public BinaryTree()
        {
            root = null;
            count = 0;
        }

        public Nodo Insert(char data)
        {
            Nodo nNodo = new Nodo(data);
            if (root == null)
            {
                root = nNodo;
                nNodo.left = null;
                nNodo.right = null;
                return root;
            }
            else
                return InsertNewNodo(root, data);
        }

        public Nodo InsertNewNodo(Nodo root, char data)
        {
            if (root == null)
            {
                root = new Nodo(data);
                root.left = null;
                root.right = null;
                count++;
            }
            else
            {
                if (data < root.caracter)
                    root.left = InsertNewNodo(root.left, data);
                else
                    root.right = InsertNewNodo(root.right, data);
            }
            return root;
        }

        public List<string> GetInOrder()
        {
            return InOrder(root);
        }

        private List<string> InOrder(Nodo nodo)
        {
            List<string> Datos = new List<string>();
            if (nodo != null)
            {
                Datos.AddRange(InOrder(nodo.left));
                Datos.Add(Convert.ToString(nodo.caracter));
                Datos.AddRange(InOrder(nodo.right));
            }
            return Datos;
        }

    }
}
