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
            this.btnProcurar = new System.Windows.Forms.Button();
            this.ttbCaminhoArquivo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvProcessos = new System.Windows.Forms.DataGridView();
            this.btnProtocolar = new System.Windows.Forms.Button();
            this.ofdArquivo = new System.Windows.Forms.OpenFileDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnProcurarPasta = new System.Windows.Forms.Button();
            this.ttbCaminhoPasta = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fbdPastaArquivos = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCarregarPlanilha = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ttbSenhaToken = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessos)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.groupBox1.Size = new System.Drawing.Size(402, 58);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credenciais TJ/SP";
            // 
            // ttbSenha
            // 
            this.ttbSenha.Location = new System.Drawing.Point(237, 24);
            this.ttbSenha.Name = "ttbSenha";
            this.ttbSenha.PasswordChar = '*';
            this.ttbSenha.Size = new System.Drawing.Size(152, 20);
            this.ttbSenha.TabIndex = 3;
            // 
            // ttbCPF
            // 
            this.ttbCPF.Location = new System.Drawing.Point(42, 24);
            this.ttbCPF.Mask = "000,000,000-00";
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
            // btnProcurar
            // 
            this.btnProcurar.Location = new System.Drawing.Point(682, 23);
            this.btnProcurar.Name = "btnProcurar";
            this.btnProcurar.Size = new System.Drawing.Size(75, 23);
            this.btnProcurar.TabIndex = 2;
            this.btnProcurar.Text = "Procurar";
            this.btnProcurar.UseVisualStyleBackColor = true;
            this.btnProcurar.Click += new System.EventHandler(this.btnProcurar_Click);
            // 
            // ttbCaminhoArquivo
            // 
            this.ttbCaminhoArquivo.Enabled = false;
            this.ttbCaminhoArquivo.Location = new System.Drawing.Point(120, 25);
            this.ttbCaminhoArquivo.Name = "ttbCaminhoArquivo";
            this.ttbCaminhoArquivo.Size = new System.Drawing.Size(556, 20);
            this.ttbCaminhoArquivo.TabIndex = 1;
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvProcessos);
            this.groupBox3.Location = new System.Drawing.Point(12, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(776, 298);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tabela de Dados";
            // 
            // dgvProcessos
            // 
            this.dgvProcessos.AllowUserToAddRows = false;
            this.dgvProcessos.AllowUserToDeleteRows = false;
            this.dgvProcessos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProcessos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcessos.Location = new System.Drawing.Point(9, 19);
            this.dgvProcessos.Name = "dgvProcessos";
            this.dgvProcessos.Size = new System.Drawing.Size(761, 273);
            this.dgvProcessos.TabIndex = 0;
            // 
            // btnProtocolar
            // 
            this.btnProtocolar.Enabled = false;
            this.btnProtocolar.Location = new System.Drawing.Point(713, 512);
            this.btnProtocolar.Name = "btnProtocolar";
            this.btnProtocolar.Size = new System.Drawing.Size(75, 23);
            this.btnProtocolar.TabIndex = 6;
            this.btnProtocolar.Text = "Protocolar";
            this.btnProtocolar.UseVisualStyleBackColor = true;
            this.btnProtocolar.Click += new System.EventHandler(this.btnProtocolar_Click);
            // 
            // ofdArquivo
            // 
            this.ofdArquivo.DefaultExt = "csv";
            this.ofdArquivo.FileName = "openFileDialog1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnProcurarPasta);
            this.groupBox4.Controls.Add(this.ttbCaminhoPasta);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(12, 144);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(776, 58);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Pasta de Arquivos";
            // 
            // btnProcurarPasta
            // 
            this.btnProcurarPasta.Location = new System.Drawing.Point(682, 23);
            this.btnProcurarPasta.Name = "btnProcurarPasta";
            this.btnProcurarPasta.Size = new System.Drawing.Size(75, 23);
            this.btnProcurarPasta.TabIndex = 2;
            this.btnProcurarPasta.Text = "Procurar";
            this.btnProcurarPasta.UseVisualStyleBackColor = true;
            this.btnProcurarPasta.Click += new System.EventHandler(this.btnProcurarPasta_Click);
            // 
            // ttbCaminhoPasta
            // 
            this.ttbCaminhoPasta.Enabled = false;
            this.ttbCaminhoPasta.Location = new System.Drawing.Point(120, 25);
            this.ttbCaminhoPasta.Name = "ttbCaminhoPasta";
            this.ttbCaminhoPasta.Size = new System.Drawing.Size(556, 20);
            this.ttbCaminhoPasta.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Caminho da Pasta:";
            // 
            // btnCarregarPlanilha
            // 
            this.btnCarregarPlanilha.Enabled = false;
            this.btnCarregarPlanilha.Location = new System.Drawing.Point(12, 512);
            this.btnCarregarPlanilha.Name = "btnCarregarPlanilha";
            this.btnCarregarPlanilha.Size = new System.Drawing.Size(114, 23);
            this.btnCarregarPlanilha.TabIndex = 7;
            this.btnCarregarPlanilha.Text = "Carregar Planilha";
            this.btnCarregarPlanilha.UseVisualStyleBackColor = true;
            this.btnCarregarPlanilha.Click += new System.EventHandler(this.btnCarregarPlanilha_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(421, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(367, 58);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Senha Token";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Senha: ";
            // 
            // ttbSenhaToken
            // 
            this.ttbSenhaToken.Location = new System.Drawing.Point(474, 36);
            this.ttbSenhaToken.Name = "ttbSenhaToken";
            this.ttbSenhaToken.PasswordChar = '*';
            this.ttbSenhaToken.Size = new System.Drawing.Size(295, 20);
            this.ttbSenhaToken.TabIndex = 1;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 545);
            this.Controls.Add(this.ttbSenhaToken);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnCarregarPlanilha);
            this.Controls.Add(this.groupBox4);
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.OpenFileDialog ofdArquivo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnProcurarPasta;
        private System.Windows.Forms.TextBox ttbCaminhoPasta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog fbdPastaArquivos;
        private System.Windows.Forms.Button btnCarregarPlanilha;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ttbSenhaToken;
    }
}

