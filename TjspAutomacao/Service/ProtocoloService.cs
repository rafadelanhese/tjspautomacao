using AutoIt;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TjspAutomacao.Model;
using TjspAutomacao.Service;
using TjspAutomacao.XPath;
using Keys = OpenQA.Selenium.Keys;

namespace TjspAutomacao.Classe
{
    class Protocolo
    {
        private readonly string ESAJ = "https://esaj.tjsp.jus.br/sajcas/login?service=https%3A%2F%2Fesaj.tjsp.jus.br%2Fpetpg%2Fj_spring_cas_security_check";
        private readonly string PETICAO_INTERMEDIARIA = "https://esaj.tjsp.jus.br/petpg/peticoes/intermediaria";
        private readonly int TEMPO_ESPERA = 4000;
        private readonly int TAMANHO_NUMERO_DESPPROCESSUAIS = 19;
        private readonly int TAMANHO_NUMERO_PROCESSO = 20;
        private IWebDriver navegador;       

        public Protocolo()
        {            
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;            
            chromeOptions.AddArguments("--start-maximized");           
            chromeOptions.AddExtension(string.Concat(Directory.GetCurrentDirectory(),"\\WebSigner.crx"));
            this.navegador = new ChromeDriver(chromeOptions);
            this.navegador.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(100);
        }
       
        public void AbrirUrlLogin()
        {
            try
            {
                navegador.Navigate().GoToUrl(ESAJ);
            }
            catch (OpenQA.Selenium.WebDriverException webDriverException)
            {
                MessageBox.Show(webDriverException.Message);
            }
        }

        public void AbrirUrlPeticaoIntermediaria()
        {
            try
            {
                navegador.Navigate().GoToUrl(PETICAO_INTERMEDIARIA);
            }
            catch (OpenQA.Selenium.WebDriverException webDriverException)
            {
                MessageBox.Show(webDriverException.Message);
            }
        }        

        public void Login(string cpf, string senha)
        {
            try
            {
                navegador.FindElement(By.Id("usernameForm")).SendKeys(cpf);
                navegador.FindElement(By.Id("passwordForm")).SendKeys(senha);
                navegador.FindElement(By.Id("pbEntrar")).Click();
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException)
            {
                navegador.Close();
            }                 
        }        

        public void Protocolar(DataGridView dgvProcessos, string caminhoPasta, string senhaToken)
        {
            bool uploadArquivos;
            foreach(DataGridViewRow dgvLinha in dgvProcessos.Rows)
            {
                string numeroProcesso = dgvLinha.Cells["Número do Processo"].Value.ToString();
                string tipoPeticao = dgvLinha.Cells["Tipo de Petição"].Value.ToString();
                string valorDespesasProcessuais = dgvLinha.Cells["Despesas Processuais"].Value.ToString();
                Thread.Sleep(TEMPO_ESPERA);
                InserirNumeroProcesso(numeroProcesso);
                Thread.Sleep(TEMPO_ESPERA);
                InserirClassificacao(tipoPeticao);
                Thread.Sleep(TEMPO_ESPERA);
                InserirDespesasProcessuais(valorDespesasProcessuais);
                Thread.Sleep(TEMPO_ESPERA);
                uploadArquivos = UploadArquivos(numeroProcesso, caminhoPasta);
                Thread.Sleep(TEMPO_ESPERA);
                SalvarRascunho();
                if(!uploadArquivos)
                    GravaValorProtocoloDGV(dgvLinha, "NÃO PROTOCOLADO");
                else
                {
                    //AssinarDocumento(senhaToken);
                    GravaValorProtocoloDGV(dgvLinha, "PROTOCOLADO");
                    GravaValorProtocoloTXT(numeroProcesso);
                }                
                Thread.Sleep(TEMPO_ESPERA);
                navegador.Navigate().GoToUrl(PETICAO_INTERMEDIARIA);
                Thread.Sleep(TEMPO_ESPERA);
            }
            navegador.Close();
        }

        private void InserirNumeroProcesso(string numeroProcesso)
        {            
            navegador.FindElement(By.Id("botaoEditarDadosBasicos")).Click();
            if (numeroProcesso.Length == TAMANHO_NUMERO_PROCESSO)
                navegador.FindElement(By.Id("processoNumero")).SendKeys(numeroProcesso + Keys.Tab);
            else
                Console.WriteLine("Número do processo menor e/ou maior que o número correto");
        }

        private void InserirClassificacao(string tipoPetição)
        {
            string campoTipoPeticao = "/html/body/span/main/div/div/div/form/div/div/div[2]/div/div/section[3]/div/div[2]/div/div[1]/div/selecao-classe/div/div/div[1]";
            if (!navegador.FindElement(By.XPath(campoTipoPeticao)).Displayed)
                navegador.FindElement(By.Id("botaoEditarClassificacao")).Click();
            
            navegador.FindElement(By.XPath(campoTipoPeticao)).Click();
            navegador.FindElement(By.Id("selectClasseIntermediaria")).SendKeys(tipoPetição + Keys.Tab);
        }
        
        private void InserirDespesasProcessuais(string valorDespesasProcessuais)
        {            
            if (!string.IsNullOrEmpty(valorDespesasProcessuais) && valorDespesasProcessuais.Length == TAMANHO_NUMERO_DESPPROCESSUAIS &&navegador.FindElement(By.Id("botaoEditarDespesas")).Displayed)
            {
                try
                {
                    navegador.FindElement(By.Id("botaoEditarDespesas")).Click();
                    navegador.FindElement(By.XPath("//*[@id='despesasProcessuaisDare']/div/div[2]/div/ng-include/div[1]/radio-input[2]")).Click();
                    navegador.FindElement(By.XPath("//*[@id='secaoGuiaCustas']/div/div/button")).Click();
                    navegador.FindElement(By.Id("numeroGuiaDare")).SendKeys(valorDespesasProcessuais + Keys.Tab);
                    Thread.Sleep(TEMPO_ESPERA);
                    navegador.FindElement(By.Id("btnSalvarGuia")).Click();
                }
                catch (OpenQA.Selenium.WebDriverException)
                {
                    MessageBox.Show("Antes de continuar confirme o número do documento, pois não foi localizado na Secretaria da Fazenda.\n Navegador será encerrado");
                    navegador.Close();
                }
            }
        }

        private bool UploadArquivos(string numeroProcesso, string caminhoArquivos)
        {            
            string[] arquivos = Directory.GetFiles(caminhoArquivos, numeroProcesso + "*.pdf", SearchOption.AllDirectories);           
            int posArquivo = 0;            

            var allowsDetection = this.navegador as IAllowsFileDetection;
            if (allowsDetection != null)
            {
                allowsDetection.FileDetector = new LocalFileDetector();
            }            
            
            if(arquivos.Length > 0)
            {                
                if (!ExistePeticao(arquivos))
                {
                    return false;
                }
                else
                {
                    arquivos = OrganizaArquivos(arquivos);
                    foreach (string arq in arquivos)
                    {
                        if (posArquivo == 0)
                        {
                            navegador.FindElement(By.Id("botaoAdicionarDocumento")).Click();
                        }
                        else
                        {
                            navegador.FindElement(By.ClassName("button__add")).Click();
                        }

                        Thread.Sleep(TEMPO_ESPERA);
                        SendKeys.SendWait(arq);
                        SendKeys.SendWait("{Enter}");
                        Thread.Sleep(TEMPO_ESPERA);
                        //Se o posArquivo for maior que 0 significa que ele já anexou o primeiro arquivo
                        //E agora vai anexar o restante dos arquivos e selecionar o seu tipo
                        if (posArquivo > 0)
                        {
                            try
                            {
                                IWebElement xpathTipoPeticao = new WebDriverWait(navegador, TimeSpan.FromSeconds(25)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(DocumentoXPath.ElementoClicavel(posArquivo))));
                                xpathTipoPeticao.Click();
                                navegador.FindElement(By.XPath(DocumentoXPath.InputTipoDocumento(posArquivo))).SendKeys(ObterNomeTipoDeDocumento(arq) + Keys.Tab);
                            }
                            catch (OpenQA.Selenium.ElementClickInterceptedException)
                            {
                            }
                        }
                        posArquivo++;
                    }
                    return true;
                }                
            }
            return false;
        }

        /* Método coloca o arquivo com a nomenclatura PETICAO na posição 0,
         * o restante do array é mantido na mesma forma uma vez que não necessidade de ter uma ordem específica
         */
        private string[] OrganizaArquivos(string[] arquivos)
        {
            string[] auxiliarArquivos = new string[arquivos.Length];
            int posicaoInicialAuxArquivos = 1;            

            foreach (string arq in arquivos)
            {
                if (arq.Contains("PETICAO"))
                    auxiliarArquivos[0] = arq;
                else
                {
                    auxiliarArquivos[posicaoInicialAuxArquivos++] = arq;                    
                }
            }           
            
            return auxiliarArquivos;
        }

        private bool ExistePeticao(string[] arquivos)
        {
            bool existePeticao = false;
            foreach (string arq in arquivos)
            {
                if (arq.Contains("PETICAO"))
                    existePeticao = !existePeticao;
            }
            
            return existePeticao;
        }
        private string ObterNomeTipoDeDocumento(string arq)
        {
            //45 - valor do - em int
            //46 - valor do . em int
            try
            {
                int posicaoHifen = arq.LastIndexOf(Char.ConvertFromUtf32(45)) + 1;

                string nomeComExtensao = arq.Substring(posicaoHifen);

                int posicaoPonto = nomeComExtensao.LastIndexOf(Char.ConvertFromUtf32(46));
                string nomeSemExtensao = nomeComExtensao.Remove(posicaoPonto);

                return nomeSemExtensao;
            }
            catch (ArgumentOutOfRangeException)
            {
                return "";
            }
            
        }
        private void SalvarRascunho()
        {
            navegador.FindElement(By.Id("botaoSalvarRascunho")).Click();
        }        

        private void GravaValorProtocoloDGV(DataGridViewRow dgvLinha, string mensagem)
        {            
            dgvLinha.Cells["Protocolo"].Value = mensagem;                 
        }

        private void GravaValorProtocoloTXT(string numeroProcesso)
        {
            PlanilhaService.InsereProtocoloTXT(numeroProcesso);
        }

        private void AssinarDocumento(string senhaToken)
        {
            try
            {
                //navegador.FindElement(By.XPath("//*[@id='containerCertificadoParaAssinatura']/div[2]/div/div/div[1]/span/span[2]")).Click();
                //navegador.FindElement(By.XPath("//*[@id='selectCertificadoParaAssinatura']")).Click();
                //*[@id="containerCertificadoParaAssinatura"]/div[2]/div/div/div[1]/span
                IWebElement weContainerCertificado = new WebDriverWait(navegador, TimeSpan.FromSeconds(25)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(CertificadoXPath.CONTAINER_CERTIFICADO_ASSINATURA)));
                navegador.FindElement(By.Id(CertificadoXPath.BOTAO_PROTOCOLAR)).Click();
                navegador.FindElement(By.XPath(CertificadoXPath.BOTAO_PROTOCOLAR_SIM)).Click();
                SendKeys.SendWait(senhaToken);
            }
            catch (OpenQA.Selenium.ElementNotInteractableException)
            {
                MessageBox.Show("Nenhum certificado disponível");
            }
                        
        }
    }
}
