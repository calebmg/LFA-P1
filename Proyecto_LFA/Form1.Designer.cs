namespace Proyecto_LFA
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBuscador = new System.Windows.Forms.Button();
            this.txbRuta = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAnalizar = new System.Windows.Forms.Button();
            this.txbExpresion = new System.Windows.Forms.TextBox();
            this.lbl_TituloER = new System.Windows.Forms.Label();
            this.lbl_MostrarR = new System.Windows.Forms.Label();
            this.lblTFLN = new System.Windows.Forms.Label();
            this.lblTFollow = new System.Windows.Forms.Label();
            this.lblTET = new System.Windows.Forms.Label();
            this.dataGV_FLN = new System.Windows.Forms.DataGridView();
            this.dataGVFollow = new System.Windows.Forms.DataGridView();
            this.dataGVET = new System.Windows.Forms.DataGridView();
            this.btnArbol = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV_FLN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVFollow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVET)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBuscador
            // 
            this.btnBuscador.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBuscador.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscador.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBuscador.Location = new System.Drawing.Point(9, 10);
            this.btnBuscador.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBuscador.Name = "btnBuscador";
            this.btnBuscador.Size = new System.Drawing.Size(128, 26);
            this.btnBuscador.TabIndex = 0;
            this.btnBuscador.Text = "Seleccionar Archivo";
            this.btnBuscador.UseVisualStyleBackColor = false;
            this.btnBuscador.Click += new System.EventHandler(this.btnBuscador_Click);
            // 
            // txbRuta
            // 
            this.txbRuta.Location = new System.Drawing.Point(170, 15);
            this.txbRuta.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txbRuta.Name = "txbRuta";
            this.txbRuta.Size = new System.Drawing.Size(360, 20);
            this.txbRuta.TabIndex = 1;
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(922, 17);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(56, 29);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAnalizar
            // 
            this.btnAnalizar.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAnalizar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalizar.Location = new System.Drawing.Point(9, 53);
            this.btnAnalizar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAnalizar.Name = "btnAnalizar";
            this.btnAnalizar.Size = new System.Drawing.Size(141, 34);
            this.btnAnalizar.TabIndex = 4;
            this.btnAnalizar.Text = "Analizar Archivo";
            this.btnAnalizar.UseVisualStyleBackColor = false;
            this.btnAnalizar.Click += new System.EventHandler(this.btnAnalizar_Click);
            // 
            // txbExpresion
            // 
            this.txbExpresion.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txbExpresion.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbExpresion.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txbExpresion.Location = new System.Drawing.Point(9, 128);
            this.txbExpresion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txbExpresion.Name = "txbExpresion";
            this.txbExpresion.Size = new System.Drawing.Size(1060, 26);
            this.txbExpresion.TabIndex = 5;
            // 
            // lbl_TituloER
            // 
            this.lbl_TituloER.AutoSize = true;
            this.lbl_TituloER.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TituloER.Location = new System.Drawing.Point(242, 98);
            this.lbl_TituloER.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_TituloER.Name = "lbl_TituloER";
            this.lbl_TituloER.Size = new System.Drawing.Size(223, 18);
            this.lbl_TituloER.TabIndex = 6;
            this.lbl_TituloER.Text = "Expresion regular del archivo";
            // 
            // lbl_MostrarR
            // 
            this.lbl_MostrarR.AutoSize = true;
            this.lbl_MostrarR.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MostrarR.ForeColor = System.Drawing.Color.Black;
            this.lbl_MostrarR.Location = new System.Drawing.Point(167, 70);
            this.lbl_MostrarR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_MostrarR.Name = "lbl_MostrarR";
            this.lbl_MostrarR.Size = new System.Drawing.Size(0, 18);
            this.lbl_MostrarR.TabIndex = 7;
            // 
            // lblTFLN
            // 
            this.lblTFLN.AutoSize = true;
            this.lblTFLN.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTFLN.Location = new System.Drawing.Point(10, 167);
            this.lblTFLN.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTFLN.Name = "lblTFLN";
            this.lblTFLN.Size = new System.Drawing.Size(199, 18);
            this.lblTFLN.TabIndex = 8;
            this.lblTFLN.Text = "Tabla 1: First, Last, Nullable";
            // 
            // lblTFollow
            // 
            this.lblTFollow.AutoSize = true;
            this.lblTFollow.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTFollow.Location = new System.Drawing.Point(494, 167);
            this.lblTFollow.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTFollow.Name = "lblTFollow";
            this.lblTFollow.Size = new System.Drawing.Size(115, 18);
            this.lblTFollow.TabIndex = 9;
            this.lblTFollow.Text = "Tabla 2: Follow";
            // 
            // lblTET
            // 
            this.lblTET.AutoSize = true;
            this.lblTET.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTET.Location = new System.Drawing.Point(764, 167);
            this.lblTET.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTET.Name = "lblTET";
            this.lblTET.Size = new System.Drawing.Size(226, 18);
            this.lblTET.TabIndex = 10;
            this.lblTET.Text = "Tabla 3: Estados y transiciones";
            // 
            // dataGV_FLN
            // 
            this.dataGV_FLN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV_FLN.Location = new System.Drawing.Point(13, 189);
            this.dataGV_FLN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGV_FLN.Name = "dataGV_FLN";
            this.dataGV_FLN.RowHeadersWidth = 51;
            this.dataGV_FLN.RowTemplate.Height = 24;
            this.dataGV_FLN.Size = new System.Drawing.Size(367, 275);
            this.dataGV_FLN.TabIndex = 12;
            // 
            // dataGVFollow
            // 
            this.dataGVFollow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVFollow.Location = new System.Drawing.Point(406, 189);
            this.dataGVFollow.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGVFollow.Name = "dataGVFollow";
            this.dataGVFollow.RowHeadersWidth = 51;
            this.dataGVFollow.RowTemplate.Height = 24;
            this.dataGVFollow.Size = new System.Drawing.Size(272, 275);
            this.dataGVFollow.TabIndex = 13;
            // 
            // dataGVET
            // 
            this.dataGVET.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVET.Location = new System.Drawing.Point(707, 189);
            this.dataGVET.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGVET.Name = "dataGVET";
            this.dataGVET.RowHeadersWidth = 51;
            this.dataGVET.RowTemplate.Height = 24;
            this.dataGVET.Size = new System.Drawing.Size(362, 275);
            this.dataGVET.TabIndex = 14;
            // 
            // btnArbol
            // 
            this.btnArbol.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnArbol.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnArbol.Location = new System.Drawing.Point(641, 98);
            this.btnArbol.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnArbol.Name = "btnArbol";
            this.btnArbol.Size = new System.Drawing.Size(107, 25);
            this.btnArbol.TabIndex = 15;
            this.btnArbol.Text = "Ver Arbol";
            this.btnArbol.UseVisualStyleBackColor = false;
            this.btnArbol.Click += new System.EventHandler(this.btnArbol_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1082, 490);
            this.Controls.Add(this.btnArbol);
            this.Controls.Add(this.dataGVET);
            this.Controls.Add(this.dataGVFollow);
            this.Controls.Add(this.dataGV_FLN);
            this.Controls.Add(this.lblTET);
            this.Controls.Add(this.lblTFollow);
            this.Controls.Add(this.lblTFLN);
            this.Controls.Add(this.lbl_MostrarR);
            this.Controls.Add(this.lbl_TituloER);
            this.Controls.Add(this.txbExpresion);
            this.Controls.Add(this.btnAnalizar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.txbRuta);
            this.Controls.Add(this.btnBuscador);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.Text = "Go 1.0.1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGV_FLN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVFollow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVET)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuscador;
        private System.Windows.Forms.TextBox txbRuta;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAnalizar;
        private System.Windows.Forms.TextBox txbExpresion;
        private System.Windows.Forms.Label lbl_TituloER;
        private System.Windows.Forms.Label lbl_MostrarR;
        private System.Windows.Forms.Label lblTFLN;
        private System.Windows.Forms.Label lblTFollow;
        private System.Windows.Forms.Label lblTET;
        private System.Windows.Forms.DataGridView dataGV_FLN;
        private System.Windows.Forms.DataGridView dataGVFollow;
        private System.Windows.Forms.DataGridView dataGVET;
        private System.Windows.Forms.Button btnArbol;
    }
}

