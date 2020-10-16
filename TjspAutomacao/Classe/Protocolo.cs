using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjspAutomacao.Classe
{
    class Protocolo
    {
        private IWebDriver navegador;

        public Protocolo()
        {
            this.navegador = new ChromeDriver();
        }

        public void AbreTJSP()
        {
            navegador.Navigate().GoToUrl("https://esaj.tjsp.jus.br/sajcas/login?service=https%3A%2F%2Fesaj.tjsp.jus.br%2Fpetpg%2Fj_spring_cas_security_check");                        
        }

        public void Login(string cpf, string senha)
        {
            navegador.FindElement(By.Id("usernameForm")).SendKeys(cpf);
            navegador.FindElement(By.Id("passwordForm")).SendKeys(senha);
        }
    }
}
