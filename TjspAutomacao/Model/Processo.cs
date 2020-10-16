using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjspAutomacao.Model
{
    class Processo
    {

        public string NumeroProcesso { get; set; }
        public string Competencia { get; set; }
        public string Classe { get; set; }
        public string AssuntoPrincipal { get; set; }
        public string OutrosAssuntos { get; set; }
        public Processo()
        {

        }
    }
}
