using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Proyecto_LFA
{
    public class MensajeError
    {
        public static string mensaje_Error;
        public static bool error_Encontrado;
    }
    public class Prueba
    {
        public static char[] reservada_SETS = { 'S', 'E', 'T', 'S' };//OPCIONAL QUE EXISTA
        public static char[] reservada_TOKENS = { 'T', 'O', 'K', 'E', 'N', 'S' };//TIENE QUE EXISTIR
        public static char[] reservada_ACTIONS = { 'A', 'C', 'T', 'I', 'O', 'N', 'S' };//TIENE QUE EXISTIR
        public static char[] reservada_RESERVADAS = { 'R', 'E', 'S', 'E', 'R', 'V', 'A', 'D', 'A', 'S', '(', ')' };//TIENE QUE EXISTIR
        public static List<string> gramatica = new List<string>();//LISTA QUE ALMACENA EL ARCHIVO
        public static List<string> ListaOriginal = new List<string>();//PARA LINEA DE ERROR
        public static bool ArchivoVacio(string direccion)// verifica que el aechivo este vacio
        {
            FileInfo propiedades = new FileInfo(direccion);
            if (propiedades.Length > 0)
            {
                return true;
            }
            return false;
        }
        public static bool SoloEspacios(string linea) 
        {
            var decide = false;
            if (decide = linea.All(x => x == ' ') == true)
            {
                return true;
            }
            return false;
        }
        public static bool SoloTabs(string linea)
        {
            var decide = false;
            if (decide = linea.All(x => x == '\t') == true)
            {
                return true;
            }
            return false;
        }
        public static void Analizador_Lexico(string rutaFile, Nodo arbol_Sets, Nodo arbol_Tokens, Nodo arbol_Actions, Nodo arbol_Error, ref int inicioTokens, ref int finalTokens)
        {
            //variables auxiliares
            var line = string.Empty;
            var no_columnaError = 0;
            var primer_Caracter = new char();
            //LEO EL ARCHIVO
            using (StreamReader reader = new StreamReader(rutaFile))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    ListaOriginal.Add(line);
                    if (string.IsNullOrEmpty(line) == false) 
                    {
                        if (SoloEspacios(line) == false && SoloTabs(line)==false)
                        {
                            gramatica.Add(line.Trim('\t'));
                        }
                    }
                }
                reader.Close();
            }
            var primer_Linea = gramatica[0].Trim(' ').ToCharArray();
            //ANALIZO LA PRIMER PALABRA---> que SETS o TOKENS esten correctamente escritas
            switch (primer_Linea[0])
            {
                case 'S':
                    if (Analizador_Reservada(reservada_SETS, primer_Linea, 0, ref no_columnaError, 1) != true)
                    { //si encuentra el error, me saca
                        MostrarError(MensajeError.mensaje_Error);
                        MensajeError.error_Encontrado = true;
                    }
                    primer_Caracter = 'S';
                    break;
                case 'T':
                    if (Analizador_Reservada(reservada_TOKENS, primer_Linea, 0, ref no_columnaError, 1) != true)
                    {
                        MostrarError(MensajeError.mensaje_Error);
                        MensajeError.error_Encontrado = true;
                    }
                    primer_Caracter = 'T';
                    break;
                default:
                    MensajeError.mensaje_Error = $"ERROR AL INICIO DEL ARCHIVO: NO VENIA LA DEFINICION CORRECTA DE TOKENS.";
                    MostrarError(MensajeError.mensaje_Error);
                    MensajeError.error_Encontrado = true;
                    break;
            }
            var i = 1;
            var inicioSets = i;
            var finalSets = i;
            if (MensajeError.error_Encontrado == false)//NO EXISTIO ERROR AL INICIO
            {
                var inicio_Gramatica = i;
                if (primer_Caracter == 'S')//HAY SETS-->
                {
                    //-------------------------------------------------LOGICA CUANDO INICIA CON SETS---------------------------------------------
                    while (gramatica[i].Contains("TOKENS") == false && MensajeError.error_Encontrado == false )//gramatica[i] == LINEA DEL ARCHIVO
                    {
                        var prueba = gramatica[i].Contains("TOKENS");
                        var filtro = gramatica[i].ToCharArray();
                        if (filtro[filtro.Length - 1] == '\'' || filtro[filtro.Length - 1] == ')' || filtro[filtro.Length - 1] == ' ')
                        {
                            no_columnaError = 0;
                            CompararArbol(arbol_Sets, gramatica[i], ref no_columnaError, Form1.st_SETS, i);
                            i++;
                            finalSets++;
                        }
                        else
                        {
                            MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, COLUMNA {no_columnaError+1}: DEFINICION INCOMPLETA O NO TERMINO CORRECTAMENTE LA ORACION";
                            MensajeError.error_Encontrado = true;
                            MostrarError(MensajeError.mensaje_Error);
                        }
                    }
                    if (i - inicio_Gramatica == 0 && MensajeError.error_Encontrado == false)//ERROR. NO VINO NINGUN SET
                    {
                        MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, COLUMNA {1}: NO VENIA NINGUN SET DEFINIDO.";
                        MensajeError.error_Encontrado = true;
                        MostrarError(MensajeError.mensaje_Error);
                    }
                    inicio_Gramatica = i;
                }
                SintacticoA.ObtenerListaSETS(gramatica, inicioSets, finalSets);
                //---------------INICIO ANALISIS DE TOKENS
                var LineaTokensLeida = false;
                inicioTokens = i;
                finalTokens = i;
                while (gramatica[i].Contains("ACTIONS") == false && MensajeError.error_Encontrado == false)
                {
                    if (LineaTokensLeida == false && primer_Caracter == 'S')//SI EL ARCHIVO INICIA CON LA SECCION DE SETS, SE DEBE DE ANALIZAR LA PALABRA RESERVADA TOKENS
                    {
                        if (Analizador_Reservada(reservada_TOKENS, gramatica[i].ToCharArray(), 0, ref no_columnaError, 1) != true)
                        {
                            MostrarError(MensajeError.mensaje_Error);
                            MensajeError.error_Encontrado = true;
                        }
                        LineaTokensLeida = true;
                        i++;
                        inicioTokens++;
                        finalTokens++;
                    }
                    else
                    {
                        var filtro = gramatica[i].ToCharArray();
                        var cerrar = false;
                        if (gramatica[i].Contains('(') && gramatica[i].Contains(')')== false)
                        {
                            cerrar = true;
                        }
                        if (filtro[filtro.Length - 1] == '\'' || filtro[filtro.Length - 1] == '}' || filtro[filtro.Length - 1] == ' ' || filtro[filtro.Length - 1] == '*' && cerrar == false)
                        {
                            no_columnaError = 0;
                            CompararArbol(arbol_Tokens, gramatica[i], ref no_columnaError, Form1.st_TOKENS, i);
                            i++;
                            finalTokens++;
                        }
                        else
                        {
                            if (gramatica[i].Contains('(') && !gramatica[i].Contains(')'))
                            {
                                MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}: NO CERRO EL PARENTESIS EN EL TOKEN";
                                MensajeError.error_Encontrado = true;
                                MostrarError(MensajeError.mensaje_Error);
                            }
                            else
                            {
                                MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, APROXIMADAMENTE EN LA COLUMNA {filtro.Length - 1}: DEFINICION INCOMPLETA";
                                MensajeError.error_Encontrado = true;
                                MostrarError(MensajeError.mensaje_Error);
                            }
                        }
                    }
                }
                //FINAL ANALISIS DE TOKEN
                if (i - inicio_Gramatica == 0 && MensajeError.error_Encontrado == false)//ERROR. NO VINO NINGUN TOKEN
                {
                    MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, COLUMNA {no_columnaError}: NO VENIA NINGUN TOKEN DEFINIDO DEFINIDO.";
                    MensajeError.error_Encontrado = true;
                    MostrarError(MensajeError.mensaje_Error);
                }
                //INICIO - ANALISIS RESERVADAS
                if (Analizador_Reservada(reservada_ACTIONS, gramatica[i].ToCharArray(), 0, ref no_columnaError, i) == true && MensajeError.error_Encontrado == false)
                {
                    no_columnaError = 0;
                    i++;
                    if (Analizador_Reservada(reservada_RESERVADAS, gramatica[i].ToCharArray(), 0, ref no_columnaError, i) == true && MensajeError.error_Encontrado == false)
                    {
                        i++;
                        //{
                        if (gramatica[i] == "{")
                        {
                            i++;
                            var contador_Actions = i;
                            while (gramatica[i].Contains("}") == false && i < gramatica.Count && MensajeError.error_Encontrado == false)
                            {
                                no_columnaError = 0;
                                var filtro = gramatica[i].ToCharArray();
                                if (filtro[filtro.Length - 1] == '\'')
                                {
                                    CompararArbol(arbol_Actions, gramatica[i], ref no_columnaError, Form1.st_ACTIONS, i);
                                    i++;
                                }
                                else
                                {
                                    MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, APROXIMADAMENTE COLUMNA {filtro.Length - 1}: DEFINICION INCOMPLETA";
                                    MensajeError.error_Encontrado = true;
                                    MostrarError(MensajeError.mensaje_Error);
                                }
                            }
                            if (i - contador_Actions == 0 || i == gramatica.Count && MensajeError.error_Encontrado == false)
                            {
                                MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, COLUMNA {1}: NO VENIA LA LLAVE FINAL";
                                MensajeError.error_Encontrado = true;
                                MostrarError(MensajeError.mensaje_Error);
                            }
                            if (gramatica[i] == "}" && i < gramatica.Count)
                            {
                                i++;
                            }
                            else
                            {
                                MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, APROXIMADAMENTE COLUMNA {1}: NO VENIA LA LLAVE FINAL DE LA FUNCION";
                                MensajeError.error_Encontrado = true;
                                MostrarError(MensajeError.mensaje_Error);
                            }
                        }
                    }
                    else
                    {
                        MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, APROXIMADAMENTE COLUMNA {no_columnaError}: NO VENIA LA PALABRA RESERVADAS ACOMPAÑANDO A ACTIONS.";
                        MensajeError.error_Encontrado = true;
                        MostrarError(MensajeError.mensaje_Error);
                    }
                }
                else
                {
                    MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, APROXIMADAMENTE COLUMNA {no_columnaError}: NO VENIA LA PALABRA ACTIONS O ESCRITA INCORRECTAMENTE.";
                    MensajeError.error_Encontrado = true;
                    MostrarError(MensajeError.mensaje_Error);
                }
                //PARA MAS FUNCIONES
                while (gramatica[i].Contains("ERROR") == false && i < gramatica.Count && MensajeError.error_Encontrado == false)
                {
                    if (gramatica[i].Contains('(') && gramatica[i].Contains(')'))
                    {
                        i++;
                        if (gramatica[i] == "{")
                        {
                            i++;
                            var contador_Actions = i;
                            while (gramatica[i].Contains("}") == false && i < gramatica.Count && MensajeError.error_Encontrado == false)
                            {
                                no_columnaError = 0;
                                var filtro = gramatica[i].ToCharArray();
                                if (filtro[filtro.Length - 1] == '\'')
                                {
                                    CompararArbol(arbol_Actions, gramatica[i], ref no_columnaError, Form1.st_ACTIONS, i);
                                    i++;
                                }
                                else
                                {
                                    MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, APROXIMADAMENTE COLUMNA {filtro.Length - 1}: DEFINICION INCOMPLETA";
                                    MensajeError.error_Encontrado = true;
                                    MostrarError(MensajeError.mensaje_Error);
                                }
                            }
                            if (i - contador_Actions == 0 || i == gramatica.Count && MensajeError.error_Encontrado == false)
                            {
                                MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, COLUMNA {1}: NO VENIA LA LLAVE FINAL";
                                MensajeError.error_Encontrado = true;
                                MostrarError(MensajeError.mensaje_Error);
                            }
                            if (gramatica[i] == "}" && i < gramatica.Count)
                            {
                                i++;
                            }
                            else
                            {
                                MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, APROXIMADAMENTE COLUMNA {1}: NO VENIA LA LLAVE FINAL DE LA FUNCION";
                                MensajeError.error_Encontrado = true;
                                MostrarError(MensajeError.mensaje_Error);
                            }
                        }
                        else
                        {
                            MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, APROXIMADAMENTE COLUMNA {1}: NO VENIA LA LLAVE INICIAL DE LA FUNCION";
                            MensajeError.error_Encontrado = true;
                            MostrarError(MensajeError.mensaje_Error);
                        }
                    }
                    else
                    {
                        MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, COLUMNA {gramatica[i].Length}: DEFINICION INCOMPLETA DE UNA FUNCION";
                        MensajeError.error_Encontrado = true;
                        MostrarError(MensajeError.mensaje_Error);
                    }
                }
                //ESTO ES PARA LOS ERRORES
                if (i < gramatica.Count)
                {
                    while (i < gramatica.Count && MensajeError.error_Encontrado == false)
                    {
                        var filtro = gramatica[i].ToCharArray();
                        if (char.IsNumber(filtro[filtro.Length - 1]))
                        {
                            no_columnaError = 0;
                            CompararArbol(arbol_Error, gramatica[i], ref no_columnaError, Form1.st_ERROR, i);
                            i++;
                        }
                        else
                        {
                            MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(gramatica[i])}, APROXIMADAMENTE COLUMNA {filtro.Length - 1}: PARA LOS ERRORES, SOLO DEBEN DE VENIR NUMEROS";
                            MensajeError.error_Encontrado = true;
                            MostrarError(MensajeError.mensaje_Error);
                        }
                    }
                }
            }
            //if (MensajeError.error_Encontrado == false && i== gramatica.Count)
            //{
            //    ArchivoCorrecto();
            //}
        }
        public static bool Analizador_Reservada(char[] reservada, char[] linea, int contador, ref int no_columnaError, int linea_Error)
        {
            if (contador < reservada.Length)
            {
                no_columnaError++;
                if (contador < linea.Length)
                {
                    if (linea[contador] == reservada[contador])
                    {
                        return Analizador_Reservada(reservada, linea, contador + 1, ref no_columnaError, linea_Error);
                    }
                }
            }
            if (contador == reservada.Length)
            {
                return true;
            }
            MensajeError.mensaje_Error = $"ERROR EN LA LINEA {linea_Error}, APROXIMADAMENTE COLUMNA {no_columnaError}: NO VENIA {reservada[contador]}.";
            return false;
        }

        static bool bandera_IzquierdaOR = false;
        static bool bandera_DerechaOR = false;
        static List<string> ids = new List<string>();
        public static void CompararArbol(Nodo arbol, string linea, ref int columna, List<char> st, int linea_Error)
        {
            //VERIFICAR CASOS OR, MAS GRANDE Y POR GRANDE
            //INORDEN
            if (arbol != null && MensajeError.error_Encontrado == false)
            {
                //----------------------------------------INICIO DE RECORRIDO
                CompararArbol(arbol.hijo_izquierdo, linea, ref columna, st, linea_Error);

                //----------------------------------------ANALISIS DEL NODO------------------------------------

                //CASO OR 1--> QUE EL LADO IZQUIERDO ERA EL BUENO
                if (arbol.id == '|' && NodoRecorrido(arbol.hijo_izquierdo) == true)
                {
                    arbol.recorrido = true;
                    bandera_IzquierdaOR = true;
                }
                //CASO OR 2--> QUE EL LADO DERECHO ERA EL BUENO
                else if (arbol.id == '|' && bandera_DerechaOR == true)
                {
                    bandera_DerechaOR = false;
                }
                //CASO OR 3--> NINGUNO VENIA BUENO
                else if (arbol.id == '|' && NodoRecorrido(arbol.hijo_izquierdo) == false && NodoRecorrido(arbol.hijo_derecho) == false)
                {
                    MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(linea)}, APROXIMADAMENTE COLUMNA {columna}: NO VENIA IDENTIFICADOR.";
                    MensajeError.error_Encontrado = true;
                    MostrarError(MensajeError.mensaje_Error);
                }
                if (MensajeError.error_Encontrado == false && bandera_IzquierdaOR == false && bandera_DerechaOR == false && columna < linea.Length)
                {
                    if (st.Contains(arbol.id) && EsHoja(arbol) != false)//SIMBOLO TERMINAL Y HOJA--> LOS QUE HAY QUE ANALIZAR
                    {
                        if (arbol.padre.id == '*')
                        {
                            if (arbol.id == 'Z')
                            {
                                while (linea[columna] == 9 || linea[columna] == 32)
                                {
                                    columna++;
                                    if (columna == linea.Length)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                while (linea[columna] == arbol.id)//AVANZAR EN LOS ESPACIOS
                                {
                                    columna++;
                                    if (columna == linea.Length)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else if (arbol.padre.id == '+')
                        {
                            //BUSCAR EL SIMBOLO TERMINAL (ARBOL.ID) EN LA LISTA
                            var verificador = columna;
                            switch (arbol.id)
                            {
                                case 'L':
                                    if (bandera_IzquierdaOR == false)
                                    {
                                        while ((char.IsUpper(linea[columna]) || linea[columna] == '*' || linea[columna] == '|' || linea[columna] == '(' || linea[columna] == '{' || linea[columna] == ')' || linea[columna] == '}') && MensajeError.error_Encontrado == false)
                                        {
                                            switch (linea[columna])
                                            {
                                                case '*':
                                                    var item = string.Empty;
                                                    for (int i = verificador; i < columna; i++)
                                                    {
                                                        item += linea[verificador];
                                                    }
                                                    ids.Add(item);
                                                    break;
                                                case '|':
                                                    if (columna < linea.Length)
                                                    {
                                                        var contador_S = 0;
                                                        var punto = columna;
                                                        while (punto < linea.Length)
                                                        {
                                                            if (char.IsUpper(linea[punto]))
                                                            {
                                                                contador_S++;
                                                            }
                                                            punto++;
                                                        }
                                                        if (contador_S == 0)
                                                        {
                                                            MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(linea)}, APROXIMADAMENTE COLUMNA {columna}: NO VENIA NADA DESPUES DEL |.";
                                                            MensajeError.error_Encontrado = true;
                                                            MostrarError(MensajeError.mensaje_Error);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(linea)}, APROXIMADAMENTE COLUMNA {columna}: NO VENIA NADA DESPUES DEL |.";
                                                        MensajeError.error_Encontrado = true;
                                                        MostrarError(MensajeError.mensaje_Error);
                                                    }
                                                    break;
                                                case '(':
                                                    if (linea.Contains(')') == false)
                                                    {
                                                        MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(linea)}, APROXIMADAMENTE COLUMNA {columna}: LA EXPRESION NO CERRO PARENTESIS.";
                                                        MensajeError.error_Encontrado = true;
                                                        MostrarError(MensajeError.mensaje_Error);
                                                    }
                                                    break;
                                                case '{':
                                                    if (linea.Contains('}') == false)
                                                    {
                                                        MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(linea)}, APROXIMADAMENTE COLUMNA {columna}: LA EXPRESION NO CERRO LLAVES.";
                                                        MensajeError.error_Encontrado = true;
                                                        MostrarError(MensajeError.mensaje_Error);
                                                    }
                                                    break;
                                            }
                                            columna++;
                                           if (columna == linea.Length)
                                           {
                                             break;
                                           }
                                        }
                                        if (columna - verificador == 0)
                                        {
                                            MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(linea)}, APROXIMADAMENTE COLUMNA {columna+1}: NO VENIA IDENTIFICADOR.";
                                            MensajeError.error_Encontrado = true;
                                            MostrarError(MensajeError.mensaje_Error);
                                        }
                                    }
                                    break;
                                case 'N':
                                    verificador = columna;
                                    while (char.IsNumber(linea[columna]))
                                    {
                                        columna++;
                                        if (columna == linea.Length)
                                        {
                                            break;
                                        }
                                        //IF BANDERA_ERROR == TRUE ---> DEVOLVER ERROR
                                    }
                                    if (columna - verificador == 0)
                                    {
                                        MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(linea)}, APROXIMADAMENTE COLUMNA {columna+1}: NO VENIA NUMERO.";
                                        MensajeError.error_Encontrado = true;
                                        MostrarError(MensajeError.mensaje_Error);
                                    }
                                    break;
                                case ' ':
                                    while (linea[columna] == ' ')
                                    {
                                        columna++;
                                        if (columna == linea.Length)
                                        {
                                            break;
                                        }
                                    }
                                    if (columna - verificador == 0)
                                    {
                                        MensajeError.mensaje_Error = $"ERROR EN LA LINEA {DevolverLineaError(linea)}, APROXIMADAMENTE COLUMNA {columna}: NO VENIA ESPACIOS.";
                                        MensajeError.error_Encontrado = true;
                                        MostrarError(MensajeError.mensaje_Error);
                                    }
                                    break;
                                case 'S':
                                    var parentesis_A = false;
                                    var parentesis_C = false;
                                    while (linea[columna] >= 32 && linea[columna] < 255 && MensajeError.error_Encontrado == false)
                                    {
                                        if (char.IsLetter(linea[columna]) == true && char.IsUpper(linea[columna]) == false)
                                        {
                                            MensajeError.mensaje_Error = $"ERROR EN LA LINEA APROXIMADAMENTE {DevolverLineaError(linea)}, APROXIMADAMENTE COLUMNA {columna}: VINO MINUSCULAS.";
                                            MensajeError.error_Encontrado = true;
                                            MostrarError(MensajeError.mensaje_Error);
                                        }
                                        columna++;
                                        if (columna == linea.Length)
                                        {
                                            break;
                                        }
                                        if (linea[columna]== '(')
                                        {
                                            parentesis_A = true;
                                        }
                                        if (linea[columna] == ')')
                                        {
                                            parentesis_A = true;
                                        }
                                    }
                                    if (columna - verificador == 0 && bandera_IzquierdaOR == false && bandera_DerechaOR == false)
                                    {
                                        MensajeError.mensaje_Error = $"ERROR EN LA LINEA APROXIMADAMENTE {DevolverLineaError(linea)}, APROXIMADAMENTE COLUMNA {columna+1}: NO VENIA UN NINGUN TERMINAL O SIMBOLO.";
                                        MensajeError.error_Encontrado = true;
                                        MostrarError(MensajeError.mensaje_Error);
                                    }
                                    break;
                            }
                            //YA TENIENDO EL SIMBOLO TERMINAL--> HAGO UN WHILE(!)
                        }
                        else if (arbol.padre.id == '.')
                        {
                            var verificador = columna;
                            var contador_Simbolos = 0;
                            switch (arbol.id)
                            {
                                case 'S':
                                    if(linea[columna] >= 32 && linea[columna] < 255)
                                    //if ((char.IsSymbol(linea[columna]) || char.IsLetterOrDigit(linea[columna])) && linea[columna + 1] != 'H')
                                    {
                                        columna++;
                                        contador_Simbolos++;
                                    }
                                    break;
                            }
                            if (columna - verificador == 0)
                            {
                                //SE HACE POR SI NO AVANZO--> NO ERA S O C
                                // PARA CARACTERES COMO: =, ', C, H, R, (, ), ., +, E, R, O, T, K, N, +
                                if (linea[columna] == arbol.id)
                                {
                                    columna++;
                                }
                                
                                //PARA CUANDO EL IZQUIERDO ESTE MALO--> LEVANTAR BANDERA DERECHA
                                else if (linea[columna] != arbol.id && VerificarPadre(arbol, '|') == true) //&& arbol.padre.hijo_izquierdo.recorrido == false)
                                {
                                    bandera_DerechaOR = true;
                                }
                                else
                                {
                                    MensajeError.mensaje_Error = $"ERROR EN LA LINEA APROXIMADAMENTE{DevolverLineaError(linea)}, APROXIMADAMENTE COLUMNA {columna}: NO VENIA UN {arbol.id}";
                                    MensajeError.error_Encontrado = true;
                                    MostrarError(MensajeError.mensaje_Error);
                                }
                            }
                        }
                        else if (arbol.padre.id == '|')
                        {
                            if (linea[columna] == arbol.id)
                            {
                                columna++;
                            }
                        }
                    }
                    //CASO +GRANDE OR *GRANDE
                    if ((arbol.id == '*' || arbol.id == '+') && arbol.padre.padre == null && columna < linea.Length)
                    {
                        DesactivarRecorrido(arbol);
                        CompararArbol(arbol, linea, ref columna, st, linea_Error);
                    }
                    arbol.recorrido = true;
                }
                //---------------------------------FIN DEL ANALISIS----------------------------
                CompararArbol(arbol.hijo_derecho, linea, ref columna, st, linea_Error);////FIN DE RECORRIDO
                //PARA BAJAR LAS BANDERAS
                if (arbol.id == '|' && (NodoRecorrido(arbol.hijo_izquierdo) == true || NodoRecorrido(arbol.hijo_derecho) == true))
                {
                    bandera_DerechaOR = false;
                    bandera_IzquierdaOR = false;
                    
                }
            }
        }
        public static bool EsHoja(Nodo nodo) //SI SE UTILIZA
        {
            if (nodo.hijo_derecho == null && nodo.hijo_izquierdo == null)
            {
                return true;
            }
            return false;
        }

        static bool bandera_PADRE = false;
        public static bool VerificarPadre(Nodo nodo, char buscado) //SI SE UTILIZA
        {
            while (nodo.padre != null)
            {
                if (nodo.padre.id == buscado)
                {
                    bandera_PADRE = true;
                }
                return VerificarPadre(nodo.padre, buscado);
            }
            return bandera_PADRE;
        }

        public static bool NodoRecorrido(Nodo nodo) //SI SE UTILIZA 
        {
            if (nodo.recorrido == true)
            {
                return true;
            }
            return false;
        }
        
        //EN EL CASO DE QUE SE TENGA QUE REPETIR UNA GRAN EXPRESION DEBIDO A QUE SU PAPA ERA +|*
        public static void DesactivarRecorrido(Nodo nodo) 
        {
            if (nodo!= null)
            {
                DesactivarRecorrido(nodo.hijo_izquierdo);
                nodo.recorrido = false;
                DesactivarRecorrido(nodo.hijo_derecho);
            }
        }

        public static int DevolverLineaError(string linea) 
        {
            var contador = 1;
            foreach (var item in ListaOriginal)
            {
                if (linea == item)
                {
                    return contador;
                }
                contador++;
            }
            return contador;
        }
        
        //POSIBLES RESULTADOS
        //public static void ArchivoCorrecto()
        //{
        //    MessageBox.Show("Formato correcto. Tabla de first, last, follow ");
        //}
        public static void MostrarError(string mensaje) //CASO NO SE CUMPLIO LA GRAMATICA
        {
            MessageBox.Show(mensaje);
        }
    }
}
