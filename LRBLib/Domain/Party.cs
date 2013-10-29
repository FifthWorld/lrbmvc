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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
    
    public partial class Party
    {
        public Party()
        {
            this.Gender = "Female";
            this.Addresses = new HashSet<Address>();
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [DisplayName("Surname")]
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        [DisplayName("First name")]
        [Required(ErrorMessage = "First name is required")]
        public string Firstname { get; set; }

        [DisplayName("Middle name")]
        public string Middlename { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DOB { get; set; }

        [DisplayName("State of Origin")]
        public string StateofOrigin { get; set; }

        [DisplayName("Home town")]
        public string HomeTown { get; set; }

        [DisplayName("Local Government Area of Applicant")]
        public string LGA { get; set; }

        [DisplayName("Office Number(If Registering for an organization)")]
        public string OfficeNo { get; set; }

        [DisplayName("Mobile / GSM Number")]
        [Required(ErrorMessage = "Phone Number is required")]
        public string MobileNo { get; set; }

        [DisplayName("Home / Land Line Number")]
        public string HomeNo { get; set; }

        [DisplayName("Fax")]
        [ScaffoldColumn(false)]
        public string Fax { get; set; }

        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Address is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public string PartyType { get; set; }

        [DisplayName("Organization Name")]
        public string OrganizationName { get; set; }

        [DisplayName("Occupation of Applicant")]
        public string Occupation { get; set; }

        [DisplayName("Name of Previous or Current Employer")]
        public string EmployerName { get; set; }

        [DisplayName("Address of The Employer Named Above")]
        public string EmployerAddress { get; set; }
    
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual Application Application { get; set; }


        public string FullName
        {
            get
            {
                return Firstname + ", " + Surname;
            }
        }
    }
}
