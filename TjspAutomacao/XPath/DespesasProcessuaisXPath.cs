using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjspAutomacao.XPath
{
    class DespesasProcessuaisXPath
    {        
        private readonly string BotaoDocumento = "//*[@id='secaoGuiaCustas']/div/div/button";
        private readonly int posInsercaoString = 30;
        private int QtdeDare = 1;

        public DespesasProcessuaisXPath()
        {

        }

        public string GetBotaoDocumentoXPath()
        {
            string strBotaoDocumento;

            strBotaoDocumento = PrimeiraDare() ? BotaoDocumento : BotaoDocumento.Insert(posInsercaoString, "[" + QtdeDare.ToString() + "]");     
            QtdeDare++;
            return strBotaoDocumento;
        } 

        public bool PrimeiraDare()
        {
            return QtdeDare == 1 ? true : false;
        }
    }
}
