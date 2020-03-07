namespace Proyecto_LFA
{
    partial class InitialForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialForm));
            this.lblInstruction1 = new System.Windows.Forms.Label();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.ArchivoPrueba = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // lblInstruction1
            // 
            this.lblInstruction1.AutoSize = true;
            this.lblInstruction1.Location = new System.Drawing.Point(61, 52);
            this.lblInstruction1.Name = "lblInstruction1";
            this.lblInstruction1.Size = new System.Drawing.Size(183, 13);
            this.lblInstruction1.TabIndex = 0;
            this.lblInstruction1.Text = "Ingrese el archivo que desea analizar";
            this.lblInstruction1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(64, 94);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFile.TabIndex = 2;
            this.btnLoadFile.Text = "Cargar";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(268, 94);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(75, 23);
            this.btnAnalyze.TabIndex = 3;
            this.btnAnalyze.Text = "Analizar";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(61, 75);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(0, 13);
            this.lblFilePath.TabIndex = 4;
            // 
            // ArchivoPrueba
            // 
            this.ArchivoPrueba.FileName = "openFileDialog1";
            this.ArchivoPrueba.Filter = "Archivos TXT (*.txt)|*.txt";
            this.ArchivoPrueba.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // InitialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 181);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.lblInstruction1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InitialForm";
            this.Text = "Go 0.0.1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInstruction1;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.OpenFileDialog ArchivoPrueba;
    }
}

