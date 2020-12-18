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
        public static string ElementoClicavel(int posicao)
        {
            return "/html/body/span/main/div/div/div/form/div/div/div[1]/div/div/div/article/section[1]/footer/div/ul/li[" + posicao.ToString() +"]/div/nav/div/ul/li[3]/span/div/div[1]/span";
        }

        public static string InputTipoDocumento(int posicao)
        {           
            if (posicao == 1)
                return "/html/body/span/main/div/div/div/form/div/div/div[1]/div/div/div/article/section[1]/footer/div/ul/li/div/nav/div/ul/li[3]/span/div/input[1]";
            else
                return "/html/body/span/main/div/div/div/form/div/div/div[1]/div/div/div/article/section[1]/footer/div/ul/li[" + posicao.ToString() +"]/div/nav/div/ul/li[3]/span/div/input[1]";
        }        
    }
}
