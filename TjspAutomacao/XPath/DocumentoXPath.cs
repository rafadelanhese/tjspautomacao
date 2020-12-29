using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjspAutomacao.Model
{        
    class DocumentoXPath
    {
        public static readonly string INPUT_DOCUMENTO = "/html/body/span/main/div/div/div/form/div/div/div[1]/div/div/div/article/section[2]/button/input";
        private string BotaoTipoDocumento = "/html/body/span/main/div/div/div/form/div/div/div[1]/div/div/div/article/section[1]/footer/div/ul/li[]/div/nav/div/ul/li[3]/span/div/div[1]/span";
        private string InputTipoDocumento = "/html/body/span/main/div/div/div/form/div/div/div[1]/div/div/div/article/section[1]/footer/div/ul/li[]/div/nav/div/ul/li[3]/span/div/input[1]";
        private readonly int posInsercaoString = 101;
        public DocumentoXPath()
        {

        }

        public string GetBotaoTipoDocumento(int posicao)
        {
            return BotaoTipoDocumento.Insert(posInsercaoString, posicao.ToString());
        }

        public string GetInputTipoDocumento(int posicao)
        {
            return InputTipoDocumento.Insert(posInsercaoString, posicao.ToString());
        }                 
    }
}
