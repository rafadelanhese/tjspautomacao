using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TjspAutomacao.Classe;
using TjspAutomacao.Service;

namespace TjspAutomacao
{
    public partial class Dashboard : Form
    {        
        public Dashboard()
        {
            InitializeComponent();            
        }                   

        private void BtnProcurar_Click(object sender, EventArgs e)
        {
            DialogResult result = ofdArquivo.ShowDialog();
            if (result == DialogResult.OK && ofdArquivo.FileName.EndsWith(".csv"))
            {
                ttbCaminhoArquivo.Text = ofdArquivo.FileName;
                BtnCarregarPlanilha.Enabled = true;
            }
            else
                MessageBox.Show("Nenhum arquivo foi selecionado ou o arquivo selecionado não é .csv");
        }

        private void BtnProtocolar_Click(object sender, EventArgs e)
        {
            Protocolo protocolo = new Protocolo();            

            if (string.IsNullOrEmpty(ttbSenhaToken.Text) || string.IsNullOrEmpty(ttbCPF.Text) || string.IsNullOrEmpty(ttbSenha.Text))
            {
                MessageBox.Show("Verificar Campos: CPF, Senha TJ/SP e Senha do token podem estar vazios ou nulos");
            }
            else
            {
                protocolo.AbrirUrlLogin();

                protocolo.Login(ttbCPF.Text, ttbSenha.Text);

                protocolo.AbrirUrlPeticaoIntermediaria();
                protocolo.Protocolar(dgvProcessos, ttbCaminhoPasta.Text, ttbSenhaToken.Text);                
            }       
            
        }

        private void BtnProcurarPasta_Click(object sender, EventArgs e)
        {
            DialogResult result = fbdPastaArquivos.ShowDialog();
            if (result == DialogResult.OK) 
            {
                ttbCaminhoPasta.Text = fbdPastaArquivos.SelectedPath;
            }
        }

        private void BtnCarregarPlanilha_Click(object sender, EventArgs e)
        {
            PlanilhaService planilha = new PlanilhaService();
            int numMinimoLinhasDGVProcessos = 1;

            PlanilhaService.CaminhoArquivo = ttbCaminhoArquivo.Text;
            dgvProcessos.DataSource = planilha.CarregaDataGridView();

            //[1] é o número mínimo para habilitar o btnProtocolar
            if (dgvProcessos.Rows.Count >= numMinimoLinhasDGVProcessos)
                BtnProtocolar.Enabled = true;
        }        
    }
}
