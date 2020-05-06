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
        public static List<string> ErrorList = new List<string>();
        public static Dictionary<string, string> DiccionarioActions = new Dictionary<string, string>(); //PARA LA FASE III
        public static Dictionary<string, string> Definicion_SETS = new Dictionary<string, string>();
        public static Dictionary<string, string> Definicion_Tokens = new Dictionary<string, string>();
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
                    var tmp2 = string.Empty;//Contiene en el nombre del TOKEN
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
                        else
                        {
                            tmp2 += elemento[j];
                        }
                    }
                    elemento = tmp;
                    var definicion_SinComillas = elemento;
                    for (int j = 0; j < definicion_SinComillas.Length; j++)
                    {
                        if (definicion_SinComillas[j]== '\'')
                        {
                            definicion_SinComillas = definicion_SinComillas.Remove(j, 1);
                            definicion_SinComillas = definicion_SinComillas.Remove(j+1, 1);
                        }
                    }
                    
                    Definicion_Tokens.Add(tmp2.Trim('='), definicion_SinComillas);
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
                                elemento = elemento.Remove(j, count);
                            }
                        }
                    }
                    var linea = elemento.Split('=');
                    elemento = linea[1];
                    var tmp3 = linea[0];
                    Definicion_Tokens.Add(tmp3.Trim(' ', '\t'), elemento.Trim(' ', '\t'));
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
                                    if (i == item.Length)
                                    {
                                        break;
                                    }
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
                Definicion_SETS.Add(linea[0].Trim(' '), linea[1].Trim(' '));
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
        public static void LlenarDiccionarioActions(List<string> gramatica,int inicio, int fin) 
        {
            for (int i = inicio; i < fin; i++)
            {
                var tmp = gramatica[i].Split('=');
                DiccionarioActions.Add(tmp[0].Trim(' '), tmp[1].Trim('\'', ' '));
            }
        }
        public static void LlenrListaErrores(List<string> gramatica, int inicio, int fin) 
        {
            for (int i = inicio; i < fin; i++)
            {
                ErrorList.Add(gramatica[i]);
            }
        }
        public static void ModificarDiccionarioTokenes()// SE MODIFICA PARA AQUELLAS DEFINICIONES DE TOKEN QUE CONTENGAN LA COMILLA COMO CARACTER
        {
            //var listaAux = new List<string>();
            //foreach (var item in Definicion_Tokens.Keys)
            //{
            //    var bandera = false;
            //    for (int i = 0; i < SetsList.Count; i++)
            //    {
            //        if (Definicion_Tokens[item].Contains(SetsList[i]))
            //        {
            //            bandera = true;
            //        }
            //    }
            //    if (bandera!= true)
            //    {
            //        for (int i = 0; i < Definicion_Tokens[item].Length; i++)
            //        {
            //            if (i + 1 < Definicion_Tokens[item].Length)
            //            {
            //                if (Definicion_Tokens[item][i] == '\'' && Definicion_Tokens[item][i + 1] != '\'' && Definicion_Tokens[item][i + 1] != '\"')
            //                {
            //                    listaAux.Add(item);
            //                    break;
            //                }
            //            }
            //        }
            //    }
                
            //}
            //for (int i = 0; i < listaAux.Count; i++)
            //{
            //    for (int j = 0; j < Definicion_Tokens[listaAux[i]].Length; j++)
            //    {
            //        if (j + 1 < Definicion_Tokens[listaAux[i]].Length)
            //        {
            //            if (Definicion_Tokens[listaAux[i]][j] == '\'' && (Definicion_Tokens[listaAux[i]][j + 1] != '\'' || Definicion_Tokens[listaAux[i]][j + 1] != '\"'))
            //            {
            //                Definicion_Tokens[listaAux[i]] = Definicion_Tokens[listaAux[i]].Remove(j, 1);
            //                Definicion_Tokens[listaAux[i]] = Definicion_Tokens[listaAux[i]].Remove(j + 1, 1);
            //            }

            //        }
            //    }
            //}
            var respuesta = new List<string>();
            foreach (var item in Definicion_Tokens.Keys)
            {
                if (Definicion_Tokens[item].Contains("'\''") || Definicion_Tokens[item].Contains("'\"'"))
                {
                    respuesta.Add(item);
                }
            }
            for (int i = 0; i < respuesta.Count; i++)
            {
                var item = Definicion_Tokens[respuesta[i]];
                for (int j = 0; j < item.Length; j++)
                {
                     if (j + 2 < item.Length)
                     {
                          if ((item[j] == '\'' || item[j] == '\"'))
                          {
                             item = item.Insert(j + 1, "\\");
                            j += 3;
                          }
                     }
                    
                }
                Definicion_Tokens[respuesta[i]] = item;
            }
            
        }
    }
}
