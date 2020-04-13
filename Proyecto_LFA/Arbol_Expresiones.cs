using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_LFA
{
    public class Arbol_Expresiones
    {
        public static Stack<char> pila_Tokens = new Stack<char>();// PILA TOKENS   T

        public static Stack<Nodo> pila_Arboles = new Stack<Nodo>();// PILA ARBOLES   S

        public static Dictionary<char, int> operadores_Precedencia = new Dictionary<char, int>();

        public static List<char> Arbol_Final = new List<char>();

        public static void LlenarDiccionarioPrecedencia(List<char> operadores) 
        {
            foreach (var item in operadores)
            {
                if (item == '*' || item == '+' || item == '?')
                {
                    operadores_Precedencia.Add(item, 1);
                }
                else if (item == '.')//concatenacion
                {
                    operadores_Precedencia.Add(item, 2);
                }
                else if (item == '|')
                {
                    operadores_Precedencia.Add(item, 3);
                }
                else if (item == '(' || item == ')')
                {
                    operadores_Precedencia.Add(item, 4);
                }
            }
        }
        public static Nodo GenerarArbol(List<char> simbolos_T, List<char> operadores, string expresion_Regular)//El que quiero insertar, su papa, su valor
        {
            var array = expresion_Regular.ToCharArray();
            for (int i = 0; i < expresion_Regular.Length; i++)//mientras existan tokens en ER
            {
                if (simbolos_T.Contains(expresion_Regular[i]) && !operadores.Contains(expresion_Regular[i]))// si token es ST
                {
                    var nodo = new Nodo(expresion_Regular[i]); //convertir ST en arbol
                    pila_Arboles.Push(nodo);// hacer push a S con nuevo arbol de ST
                }
                else if (expresion_Regular[i] == '\\')//OPERADOR QUE ESTA COMO ST
                {
                    var nodo = new Nodo(expresion_Regular[i+1]); //convertir ST en arbol
                    pila_Arboles.Push(nodo);// hacer push a S con nuevo arbol de ST
                    i++;
                }
                //ESTOS SON PARA AGRUPACION
                else if (expresion_Regular[i] == '(') // sino si token es (
                {
                    pila_Tokens.Push(expresion_Regular[i]);
                }
                else if (expresion_Regular[i] == ')') //sino si token es )
                {
                    while (pila_Tokens.Count > 0 && pila_Tokens.Peek() != '(') //mientras que
                    {//longitud de T mayor a 0 y cabeza de T sea diferente de (
                        if (pila_Tokens.Count == 0)// si longitud de T > 0
                        {
                            //ERROR---> FALTAN OPERANDOS
                        }
                        if (pila_Arboles.Count < 2)// si longitud de S < 2
                        {
                            //ERROR---> FALTAN OPERANDOS
                        }
                        //VERIFICAR
                        var temp = new Nodo(pila_Tokens.Pop()); //hacer pop a T y convertirlo en arbol
                        var hijo_derecho = pila_Arboles.Pop();
                        temp.hijo_derecho = hijo_derecho;//hacer pop a S y convertirlo en hijo derecho de temp
                        hijo_derecho.padre = temp;
                        var hijo_izq = pila_Arboles.Pop();
                        temp.hijo_izquierdo = hijo_izq;//hacer pop a S y convertirlo en hijo izquierdo de temp
                        hijo_izq.padre = temp;
                        pila_Arboles.Push(temp);//hacer push de temp en pila 
                    }
                    pila_Tokens.Pop();//hacer pop a T con ultimo dato
                }
                //ALGUNOS OPERADORES PUEDEN SER TAMBIEN ST DE LA EXPRESION
                else if (operadores.Contains(expresion_Regular[i]) && expresion_Regular[i-1] != '\\')// sino si token es op
                {
                    if (operadores_Precedencia[expresion_Regular[i]] == 1)// si op es unario
                    {
                        var nodo = new Nodo(expresion_Regular[i]);// convertir op en arbol
                        if (pila_Arboles.Count == 0)//si longitud de S es menor que 0
                        {
                            //ERROR--> FALTAN OPERANDOS
                        }
                        var hijo_izq = pila_Arboles.Pop();
                        nodo.hijo_izquierdo = hijo_izq;// hacer pop de S y asignarlo como hijo izquierdo
                        hijo_izq.padre = nodo;
                        pila_Arboles.Push(nodo);
                    }
                    else if(pila_Tokens.Count !=0 && pila_Tokens.Peek() != '(' && VerificarPrecedencia(expresion_Regular[i], operadores) != false)//sino si T no esta vacia y el top op en T != '('
                    {//y precedencia de token es menor a ultimo op en T
                        var temp = new Nodo(pila_Tokens.Pop());
                        if (pila_Arboles.Count <2)
                        {
                            //ERROR= FALTAN OPERANDOS
                        }
                        //AGREGAR PADRE
                        var hijo_der = pila_Arboles.Pop();
                        temp.hijo_derecho = hijo_der;
                        hijo_der.padre = temp;
                        var hijo_izq = pila_Arboles.Pop();
                        temp.hijo_izquierdo = hijo_izq;
                        hijo_izq.padre = temp;
                        pila_Arboles.Push(temp);
                        //sacar a op de T, volverlo arbol, llamarlo

                    }
                    if (operadores_Precedencia[expresion_Regular[i]] != 1)//no es unario
                    {
                        pila_Tokens.Push(expresion_Regular[i]);
                    }
                }
                else
                {
                    //ERROR= TOKEN NO ENCONTRADO
                }
            }
            //COMPROBAR ARBOL MEDIANTE INORDEN
            In_Orden(pila_Arboles.Peek());
            return pila_Arboles.Peek();
        }

        public static bool TopOp(List<char> operadores) 
        {
            foreach (var item in pila_Tokens)
            {
                if (operadores.Contains(item))
                {
                    if (item == '(')
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool VerificarPrecedencia(char token, List<char> operadores) 
        {
            if (operadores_Precedencia[token] <= operadores_Precedencia[pila_Tokens.Peek()])
            {
                return true;
            }
            return false;
        }
        public static void In_Orden(Nodo hoja) 
        {
            if (hoja != null)
            {
                In_Orden(hoja.hijo_izquierdo);
                Arbol_Final.Add(hoja.id);
                In_Orden(hoja.hijo_derecho);
            }
        }
    }
}
