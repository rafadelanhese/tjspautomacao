using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TjspAutomacao.Service
{
    class PlanilhaService
    {
        public static string CaminhoArquivo { get; set; }
        private readonly int NUM_MINIMO_LINHAS = 1;
        private readonly int NUM_MINIMO_COLUNAS = 1;
        private readonly int NUM_CORRETO_COLUNAS = 4;

        private DataTable dtProcessos;
        public PlanilhaService()
        {
            this.dtProcessos = new DataTable();
            InicializaDataTable();
        }

        private void InicializaDataTable()
        {
            if(dtProcessos.Columns.Count < NUM_MINIMO_COLUNAS)
            {
                dtProcessos.Columns.Add(new DataColumn("Número do Processo"));
                dtProcessos.Columns.Add(new DataColumn("Tipo de petição"));
                dtProcessos.Columns.Add(new DataColumn("Despesas Processuais"));
                dtProcessos.Columns.Add(new DataColumn("Protocolo"));
            }            
        }

        public DataTable CarregaDataGridView()
        {
            if (string.IsNullOrEmpty(CaminhoArquivo))
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

                String[] linha = System.IO.File.ReadAllLines(CaminhoArquivo);

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

        public static void InsereProtocoloCSV(string numeroProcesso)
        {
            string diretorioArquivo = DiretorioArquivoFormatado();
           
            using (StreamWriter streamWriter = new StreamWriter(diretorioArquivo, true, Encoding.Default))
            {
                streamWriter.WriteLineAsync(string.Concat(numeroProcesso," Data: " + DataAtualFormatada() + " Hora: " + HoraAtualFormatada()));
                streamWriter.Close();
            }
        }

        private static string DataAtualFormatada()
        {
            return DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
        }

        private static string HoraAtualFormatada()
        {
            return DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        }

        private static string DiretorioArquivoFormatado()
        {
            int posicaoPonto = CaminhoArquivo.LastIndexOf(Char.ConvertFromUtf32(92));
            string diretorioArquivo = CaminhoArquivo.Remove(posicaoPonto);
            return diretorioArquivo + "\\Relatatório Proc. Protocolados" + DataAtualFormatada() + ".txt";
        }
    }
}
