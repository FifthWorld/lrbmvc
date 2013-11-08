using LRB.Lib.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LRB.Lib.Repositories
{
    public class LandsContext:DbContext
    {
        public LandsContext()
            : base("name=LandsDBContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //throw new UnintentionalCodeFirstException();
        }

        public DbSet<Party> Parties { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DocumentManager> DocumentManagers { get; set; }
    }
}