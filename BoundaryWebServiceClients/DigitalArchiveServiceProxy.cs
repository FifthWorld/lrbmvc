using System;
using System.Security.Cryptography.X509Certificates;
using org.sola.webservices.digitalarchive;
using org.sola.webservices.digitalarchive.extra;

namespace org.sola.services.boundary.wsclients
{
    public class DigitalArchiveServiceProxy: IDigitalarchiveService
    {

        private static readonly DigitalArchiveServiceProxy _instance = new DigitalArchiveServiceProxy();
        private String pWord; // Might what to store this in SecureString class
        private String uName;

        public static DigitalArchiveServiceProxy Instance
        {
            get
            {
                return _instance;
            }
        }

        private DigitalArchiveServiceProxy() { }

        public void SetCredentials(string userName, string password)
        {
            uName = userName;
            pWord = password;
        }

        public bool CheckConnection()
        {
            bool result = false;
            using (DigitalArchiveClient client = new DigitalArchiveClient())
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

        private void ConfigureClient(DigitalArchiveClient client)
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

        public webservices.digitalarchive.extra.documentTO CreateDocument(webservices.digitalarchive.extra.documentBinaryTO documentBinary)
        {
            documentTO result = null;
            using (DigitalArchiveClient client= new DigitalArchiveClient())
            {
                ConfigureClient(client);
                result = client.CreateDocument(documentBinary);
            }
            return result;
        }

        public webservices.digitalarchive.extra.documentTO CreateDocumentFromServer(webservices.digitalarchive.extra.documentBinaryTO documentBinary, string fileName)
        {
            documentTO result = null;
            using (DigitalArchiveClient client = new DigitalArchiveClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.CreateDocument(documentBinary);
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

        public webservices.digitalarchive.extra.fileInfoTO[] GetAllFiles()
        {
            throw new NotImplementedException();
        }

        public webservices.digitalarchive.extra.documentBinaryTO GetDocument(string documentId)
        {
            documentBinaryTO result = null;
            using (DigitalArchiveClient client = new DigitalArchiveClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.GetDocument(documentId);
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

        public webservices.digitalarchive.extra.fileInfoTO getFileBinary(string fileName)
        {
            throw new NotImplementedException();
        }

        public webservices.digitalarchive.extra.fileBinaryTO GetFileThumbnail(string fileName)
        {
            throw new NotImplementedException();
        }

        public webservices.digitalarchive.extra.documentTO SaveDocument(webservices.digitalarchive.extra.documentTO document)
        {
            documentTO result = null;
            using (DigitalArchiveClient client = new DigitalArchiveClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.SaveDocument(document);
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
    }
}
