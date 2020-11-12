using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = OpenQA.Selenium.Keys;

namespace TjspAutomacao.Classe
{
    class Protocolo
    {
        private readonly string ESAJ = "https://esaj.tjsp.jus.br/sajcas/login?service=https%3A%2F%2Fesaj.tjsp.jus.br%2Fpetpg%2Fj_spring_cas_security_check";
        private readonly string PETICAO_CONSULTA = "https://esaj.tjsp.jus.br/petpg/peticoes/intermediaria";
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
            int posNumProcesso = 0;
            for (int posLinha = 0; posLinha < dgvProcessos.Rows.Count - 1; posLinha++)
            {
                string numeroProcesso = dgvProcessos.Rows[posLinha].Cells[posNumProcesso].Value.ToString();
                UploadArquivos(numeroProcesso, caminhoPasta);
            }
        }
        private void UploadArquivos(string numeroProcesso, string caminhoArquivos)
        {
            string[] arquivos = Directory.GetFiles(caminhoArquivos, "*.pdf");
            bool addPrimeiroArq = false;
            IWebElement upload;

            var allowsDetection = this.navegador as IAllowsFileDetection;
            if (allowsDetection != null)
            {
                allowsDetection.FileDetector = new LocalFileDetector();
            }
            
            foreach (string arq in arquivos)
            {
                
                if (arq.Contains(numeroProcesso))
                {
                    Thread.Sleep(5000);
                    if (!addPrimeiroArq)
                    {                        
                        upload = navegador.FindElement(By.Id("botaoAdicionarDocumento"));
                        addPrimeiroArq = !addPrimeiroArq;
                    }
                    else
                        upload = navegador.FindElement(By.ClassName("button__add"));
                    
                    upload.Click();
                    Thread.Sleep(2000);
                    SendKeys.SendWait(arq);
                    SendKeys.SendWait("{Enter}");
                    Console.WriteLine(arq);
                    Thread.Sleep(2000);                                      
                }                
            }
        }
    }
}
