using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using SimpleSecurity.Repositories;
using WebMatrix.WebData;

namespace SimpleSecurity
{
    public class InitSecurityDb : DropCreateDatabaseIfModelChanges<SecurityContext>
    {
        protected override void Seed(SecurityContext context)
        {

            WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection("DefaultConnection",
               "UserProfile", "UserId", "UserName", autoCreateTables: true);
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (membership.GetUser("test", false) == null)
            {
                membership.CreateUserAndAccount("test", "test");
            }
            if (!roles.GetRolesForUser("test").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "test" }, new[] { "admin" });
            }
            if (membership.GetUser("joe", false) == null)
            {
                membership.CreateUserAndAccount("joe", "test");
            }

        }
    }
}