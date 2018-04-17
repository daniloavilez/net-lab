namespace TesteImposto
{
    partial class FormImposto
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNomeCliente = new System.Windows.Forms.TextBox();
            this.txtEstadoOrigem = new System.Windows.Forms.TextBox();
            this.txtEstadoDestino = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewPedidos = new System.Windows.Forms.DataGridView();
            this.buttonGerarNotaFiscal = new System.Windows.Forms.Button();
            this.ddlEstadoOrigem = new System.Windows.Forms.ComboBox();
            this.ddlEstadoDestino = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome do Cliente";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Estado Origem";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Estado Destino";
            // 
            // textBoxNomeCliente
            // 
            this.textBoxNomeCliente.Location = new System.Drawing.Point(127, 11);
            this.textBoxNomeCliente.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxNomeCliente.Name = "textBoxNomeCliente";
            this.textBoxNomeCliente.Size = new System.Drawing.Size(1251, 22);
            this.textBoxNomeCliente.TabIndex = 3;
            // 
            // txtEstadoOrigem
            // 
            this.txtEstadoOrigem.Location = new System.Drawing.Point(127, 38);
            this.txtEstadoOrigem.Margin = new System.Windows.Forms.Padding(4);
            this.txtEstadoOrigem.MaxLength = 2;
            this.txtEstadoOrigem.Name = "txtEstadoOrigem";
            this.txtEstadoOrigem.Size = new System.Drawing.Size(1251, 22);
            this.txtEstadoOrigem.TabIndex = 4;
            this.txtEstadoOrigem.Visible = false;
            // 
            // txtEstadoDestino
            // 
            this.txtEstadoDestino.Location = new System.Drawing.Point(127, 65);
            this.txtEstadoDestino.Margin = new System.Windows.Forms.Padding(4);
            this.txtEstadoDestino.MaxLength = 2;
            this.txtEstadoDestino.Name = "txtEstadoDestino";
            this.txtEstadoDestino.Size = new System.Drawing.Size(1251, 22);
            this.txtEstadoDestino.TabIndex = 5;
            this.txtEstadoDestino.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 114);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Itens do pedido";
            // 
            // dataGridViewPedidos
            // 
            this.dataGridViewPedidos.AllowUserToOrderColumns = true;
            this.dataGridViewPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPedidos.Location = new System.Drawing.Point(8, 134);
            this.dataGridViewPedidos.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewPedidos.Name = "dataGridViewPedidos";
            this.dataGridViewPedidos.Size = new System.Drawing.Size(1371, 400);
            this.dataGridViewPedidos.TabIndex = 7;
            // 
            // buttonGerarNotaFiscal
            // 
            this.buttonGerarNotaFiscal.Location = new System.Drawing.Point(1209, 542);
            this.buttonGerarNotaFiscal.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGerarNotaFiscal.Name = "buttonGerarNotaFiscal";
            this.buttonGerarNotaFiscal.Size = new System.Drawing.Size(169, 28);
            this.buttonGerarNotaFiscal.TabIndex = 8;
            this.buttonGerarNotaFiscal.Text = "Gerar Nota Fiscal";
            this.buttonGerarNotaFiscal.UseVisualStyleBackColor = true;
            this.buttonGerarNotaFiscal.Click += new System.EventHandler(this.buttonGerarNotaFiscal_Click);
            // 
            // ddlEstadoOrigem
            // 
            this.ddlEstadoOrigem.FormattingEnabled = true;
            this.ddlEstadoOrigem.Items.AddRange(new object[] {
            "",
            "SP",
            "MG"});
            this.ddlEstadoOrigem.Location = new System.Drawing.Point(127, 39);
            this.ddlEstadoOrigem.Name = "ddlEstadoOrigem";
            this.ddlEstadoOrigem.Size = new System.Drawing.Size(1252, 24);
            this.ddlEstadoOrigem.TabIndex = 4;
            // 
            // ddlEstadoDestino
            // 
            this.ddlEstadoDestino.FormattingEnabled = true;
            this.ddlEstadoDestino.Items.AddRange(new object[] {
            "",
            "RJ",
            "PE",
            "MG",
            "PB",
            "PR",
            "PI",
            "RO",
            "SE",
            "TO",
            "PA"});
            this.ddlEstadoDestino.Location = new System.Drawing.Point(127, 67);
            this.ddlEstadoDestino.Name = "ddlEstadoDestino";
            this.ddlEstadoDestino.Size = new System.Drawing.Size(1252, 24);
            this.ddlEstadoDestino.TabIndex = 5;
            // 
            // FormImposto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1391, 587);
            this.Controls.Add(this.ddlEstadoDestino);
            this.Controls.Add(this.ddlEstadoOrigem);
            this.Controls.Add(this.buttonGerarNotaFiscal);
            this.Controls.Add(this.dataGridViewPedidos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEstadoDestino);
            this.Controls.Add(this.txtEstadoOrigem);
            this.Controls.Add(this.textBoxNomeCliente);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormImposto";
            this.Text = "Calculo de imposto";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPedidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNomeCliente;
        private System.Windows.Forms.TextBox txtEstadoOrigem;
        private System.Windows.Forms.TextBox txtEstadoDestino;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridViewPedidos;
        private System.Windows.Forms.Button buttonGerarNotaFiscal;
        private System.Windows.Forms.ComboBox ddlEstadoOrigem;
        private System.Windows.Forms.ComboBox ddlEstadoDestino;
    }
}

