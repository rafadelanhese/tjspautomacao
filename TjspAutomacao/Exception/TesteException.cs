namespace TjspAutomacao.Exception
{
    public class TesteException : OpenQA.Selenium.NotFoundException
    {
        public TesteException()
        {
        }

        public TesteException(string message) : base(message)
        {            
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
