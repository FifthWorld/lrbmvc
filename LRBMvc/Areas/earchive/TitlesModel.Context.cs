﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LRBMvc.Areas.earchive
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LandTitles : DbContext
    {
        public LandTitles()
            : base("name=LandTitles")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<LandOwner> LandOwners { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
