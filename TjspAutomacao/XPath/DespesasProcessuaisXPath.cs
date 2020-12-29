using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjspAutomacao.XPath
{
    class DespesasProcessuaisXPath
    {        
        private readonly string BOTAO_DOCUMENTO = "//*[@id='secaoGuiaCustas']/div/div/button";
        private int QtdeDare = 1;

        public DespesasProcessuaisXPath()
        {

        }

        public string GetBotaoDocumentoXPath()
        {
            if(QtdeDare == 1)
            {
                QtdeDare++;
                return BOTAO_DOCUMENTO;
            }

            string auxQtdeDare = QtdeDare.ToString();
            QtdeDare++;
            return BOTAO_DOCUMENTO.Insert(30, "[" + auxQtdeDare + "]");
        } 

        public bool PrimeiraDare()
        {
            return QtdeDare == 1 ? true : false;
        }
    }
}
