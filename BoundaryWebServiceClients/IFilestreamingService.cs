using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.sola.services.boundary.wsclients
{
    public interface IFilestreamingService
    {
        void SetCredentials(string userName, string password);
        bool CheckConnection();
        string Upload(byte[] fileContent);
        string Download(string pathFileName);
    }
}
