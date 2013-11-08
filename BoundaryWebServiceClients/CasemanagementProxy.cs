using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using org.sola.webservices.casemanagement;
using org.sola.webservices.casemanagement.extra;

namespace org.sola.services.boundary.wsclients
{
    public sealed class CasemanagementProxy : ICaseManagementService
    {

        private static readonly CasemanagementProxy _instance = new CasemanagementProxy();
        private String pWord; // Might what to store this in SecureString class
        private String uName;

        public static CasemanagementProxy Instance
        {
            get
            {
                return _instance;
            }
        }

        private CasemanagementProxy() { }

        public void SetCredentials(string userName, string password)
        {
            uName = userName;
            pWord = password;
        }

        public bool CheckConnection()
        {
            bool result = false;
            using (CaseManagementClient client = new CaseManagementClient())
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

        private void ConfigureClient(CaseManagementClient client)
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

        public webservices.casemanagement.extra.partyTO SaveParty(webservices.casemanagement.extra.partyTO party)
        {
            partyTO result = null;
            using (CaseManagementClient client = new CaseManagementClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.SaveParty(party);
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

        public webservices.casemanagement.extra.sourceTO SaveSource(webservices.casemanagement.extra.sourceTO source)
        {
            throw new NotImplementedException();
        }


        public applicationTO SaveApplication(applicationTO application)
        {
            applicationTO result = null;
            using (CaseManagementClient client = new CaseManagementClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.SaveApplication(application);
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

        public applicationTO CreateApplication(applicationTO application)
        {
            applicationTO result = null;
            using (CaseManagementClient client = new CaseManagementClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.CreateApplication(application);
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

        public applicationTO GetApplication(string id)
        {
            applicationTO result = null;
            using (CaseManagementClient client= new CaseManagementClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.GetApplication(id);
                    client.Close();
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw;
                }
            }
            return result;
        }

        public applicationTO GetApplicationViaTransactionId(string id)
        {
            applicationTO result = null;
            using (CaseManagementClient client = new CaseManagementClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.getApplicationByTransactionId(id);
                    client.Close();
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw;
                }
            }
            return result;
        }

    }
}
