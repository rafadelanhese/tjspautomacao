namespace TjspAutomacao
{
    partial class Dashboard
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ttbSenha = new System.Windows.Forms.TextBox();
            this.ttbCPF = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvProcessos = new System.Windows.Forms.DataGridView();
            this.btnProtocolar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ttbCaminhoArquivo = new System.Windows.Forms.TextBox();
            this.btnProcurar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ttbSenha);
            this.groupBox1.Controls.Add(this.ttbCPF);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credenciais TJ/SP";
            // 
            // ttbSenha
            // 
            this.ttbSenha.Location = new System.Drawing.Point(237, 24);
            this.ttbSenha.Name = "ttbSenha";
            this.ttbSenha.Size = new System.Drawing.Size(152, 20);
            this.ttbSenha.TabIndex = 3;
            // 
            // ttbCPF
            // 
            this.ttbCPF.Location = new System.Drawing.Point(42, 24);
            this.ttbCPF.Mask = "000,000,000,-00";
            this.ttbCPF.Name = "ttbCPF";
            this.ttbCPF.Size = new System.Drawing.Size(106, 20);
            this.ttbCPF.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Senha:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CPF:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnProcurar);
            this.groupBox2.Controls.Add(this.ttbCaminhoArquivo);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 58);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tabela de Processos";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvProcessos);
            this.groupBox3.Location = new System.Drawing.Point(12, 140);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(776, 298);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tabela de Dados";
            // 
            // dgvProcessos
            // 
            this.dgvProcessos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcessos.Location = new System.Drawing.Point(9, 19);
            this.dgvProcessos.Name = "dgvProcessos";
            this.dgvProcessos.Size = new System.Drawing.Size(761, 273);
            this.dgvProcessos.TabIndex = 0;
            // 
            // btnProtocolar
            // 
            this.btnProtocolar.Location = new System.Drawing.Point(713, 444);
            this.btnProtocolar.Name = "btnProtocolar";
            this.btnProtocolar.Size = new System.Drawing.Size(75, 23);
            this.btnProtocolar.TabIndex = 6;
            this.btnProtocolar.Text = "Protocolar";
            this.btnProtocolar.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Caminho do Arquivo:";
            // 
            // ttbCaminhoArquivo
            // 
            this.ttbCaminhoArquivo.Location = new System.Drawing.Point(120, 25);
            this.ttbCaminhoArquivo.Name = "ttbCaminhoArquivo";
            this.ttbCaminhoArquivo.Size = new System.Drawing.Size(556, 20);
            this.ttbCaminhoArquivo.TabIndex = 1;
            // 
            // btnProcurar
            // 
            this.btnProcurar.Location = new System.Drawing.Point(682, 25);
            this.btnProcurar.Name = "btnProcurar";
            this.btnProcurar.Size = new System.Drawing.Size(75, 23);
            this.btnProcurar.TabIndex = 2;
            this.btnProcurar.Text = "Procurar";
            this.btnProcurar.UseVisualStyleBackColor = true;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 473);
            this.Controls.Add(this.btnProtocolar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox ttbSenha;
        private System.Windows.Forms.MaskedTextBox ttbCPF;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvProcessos;
        private System.Windows.Forms.Button btnProtocolar;
        private System.Windows.Forms.Button btnProcurar;
        private System.Windows.Forms.TextBox ttbCaminhoArquivo;
        private System.Windows.Forms.Label label3;
    }
}

