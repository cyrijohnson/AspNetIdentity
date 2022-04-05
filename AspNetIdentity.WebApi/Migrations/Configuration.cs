namespace AspNetIdentity.WebApi.Migrations
{
    using AspNetIdentity.WebApi.Infrastructure;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AspNetIdentity.WebApi.Infrastructure.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AspNetIdentity.WebApi.Infrastructure.ApplicationDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "copadmin@cop.com",
                Email = "copadmin@cop.com",
                EmailConfirmed = true,
                FirstName = "COP",
                LastName = "Admin",
                Level = 1,
                JoinDate = DateTime.Now
            };

            manager.Create(user, "4bXqrYAvs4NGd+%*");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
                roleManager.Create(new IdentityRole { Name = "TopMgmt" });
                roleManager.Create(new IdentityRole { Name = "BlkCoor" });
                roleManager.Create(new IdentityRole { Name = "NatHead" });
                roleManager.Create(new IdentityRole { Name = "AreaHead" });
                roleManager.Create(new IdentityRole { Name = "DistPastor" });
                roleManager.Create(new IdentityRole { Name = "PresElder" });
                roleManager.Create(new IdentityRole { Name = "RccHead" });
            }

            var adminUser = manager.FindByName("copadmin@cop.com");

            manager.AddToRoles(adminUser.Id, new string[] { "RccHead", "SuperAdmin", "TopMgmt", "BlkCoor", "NatHead", "AreaHead", "DistPastor", "PresElder" });
        }
    }
}
