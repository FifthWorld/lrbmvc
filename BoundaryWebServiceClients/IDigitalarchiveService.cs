using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.sola.webservices.digitalarchive.extra;

namespace org.sola.services.boundary.wsclients
{
    public interface IDigitalarchiveService
    {
        void SetCredentials(string userName, string password);
        bool CheckConnection();
        documentTO CreateDocument(documentBinaryTO documentBinary);
        documentTO CreateDocumentFromServer(documentBinaryTO documentBinary, string fileName);
        fileInfoTO[] GetAllFiles();
        documentBinaryTO GetDocument(string documentId);
        fileInfoTO getFileBinary(string fileName);
        fileBinaryTO GetFileThumbnail(string fileName);
        documentTO SaveDocument(documentTO document);
    }
}
