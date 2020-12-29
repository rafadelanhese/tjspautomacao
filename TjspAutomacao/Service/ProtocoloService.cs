using AutoIt;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TjspAutomacao.Model;
using TjspAutomacao.Service;
using TjspAutomacao.XPath;
using Keys = OpenQA.Selenium.Keys;

namespace TjspAutomacao.Classe
{
    class ProtocoloService
    {
        private readonly string ESAJ = "https://esaj.tjsp.jus.br/sajcas/login?service=https%3A%2F%2Fesaj.tjsp.jus.br%2Fpetpg%2Fj_spring_cas_security_check";
        private readonly string LOGIN_ESAJ = "https://esaj.tjsp.jus.br/sajcas/login?service=https%3A%2F%2Fesaj.tjsp.jus.br%2Fesaj%2Fj_spring_cas_security_check";
        private readonly string PETICAO_INTERMEDIARIA = "https://esaj.tjsp.jus.br/petpg/peticoes/intermediaria";
        private readonly TimeSpan TEMPO_ESPERA = TimeSpan.FromSeconds(5);
        private readonly int TAMANHO_NUMERO_DARE = 19;
        private readonly int TAMANHO_NUMERO_PROCESSO = 25;
        private IWebDriver navegador;       

        public ProtocoloService()
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
                if(dgvLinha != null)
                {
                    string numeroProcesso = dgvLinha.Cells["Número do Processo"].Value.ToString();
                    string tipoPeticao = dgvLinha.Cells["Tipo de Petição"].Value.ToString();
                    string numeroDocumento = dgvLinha.Cells["Despesas Processuais"].Value.ToString();
                    Thread.Sleep(TEMPO_ESPERA);
                    InserirNumeroProcesso(numeroProcesso);
                    Thread.Sleep(TEMPO_ESPERA);
                    InserirClassificacao(tipoPeticao);
                    //Thread.Sleep(TEMPO_ESPERA);
                    //InserirDespesasProcessuais(numeroDocumento);
                    Thread.Sleep(TEMPO_ESPERA);
                    uploadArquivos = UploadArquivos(numeroProcesso, caminhoPasta);
                    //Thread.Sleep(TEMPO_ESPERA);
                    //SalvarRascunho();
                    if (!uploadArquivos)
                        GravaValorProtocoloDGV(dgvLinha, "NÃO PROTOCOLADO");
                    else
                    {
                        AssinarDocumento(senhaToken);
                        GravaValorProtocoloDGV(dgvLinha, "PROTOCOLADO");
                        GravaValorProtocoloTXT(numeroProcesso);
                    }
                    Thread.Sleep(TEMPO_ESPERA);
                    navegador.Navigate().GoToUrl(PETICAO_INTERMEDIARIA);
                    Thread.Sleep(TEMPO_ESPERA);
                }                
            }
            navegador.Close();
        }

        private void InserirNumeroProcesso(string numeroProcesso)
        {
            try
            {
                IWebElement botaoEditarDadosBasicos = GetWebDriverWait(By.Id("botaoEditarDadosBasicos"));
                botaoEditarDadosBasicos.Click();
               
                if (numeroProcesso.Length == TAMANHO_NUMERO_PROCESSO)
                    navegador.FindElement(By.Id("processoNumero")).SendKeys(numeroProcesso + Keys.Tab);
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                MessageBox.Show("Não achou o elemento Informar");
                navegador.Close();
            }
            catch (OpenQA.Selenium.ElementClickInterceptedException)
            {
                MessageBox.Show("Botão Informar não está disponível para o clique");
                navegador.Close();
            }
        }

        private void InserirClassificacao(string tipoPetição)
        {
            string campoTipoPeticao = "/html/body/span/main/div/div/div/form/div/div/div[2]/div/div/section[3]/div/div[2]/div/div[1]/div/selecao-classe/div/div/div[1]";
            if (!navegador.FindElement(By.XPath(campoTipoPeticao)).Displayed)
                navegador.FindElement(By.Id("botaoEditarClassificacao")).Click();
            
            navegador.FindElement(By.XPath(campoTipoPeticao)).Click();
            navegador.FindElement(By.Id("selectClasseIntermediaria")).SendKeys(tipoPetição + Keys.Tab);
        }
        
        private void InserirDespesasProcessuais(string numeroDocumento)
        {            
            if (!string.IsNullOrEmpty(numeroDocumento) && navegador.FindElement(By.Id("botaoEditarDespesas")).Displayed)
            {
                DespesasProcessuaisXPath despesas = new DespesasProcessuaisXPath();
                if (numeroDocumento.Length > TAMANHO_NUMERO_DARE)
                {                    
                    string[] listaNumeroDocumento = numeroDocumento.Split('-');
                    foreach (string numDoc in listaNumeroDocumento)
                    {
                        if(!string.IsNullOrEmpty(numDoc))
                        {
                            InserirDare(numDoc, despesas);                           
                        }                        
                    }
                }
                else
                {
                    InserirDare(numeroDocumento, despesas);
                }                
            }
        }

        private void InserirPoloAtivo()
        {
            //string[] dados = { "07.282.377/0001-20", "60.942.281/0001-23", "07.297.359/0001-11", "61.416.244/0001-44", "77.882.504/0002-98", "07.282.377/0001-20" };
            List<string> listCNPJ = new List<string>() {"07.282.377/0001-20", "60.942.281/0001-23", "07.297.359/0001-11", "61.416.244/0001-44", "77.882.504/0002-98", "07.282.377/0001-20"};           

        }

        private void InserirDare(string numeroDocumento, DespesasProcessuaisXPath despesas)
        {
            if (numeroDocumento.Length == TAMANHO_NUMERO_DARE)
            {
                try
                {
                    if(despesas.PrimeiraDare())
                    {
                        navegador.FindElement(By.Id("botaoEditarDespesas")).Click();
                        if (!navegador.FindElement(By.Id("guiaCustasEmitida")).Selected)
                            navegador.FindElement(By.Id("guiaCustasEmitida")).Click();
                    }
                    navegador.FindElement(By.XPath(despesas.GetBotaoDocumentoXPath())).Click();
                    navegador.FindElement(By.Id("numeroGuiaDare")).SendKeys(numeroDocumento + Keys.Tab);
                    IWebElement btnSalvarGuia = GetWebDriverWait(By.Id("btnSalvarGuia"));
                    btnSalvarGuia.Click();
                }
                catch (OpenQA.Selenium.NoSuchElementException elementException)
                {
                    MessageBox.Show("Elemento não encontrado na página: " + elementException.InnerException.Source);
                    navegador.Close();
                }
                catch(OpenQA.Selenium.ElementClickInterceptedException elementClickInterceptedException)
                {
                    MessageBox.Show("Erro: " + elementClickInterceptedException.Message);
                    navegador.Close();
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
            List<string> listArquivos = arquivos.ToList<string>();            

            if (listArquivos.Count > 0 && listArquivos.Exists(arquivo => arquivo.Contains("PETICAO")))
            {              
                int indexPeticao = listArquivos.FindIndex(arquivo => arquivo.Contains("PETICAO"));                
                
                navegador.FindElement(By.XPath("//*[@id='botaoAdicionarDocumento']/input")).SendKeys(@listArquivos[indexPeticao]);
                
                listArquivos.RemoveAt(indexPeticao);
                
                listArquivos.ForEach(delegate (string arquivo)
                {
                    navegador.FindElement(By.XPath(DocumentoXPath.INPUT_DOCUMENTO)).SendKeys(@arquivo);
                });
                
                try
                {
                    //InsereTipoDocumento
                    Thread.Sleep(TimeSpan.FromSeconds(15));
                    DocumentoXPath documentoXPath = new DocumentoXPath();
                    int posInputTipoDoc = 1;
                    listArquivos.ForEach(delegate (string arquivo)
                    {
                        IWebElement tipoPeticao = GetWebDriverWait(By.XPath(documentoXPath.GetBotaoTipoDocumento(posInputTipoDoc)));
                        tipoPeticao.Click();

                        navegador.FindElement(By.XPath(documentoXPath.GetInputTipoDocumento(posInputTipoDoc))).SendKeys(ObterNomeTipoDeDocumento(arquivo) + Keys.Tab);
                        posInputTipoDoc++;
                    });
                    return true;
                }
                catch (OpenQA.Selenium.ElementClickInterceptedException)
                {
                    return false;
                }                
            }
            else
            {
                MessageBox.Show("Não foi encontrada a Petição no diretório de arquivos");
                navegador.Quit();
                return false;
            }           
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
                AcessarCertificadoDigital(senhaToken);
            }
            catch (OpenQA.Selenium.ElementNotInteractableException)
            {
                MessageBox.Show("Nenhum certificado disponível");
            }
                        
        }

        public void LoginCertificadoDigital(string senhaToken)
        {
            try
            {
                navegador.Navigate().GoToUrl(LOGIN_ESAJ);                                            

                IWebElement abaCertificadoDigital = GetWebDriverWait(By.XPath("//*[@id='tabs']/ul/li[2]"));
                abaCertificadoDigital.Click();

                //Seleciona o certificado pelo texto
                //SelectElement selectCertificados = new SelectElement(navegador.FindElement(By.Id("certificados")));
                //selectCertificados.SelectByText("[Não ICP-Brasil] 189.9.32.218 - Validade: 26/2/2021");


                IWebElement botaoEntrar = GetWebDriverWait(By.Id("submitCertificado"));
                botaoEntrar.Click();

                AcessarCertificadoDigital(senhaToken);
            }
            catch (Exception e)
            {

            }
            
        }

        private IWebElement GetWebDriverWait(By elemento)
        {
            return new WebDriverWait(navegador, TimeSpan.FromMinutes(1)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elemento));
        }

        private bool AcessarCertificadoDigital(string senhaToken)
        {
            try
            {
                if (AutoItX.WinWaitActive("Alerta de Segurança") == 1)
                {
                    System.Drawing.Rectangle janelaAlertaSegurança = AutoItX.WinGetPos("Alerta de Segurança");

                    //Clica no no checkbox: Não me pergunte novamente para este site e certificado
                    AutoItX.MouseClick("left", janelaAlertaSegurança.X + 25, janelaAlertaSegurança.Y + 199, 1, 30);

                    //Clica no botão Permitir do WebSigner
                    AutoItX.MouseClick("left", janelaAlertaSegurança.X + 265, janelaAlertaSegurança.Y + 255, 1, 30);
                }

                if (AutoItX.WinWaitActive("Introduzir PIN") == 1)
                {
                    System.Drawing.Rectangle janelaPIN = AutoItX.WinGetPos("Introduzir PIN");

                    //Clica no input para colocar o cursor no lugar correto
                    AutoItX.MouseClick("left", janelaPIN.X + 115, janelaPIN.Y + 65, 1, 30);

                    //Envia a senha do token para o input de texto
                    AutoItX.ControlSend("Introduzir PIN", "", "", senhaToken, 0);

                    //linha abaixo clica no botão OK do após colocar a senha
                    //AutoItX.MouseClick("left", janelaPIN.X + 145, janelaPIN.Y + 140, 1, 30);
                }

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        } 
    }
}
