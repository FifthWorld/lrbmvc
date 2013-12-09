using org.sola.services.boundary.wsclients;
using org.sola.webservices.casemanagement.extra;
using org.sola.webservices.digitalarchive.extra;
using System;

namespace ApplicationLibrary
{
    public class SolaDocService
    {
        string username, password;
        public SolaDocService(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public SolaDocService()
        {
            
        }

        /*
         * Makes a SourceTO object out of a documentBinaryTO and the Owner,
         * Useful when attaching SourceTO to application
         */
        public sourceTO makeSourceTO(byte[] content, string fileExtension, string fileDescription, String Ownername)
        {
            
            string fileName = UploadDocument(content);
            documentBinaryTO doc = digitizeDocument(fileName, fileExtension, fileDescription);
            sourceTO srcTO = new sourceTO()
            {
                submission = DateTime.Now,
                archiveDocument = new org.sola.webservices.casemanagement.extra.documentTO() { description = doc.description, extension = doc.extension, id = doc.id, nr = doc.nr, rowId = doc.rowId, rowVersion = doc.rowVersion },
                typeCode = doc.description,
                content = doc.description,
                ownerName = Ownername,
                mainType = "documentDigital",
                laNr = "landonline",
                availabilityStatusCode = "available",
                referenceNr = "solaDoc/" + doc.fileName,
                recordation = DateTime.Now,
                recordationSpecified = true
            };

            return srcTO;
        }

        private string UploadDocument(byte[] document)
        {
            IFilestreamingService filestreamingService = FilestreamingServiceProxy.Instance;
            filestreamingService.SetCredentials(username, password);
            string fileName = filestreamingService.Upload(document);

            
            return fileName;
        }

        private documentBinaryTO digitizeDocument(string fileName, string fileExtension, string fileDescription)
        {
            IDigitalarchiveService digitalArchiveService = DigitalArchiveServiceProxy.Instance;
            digitalArchiveService.SetCredentials(username, password);
            var document = digitalArchiveService.CreateDocumentFromServer(new org.sola.webservices.digitalarchive.extra.documentBinaryTO()
            {
                fileName = fileName,
                description = fileDescription,
                extension = getExtension(fileExtension),
            }, fileName);

            return digitalArchiveService.GetDocument(document.id);
        }

        /* This is a helper method that takes in the extension of a file and returns the content Type
         */
        private string getExtension(string contentType)
        {
            switch (contentType)
            {
                case "text/plain":
                    return "txt";
                case "application/pdf":
                    return "pdf";
                case "image/jpeg":
                    return "jpeg";
                case "image/png":
                    return "png";
                case "application/msword":
                    return "doc";
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                    return "docx";
                case "application/vnd.ms-excel":
                    return "xls";
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                    return "xlsx";
                default:
                    return "txt";
            }
        }
    }
}
