using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_LFA
{
    public class Helpers
    {
        public static Stack<char> pila_Tokens = new Stack<char>();// PILA TOKENS   T

        public static Stack<Nodo_Generico> pila_Arboles = new Stack<Nodo_Generico>();// PILA ARBOLES   S

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
        public static Nodo_Generico GenerarArbol(List<string> simbolos_T, List<char> operadores, string expresion_Regular)//El que quiero insertar, su papa, su valor
        {
            for (int i = 0; i < expresion_Regular.Length; i++)//mientras existan tokens en ER
            {
                if (char.IsLetter(expresion_Regular[i]) && char.IsLetter(expresion_Regular[i+1]))
                {
                    var tmp = string.Empty;
                    while (char.IsLetter(expresion_Regular[i]))
                    {
                        tmp += expresion_Regular[i];
                        i++;
                    }
                    i--;
                    //--------------->
                    var nodo = new Nodo_Generico(tmp);
                    pila_Arboles.Push(nodo);
                }
                else if (simbolos_T.Contains(expresion_Regular[i].ToString()) && !operadores.Contains(expresion_Regular[i]))// si token es ST
                {
                    var nodo = new Nodo_Generico(expresion_Regular[i].ToString()); //convertir ST en arbol
                    pila_Arboles.Push(nodo);// hacer push a S con nuevo arbol de ST
                }
                else if (expresion_Regular[i] == '\\')//OPERADOR QUE ESTA COMO ST
                {
                    var nodo = new Nodo_Generico(expresion_Regular[i + 1].ToString()); //convertir ST en arbol
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
                        var temp = new Nodo_Generico(pila_Tokens.Pop().ToString()); //hacer pop a T y convertirlo en arbol
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
                else if (operadores.Contains(expresion_Regular[i]) && expresion_Regular[i - 1] != '\\')// sino si token es op
                {
                    if (operadores_Precedencia[expresion_Regular[i]] == 1)// si op es unario
                    {
                        var nodo = new Nodo_Generico(expresion_Regular[i].ToString());// convertir op en arbol
                        if (pila_Arboles.Count == 0)//si longitud de S es menor que 0
                        {
                            //ERROR--> FALTAN OPERANDOS
                        }
                        var hijo_izq = pila_Arboles.Pop();
                        nodo.hijo_izquierdo = hijo_izq;// hacer pop de S y asignarlo como hijo izquierdo
                        hijo_izq.padre = nodo;
                        pila_Arboles.Push(nodo);
                    }
                    else if (pila_Tokens.Count != 0 && pila_Tokens.Peek() != '(' && VerificarPrecedencia(expresion_Regular[i], operadores) != false)//sino si T no esta vacia y el top op en T != '('
                    {//y precedencia de token es menor a ultimo op en T
                        var temp = new Nodo_Generico(pila_Tokens.Pop().ToString());
                        if (pila_Arboles.Count < 2)
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

        public static void FLN(Nodo_Generico arbol_Expresion, ref int contador, ref Dictionary<int, List<int>> follows, ref Dictionary<int, string> hojas) 
        {
            if (arbol_Expresion != null)
            {
                FLN(arbol_Expresion.hijo_izquierdo, ref contador, ref follows, ref hojas);
                FLN(arbol_Expresion.hijo_derecho, ref contador, ref follows, ref hojas);
                if (Verificar_esHoja(arbol_Expresion) == true)
                {
                    arbol_Expresion.numero_hoja = contador;
                    var list_Aux = new List<int>();
                    follows.Add(contador, list_Aux);
                    hojas.Add(contador, arbol_Expresion.id);
                    //Genero First, last
                    Obtener_First(arbol_Expresion, true);
                    Obtener_Last(arbol_Expresion, true);
                    contador++;
                }
                else
                {
                    Obtener_First(arbol_Expresion, false);
                    Obtener_Last(arbol_Expresion, false);
                    Obtener_Nullable(arbol_Expresion);
                }
            }
        }
        public static bool Verificar_esHoja(Nodo_Generico nodo) 
        {
            if (nodo.hijo_derecho == null && nodo.hijo_izquierdo == null)
            {
                return true;
            }
            return false;
        }
        public static void Obtener_First(Nodo_Generico nodo, bool es_Hoja) 
        {
            if (es_Hoja == true)
            {
                nodo.first.Add(nodo.numero_hoja);
            }
            else
            {
                switch (nodo.id)
                {
                    case "|"://F= F(IZQ)+F(DER)
                        for (int i = 0; i < nodo.hijo_izquierdo.first.Count; i++)
                        {
                            nodo.first.Add(nodo.hijo_izquierdo.first[i]);
                        }
                        for (int i = 0; i < nodo.hijo_derecho.first.Count; i++)
                        {
                            nodo.first.Add(nodo.hijo_derecho.first[i]);
                        }
                        break;
                    case ".":// if N(IZQ): F=F(IZQ)+F(DER)-----------------ELSE: F=F(IZQ)
                        if (nodo.hijo_izquierdo.nullable == true)
                        {
                            for (int i = 0; i < nodo.hijo_izquierdo.first.Count; i++)
                            {
                                nodo.first.Add(nodo.hijo_izquierdo.first[i]);
                            }
                            for (int i = 0; i < nodo.hijo_derecho.first.Count; i++)
                            {
                                nodo.first.Add(nodo.hijo_derecho.first[i]);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < nodo.hijo_izquierdo.first.Count; i++)
                            {
                                nodo.first.Add(nodo.hijo_izquierdo.first[i]);
                            }
                        }
                        break;
                    case "*"://F=F(IZQ)
                        for (int i = 0; i < nodo.hijo_izquierdo.first.Count; i++)
                        {
                            nodo.first.Add(nodo.hijo_izquierdo.first[i]);
                        }
                        break;
                    case "+"://F=F(IZQ)
                        for (int i = 0; i < nodo.hijo_izquierdo.first.Count; i++)
                        {
                            nodo.first.Add(nodo.hijo_izquierdo.first[i]);
                        }
                        break;
                    case "?"://F=F(IZQ)
                        for (int i = 0; i < nodo.hijo_izquierdo.first.Count; i++)
                        {
                            nodo.first.Add(nodo.hijo_izquierdo.first[i]);
                        }
                        break;
                }
            }
        }
        public static void Obtener_Last(Nodo_Generico nodo, bool es_Hoja)
        {
            if (es_Hoja == true)
            {
                nodo.last.Add(nodo.numero_hoja);
            }
            else
            {
                switch (nodo.id)
                {
                    case "|"://L= L(IZQ)+L(DER)
                        for (int i = 0; i < nodo.hijo_izquierdo.last.Count; i++)
                        {
                            nodo.last.Add(nodo.hijo_izquierdo.last[i]);
                        }
                        for (int i = 0; i < nodo.hijo_derecho.last.Count; i++)
                        {
                            nodo.last.Add(nodo.hijo_derecho.last[i]);
                        }
                        break;
                    case ".":// if N(DER): L=L(IZQ)+L(DER)-----------------ELSE: L=L(DER)
                        if (nodo.hijo_derecho.nullable == true)
                        {
                            for (int i = 0; i < nodo.hijo_izquierdo.last.Count; i++)
                            {
                                nodo.last.Add(nodo.hijo_izquierdo.last[i]);
                            }
                            for (int i = 0; i < nodo.hijo_derecho.last.Count; i++)
                            {
                                nodo.last.Add(nodo.hijo_derecho.last[i]);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < nodo.hijo_derecho.last.Count; i++)
                            {
                                nodo.last.Add(nodo.hijo_derecho.last[i]);
                            }
                        }
                        break;
                    case "*"://L=L(IZQ)
                        for (int i = 0; i < nodo.hijo_izquierdo.last.Count; i++)
                        {
                            nodo.last.Add(nodo.hijo_izquierdo.last[i]);
                        }
                        break;
                    case "+"://F=F(IZQ)
                        for (int i = 0; i < nodo.hijo_izquierdo.last.Count; i++)
                        {
                            nodo.last.Add(nodo.hijo_izquierdo.last[i]);
                        }
                        break;
                    case "?"://F=F(IZQ)
                        for (int i = 0; i < nodo.hijo_izquierdo.last.Count; i++)
                        {
                            nodo.last.Add(nodo.hijo_izquierdo.last[i]);
                        }
                        break;
                }
            }
        }
        public static void Obtener_Nullable(Nodo_Generico nodo) 
        {
            switch (nodo.id)
            {
                case "|"://N= N(IZQ) || N(DER)
                    if (nodo.hijo_derecho.nullable == true || nodo.hijo_izquierdo.nullable == true)
                    {
                        nodo.nullable = true;
                    }
                    break;
                case "."://N= N(IZQ) && N(DER)
                    if (nodo.hijo_derecho.nullable == true && nodo.hijo_izquierdo.nullable == true)
                    {
                        nodo.nullable = true;
                    }
                    break;
                case "*"://N = TRUE
                    nodo.nullable = true;
                    break;
                case "+"://N=N(IZQ)
                    if (nodo.hijo_izquierdo.nullable == true)
                    {
                        nodo.nullable = true;
                    }
                    break;
                case "?"://N = TRUE
                    nodo.nullable = true;
                    break;
            }
        }
        public static void Generar_Follow(Nodo_Generico arbol, ref Dictionary<int, List<int>> tabla_follow) 
        {
            if (arbol!= null)
            {
                Generar_Follow(arbol.hijo_izquierdo, ref tabla_follow);
                Generar_Follow(arbol.hijo_derecho, ref tabla_follow);
                if (Verificar_esHoja(arbol) == false)
                {
                    switch (arbol.id)
                    {
                        case ".":
                            for (int i = 0; i < arbol.hijo_izquierdo.last.Count; i++)
                            {
                                for (int j = 0; j < arbol.hijo_derecho.first.Count; j++)
                                {
                                    if (!tabla_follow[arbol.hijo_izquierdo.last[i]].Contains(arbol.hijo_derecho.first[j]))
                                    {
                                        tabla_follow[arbol.hijo_izquierdo.last[i]].Add(arbol.hijo_derecho.first[j]);
                                    }
                                }
                            }
                            break;
                        case "+":
                            for (int i = 0; i < arbol.hijo_izquierdo.last.Count; i++)
                            {
                                for (int j = 0; j < arbol.hijo_izquierdo.first.Count; j++)
                                {
                                    if (!tabla_follow[arbol.hijo_izquierdo.last[i]].Contains(arbol.hijo_izquierdo.first[j]))
                                    {
                                        tabla_follow[arbol.hijo_izquierdo.last[i]].Add(arbol.hijo_izquierdo.first[j]);
                                    }
                                }
                            }
                            break;
                        case "*":
                            for (int i = 0; i < arbol.hijo_izquierdo.last.Count; i++)
                            {
                                for (int j = 0; j < arbol.hijo_izquierdo.first.Count; j++)
                                {
                                    if (!tabla_follow[arbol.hijo_izquierdo.last[i]].Contains(arbol.hijo_izquierdo.first[j]))
                                    {
                                        tabla_follow[arbol.hijo_izquierdo.last[i]].Add(arbol.hijo_izquierdo.first[j]);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            
            
        }
        public static Dictionary<List<int>, List<List<int>>> GenerarEstados_Transiciones(List<string> st_SINTACTICO, List<int> first, Dictionary<int, string> diccionario_hojas, Dictionary<int, List<int>> tabla_follow) 
        {
            var lista_Estados = new List<List<int>>();
            var lista_FiltroEstados = new List<List<int>>(); //Para no agregar repetidos
            var diccionario_ET = new Dictionary<List<int>, List<List<int>>>();
            lista_Estados.Add(first);
            lista_FiltroEstados.Add(first);
            while (lista_Estados.Count>0)//Para recorrer todos los estados
            {
                var lista_transiciones = new List<List<int>>();
                for (int i = 0; i < st_SINTACTICO.Count; i++)
                {
                    var transicion = new List<int>();
                    for (int j = 0; j < lista_Estados[0].Count; j++)
                    {
                        var aux = lista_Estados[0];
                        if (diccionario_hojas[aux[j]] == st_SINTACTICO[i])
                        {
                            var tmp = tabla_follow[aux[j]];
                            for (int k = 0; k < tmp.Count; k++)
                            {
                                if (!transicion.Contains(tmp[k]))
                                {
                                    transicion.Add(tmp[k]);
                                }
                            }
                        }
                    }
                    //Ordeno mi transicion
                    if (transicion.Count >0)
                    {
                        transicion.Sort();
                        lista_transiciones.Add(transicion);
                        //Verificar que esta transicion sea un nuevo estado
                        var contador_iguales = 0;
                        for (int m = 0; m < lista_FiltroEstados.Count; m++)
                        {
                            if (transicion.SequenceEqual(lista_FiltroEstados[m]) == true)
                            {
                                contador_iguales++;
                            }
                        }
                        if (contador_iguales == 0)
                        {
                            lista_Estados.Add(transicion);
                            lista_FiltroEstados.Add(transicion);
                        }
                    }
                    else
                    {
                        transicion.Add(0);
                        lista_transiciones.Add(transicion);
                    }
                }
                diccionario_ET.Add(lista_Estados[0], lista_transiciones);
                lista_Estados.Remove(lista_Estados[0]);
            }
            return diccionario_ET;
        }
        public static void GenerarFilas_FLN(Nodo_Generico nodo) 
        {
            if (nodo!=null)
            {
                GenerarFilas_FLN(nodo.hijo_izquierdo);
                GenerarFilas_FLN(nodo.hijo_derecho);
                var row = Form1.DataTableFLN.NewRow();
                row["Simbolo"] = nodo.id;
                var aux = string.Empty;
                for (int i = 0; i < nodo.first.Count; i++)
                {
                    aux += $"{nodo.first[i]},";
                }
                aux = aux.Remove(aux.Length-1,1);
                row["First"] = aux;
                aux = string.Empty;
                for (int i = 0; i < nodo.last.Count; i++)
                {
                    aux += $"{nodo.last[i]},";
                }
                aux = aux.Remove(aux.Length - 1, 1);
                row["Last"] = aux;
                aux = string.Empty;
                if (nodo.nullable == false)
                {
                    row["Nullable"] = "false";
                }
                else
                {
                    row["Nullable"] = "true";
                }
                Form1.DataTableFLN.Rows.Add(row);
            }

        }
        public static void GenerarFilas_FOLLOW(Dictionary<int, List<int>> diccionario_SF) 
        {
            foreach (var item in diccionario_SF.Keys)
            {
                var row = Form1.DataTableFOLLOW.NewRow();
                row["Simbolo"] = item;
                var aux = string.Empty;
                for (int i = 0; i < diccionario_SF[item].Count; i++)
                {
                    aux += $"{diccionario_SF[item][i]},";
                }
                if (aux.Length>0)
                {
                    aux = aux.Remove(aux.Length - 1, 1);
                }
                else
                {
                    aux = "-";
                }
                row["Follow"] = aux;
                aux = string.Empty;
                Form1.DataTableFOLLOW.Rows.Add(row);
            }
        }
        public static void GenerarFilas_ET(Dictionary<List<int>, List<List<int>>> estado_transicion, List<string>st) 
        {
            foreach (var item in estado_transicion.Keys)
            {
                var row = Form1.DataTableET.NewRow();
                var aux = string.Empty;
                for (int i = 0; i < item.Count; i++)
                {
                    aux += $"{item[i]},";
                }
                aux = aux.Remove(aux.Length-1, 1);
                row["Estado"] = aux;
                aux = string.Empty;
                for (int i = 0; i < estado_transicion[item].Count; i++)
                {
                    for (int j = 0; j < estado_transicion[item][i].Count; j++)
                    {
                        aux += $"{estado_transicion[item][i][j]},";
                    }
                    aux = aux.Remove(aux.Length - 1, 1);
                    row[st[i]] = aux;
                    aux = string.Empty;
                }
                Form1.DataTableET.Rows.Add(row);
            }
        }
    }
}
