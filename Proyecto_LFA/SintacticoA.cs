using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_LFA
{
    public class SintacticoA
    {
        public static List<string> TokensList = new List<string>();//Lista que contiene tokens ya para la creacion de un arbol
        public static List<string> SetsList = new List<string>();//Lista que contiene los SETS declarados para manejo de operaciones
        public static List<char> Operadoresist = new List<char>();//Lista que contiene los operadores declarados para manejo de operaciones
        public static string Tokenizar(List<string> seccionTokens, int inicioTokens, int finTokens, ref bool error_Encontrado) 
        {
            var listaAux = new List<string>();
            //-------------------------PASO 1: En este ciclo se separa y se almacena la definicion de cada token para la creacion de la expresion
            for (int i = inicioTokens; i < finTokens; i++)
            {
                var elemento = seccionTokens[i];
                //casos donde el = es un ST
                if (elemento.Contains("'='"))
                {
                    var tmp = string.Empty;
                    for (int j = 0; j < elemento.Length; j++)
                    {
                        if (elemento[j] == '\'')
                        {
                            while (j < elemento.Length)
                            {
                                tmp += elemento[j];
                                j++;
                            }
                        }
                    }
                    elemento = tmp;
                }
                else if (!elemento.Contains("'='"))
                {
                    if (elemento.Contains("RESERVADAS"))
                    {
                        for (int j = 0; j < elemento.Length; j++)
                        {
                            if (elemento[j] == '{')
                            {
                                var count = elemento.Length - j;
                                elemento = elemento.Remove(j, count - 1);
                            }
                        }
                    }
                    var linea = elemento.Split('=');
                    elemento = linea[1];
                }
                var trim_chars = new char[] { ' ', '\t' };
                elemento = elemento.Trim(trim_chars);
                listaAux.Add(elemento);
            }
            //En este ciclo, se procede a tokenizar
            foreach(var item in listaAux)
            {
                if (MensajeError.error_Encontrado != true)
                {
                    var tmp = item;
                    //-----Filtro 0: Si encuentra muchas letras juntas, verificar que se encuentre en SETS
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        if (char.IsLetter(item[i]) && i + 1 < item.Length)
                        {
                            if (char.IsLetter(item[i + 1]))
                            {
                                var verificador = string.Empty;
                                while (char.IsLetter(item[i]))
                                {
                                    verificador += item[i];
                                    i++;
                                }
                                if (VerificandoSet(verificador) != true)
                                {
                                    MensajeError.mensaje_Error = $"La palabra {verificador} no ha sido declarada como set previamente";
                                    Prueba.MostrarError(MensajeError.mensaje_Error);
                                    MensajeError.error_Encontrado = true;
                                    error_Encontrado = true;
                                }
                            }
                        }
                    }
                    //-----Filtro #1: Sustituir espacios por puntos
                    if (tmp.Contains(" ") && tmp.Contains("' '") == false)
                    {
                        tmp = tmp.Replace(' ', '.');
                    }
                    //-----Filtro #2: Quitar los puntos innecesarios
                    if (tmp.Contains(".") && !tmp.Contains("'.'"))
                    {
                        for (int i = 0; i < tmp.Length; i++)
                        {
                            //item[i+1] == '|' || item[i + 1] == ')' || item[i - 1] == '|' || item[i + 1] == '(' || item[i + 1] == '*'
                            if (tmp[i] == '.' && ((Arbol_Expresiones.operadores_Precedencia.ContainsKey(tmp[i + 1]) && tmp[i + 1] != '(') || tmp[i - 1] == '|' || tmp[i - 1] == '('))
                            {
                                tmp = tmp.Remove(i, 1);
                            }
                        }
                    }
                    //-----Filtro #3: Agrupar para | sin parentesis
                    if (tmp.Contains("|"))
                    {
                        for (int i = 0; i < tmp.Length; i++)
                        {
                            if (tmp[i] == '|')
                            {
                                var adelante = i;
                                var atras = i;
                                var cierra = false;
                                var abre = false;

                                for (int j = adelante; j < tmp.Length; j++)
                                {
                                    if (tmp[j] == ')')
                                    {
                                        cierra = true;
                                    }
                                }
                                for (int j = atras; j > 0; j--)
                                {
                                    if (tmp[j] == '(')
                                    {
                                        abre = true;
                                    }
                                }
                                if (abre == false && cierra == false)
                                {
                                    tmp = tmp.Insert(i + 1, "(");
                                    tmp = tmp.Insert(i, ")");
                                    i++;
                                    //tmp = $"({tmp})";
                                }
                            }
                        }
                    }
                    //----->Filtro #4: Quitar comilas simples (PRUEBA B)
                    for (int i = 0; i < tmp.Length; i++)
                    {
                        if (tmp[i] == '\'')
                        {
                            tmp = tmp.Remove(i, 1);
                            tmp = tmp.Remove(i + 1, 1);
                            if (i + 1 < tmp.Length)
                            {
                                if (tmp[i + 1] != '.' && tmp[i + 1] != '|' && tmp[i + 1] != ')')
                                {
                                    tmp = tmp.Insert(i + 1, ".");
                                }
                            }
                            if (Arbol_Expresiones.operadores_Precedencia.ContainsKey(tmp[i]))
                            {
                                tmp = tmp.Insert(i, "\\");
                            }
                        }
                    }
                    TokensList.Add($"({tmp})|");
                }
            }
            var expresion_Tokens = string.Empty;
            for (int i = 0; i < TokensList.Count; i++)
            {
                expresion_Tokens += TokensList[i];
            }
            expresion_Tokens = expresion_Tokens.Remove(expresion_Tokens.Length-1, 1);
            expresion_Tokens = $"({expresion_Tokens}.#)";
            return expresion_Tokens;
        }
        public static void ObtenerListaSETS(List<string> seccionSETS, int inicioSETS, int finalSETS) 
        {
            for (int i = inicioSETS; i < finalSETS; i++)
            {
                var linea = seccionSETS[i].Split('=');
                var elemento = linea[0].Trim(' ');
                SetsList.Add(elemento);
            }
        }
        public static bool VerificandoSet(string set) 
        {
            if (SetsList.Contains(set))
            {
                return true;
            }
            return false;
        }
    }
}
