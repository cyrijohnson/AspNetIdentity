namespace AspNetIdentity.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    FirstName = c.String(nullable: false, maxLength: 100),
                    LastName = c.String(nullable: false, maxLength: 100),
                    Level = c.Byte(nullable: false),
                    JoinDate = c.DateTime(nullable: false),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);



            CreateTable(
                "dbo.MetaDistrict",
                c => new
                {
                    DistrictId = c.String(nullable: false, maxLength: 128),
                    AreaId = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                    DistrictName = c.String(nullable: false, maxLength: 128),
                    DistrictAddressId = c.String(nullable: true, maxLength: 512),
                })
                .PrimaryKey(t => new { t.DistrictId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.MetaArea", t => t.AreaId, cascadeDelete: false)
                .ForeignKey("dbo.Address", t => t.DistrictAddressId, cascadeDelete: false)
                .Index(t => t.DistrictId);

            CreateTable(
                "dbo.MetaArea",
                c => new
                {
                    AreaId = c.String(nullable: false, maxLength: 128),
                    RccId = c.String(nullable: false, maxLength: 128),
                    AreaName = c.String(nullable: false, maxLength: 128),
                    AreaAddressId = c.String(nullable: true, maxLength: 512),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.AreaId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.MetaRcc", t => t.RccId, cascadeDelete: false)
                .ForeignKey("dbo.Address", t => t.AreaAddressId, cascadeDelete: false)
                .Index(t => t.AreaId);

            CreateTable(
                "dbo.MetaRcc",
                c => new
                {
                    RccId = c.String(nullable: false, maxLength: 128),
                    CountryId = c.String(nullable: false, maxLength: 128),
                    RccName = c.String(nullable: false, maxLength: 128),
                    RccDescription = c.String(nullable: true, maxLength: 512),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.RccId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.MetaCountry", t => t.CountryId, cascadeDelete: false)
                .Index(t => t.RccId);

            CreateTable(
                "dbo.MetaCountry",
                c => new
                {
                    CountryId = c.String(nullable: false, maxLength: 128),
                    BlockId = c.String(nullable: false, maxLength: 128),
                    CountryName = c.String(nullable: false, maxLength: 128),
                    CountryDescription = c.String(nullable: true, maxLength: 512),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.CountryId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.MetaBlock", t => t.BlockId, cascadeDelete: false)
                .Index(t => t.CountryId);

            CreateTable(
                "dbo.MetaBlock",
                c => new
                {
                    BlockId = c.String(nullable: false, maxLength: 128),
                    BlockName = c.String(nullable: false, maxLength: 128),
                    BlockDescription = c.String(nullable: true, maxLength: 512),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.BlockId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.BlockId);

            CreateTable(
                "dbo.MetaLocalAssembly",
                c => new
                {
                    LocalAssemblyId = c.String(nullable: false, maxLength: 128),
                    DistrictId = c.String(nullable: false, maxLength: 128),
                    LocalAssemblyName = c.String(nullable: true, maxLength: 128),
                    LocalAssemblyAddressId = c.String(nullable: true, maxLength: 512),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LocalAssemblyId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.MetaDistrict", t => t.DistrictId, cascadeDelete: false)
                .ForeignKey("dbo.Address", t => t.LocalAssemblyAddressId, cascadeDelete: false)
                .Index(t => t.LocalAssemblyId);

            CreateTable(
               "dbo.MetaProfCategory",
               c => new
               {
                   ProfCatId = c.String(nullable: false, maxLength: 128),
                   ProfCatName = c.String(nullable: false, maxLength: 128),
                   ProfCatDescription = c.String(nullable: true, maxLength: 128),
               })
               .PrimaryKey(t => new { t.ProfCatId })
               .Index(t => t.ProfCatId);

            CreateTable(
               "dbo.SalvationStatus",
               c => new
               {
                   AssuranceId = c.String(nullable: false, maxLength: 128),
                   MemberId = c.String(nullable: false, maxLength: 128),
                   LocalAssemblyId = c.String(nullable: false, maxLength: 128),
                   OfficiatingMinister = c.String(nullable: false, maxLength: 128),
                   SalvationStatus = c.String(nullable: true, maxLength: 128),
                   communicant = c.String(nullable: true, maxLength: 128),
                   date = c.String(nullable: false, maxLength: 128),
               })
               .PrimaryKey(t => new { t.AssuranceId })
               .ForeignKey("dbo.MemberList", t => t.MemberId, cascadeDelete: false)
                .ForeignKey("dbo.MetaLocalAssembly", t => t.LocalAssemblyId, cascadeDelete: false)
               .Index(t => t.AssuranceId);

            CreateTable(
               "dbo.BaptismStatus",
               c => new
               {
                   BaptismStatusId = c.String(nullable: false, maxLength: 128),
                   MemberId = c.String(nullable: false, maxLength: 128),
                   LocalAssemblyId = c.String(nullable: false, maxLength: 128),
                   OfficiatingMinister = c.String(nullable: false, maxLength: 128),
                   TypeOfBaptism = c.String(nullable: true, maxLength: 128),
                   date = c.String(nullable: false, maxLength: 128),
               })
               .PrimaryKey(t => new { t.BaptismStatusId })
               .ForeignKey("dbo.MemberList", t => t.MemberId, cascadeDelete: false)
                .ForeignKey("dbo.MetaLocalAssembly", t => t.LocalAssemblyId, cascadeDelete: false)
               .Index(t => t.BaptismStatusId);

            CreateTable(
               "dbo.SocialMediaHandler",
               c => new
               {
                   SocialMediaId = c.String(nullable: false, maxLength: 128),
                   MemberId = c.String(nullable: false, maxLength: 128),
                   SocialMediaName = c.String(nullable: false, maxLength: 128),
                   SocialMediaHandler = c.String(nullable: false, maxLength: 512),
               })
               .PrimaryKey(t => new { t.SocialMediaId })
               .ForeignKey("dbo.MemberList", t => t.MemberId, cascadeDelete: false)
               .Index(t => t.SocialMediaId);

            CreateTable(
                "dbo.MemberList",
                c => new
                {
                    MemberId = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: true, maxLength: 128),
                    LocalAssemblyId = c.String(nullable: false, maxLength: 128),
                    ProfCatId = c.String(nullable: true, maxLength: 128),
                    AddressId = c.String(nullable: true, maxLength: 128),
                    AssuranceId = c.String(nullable: true, maxLength: 128),
                    HomeCellId = c.String(nullable: true, maxLength: 128),
                    Title = c.String(nullable: false, maxLength: 128),
                    FirstName = c.String(nullable: false, maxLength: 128),
                    MiddleName = c.String(nullable: false, maxLength: 128),
                    LastName = c.String(nullable: false, maxLength: 128),
                    DOB = c.String(nullable: true, maxLength: 128),
                    Gender = c.String(nullable: false, maxLength: 128),
                    Nationality = c.String(nullable: true, maxLength: 128),
                    CountryOfBirth = c.String(nullable: true, maxLength: 128),
                    CountryOfResidence = c.String(nullable: true, maxLength: 128),
                    MarritalStatus = c.String(nullable: false, maxLength: 128),
                    SpouseName = c.String(nullable: true, maxLength: 128),
                    SpouseId = c.String(nullable: true, maxLength: 128),
                    MobileNumber = c.String(nullable: true, maxLength: 128),
                    EmailAddress = c.String(nullable: true, maxLength: 128),
                    TelephoneNum = c.String(nullable: true, maxLength: 128),
                    Profession = c.String(nullable: true, maxLength: 128),
                    ChurchStatus = c.String(nullable: true, maxLength: 128),
                    EmergencyContact = c.String(nullable: true, maxLength: 128),
                    ChildFlag = c.String(nullable: false, maxLength: 20),
                })
                .PrimaryKey(t => new { t.MemberId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.MetaLocalAssembly", t => t.LocalAssemblyId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.ProfCatId, cascadeDelete: false)
                .ForeignKey("dbo.SalvationStatus", t => t.AssuranceId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.HomeCellId, cascadeDelete: false)
                .ForeignKey("dbo.Address", t => t.AddressId, cascadeDelete: false)
                .Index(t => t.MemberId);

            CreateTable(
               "dbo.Address",
               c => new
               {
                   AddressId = c.String(nullable: false, maxLength: 128),
                   HouseNumber = c.String(nullable: true, maxLength: 128),
                   StreetName = c.String(nullable: true, maxLength: 128),
                   Address1 = c.String(nullable: false, maxLength: 512),
                   Address2 = c.String(nullable: false, maxLength: 512),
                   Address3 = c.String(nullable: false, maxLength: 512),
               })
               .PrimaryKey(t => new { t.AddressId })
               .Index(t => t.AddressId);

            CreateTable(
               "dbo.CPRelation",
               c => new
               {
                   ChildId = c.String(nullable: false, maxLength: 128),
                   ParentId = c.String(nullable: true, maxLength: 128),
               })
               .PrimaryKey(t => new { t.ChildId, t.ParentId })
               .Index(t => t.ChildId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MetaDistrict");
        }
    }
}
