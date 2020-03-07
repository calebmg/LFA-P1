using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Proyecto_LFA
{
    public partial class InitialForm : Form
    {
        public InitialForm()
        {
            InitializeComponent();
        }

        public static string regExSETS = "( *.L+. *.=. *.(('.S.')|(C.H.R.\\(.N+.\\))).(((\\..\\.)|\\+).(('.S.')|(C.H.R.\\(.N+.\\))). *)*)";
        public static string regExTOKENS = "( *.T.O.K.E.N.Z*.N+.Z*.=.Z*.((('.S.')|L+). *)+)";
        public static string regExACTIONS = "( *.N+. *.=. *.'.L+.')";
        public static string regExERROR = "( *.E.R.R.O.R. *.=. *.N+)";


        public static List<char> oper = new List<char>();
        public static List<char> stSETS = new List<char>();
        public static List<char> stTOKENS = new List<char>();
        public static List<char> stACTIONS = new List<char>();
        public static List<char> stERROR = new List<char>();


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            if(ArchivoPrueba.ShowDialog() == DialogResult.OK)
            {
                lblFilePath.Text = ArchivoPrueba.FileName;
            }

        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (lblFilePath.Text != "")
            {
                if (Implementation.EmptyFile(lblFilePath.Text) != false)
                {
                    RegEx.FillInOP(oper);

                    RegEx.GenerateST(oper, stSETS, regExSETS);
                    RegEx.GenerateST(oper, stTOKENS, regExTOKENS);
                    RegEx.GenerateST(oper, stACTIONS, regExACTIONS);
                    RegEx.GenerateST(oper, stERROR, regExERROR);


                    ExpressionTree.FillInDictionaryHierarchy(oper);


                    var treeSETS = ExpressionTree.GenerateTree(stSETS, oper, regExSETS);
                    var treeTOKENS = ExpressionTree.GenerateTree(stTOKENS, oper, regExTOKENS);
                    var treeACTIONS = ExpressionTree.GenerateTree(stACTIONS, oper, regExACTIONS);
                    var treeERROR = ExpressionTree.GenerateTree(stERROR, oper, regExERROR);

                    Implementation.ReadFile(lblFilePath.Text, treeSETS, treeTOKENS, treeACTIONS, treeERROR);
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
    }
}
