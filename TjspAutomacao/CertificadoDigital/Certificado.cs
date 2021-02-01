using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace TjspAutomacao.CertificadoDigital
{
    class Certificado
    {
        private X509Certificate Certificate = new X509Certificate();

        public Certificado()
        {

        }
        public List<string> GetAllCertificates()
        {
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;

            List<string> listCertificados = new List<string>();
            foreach (X509Certificate2 cert in collection)
            {

                if (!string.IsNullOrEmpty(cert.FriendlyName))
                {
                    int indiceParenteses = cert.FriendlyName.IndexOf('(');                   
                    listCertificados.Add(string.Concat(cert.FriendlyName.Remove(indiceParenteses), "- Validade: " + cert.GetExpirationDateString().Substring(0, 11)));
                }

            }
            store.Close();

            return listCertificados;
        }
    }
}
