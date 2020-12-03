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

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            DialogResult result = ofdArquivo.ShowDialog();
            if (result == DialogResult.OK && ofdArquivo.FileName.EndsWith(".csv"))
            {
                ttbCaminhoArquivo.Text = ofdArquivo.FileName;
                btnCarregarPlanilha.Enabled = true;
            }
            else
                MessageBox.Show("Nenhum arquivo foi selecionado ou o arquivo selecionado não é .csv");
        }

        private void btnProtocolar_Click(object sender, EventArgs e)
        {
            Protocolo protocolo = new Protocolo();
            Boolean passoExecutado;            

            protocolo.AbrirURL("ESAJ");
            
            passoExecutado = protocolo.Login(ttbCPF.Text, ttbSenha.Text);

            if (passoExecutado)
            {
                protocolo.AbrirURL("PETICAO_INTERMEDIARIA");
                protocolo.Protocolar(dgvProcessos, ttbCaminhoPasta.Text);
            }
            else
                MessageBox.Show("Login e/ou senha inválidos");
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
            PlanilhaService planilha = new PlanilhaService();
            int numMinimoLinhasDGVProcessos = 1;

            PlanilhaService.CaminhoArquivo = ttbCaminhoArquivo.Text;
            dgvProcessos.DataSource = planilha.CarregaDataGridView();

            //[1] é o número mínimo para habilitar o btnProtocolar
            if (dgvProcessos.Rows.Count >= numMinimoLinhasDGVProcessos)
                btnProtocolar.Enabled = true;
        }
    }
}
