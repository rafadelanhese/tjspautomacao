using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Keys = OpenQA.Selenium.Keys;

namespace TjspAutomacao.Classe
{
    class Protocolo
    {
        private readonly string ESAJ = "https://esaj.tjsp.jus.br/sajcas/login?service=https%3A%2F%2Fesaj.tjsp.jus.br%2Fpetpg%2Fj_spring_cas_security_check";
        private readonly string PETICAO_CONSULTA = "https://esaj.tjsp.jus.br/petpg/peticoes/intermediaria";
        private readonly int TEMPO_ESPERA = 3000;
        private IWebDriver navegador;

        public Protocolo()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;            
            this.navegador = new ChromeDriver(chromeOptions);
            this.navegador.Manage().Window.Maximize();
        }
       
        public void AbrirURL(string url)
        {            
            try
            {
                if(url.Equals("ESAJ"))
                    navegador.Navigate().GoToUrl(ESAJ);
                if (url.Equals("PETICAO_INTERMEDIARIA"))
                {                    
                    navegador.Navigate().GoToUrl(PETICAO_CONSULTA);
                }
            }
            catch (OpenQA.Selenium.WebDriverException webDriverException)
            {
                MessageBox.Show(webDriverException.Message);                
            }            
        }

        public Boolean Login(string cpf, string senha)
        {
            Boolean metodoExecutado;

            if(string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(senha))
            {
                MessageBox.Show("Login ou senha é nulo ou está em branco");
                metodoExecutado = false;
            }
            else
            {
                navegador.FindElement(By.Id("usernameForm")).SendKeys(cpf);
                navegador.FindElement(By.Id("passwordForm")).SendKeys(senha);
                navegador.FindElement(By.Id("pbEntrar")).Click();
                metodoExecutado = true;
            }

            return metodoExecutado;
        }        

        public void Protocolar(DataGridView dgvProcessos, string caminhoPasta)
        {
            foreach(DataGridViewRow dgvLinha in dgvProcessos.Rows)
            {
                string numeroProcesso = dgvLinha.Cells["Número do Processo"].Value.ToString();
                string tipoPeticao = dgvLinha.Cells["Tipo de Petição"].Value.ToString();               
                Thread.Sleep(TEMPO_ESPERA);
                InserirNumeroProcesso(numeroProcesso);
                Thread.Sleep(TEMPO_ESPERA);
                InserirClassificacao(tipoPeticao);
                Thread.Sleep(TEMPO_ESPERA);
                UploadArquivos(numeroProcesso, caminhoPasta);
            }           
        }

        private void InserirNumeroProcesso(string numeroProcesso)
        {            
            navegador.FindElement(By.Id("botaoEditarDadosBasicos")).Click();
            navegador.FindElement(By.Id("processoNumero")).SendKeys(numeroProcesso + Keys.Tab);
        }

        private void InserirClassificacao(string tipoPetição)
        {
            string campoTipoPeticao = "/html/body/span/main/div/div/div/form/div/div/div[2]/div/div/section[3]/div/div[2]/div/div[1]/div/selecao-classe/div/div/div[1]";
            if (!navegador.FindElement(By.XPath(campoTipoPeticao)).Displayed)
                navegador.FindElement(By.Id("botaoEditarClassificacao")).Click();
            
            navegador.FindElement(By.XPath(campoTipoPeticao)).Click();
            navegador.FindElement(By.Id("selectClasseIntermediaria")).SendKeys(tipoPetição + Keys.Tab);
        }
        private void UploadArquivos(string numeroProcesso, string caminhoArquivos)
        {
            string[] arquivos = Directory.GetFiles(caminhoArquivos, "*.pdf");                       

            var allowsDetection = this.navegador as IAllowsFileDetection;
            if (allowsDetection != null)
            {
                allowsDetection.FileDetector = new LocalFileDetector();
            }
            
            foreach (string arq in arquivos)
            {                
                if (arq.Contains(numeroProcesso))
                {   
                    try
                    {
                        navegador.FindElement(By.Id("botaoAdicionarDocumento")).Click();
                    }
                    catch(OpenQA.Selenium.NoSuchElementException ex)
                    {                        
                        navegador.FindElement(By.ClassName("button__add")).Click();
                    }
                    //if (navegador.FindElement(By.Id("botaoAdicionarDocumento")).Displayed) 
                        
                    //else
                    //    navegador.FindElement(By.ClassName("button__add")).Click();
                                       
                    Thread.Sleep(TEMPO_ESPERA);
                    SendKeys.SendWait(arq);
                    SendKeys.SendWait("{Enter}");                   
                    Thread.Sleep(TEMPO_ESPERA);                                      
                }                
            }
        }
    }
}
