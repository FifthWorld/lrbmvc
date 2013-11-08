//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LRB.Lib.Domain
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using SimpleSecurity;
    
    public class ApplicationType
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
    
    public partial class Application
    {
        public Application()
        {
            this.Properties = new HashSet<Property>();
            this.Parties = new HashSet<Party>();
            this.Documents = new HashSet<Document>();
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string UserId { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<bool> SubmittedbyApplicant { get; set; }

        public string OtherRelevantInfo { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> SubmissionDate { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime StartDate { get; set; }

        [ScaffoldColumn(false)]
        public String Status { get; set; }

        [DisplayName("Please select the Type of Application")]
        public string ApplicationType { get; set; }

        [ScaffoldColumn(false)]
        public String SolaId { get; set; }
        
        public Party ContactPerson
        {
            get
            {
                Party party = this.Parties.Where(p=>p.PartyType=="ContactPerson").FirstOrDefault();
                if (null != party)
                {
                    return party;
                }
                var user = WebSecurity.GetCurrentUser();
                party = new Party() { 

                };
                this.Parties.Add(party);
                return party;
            }
        }

        public Property PrimaryProperty
        {
            get
            {
                Property property = this.Properties.FirstOrDefault();
                if (null != property)
                {
                    return property;
                }
                property = new Property() { 

                };
                this.Properties.Add(property);
                return property;
            }
        }
    
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Party> Parties { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
