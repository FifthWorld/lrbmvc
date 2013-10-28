using LRB;
using LRB.Lib;
using LRB.Lib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LRBMvc.Models
{
    public class ApplicationModels
    {
    }

    public class PersonalContactInfoDTO
    {
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string StateofOrigin { get; set; }
        public string HomeTown { get; set; }
        public string LGA { get; set; }        
        public string MobileNo { get; set; }        
        public string EmployerName { get; set; }
        public string EmployerAddress { get; set; }
        public string Occupation { get; set; }

        public List<String> Titles = new List<String>(){
            "Mr", "Mrs", "Miss"
        };

        public Party ToEntity()
        {
            Party party = new Party()
            {
                Surname = Surname,
                Firstname = Firstname,
                Middlename = Middlename,
                DOB = DOB,
                StateofOrigin = StateofOrigin,
                HomeTown = HomeTown,
                LGA = LGA,
                MobileNo = MobileNo,
                EmployerName = EmployerName,
                EmployerAddress = EmployerAddress,
                Occupation= Occupation
            };
            return party;
        }
    }

    public class OrganizationContactInfoDTO
    {
        public string PhoneNo { get; set; }
        public string OrganizationName { get; set; }
        

        public Party ToEntity(Party ContactPerson)
        {
            ContactPerson.OfficeNo = PhoneNo;
            ContactPerson.OrganizationName = OrganizationName;
            return ContactPerson;
        }
    }

    public class ApplicationDTO
    {

    }

    [DataContract]
    public class DocumentDTO
    {
        public int Id { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string DocumentType { get; set; }
        [DataMember]
        public string Extension { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int appId { get; set; }
        public byte[] Content { get; set; }

        public static DocumentDTO getDTO(Document doc)
        {
            return new DocumentDTO()
            {
                Id = doc.Id,
                appId=doc.Id,
                Description=doc.Description,
                DocumentType=doc.DocumentType,
                Extension=doc.Extension,
                FileName=doc.FileName
            };
        }

        public static IEnumerable<DocumentDTO> GetDTOs (IEnumerable<Document> documents)
        {
            List<DocumentDTO> docDTOs = new List<DocumentDTO>();
            foreach (var doc in documents)
            {
                docDTOs.Add(DocumentDTO.getDTO(doc));
            }

            return docDTOs;
        }

        public Document ToEntity()
        {
            return new Document()
            {
                Application = LandRecords.GetApplication(appId),
                Content = Content,
                Description = Description,
                DocumentType = DocumentType,
                Extension = Extension,
                FileName = FileName,
                Id = Id
            };
        }

    }

}