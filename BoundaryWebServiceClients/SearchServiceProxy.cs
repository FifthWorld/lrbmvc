using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.sola.webservices.search;
using org.sola.webservices.search.extra;
using System.Security.Cryptography.X509Certificates;

namespace org.sola.services.boundary.wsclients
{
  
        /// <summary>
    /// Acts as the proxy to the Search Service. Created as a signleton to ensure
    /// only one reference to the service exists in the client thread. 
    /// </summary>
    public sealed class SearchServiceProxy : ISearchService
    {
        private static readonly SearchServiceProxy _instance = new SearchServiceProxy();
        private String pWord; // Might what to store this in SecureString class
        private String uName;

        // Obtain the global service instance. 
        public static SearchServiceProxy Instance
        {
            get
            {
                return _instance;
            }
        }

        private SearchServiceProxy() { }


        #region Configure Service

        /// <summary>
        /// Configures the client with the appropriate security options.  
        /// </summary>
        /// <param name="client"></param>
        private void ConfigureClient(SearchClient client)
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

        #endregion Configure Service

        /// <summary>
        /// Set the username and password credentails to use when connecting to the web services. 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public void SetCredentials(string userName, string password)
        {
            uName = userName;
            pWord = password;
        }

        /// <summary>
        /// Returns true of the web service client is able to successfully connect to the target web service. 
        /// </summary>
        /// <returns></returns>
        public bool CheckConnection()
        {
            bool result = false;
            using (SearchClient client = new SearchClient())
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

        /// <summary>
        /// Retrieves all active users from the database
        /// </summary>
        /// <returns></returns>
        public userSearchResultTO[] GetActiveUsers()
        {
            userSearchResultTO[] result;
            using (SearchClient client = new SearchClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.GetActiveUsers(); 
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
