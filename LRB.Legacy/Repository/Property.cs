//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LRB.Legacy.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Property
    {
        public Property()
        {
            this.LandOwners = new HashSet<LandOwner>();
            this.Transactions = new HashSet<Transaction>();
        }
    
        public int Id { get; set; }
        public string fileno { get; set; }
        public string doe { get; set; }
        public string owner_type { get; set; }
        public string blockno { get; set; }
        public string plotno { get; set; }
        public string houseno { get; set; }
        public string location { get; set; }
        public string town { get; set; }
        public string lga { get; set; }
        public string industryname { get; set; }
        public string typeofuse { get; set; }
        public string lud { get; set; }
        public string effdate { get; set; }
        public string rvalue { get; set; }
        public string revp { get; set; }
        public string premium { get; set; }
        public string rent { get; set; }
        public string areasize { get; set; }
        public string areaunit { get; set; }
        public string sda { get; set; }
        public string spn { get; set; }
        public string prkno { get; set; }
        public string dod { get; set; }
        public string drv { get; set; }
        public string drn { get; set; }
        public string drp { get; set; }
    
        public virtual ICollection<LandOwner> LandOwners { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}