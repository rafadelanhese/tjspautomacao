using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TjspAutomacao.Classe
{
    class Protocolo
    {
        private readonly string ESAJ = "https://eesaj.tjsp.jus.br/sajcas/login?service=https%3A%2F%2Fesaj.tjsp.jus.br%2Fpetpg%2Fj_spring_cas_security_check";
        private readonly string PETICAO_INICIAL = "https://esaj.tjsp.jus.br/petpg/abrirNovaPeticaoInicial.do?instancia=PG";
        private IWebDriver navegador;

        public Protocolo()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;            
            this.navegador = new ChromeDriver(chromeOptions);
        }
       
        public void AbrirURL(string url)
        {            
            try
            {
                if(url.Equals("ESAJ"))
                    navegador.Navigate().GoToUrl(ESAJ);
                if (url.Equals("PETICAO_INICIAL"))
                    navegador.Navigate().GoToUrl(PETICAO_INICIAL);
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

        public void UploadArquivos()
        {
            
        }
    }
}
