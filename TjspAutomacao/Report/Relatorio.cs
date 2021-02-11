using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjspAutomacao.Report
{
    class Relatorio
    {  

        public static void GravaProtocolo(string numeroProcesso)
        {
            string arquivo = string.Concat(Directory.GetCurrentDirectory(), "\\Reports\\Relatório Proc. Protocolados " + DateTime.Now.ToString("dd-MM-yyyy") + ".txt");

            using (StreamWriter outputFile = new StreamWriter(arquivo, true, Encoding.UTF8))
            {               
                outputFile.WriteLineAsync(string.Concat(numeroProcesso," ", DateTime.Now.ToString("dd/MM/yyyy HH:mm")));
                outputFile.Close();
            }
        }        
    }
}
