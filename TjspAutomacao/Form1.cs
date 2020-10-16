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
            if (result == DialogResult.OK)
            {
                ttbCaminhoArquivo.Text = ofdArquivo.FileName;
            }
        }

        private void btnProtocolar_Click(object sender, EventArgs e)
        {
            Protocolo protocolo = new Protocolo();
            Boolean passoExecutado;

            protocolo.AbrirURL("ESAJ");
            
            passoExecutado = protocolo.Login(ttbCPF.Text, ttbSenha.Text);

            if (passoExecutado)
                protocolo.AbrirURL("PETICAO_INICIAL");
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
            Planilha planilha = new Planilha();
            int numMinimoLinhasDGVProcessos = 1;
            
            dgvProcessos.DataSource = planilha.CarregaDataGridView(ttbCaminhoArquivo.Text);

            //[1] é o número mínimo para habilitar o btnProtocolar
            if (dgvProcessos.Rows.Count >= numMinimoLinhasDGVProcessos)
                btnProtocolar.Enabled = true;
        }
    }
}
