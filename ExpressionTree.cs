using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_LFA
{
    public class ExpressionTree
    {
        public static Stack<char> stackTOKENS = new Stack<char>();
        public static Stack<Node> stackTREES = new Stack<Node>();
        public static Dictionary<char, int> opHierarchy = new Dictionary<char, int>();
        public static List<char> FinalTree = new List<char>();

        public static void FillInDictionaryHierarchy(List<char> op)
        {
            foreach (var item in op)
            {
                if (item == '*' || item == '+' || item == '?')
                {
                    opHierarchy.Add(item, 1);
                }
                else if (item == '.')
                {
                    opHierarchy.Add(item, 2);
                }
                else if (item == '|')
                {
                    opHierarchy.Add(item, 3);
                }
                else if (item == '(' || item == ')')
                {
                    opHierarchy.Add(item, 4);
                }
            }
        }

        public static Node CreateTree(List<char> simbolosT, List<char> op, string regEx)
        {
            var array = regEx.ToCharArray();
            for (int i = 0; i < regEx.Length; i++)
            {
                if (simbolosT.Contains(regEx[i]) && !op.Contains(regEx[i]))
                {
                    var node = new Node(regEx[i]);
                    stackTREES.Push(node);
                }
                else if (regEx[i] == '\\')
                {
                    var node = new Node(regEx[i + 1]);
                    stackTREES.Push(node);
                    i++;
                }
                else if (regEx[i] == '(')
                {
                    stackTOKENS.Push(regEx[i]);
                }
                else if (regEx[i] == ')')
                {
                    while (stackTOKENS.Count > 0 && stackTOKENS.Peek() != '(')
                    {
                        if (stackTOKENS.Count == 0)
                        {
                            //ERROR---> FALTAN OPERANDOS
                        }
                        if (stackTREES.Count < 2)
                        {
                            //ERROR---> FALTAN OPERANDOS
                        }
                        var temp = new Node(stackTOKENS.Pop());
                        var sonRight = stackTREES.Pop();
                        temp.rightNode = sonRight;
                        sonRight.father = temp;
                        var sonLeft = stackTREES.Pop();
                        temp.leftNode = sonLeft;
                        sonLeft.father = temp;
                        stackTREES.Push(temp);
                    }
                    stackTOKENS.Pop();
                }
                else if (op.Contains(regEx[i]) && regEx[i -1] != '\\')
                {
                    if (opHierarchy[regEx[i]] == 1)
                    {
                        var node = new Node(regEx[i]);
                        if (stackTREES.Count == 0)
                        {
                            //ERROR--> FALTAN OPERANDOS
                        }
                        var sonLeft = stackTREES.Pop();
                        node.leftNode = sonLeft;
                        sonLeft.father = node;
                        stackTREES.Push(node);
                    }
                    else if (stackTOKENS.Count != 0 && stackTOKENS.Peek() != '(' && CheckHierarchy(regEx[i], op) != false)
                    {
                        var temp = new Node(stackTOKENS.Pop());
                        if (stackTREES.Count < 2)
                        {
                            //ERROR= FALTAN OPERANDOS
                        }
                        var hijo_der = stackTREES.Pop();
                        temp.rightNode = hijo_der;
                        hijo_der.father = temp;
                        var hijo_izq = stackTREES.Pop();
                        temp.leftNode = hijo_izq;
                        hijo_izq.father = temp;
                        stackTREES.Push(temp);
                    }
                    if (opHierarchy[regEx[i]] != 1)
                    {
                        stackTOKENS.Push(regEx[i]);
                    }
                }
                else
                {
                    //ERROR= TOKEN NO ENCONTRADO
                }
            }
            InOrden(stackTREES.Peek());
            return stackTREES.Peek();
        }

        public static bool TopOp(List<char> op)
        {
            foreach (var item in stackTOKENS)
            {
                if (op.Contains(item))
                {
                    if (item == '(')
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool CheckHierarchy(char token, List<char> op)
        {
            if (opHierarchy[token] <= opHierarchy[stackTOKENS.Peek()])
            {
                return true;
            }
            return false;
        }

        public static void InOrden(Node leaf)
        {
            if (leaf != null)
            {
                InOrden(leaf.leftNode);
                FinalTree.Add(leaf.info);
                InOrden(leaf.rightNode);
            }
        }
    }
}
