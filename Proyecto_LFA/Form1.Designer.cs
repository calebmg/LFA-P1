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
            this.btnGenerar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV_FLN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVFollow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGVET)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBuscador
            // 
            this.btnBuscador.BackColor = System.Drawing.Color.Gainsboro;
            this.btnBuscador.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscador.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBuscador.Location = new System.Drawing.Point(15, 13);
            this.btnBuscador.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBuscador.Name = "btnBuscador";
            this.btnBuscador.Size = new System.Drawing.Size(171, 34);
            this.btnBuscador.TabIndex = 0;
            this.btnBuscador.Text = "Seleccionar Archivo";
            this.btnBuscador.UseVisualStyleBackColor = false;
            this.btnBuscador.Click += new System.EventHandler(this.btnBuscador_Click);
            // 
            // txbRuta
            // 
            this.txbRuta.BackColor = System.Drawing.Color.White;
            this.txbRuta.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbRuta.Location = new System.Drawing.Point(230, 19);
            this.txbRuta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbRuta.Name = "txbRuta";
            this.txbRuta.Size = new System.Drawing.Size(479, 23);
            this.txbRuta.TabIndex = 1;
            this.txbRuta.TextChanged += new System.EventHandler(this.txbRuta_TextChanged);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSalir.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(786, 23);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 24);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAnalizar
            // 
            this.btnAnalizar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnAnalizar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalizar.Location = new System.Drawing.Point(20, 69);
            this.btnAnalizar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAnalizar.Name = "btnAnalizar";
            this.btnAnalizar.Size = new System.Drawing.Size(165, 47);
            this.btnAnalizar.TabIndex = 4;
            this.btnAnalizar.Text = "Analizar Archivo";
            this.btnAnalizar.UseVisualStyleBackColor = false;
            this.btnAnalizar.Click += new System.EventHandler(this.btnAnalizar_Click);
            // 
            // txbExpresion
            // 
            this.txbExpresion.BackColor = System.Drawing.Color.White;
            this.txbExpresion.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbExpresion.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txbExpresion.Location = new System.Drawing.Point(15, 168);
            this.txbExpresion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbExpresion.Name = "txbExpresion";
            this.txbExpresion.Size = new System.Drawing.Size(1356, 23);
            this.txbExpresion.TabIndex = 5;
            this.txbExpresion.TextChanged += new System.EventHandler(this.txbExpresion_TextChanged);
            // 
            // lbl_TituloER
            // 
            this.lbl_TituloER.AutoSize = true;
            this.lbl_TituloER.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TituloER.Location = new System.Drawing.Point(326, 129);
            this.lbl_TituloER.Name = "lbl_TituloER";
            this.lbl_TituloER.Size = new System.Drawing.Size(193, 17);
            this.lbl_TituloER.TabIndex = 6;
            this.lbl_TituloER.Text = "Expresion regular del archivo";
            this.lbl_TituloER.Click += new System.EventHandler(this.lbl_TituloER_Click);
            // 
            // lbl_MostrarR
            // 
            this.lbl_MostrarR.AutoSize = true;
            this.lbl_MostrarR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MostrarR.ForeColor = System.Drawing.Color.Red;
            this.lbl_MostrarR.Location = new System.Drawing.Point(223, 91);
            this.lbl_MostrarR.Name = "lbl_MostrarR";
            this.lbl_MostrarR.Size = new System.Drawing.Size(0, 17);
            this.lbl_MostrarR.TabIndex = 7;
            // 
            // lblTFLN
            // 
            this.lblTFLN.AutoSize = true;
            this.lblTFLN.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTFLN.Location = new System.Drawing.Point(16, 219);
            this.lblTFLN.Name = "lblTFLN";
            this.lblTFLN.Size = new System.Drawing.Size(179, 17);
            this.lblTFLN.TabIndex = 8;
            this.lblTFLN.Text = "Tabla 1: First, Last, Nullable";
            this.lblTFLN.Click += new System.EventHandler(this.lblTFLN_Click);
            // 
            // lblTFollow
            // 
            this.lblTFollow.AutoSize = true;
            this.lblTFollow.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTFollow.Location = new System.Drawing.Point(662, 219);
            this.lblTFollow.Name = "lblTFollow";
            this.lblTFollow.Size = new System.Drawing.Size(104, 17);
            this.lblTFollow.TabIndex = 9;
            this.lblTFollow.Text = "Tabla 2: Follow";
            this.lblTFollow.Click += new System.EventHandler(this.lblTFollow_Click);
            // 
            // lblTET
            // 
            this.lblTET.AutoSize = true;
            this.lblTET.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTET.Location = new System.Drawing.Point(1022, 219);
            this.lblTET.Name = "lblTET";
            this.lblTET.Size = new System.Drawing.Size(200, 17);
            this.lblTET.TabIndex = 10;
            this.lblTET.Text = "Tabla 3: Estados y transiciones";
            this.lblTET.Click += new System.EventHandler(this.lblTET_Click);
            // 
            // dataGV_FLN
            // 
            this.dataGV_FLN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV_FLN.Location = new System.Drawing.Point(17, 248);
            this.dataGV_FLN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGV_FLN.Name = "dataGV_FLN";
            this.dataGV_FLN.RowHeadersWidth = 51;
            this.dataGV_FLN.RowTemplate.Height = 24;
            this.dataGV_FLN.Size = new System.Drawing.Size(489, 276);
            this.dataGV_FLN.TabIndex = 12;
            // 
            // dataGVFollow
            // 
            this.dataGVFollow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVFollow.Location = new System.Drawing.Point(541, 248);
            this.dataGVFollow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGVFollow.Name = "dataGVFollow";
            this.dataGVFollow.RowHeadersWidth = 51;
            this.dataGVFollow.RowTemplate.Height = 24;
            this.dataGVFollow.Size = new System.Drawing.Size(363, 276);
            this.dataGVFollow.TabIndex = 13;
            // 
            // dataGVET
            // 
            this.dataGVET.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVET.Location = new System.Drawing.Point(943, 248);
            this.dataGVET.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGVET.Name = "dataGVET";
            this.dataGVET.RowHeadersWidth = 51;
            this.dataGVET.RowTemplate.Height = 24;
            this.dataGVET.Size = new System.Drawing.Size(428, 276);
            this.dataGVET.TabIndex = 14;
            // 
            // btnArbol
            // 
            this.btnArbol.BackColor = System.Drawing.Color.Gainsboro;
            this.btnArbol.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnArbol.Location = new System.Drawing.Point(786, 129);
            this.btnArbol.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnArbol.Name = "btnArbol";
            this.btnArbol.Size = new System.Drawing.Size(143, 33);
            this.btnArbol.TabIndex = 15;
            this.btnArbol.Text = "Ver Arbol";
            this.btnArbol.UseVisualStyleBackColor = false;
            this.btnArbol.Click += new System.EventHandler(this.btnArbol_Click);
            // 
            // btnGenerar
            // 
            this.btnGenerar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnGenerar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.Location = new System.Drawing.Point(953, 129);
            this.btnGenerar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(201, 33);
            this.btnGenerar.TabIndex = 16;
            this.btnGenerar.Text = "Generar escaner";
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(1388, 540);
            this.Controls.Add(this.btnGenerar);
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
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Generador de automata";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Button btnGenerar;
    }
}

