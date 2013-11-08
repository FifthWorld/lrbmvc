using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using org.sola.webservices.referencedata;
using org.sola.webservices.referencedata.extra;

namespace org.sola.services.boundary.wsclients
{
    public class ReferenceDataServiceProxy : IReferencedataService
    {
        private static readonly ReferenceDataServiceProxy _instance = new ReferenceDataServiceProxy();
        private String pWord; // Might what to store this in SecureString class
        private String uName;
        private readonly string en = "it";

        public static ReferenceDataServiceProxy Instance
        {
            get
            {
                return _instance;
            }
        }

        private ReferenceDataServiceProxy() { }

        public void SetCredentials(string userName, string password)
        {
            uName = userName;
            pWord = password;
        }

        public bool CheckConnection()
        {
            bool result = false;
            using (ReferenceDataClient client = new ReferenceDataClient())
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

        private void ConfigureClient(ReferenceDataClient client)
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

        applicationStatusTypeTO[] IReferencedataService.GetApplicationStatusTypes()
        {
            applicationStatusTypeTO[] result = null;
            using (ReferenceDataClient client = new ReferenceDataClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.GetApplicationStatusTypes(en);
                    client.Close();
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw ex;
                }
                return result;
            }
        }

        communicationTypeTO[] IReferencedataService.GetCommunicationTypes()
        {
            communicationTypeTO[] result = null;
            using (ReferenceDataClient client = new ReferenceDataClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.GetCommunicationTypes(en);
                    client.Close();
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw ex;
                }
                return result;
            }
        }

        genderTypeTO[] IReferencedataService.GetGenderTypes()
        {
            genderTypeTO[] result = null;
            using (ReferenceDataClient client = new ReferenceDataClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.GetGenderTypes(en);
                    client.Close();
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw ex;
                }
                return result;
            }
        }

        partyRoleTypeTO[] IReferencedataService.GetPartyRoleTypes()
        {
            partyRoleTypeTO[] result = null;
            using (ReferenceDataClient client = new ReferenceDataClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.GetPartyRoles(en);
                    client.Close();
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw ex;
                }
                return result;
            }
        }

        partyTypeTO[] IReferencedataService.GetPartyTypes()
        {
            partyTypeTO[] result = null;
            using (ReferenceDataClient client = new ReferenceDataClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.GetPartyTypes(en);
                    client.Close();
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw ex;
                }
                return result;
            }
        }

        registrationStatusTypeTO[] IReferencedataService.GetRegistrationTypes()
        {
            registrationStatusTypeTO[] result = null;
            using (ReferenceDataClient client = new ReferenceDataClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.GetRegistrationStatusTypes(en);
                    client.Close();
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw ex;
                }
                return result;
            }
        }

        requestTypeTO[] IReferencedataService.GetRequestTypes()
        {
            requestTypeTO[] result = null;
            using (ReferenceDataClient client = new ReferenceDataClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.GetRequestTypes(en);
                    client.Close();
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw ex;
                }
                return result;
            }
        }

        sourceTypeTO[] IReferencedataService.GetSourceTypes()
        {
            sourceTypeTO[] result = null;
            using (ReferenceDataClient client = new ReferenceDataClient())
            {
                ConfigureClient(client);
                try
                {
                    client.Open();
                    result = client.GetSourceTypes(en);
                    client.Close();
                }
                catch (Exception ex)
                {
                    client.Abort();
                    throw ex;
                }
                return result;
            }
        }

        //    capacityTypeTO[] IReferencedataService.GetCapacityTypes()
        //    {
        //        capacityTypeTO[] result = null;
        //        using (ReferenceDataClient client = new ReferenceDataClient())
        //        {
        //            ConfigureClient(client);
        //            try
        //            {
        //                client.Open();
        //                result = client.GetCapacityTypes(en);
        //                client.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                client.Abort();
        //                throw ex;
        //            }
        //            return result;
        //        }
        //    }

        //    homeTownTypeTO[] IReferencedataService.GetHomeTownTypes()
        //    {
        //        homeTownTypeTO[] result = null;
        //        using (ReferenceDataClient client = new ReferenceDataClient())
        //        {
        //            ConfigureClient(client);
        //            try
        //            {
        //                client.Open();
        //                result = client.GetHomeTownTypes(en);
        //                client.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                client.Abort();
        //                throw ex;
        //            }
        //            return result;
        //        }
        //    }

        //    lgaTypeTO[] IReferencedataService.GetLgaTypes()
        //    {
        //        lgaTypeTO[] result = null;
        //        using (ReferenceDataClient client = new ReferenceDataClient())
        //        {
        //            ConfigureClient(client);
        //            try
        //            {
        //                client.Open();
        //                result = client.GetLgaTypes(en);
        //                client.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                client.Abort();
        //                throw ex;
        //            }
        //            return result;
        //        }
        //    }

        //    occupationTypeTO[] IReferencedataService.GetOccupationTypes()
        //    {
        //        occupationTypeTO[] result = null;
        //        using (ReferenceDataClient client = new ReferenceDataClient())
        //        {
        //            ConfigureClient(client);
        //            try
        //            {
        //                client.Open();
        //                result = client.GetOccupationTypes(en);
        //                client.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                client.Abort();
        //                throw ex;
        //            }
        //            return result;
        //        }
        //    }

        //    propertyTypeTO[] IReferencedataService.GetPropertyTypes()
        //    {
        //        propertyTypeTO[] result = null;
        //        using (ReferenceDataClient client = new ReferenceDataClient())
        //        {
        //            ConfigureClient(client);
        //            try
        //            {
        //                client.Open();
        //                result = client.GetPropertyTypes(en);
        //                client.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                client.Abort();
        //                throw ex;
        //            }
        //            return result;
        //        }
        //    }

        //    stateTypeTO[] IReferencedataService.GetStateTypes()
        //    {
        //        stateTypeTO[] result = null;
        //        using (ReferenceDataClient client = new ReferenceDataClient())
        //        {
        //            ConfigureClient(client);
        //            try
        //            {
        //                client.Open();
        //                result = client.GetStateTypes(en);
        //                client.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                client.Abort();
        //                throw ex;
        //            }
        //            return result;
        //        }
        //    }

        //    titleTypeTO[] IReferencedataService.GetTitleTypes()
        //    {
        //        titleTypeTO[] result = null;
        //        using (ReferenceDataClient client = new ReferenceDataClient())
        //        {
        //            ConfigureClient(client);
        //            try
        //            {
        //                client.Open();
        //                result = client.GetTitleTypes(en);
        //                client.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                client.Abort();
        //                throw ex;
        //            }
        //            return result;
        //        }
        //    }

        //    useTypeTO[] IReferencedataService.GetUseTypes()
        //    {
        //        useTypeTO[] result = null;
        //        using (ReferenceDataClient client = new ReferenceDataClient())
        //        {
        //            ConfigureClient(client);
        //            try
        //            {
        //                client.Open();
        //                result = client.GetUseTypes(en);
        //                client.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                client.Abort();
        //                throw ex;
        //            }
        //            return result;
        //        }
        //    }
        //}

    }
}