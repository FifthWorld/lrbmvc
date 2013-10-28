using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using org.sola.webservices.filestreaming;

namespace org.sola.services.boundary.wsclients
{
    public class FilestreamingServiceProxy: IFilestreamingService
    {
        private static readonly IFilestreamingService _instance = new FilestreamingServiceProxy();
        private String pWord; // Might what to store this in SecureString class
        private String uName;

        public static IFilestreamingService Instance
        {
            get
            {
                return _instance;
            }
        }

        private FilestreamingServiceProxy() { }

        public void SetCredentials(string userName, string password)
        {
            uName = userName;
            pWord = password;
        }

        public bool CheckConnection()
        {
            bool result = false;
            using (FileStreamingClient client = new FileStreamingClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.CheckConnection();
                    client.Close();
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw ex;
                }
            }
            return result;
        }

        private void ConfigureClient(FileStreamingClient client)
        {
            // Load the certificate from the Project Resources. 

            // Note that this certificate has been generated from the default SOLA Development keystore.jks. Instructions on how to 
            // generate the cert using the Java keytool can be found here. 
            // http://stackoverflow.com/questions/5529541/developing-a-net-client-that-consumes-a-secure-metro-2-1-web-service
            // Note that it is also necessary to add DNS identity configuration to the app.config. The default DNS identity in this
            // certificate is xwssecurityserver. 
            X509Certificate2 cert = new X509Certificate2(org.sola.services.boundary.wsclients.Properties.Resources.ServerCertificate);
            client.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
            client.ClientCredentials.UserName.UserName = uName;
            client.ClientCredentials.UserName.Password = pWord;
        }

        public string Upload(byte[] fileContent)
        {
            string result = null;
            using (FileStreamingClient client= new FileStreamingClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.Upload(fileContent);
                    client.Close();
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw ex;
                }
            }
            return result;
        }

        public string Download(string pathFileName)
        {
            throw new NotImplementedException();
        }
    }
}
