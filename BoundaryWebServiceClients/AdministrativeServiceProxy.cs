using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using org.sola.webservices;
using org.sola.webservices.search;
using org.sola.webservices.search.extra;
using org.sola.webservices.administrative;
using org.sola.webservices.administrative.extra;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Security;

namespace org.sola.services.boundary.wsclients
{
    /// <summary>
    /// Acts as the proxy to the Administrative Service. Created as a signleton to ensure
    /// only one reference to the service exists in the client thread. 
    /// </summary>
    public sealed class AdministrativeServiceProxy : IAdministrativeService
    {
        private static readonly AdministrativeServiceProxy _instance = new AdministrativeServiceProxy();
        private String pWord; // Might what to store this in SecureString class
        private String uName;

        // Obtain the global service instance. 
        public static AdministrativeServiceProxy Instance
        {
            get
            {
                return _instance;
            }
        }

        private AdministrativeServiceProxy() { }


        #region Configure Service

        /// <summary>
        /// Configures the client with the appropriate security options.  
        /// </summary>
        /// <param name="client"></param>
        private void ConfigureClient(AdministrativeClient client)
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
            using (AdministrativeClient client = new AdministrativeClient())
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
        /// Returns the BA Unit for the specified identifier. 
        /// </summary>
        /// <returns></returns>
        public baUnitTO GetBaUnitById(string baUnitId)
        {
            baUnitTO result = null;
            using (AdministrativeClient client = new AdministrativeClient())
            {
                
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.GetBaUnitById(baUnitId);
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
        /// Saves the details of a baUnit. 
        /// </summary>
        /// <returns></returns>
        public baUnitTO SaveBaUnit(string serviceId, baUnitTO baUnit)
        {
            baUnitTO result = null;
            using (AdministrativeClient client = new AdministrativeClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.SaveBaUnit(serviceId, baUnit);
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
        /// Creates the baUnit and links it to the service indicated
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="baUnit"></param>
        /// <returns></returns>
        public baUnitTO CreateBaUnit(string serviceId, baUnitTO baUnit)
        {
            baUnitTO result = null;
            using (AdministrativeClient client = new AdministrativeClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.CreateBaUnit(serviceId, baUnit);
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
