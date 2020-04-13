using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Proyecto_LFA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBuscador_Click(object sender, EventArgs e)
        {
            OpenFileDialog examinar = new OpenFileDialog();
            if (examinar.ShowDialog() == DialogResult.OK)// si el archivo es correctamente encontrado
            {
                txbRuta.Text = examinar.FileName;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //EXPRESIONES REGULARES
        //"( *.L+. *.=. *.(('.S.')|(C.H.R.\\(.N+.\\))).(((\\..\\.)|\\+).(('.S.')|(C.H.R.\\(.N+.\\))))*)"
        //"( *.T.O.K.E.N. *.N+. *.=. *.((('.S.')|S+). *)+)"
        public static string expresionR_SETS = "( *.L+. *.=. *.(('.S.')|(C.H.R.\\(.N+.\\))).(((\\..\\.)|\\+).(('.S.')|(C.H.R.\\(.N+.\\))). *)*)";
        public static string expresionR_TOKENS = "( *.T.O.K.E.N.Z*.N+.Z*.=.Z*.((('.S.')|L+). *)+)";
        public static string expresionR_ACTIONS = "( *.N+. *.=. *.'.L+.')";
        public static string expresionR_ERROR = "( *.E.R.R.O.R. *.=. *.N+)";
        //LISTAS PARA ALMACENAR LOS SIMBOLOS TERMINALES Y OPERADORES
        public static List<char> operadores = new List<char>();
        public static List<char> st_SETS = new List<char>();
        public static List<char> st_TOKENS = new List<char>();
        public static List<char> st_ACTIONS = new List<char>();
        public static List<char> st_ERROR = new List<char>();
        public static List<string> st_SINTACTICO = new List<string>();

        //DataTable Estaticos 
        public static DataTable DataTableFLN = new DataTable();
        public static DataTable DataTableFOLLOW = new DataTable();
        public static DataTable DataTableET = new DataTable();

        //ARBOL
        public static Nodo_Generico arbol_Sintactico = new Nodo_Generico("-");
        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            if (txbRuta.Text != "")// si no hay ruta del archivo
            {
                if (Prueba.ArchivoVacio(txbRuta.Text) != false) //el archivo no esta vacio
                {
                    //GENERAR LISTA DE OPERADORES 
                    Expresiones_Regulares.LlenarOP(operadores);

                    //GENERACION DE LISTAS ST
                    Expresiones_Regulares.Generar_ST(operadores, st_SETS, expresionR_SETS);
                    Expresiones_Regulares.Generar_ST(operadores, st_TOKENS, expresionR_TOKENS);
                    Expresiones_Regulares.Generar_ST(operadores, st_ACTIONS, expresionR_ACTIONS);
                    Expresiones_Regulares.Generar_ST(operadores, st_ERROR, expresionR_ERROR);

                    //GENERAR DICCIONARIO SOBRE VALORES DE PRECEDENCIA
                    Arbol_Expresiones.LlenarDiccionarioPrecedencia(operadores);
                    Helpers.LlenarDiccionarioPrecedencia(operadores);
                    //GENERACION DE ARBOLES DE EXPRESION
                    var arbol_SETS = Arbol_Expresiones.GenerarArbol(st_SETS, operadores, expresionR_SETS);
                    var arbol_TOKENS = Arbol_Expresiones.GenerarArbol(st_TOKENS, operadores, expresionR_TOKENS);
                    var arbol_ACTIONS = Arbol_Expresiones.GenerarArbol(st_ACTIONS, operadores, expresionR_ACTIONS);
                    var arbol_ERROR = Arbol_Expresiones.GenerarArbol(st_ERROR, operadores, expresionR_ERROR);

                    var inicioTokens = 0;
                    var finalTokens = 0;

                    //ANALIZADOR LEXICO
                    Prueba.Analizador_Lexico(txbRuta.Text, arbol_SETS, arbol_TOKENS, arbol_ACTIONS, arbol_ERROR, ref inicioTokens, ref finalTokens);//LOGICA PARA LECTURA DEL ARCHIVO

                    if (MensajeError.error_Encontrado == false)
                    {
                        //------------------------------> ANALIZADOR SINTACTICO
                        //var txbER = string.Empty;
                        var error_Sintactico = false;
                        //1.Tokenizar expresion
                        var expresion_TokensS = SintacticoA.Tokenizar(Prueba.gramatica, inicioTokens, finalTokens, ref error_Sintactico);
                        if (error_Sintactico != true)
                        {
                            Expresiones_Regulares.ST_Sintactico(st_SINTACTICO, operadores, expresion_TokensS);
                            //2. Generar arbol de expresion
                            arbol_Sintactico = Helpers.GenerarArbol(st_SINTACTICO, operadores, expresion_TokensS);
                            var contador_Hojas = 1;
                            var tabla_follow = new Dictionary<int, List<int>>();
                            var diccionario_hojas = new Dictionary<int, string>();

                            //3. Enumerar hojas; obtener first, last & nullable
                            Helpers.FLN(arbol_Sintactico, ref contador_Hojas, ref tabla_follow, ref diccionario_hojas);
                            //4. Generar tabla de follows
                            Helpers.Generar_Follow(arbol_Sintactico, ref tabla_follow);
                            //5.Generar tabla de transiciones y estados
                            var diccionario_EstadoTransicion = Helpers.GenerarEstados_Transiciones(st_SINTACTICO, arbol_Sintactico.first, diccionario_hojas, tabla_follow);

                            //6.Mostrar todo en un data grid View
                            txbExpresion.Text = expresion_TokensS;
                            lbl_MostrarR.Text = "Formato correcto: Tablas de First, Last, Nullable, Follow, Estados y Transiciones de acuerdo a la expresión regular ingresada.";

                            //TABLA 1: FLN
                            DataTableFLN.Columns.Add("Simbolo");
                            DataTableFLN.Columns.Add("First");
                            DataTableFLN.Columns.Add("Last");
                            DataTableFLN.Columns.Add("Nullable");
                            Helpers.GenerarFilas_FLN(arbol_Sintactico);
                            dataGV_FLN.DataSource = DataTableFLN;

                            //TABLA 2: FOLLOW
                            DataTableFOLLOW.Columns.Add("Simbolo");
                            DataTableFOLLOW.Columns.Add("Follow");
                            Helpers.GenerarFilas_FOLLOW(tabla_follow);
                            dataGVFollow.DataSource = DataTableFOLLOW;

                            //TABLA 3: ESTADOS Y TRANSICIONES
                            DataTableET.Columns.Add("Estado");
                            for (int i = 0; i < st_SINTACTICO.Count; i++)
                            {
                                DataTableET.Columns.Add(st_SINTACTICO[i]);
                            }
                            Helpers.GenerarFilas_ET(diccionario_EstadoTransicion, st_SINTACTICO);
                            dataGVET.DataSource = DataTableET;

                            //EXTRA----> DIBUJAR EL ARBOL
                            //FormArbol.DibujarArbol(arbol_Sintactico, 0);

                        }
                    }

                }
                else
                {
                    MessageBox.Show("Archivo Vacio");
                }
            }
            else
            {
                MessageBox.Show("Por favor, primero seleccione un archivo");
            }
        }
        public static Thread th;
        private void btnArbol_Click(object sender, EventArgs e)
        {
            var nuevoForm = new FormArbol();
            nuevoForm.Show();
        }
    }
}
