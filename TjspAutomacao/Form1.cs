using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TjspAutomacao.Classe;

namespace TjspAutomacao
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            InicializaDataGridView();
        }   
        
        private void InicializaDataGridView()
        {
            dgvProcessos.Columns.Add("Núm. Processo", "Núm. Processo");
            dgvProcessos.Columns.Add("Foro", "Foro");
            dgvProcessos.Columns.Add("Competência", "Competência");
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            DialogResult result = ofdArquivo.ShowDialog();
            if (result == DialogResult.OK)
            {
                ttbCaminhoArquivo.Text = ofdArquivo.FileName;
            }
        }

        private void btnProtocolar_Click(object sender, EventArgs e)
        {
            Protocolo protocolo = new Protocolo();

            string cpf = ttbCPF.Text;
            string senha = ttbSenha.Text;

            protocolo.AbreTJSP();
            protocolo.Login(cpf, senha);
        }

        private void btnProcurarPasta_Click(object sender, EventArgs e)
        {
            DialogResult result = fbdPastaArquivos.ShowDialog();
            if (result == DialogResult.OK) 
            {
                ttbCaminhoPasta.Text = fbdPastaArquivos.SelectedPath;
            }
        }

        private void btnCarregarPlanilha_Click(object sender, EventArgs e)
        {

        }
    }
}
