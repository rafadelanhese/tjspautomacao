using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TjspAutomacao.Service
{
    class Planilha
    {
        private readonly int NUM_MINIMO_LINHAS = 1;
        private readonly int NUM_MINIMO_COLUNAS = 1;
        private readonly int NUM_CORRETO_COLUNAS = 3;

        private DataTable dtProcessos;
        public Planilha()
        {
            this.dtProcessos = new DataTable();
            InicializaDataTable();
        }

        private void InicializaDataTable()
        {
            if(dtProcessos.Columns.Count < NUM_MINIMO_COLUNAS)
            {
                dtProcessos.Columns.Add(new DataColumn("Número do Processo"));
                dtProcessos.Columns.Add(new DataColumn("Foro"));
                dtProcessos.Columns.Add(new DataColumn("Competência"));
            }            
        }

        public DataTable CarregaDataGridView(string caminhoArquivo)
        {
            if (string.IsNullOrEmpty(caminhoArquivo))
            {
                MessageBox.Show("O caminho do arquivo é nulo ou vazio");
            }
            else
            {
                //Verifico se há algum dado na datagridview, se tiver limpa o datagrid
                if (dtProcessos.Rows.Count > NUM_MINIMO_LINHAS)
                {
                    dtProcessos.Rows.Clear();
                }

                String[] linha = System.IO.File.ReadAllLines(caminhoArquivo);

                String[] cabecalho = linha[0].Split(Convert.ToChar(";"));                

                if(cabecalho.Length < NUM_CORRETO_COLUNAS || cabecalho.Length > NUM_CORRETO_COLUNAS)
                {
                    MessageBox.Show("Planilha com número incorreto de colunas, a quantidade correta é :" +  NUM_CORRETO_COLUNAS.ToString());
                }
                else
                {                    
                    for (int posLinha = 1; posLinha < linha.Length; posLinha++)
                    {
                        String[] dados = linha[posLinha].Split(Convert.ToChar(";"));
                        dtProcessos.Rows.Add(dados);
                    }
                }                           
            }
            return dtProcessos;
        }
    }
}
