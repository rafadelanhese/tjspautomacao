﻿using AutoIt;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;
using System.IO;
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
        private readonly string PETICAO_INTERMEDIARIA = "https://esaj.tjsp.jus.br/petpg/peticoes/intermediaria";
        private readonly int TEMPO_ESPERA = 4000;
        private readonly int TAMANHO_NUMERO_DESPPROCESSUAIS = 19;
        private readonly int TAMANHO_NUMERO_PROCESSO = 20;
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
                    Thread.Sleep(TEMPO_ESPERA);
                    InserirDespesasProcessuais(numeroDocumento);
                    Thread.Sleep(TEMPO_ESPERA);
                    uploadArquivos = UploadArquivos(numeroProcesso, caminhoPasta);
                    Thread.Sleep(TEMPO_ESPERA);
                    SalvarRascunho();
                    if (!uploadArquivos)
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
            }
            navegador.Close();
        }

        private void InserirNumeroProcesso(string numeroProcesso)
        {
            try
            {
                navegador.FindElement(By.Id("botaoEditarDadosBasicos")).Click();
                if (numeroProcesso.Length == TAMANHO_NUMERO_PROCESSO)
                    navegador.FindElement(By.Id("processoNumero")).SendKeys(numeroProcesso + Keys.Tab);
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {

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
                if(numeroDocumento.Length > TAMANHO_NUMERO_DESPPROCESSUAIS)
                {
                    bool inseriuPrimeiraDare = true;
                    string[] listaNumeroDocumento = numeroDocumento.Split(' ');
                    foreach (string numDoc in listaNumeroDocumento)
                    {
                        if(!string.IsNullOrEmpty(numDoc))
                        {
                            InsereDare(numDoc, inseriuPrimeiraDare);
                            inseriuPrimeiraDare = false;
                        }                        
                    }
                }
                else
                {
                    InsereDare(numeroDocumento, true);
                }                                  
            }
        }

        private void InsereDare(string numeroDocumento, bool primeiraDare)
        {
            if (numeroDocumento.Length == TAMANHO_NUMERO_DESPPROCESSUAIS)
            {
                try
                {
                    if(primeiraDare)
                    {
                        navegador.FindElement(By.Id("botaoEditarDespesas")).Click();
                        navegador.FindElement(By.Id("guiaCustasEmitida")).Click();
                    }                                        
                    navegador.FindElement(By.XPath("//*[@id='secaoGuiaCustas']/div/div/button")).Click();
                    navegador.FindElement(By.Id("numeroGuiaDare")).SendKeys(numeroDocumento + Keys.Tab);
                    Thread.Sleep(TEMPO_ESPERA);
                    navegador.FindElement(By.Id("btnSalvarGuia")).Click();                    
                }
                catch (OpenQA.Selenium.NoSuchElementException elementException)
                {
                    MessageBox.Show("Elemento não encontrado na página: " + elementException.InnerException.Source);
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
                            InsereDiretorioArquivo(arq);
                        }
                        else
                        {                            
                            try
                            {
                                navegador.FindElement(By.ClassName("button__add")).Click();
                                InsereDiretorioArquivo(arq);
                                IWebElement xpathTipoPeticao = new WebDriverWait(navegador, TimeSpan.FromMinutes(1)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath(DocumentoXPath.ElementoClicavel(posArquivo))));
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

        private void InsereDiretorioArquivo(string arquivo)
        {
            Thread.Sleep(TEMPO_ESPERA);
            SendKeys.SendWait(arquivo);
            SendKeys.SendWait("{Enter}");
            Thread.Sleep(TEMPO_ESPERA);
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

        public void LoginCertificadoDigital()
        {

            try
            {
                navegador.Navigate().GoToUrl("https://esaj.tjsp.jus.br/sajcas/login?service=https%3A%2F%2Fesaj.tjsp.jus.br%2Fesaj%2Fj_spring_cas_security_check");
                Thread.Sleep(TEMPO_ESPERA + 5000);
                navegador.FindElement(By.XPath("//*[@id='tabs']/ul/li[2]")).Click();

                Thread.Sleep(TEMPO_ESPERA + 2000);
                navegador.FindElement(By.Id("submitCertificado")).Click();
                Thread.Sleep(TEMPO_ESPERA);
                if (AutoItX.WinWaitActive("Alerta de Segurança") == 1)
                {
                    System.Drawing.Rectangle janelaAlertaSegurança = AutoItX.WinGetPos("Alerta de Segurança");
                    Thread.Sleep(TEMPO_ESPERA);
                    AutoItX.MouseClick("left", janelaAlertaSegurança.X + 265, janelaAlertaSegurança.Y + 255, 1);
                    //Cursor.Position = new Point(rectangle.X + 265, rectangle.Y + 255);                                
                    if (AutoItX.WinWaitActive("Introduzir PIN") == 1)
                    {
                        Thread.Sleep(TEMPO_ESPERA);
                        System.Drawing.Rectangle janelaPIN = AutoItX.WinGetPos("Introduzir PIN");
                        Cursor.Position = new Point(janelaPIN.X + 145, janelaPIN.Y + 140);
                        AutoItX.ControlSend("Introduzir PIN", "", "", "123456789", 0);
                        //linha abaixo clica no botão OK do após colocar a senha
                        //AutoItX.MouseClick("left", janelaPIN.X + 145, janelaPIN.Y + 140, 1);
                    }
                }
            }
            catch (Exception e)
            {

            }
            
        }
    }
}
