using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_LFA
{
    public class Node
    {
        public Node root { get; set; }
        public Node rightNode { get; set; }
        public Node leftNode { get; set; }
        public Node father { get; set; }
        public bool route { get; set; }
        public char info { get; set; }

        public Node(char value)
        {
            this.rightNode = null;
            this.leftNode = null;
            this.father = null;
            this.route = false;
            info = value;
        }
    }
}
