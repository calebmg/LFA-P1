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

        public string regularPhrase = "(∙)";

        public void MoiAlgorithm()
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            var RutaLectura = "";
            var TBuffer = 1000;
            var texto = "";
            if(ArchivoPrueba.ShowDialog() == DialogResult.OK)
            {
                RutaLectura = ArchivoPrueba.FileName;
                using (var stream = new FileStream(RutaLectura, FileMode.Open))
                {
                    using (var Lector = new StreamReader(stream))
                    {
                        var BytesBuffer = new byte[TBuffer];
                        while (!Lector.EndOfStream)
                        {
                            texto = Lector.ReadToEnd();
                        }
                    }
                }

                //MoiAlgoithm


                lblFilePath.Text = ArchivoPrueba.FileName;
            }

        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
