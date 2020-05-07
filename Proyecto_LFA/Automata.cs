using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_LFA
{
    public class Automata
    {
        public static Dictionary<int, List<int>> Estados_Abreviados = new Dictionary<int, List<int>>();//SUSTITUCION DEL NOMBRE DE ESTADOS A NUMEROS
        public static List<int> Cantidad_Transiciones = new List<int>();
        public static List<int> Estados_Aceptacion = new List<int>();
        public static void EscribirNuevaSolucion() 
        {
            LlenarDiccionarioAbreviado();
            LlenarContadorTransiciones();
            LlenarListaAceptados();
            using (var writer = new StreamWriter("C:\\Users\\Boris Moran\\Desktop\\Escaner\\Program.cs"))
            {
                writer.WriteLine("using System;");
                writer.WriteLine("using System.Collections.Generic;");
                writer.WriteLine("using System.Linq;");
                writer.WriteLine("using System.Text;");
                writer.WriteLine("using System.Threading.Tasks;");
                writer.WriteLine("namespace Escaner");
                writer.WriteLine("{");
                writer.WriteLine("      public class program");
                writer.WriteLine("      {");
                writer.WriteLine("          public static string cadena = string.Empty; //variable donde se almacena la cadena ingresada por el usuario");
                writer.WriteLine("          public static List<int> Estados_Aceptacion = new List<int>();");
                writer.WriteLine("          public static List<string> Error_List = new List<string>();");
                writer.WriteLine("          public static List<string> Lexema = new List<string>();");
                writer.WriteLine("          public static Dictionary<string, string> Diccionario_actions = new Dictionary<string, string>();");
                writer.WriteLine("          public static Dictionary<string, string> Definicion_Tokens = new Dictionary<string, string>();");
                writer.WriteLine("          static void Main(string[] args)");
                writer.WriteLine("          {");
                writer.WriteLine("              Console.WriteLine(\"Ingresar cadena a escanear\");");
                writer.WriteLine("              cadena = Console.ReadLine();");
                writer.WriteLine("              cadena = cadena.ToLower();");
                writer.WriteLine("              if(cadena.Length > 0)");
                writer.WriteLine("              {");
                writer.WriteLine("                  LlenarListaAceptados();");
                writer.WriteLine("                  LlenarDiccionarioActions();");
                writer.WriteLine("                  LlenarDiccionario_DefinicionTokens();");
                writer.WriteLine("                  Llenar_ListaErrores();");
                writer.WriteLine("                  var resultado_cadena = EvaluarCadena(cadena);");
                writer.WriteLine("                  if(resultado_cadena == true) //Toda la cadena es aceptada(EN TEORIA)");
                writer.WriteLine("                  {");
                writer.WriteLine("                      Console.WriteLine(\"La cadena fue aceptada.\");");
                writer.WriteLine("                      MostrarComponentesLexicos(cadena.Split(' '));");
                writer.WriteLine("                  }");
                writer.WriteLine("                  else");
                writer.WriteLine("                  {");
                writer.WriteLine("                      Console.WriteLine(\"La cadena no fue aceptada.\");");
                writer.WriteLine("                      Console.WriteLine($\"{Error_List[0]}\");");
                writer.WriteLine("                  }");
                writer.WriteLine("              }");
                writer.WriteLine("              else");
                writer.WriteLine("              {");
                writer.WriteLine("                      Console.WriteLine(\"No se puede realizar analisis de una cadena vacia\");");
                writer.WriteLine("              }");
                writer.WriteLine("              Console.ReadKey();");
                writer.WriteLine("          }");
                writer.WriteLine("");
                writer.WriteLine("          public static bool EvaluarCadena(string cadena)");
                writer.WriteLine("          {");
                writer.WriteLine("              var resultado_cadena = true;");
                writer.WriteLine("              var vectorPalabras = cadena.Split(' ');");
                writer.WriteLine("              var error_encontrado = 0;");
                writer.WriteLine("              for(int i=0; i< vectorPalabras.Length; i++)");
                writer.WriteLine("              {");
                writer.WriteLine("                  var estado_temporal = 0;");
                writer.WriteLine("                  var temp= vectorPalabras[i];");
                writer.WriteLine("                  var lexema= string.Empty;");
                writer.WriteLine("                  if(temp.Length > 0)");
                writer.WriteLine("                  {");
                writer.WriteLine("                      for(int j=0; j < temp.Length; j++) //Para recorrer cada caracter de cada palabra");
                writer.WriteLine("                      {");
                writer.WriteLine("                          var existe = false;");
                writer.WriteLine("                          switch(estado_temporal)");
                writer.WriteLine("                          {");
                                                                for (int i = 0; i < Form1.diccionario_EstadoTransicion.Count; i++)
                                                                {
                                                                    var contador_ifs = 0;
                writer.WriteLine($"                                 case {i}:");
                                                                    for (int j = 0; j < Form1.st_SINTACTICO.Count; j++)
                                                                    {
                                                                        if (VerificarSiSeHace(i, j) == true)
                                                                        {
                                                                            if (SintacticoA.SetsList.Contains(Form1.st_SINTACTICO[j]))
                                                                            {
                                                                                //CREAR METODO PARA DEFINIR EL RANGO DE VALORES PARA EL CARACTER
                                                                                if (contador_ifs == 0)
                                                                                {
                writer.WriteLine($"                                            if({DevolverLineaIf(Form1.st_SINTACTICO[j])}) //Definido para el SET {Form1.st_SINTACTICO[j]}");
                writer.WriteLine("                                            {");
                writer.WriteLine($"                                               estado_temporal = {DefinirEstado(i, Form1.st_SINTACTICO[j], j)};");
                writer.WriteLine("                                                existe = true;");
                writer.WriteLine($"                                               lexema += \"{Form1.st_SINTACTICO[j]} \";");
                writer.WriteLine("                                            }");
                                                                                    contador_ifs++;
                                                                                }
                                                                                else
                                                                                {
                writer.WriteLine($"                                            else if({DevolverLineaIf(Form1.st_SINTACTICO[j])}) //Definido para el SET {Form1.st_SINTACTICO[j]}");
                writer.WriteLine("                                            {");
                writer.WriteLine($"                                               estado_temporal = {DefinirEstado(i, Form1.st_SINTACTICO[j], j)};");
                writer.WriteLine("                                                existe = true;");
                writer.WriteLine($"                                               lexema += \"{Form1.st_SINTACTICO[j]} \";");
                writer.WriteLine("                                            }");
                                                                                    contador_ifs++;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (contador_ifs == 0)
                                                                                {
                                                                                    if (Form1.st_SINTACTICO[j] == "\'")
                                                                                    {
                writer.WriteLine($"                                           if (temp[j] == \'\\{Form1.st_SINTACTICO[j]}\') //Definido para  {Form1.st_SINTACTICO[j]}");
                writer.WriteLine("                                           {");
                writer.WriteLine($"                                               estado_temporal = {DefinirEstado(i, Form1.st_SINTACTICO[j], j)};");
                writer.WriteLine("                                                existe = true;");
                writer.WriteLine($"                                               lexema += \"\\{Form1.st_SINTACTICO[j]} \";");
                writer.WriteLine("                                           }");
                                                                                    }
                                                                                    else
                                                                                    {
                writer.WriteLine($"                                           if (temp[j] == \'{Form1.st_SINTACTICO[j]}\') //Definido para  {Form1.st_SINTACTICO[j]}");
                writer.WriteLine("                                           {");
                writer.WriteLine($"                                               estado_temporal = {DefinirEstado(i, Form1.st_SINTACTICO[j], j)};");
                writer.WriteLine("                                                existe = true;");
                                                                                        if (Form1.st_SINTACTICO[j] == "\"")
                                                                                        {
                writer.WriteLine($"                                               lexema += \"\\{Form1.st_SINTACTICO[j]} \";");
                                                                                        }
                                                                                        else
                                                                                        {
                writer.WriteLine($"                                               lexema += \"{Form1.st_SINTACTICO[j]} \";");
                                                                                        }
                                        
                writer.WriteLine("                                           }");
                                                                                    }   
                                                                                    contador_ifs++;
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (Form1.st_SINTACTICO[j] == "\'")
                                                                                    {
                writer.WriteLine($"                                           else if (temp[j] == \'\\{Form1.st_SINTACTICO[j]}\') //Definido para  {Form1.st_SINTACTICO[j]}");
                writer.WriteLine("                                           {");
                writer.WriteLine($"                                               estado_temporal = {DefinirEstado(i, Form1.st_SINTACTICO[j], j)};");
                writer.WriteLine("                                                existe = true;");
                writer.WriteLine($"                                               lexema += \"\\{Form1.st_SINTACTICO[j]} \";");
                writer.WriteLine("                                           }");
                                                                                    }
                                                                                    else
                                                                                    {
                writer.WriteLine($"                                          else if (temp[j] == \'{Form1.st_SINTACTICO[j]}\') //Definido para  {Form1.st_SINTACTICO[j]}");
                writer.WriteLine("                                           {");
                writer.WriteLine($"                                               estado_temporal = {DefinirEstado(i, Form1.st_SINTACTICO[j], j)};");
                writer.WriteLine("                                                existe = true;");
                                                                                    if (Form1.st_SINTACTICO[j] == "\"")
                                                                                    {
                writer.WriteLine($"                                               lexema += \"\\{Form1.st_SINTACTICO[j]} \";");
                                                                                    }
                                                                                    else
                                                                                    {
                writer.WriteLine($"                                               lexema += \"{Form1.st_SINTACTICO[j]} \";");
                                                                                    }
                writer.WriteLine("                                           }");
                                                                                    }
                                                                                contador_ifs++;
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                writer.WriteLine($"                                     break;");
                                                                }
                writer.WriteLine("                          }");
                writer.WriteLine("                          if(existe == false) //NO EXISTE LA PALABRA EN LA GRAMATICA");
                writer.WriteLine("                          {");
                writer.WriteLine("                              Console.WriteLine($\"El caracter {temp[j]} no se encuentra definido \");");
                writer.WriteLine("                              error_encontrado++;");
                writer.WriteLine("                          }");
                writer.WriteLine("                      }");
                writer.WriteLine("                       Lexema.Add(lexema);");
                writer.WriteLine("                  }");
                writer.WriteLine("                  if(ComprobarEstado(estado_temporal) == false || error_encontrado != 0) //Comprueba que se encuentre en un estado de aceptacion luego de evaluar la palabra");
                writer.WriteLine("                  {");
                writer.WriteLine("                      resultado_cadena = false;");
                writer.WriteLine("                      break;");
                writer.WriteLine("                  }");
                writer.WriteLine("              }");
                writer.WriteLine("              return resultado_cadena;");
                writer.WriteLine("          }");

                //METODOS AUX
                writer.WriteLine("          public static bool ComprobarEstado(int estado)");
                writer.WriteLine("          {");
                writer.WriteLine("              if(Estados_Aceptacion.Contains(estado))");
                writer.WriteLine("              {");
                writer.WriteLine("                 return true;");
                writer.WriteLine("              }");
                writer.WriteLine("              return false;");
                writer.WriteLine("          }");
                writer.WriteLine("          public static void MostrarComponentesLexicos(string[] palabra)");
                writer.WriteLine("          {");
                writer.WriteLine("              for(int i=0; i < palabra.Length; i++)");
                writer.WriteLine("              {");
                writer.WriteLine("                 var tmp = palabra[i];");
                writer.WriteLine("                 if(BuscarCadena_DiccionarioActions(tmp) == false)");
                writer.WriteLine("                 {");
                writer.WriteLine("                      ImprimirTokens_ConNuevaCadena(Lexema[i], tmp);");
                writer.WriteLine("                 }");
                writer.WriteLine("              }");
                writer.WriteLine("          }");
                writer.WriteLine("          public static bool BuscarCadena_DiccionarioActions(string tmp)");
                writer.WriteLine("          {");
                writer.WriteLine("              var bandera = false;");
                writer.WriteLine("              foreach (var item in Diccionario_actions.Keys)");
                writer.WriteLine("              {");
                writer.WriteLine("                  if(tmp == Diccionario_actions[item])");
                writer.WriteLine("                  {");
                writer.WriteLine("                      Console.WriteLine($\"{tmp} = {item}\");");
                writer.WriteLine("                      bandera = true;");
                writer.WriteLine("                  }");
                writer.WriteLine("              }");
                writer.WriteLine("              return bandera;");
                writer.WriteLine("          }");
                writer.WriteLine("          public static void ImprimirTokens_ConNuevaCadena(string cadena_Seteada, string cadena)");
                writer.WriteLine("          {");
                writer.WriteLine("              cadena_Seteada = cadena_Seteada.Remove(cadena_Seteada.Length-1, 1);");
                writer.WriteLine("              var cadena_array = cadena_Seteada.Split(' ');");
                writer.WriteLine("              foreach (var item in Definicion_Tokens.Keys)");
                writer.WriteLine("              {");
                writer.WriteLine("                  var contador = 0;");
                writer.WriteLine("                  for(int i=0; i< cadena_array.Length; i++)");
                writer.WriteLine("                  {");
                writer.WriteLine("                      if(Definicion_Tokens[item].Contains(cadena_array[i]))");
                writer.WriteLine("                      {");
                writer.WriteLine("                          contador++;");
                writer.WriteLine("                      }");
                writer.WriteLine("                  }");
                writer.WriteLine("                  if(contador == cadena_array.Length) //Es ese TOKEN");
                writer.WriteLine("                  {");
                writer.WriteLine("                          var numeroToken = item.Split(' ');");
                writer.WriteLine("                          Console.WriteLine($\"{cadena} = {numeroToken[numeroToken.Length -1].Trim(' ')}\");");
                writer.WriteLine("                          break;");
                writer.WriteLine("                  }");
                writer.WriteLine("              }");
                writer.WriteLine("          }");
                //LLENAR INFORMACION
                writer.WriteLine("          public static void LlenarListaAceptados()");
                writer.WriteLine("          {");
                                                for (int i = 0; i < Estados_Aceptacion.Count; i++)
                                                {
                writer.WriteLine($"                 Estados_Aceptacion.Add({Estados_Aceptacion[i]});");
                                                }
                writer.WriteLine("          }");

                writer.WriteLine("          public static void LlenarDiccionarioActions()");
                writer.WriteLine("          {");
                foreach (var item in SintacticoA.DiccionarioActions.Keys)
                {
                    writer.WriteLine($"                 Diccionario_actions.Add(\"{item}\",\"{SintacticoA.DiccionarioActions[item]}\");");
                }
                writer.WriteLine("          }");
                writer.WriteLine("          public static void LlenarDiccionario_DefinicionTokens()");
                writer.WriteLine("          {");
                SintacticoA.ModificarDiccionarioTokenes();
                foreach (var item in SintacticoA.Definicion_Tokens.Keys)
                {
                writer.WriteLine($"                 Definicion_Tokens.Add(\"{item}\",\"{SintacticoA.Definicion_Tokens[item]}\");");
                }
                writer.WriteLine("          }");
                writer.WriteLine("          public static void Llenar_ListaErrores()");
                writer.WriteLine("          {");
                for (int i = 0; i < SintacticoA.ErrorList.Count; i++)
                {
                    writer.WriteLine($"                 Error_List.Add(\"{SintacticoA.ErrorList[i]}\");");
                }
                writer.WriteLine("          }");

                writer.WriteLine("      }");
                writer.WriteLine("}");
                writer.Close();
            }
        }
        public static string DevolverLineaIf(string keySet) 
        {
            var respuesta = SintacticoA.Definicion_SETS[keySet];
            var condicion = string.Empty;
            var bandera_comilla = false;
            if (keySet == "CHARSET")
            {
                var valor_inicial = string.Empty;
                var valor_final = string.Empty;
                for (int i = 0; i < respuesta.Length; i++)
                {
                    if (respuesta[i] == '(')
                    {
                        i++;
                        if (valor_inicial.Length == 0)
                        {
                            while (respuesta[i] != ')')
                            {
                                valor_inicial += respuesta[i];
                                i++;
                            }
                        }
                        else
                        {
                            while (respuesta[i] != ')')
                            {
                                valor_final += respuesta[i];
                                i++;
                            }
                        }
                    }
                }
                condicion = $"(temp[j] >= {valor_inicial} && temp[j] <= {valor_final})";
            }
            else
            {
                for (int i = 0; i < respuesta.Length; i++)
                {
                    if (respuesta[i] == '\'' && bandera_comilla == false)
                    {
                        if (i + 3 < respuesta.Length)
                        {
                            condicion += $"(temp[j] >= {Convert.ToByte(respuesta[i + 1])} ";
                            i += 2;
                            bandera_comilla = true;
                        }
                        else
                        {
                            condicion += $"(temp[j] == {Convert.ToByte(respuesta[i + 1])})";
                            i += 2;
                        }
                    }
                    else if (respuesta[i] == '\'' && bandera_comilla == true)
                    {
                        condicion += $"temp[j]<= {Convert.ToByte(respuesta[i + 1])})";
                        i += 2;
                        bandera_comilla = false;
                    }
                    else if (respuesta[i] == '.')
                    {
                        condicion += " && ";
                        i++;
                    }
                    else if (respuesta[i] == '+')
                    {
                        condicion += " || ";
                    }
                }
            }
            return condicion;
        }
        public static int DefinirEstado(int estadoEmisor, string transicion, int posicion_transicion) 
            {
            var lista_Estado_Temporal = Estados_Abreviados[estadoEmisor];
            var lista_deListas = new List<List<int>>();
            var nuevo_Estado = new List<int>();
            foreach (var item in Form1.diccionario_EstadoTransicion.Keys)
            {
                if (lista_Estado_Temporal.SequenceEqual(item))
                {
                    lista_deListas = Form1.diccionario_EstadoTransicion[item];
                    break;
                }
            }
            nuevo_Estado = lista_deListas[posicion_transicion];
            var resultado = 0;
            foreach (var item in Estados_Abreviados.Keys)
            {
                if (nuevo_Estado.SequenceEqual(Estados_Abreviados[item]))
                {
                    resultado = item;
                    break;
                }
            }
            return resultado;
        }
        public static void LlenarDiccionarioAbreviado() 
        {
            var numero_Estado = 0;
            foreach (var item in Form1.diccionario_EstadoTransicion.Keys)
            {
                Estados_Abreviados.Add(numero_Estado, item);
                numero_Estado++;
            }
        }
        public static void LlenarContadorTransiciones() 
        {
            foreach (var item in Form1.diccionario_EstadoTransicion.Keys)
            {
                var contador = 0;
                var Lista_Transiciones = Form1.diccionario_EstadoTransicion[item];
                for (int i = 0; i < Lista_Transiciones.Count-1; i++)
                {
                    var temp = Lista_Transiciones[i];
                    if (temp[0] != 0 )
                    {
                        contador++;
                    }
                }
                Cantidad_Transiciones.Add(contador);
            }
        }
        public static bool VerificarSiSeHace(int estado, int comprobador) 
        {
            var respuesta = true;
            var Estado = Estados_Abreviados[estado];
            var ConjuntoListas = Form1.diccionario_EstadoTransicion[Estado];
            var lista_A_analizar = ConjuntoListas[comprobador];
            if (lista_A_analizar[0] == 0)
            {
                respuesta = false;
            }
            return respuesta;
        }
        public static void LlenarListaAceptados() 
        {
            foreach (var item in Estados_Abreviados.Keys)
            {
                if (Estados_Abreviados[item].Contains(Form1.SimboloTerminal))
                {
                    Estados_Aceptacion.Add(item);
                }
            }
        }
    }
}
