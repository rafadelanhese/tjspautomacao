using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjspAutomacao.Model
{
    class Documento
    {
        private static string Section = "/html/body/span/main/div/div/div/form/div/div/div[1]/div/div/div/article/section[1]";
        public static string XPathElementoClicavel(int posicao)
        {
            return  Section + "/footer/div/ul/li["+ posicao.ToString() +"]/div/nav/div/ul/li[3]/span/div/div[1]/span/span[1]";
        }

        public static string XPathInputTipoDocumento(int posicao)
        {
            if (posicao == 1)
                return Section + "/footer/div/ul/li/div/nav/div/ul/li[3]/span/div/input[1]";
            else
                return Section + "/footer/div/ul/li[" + posicao.ToString() +"]/div/nav/div/ul/li[3]/span/div/input[1]";
        }

        public static string XPathDocumentoInserido(int posicao)
        {
            string resultado = "";
            if (posicao == 0)
                resultado = Section + "/main/div/div/nav/div/ul/li[2]/span/a";
            else
                if(posicao == 1)
                    resultado = Section + "/footer/div/ul/li/div/nav/div/ul/li[2]/span/a";
                else
                resultado = Section + "/footer/div/ul/li["+ posicao.ToString() +"]/div/nav/div/ul/li[2]/span/a";
            return resultado;
        }
    }
}
