namespace AspNetIdentity.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(nullable: false),
                        AddressLine2 = c.String(),
                        Locality = c.String(),
                        City = c.String(),
                        Region = c.String(),
                        PostalCode = c.Int(nullable: false),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.BaptistStatus",
                c => new
                    {
                        BaptismId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        LocalAssemblyId = c.Int(nullable: false),
                        Minister = c.Int(nullable: false),
                        Type = c.String(),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.BaptismId);
            
            CreateTable(
                "dbo.GroupMemberRelations",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.MemberId });
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        LocalAssemblyId = c.Int(nullable: false),
                        ProfCatId = c.Int(nullable: false),
                        MemberAddress = c.Int(nullable: false),
                        AssuranceId = c.Int(nullable: false),
                        BaptismStatusId = c.Int(nullable: false),
                        HomeCellId = c.Int(nullable: false),
                        Title = c.String(),
                        FirstName = c.String(nullable: false),
                        MiddleName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DOB = c.String(),
                        Gender = c.String(),
                        Nationality = c.String(),
                        CountryOfBirth = c.String(),
                        CountryOfResidence = c.String(),
                        MaritalStatus = c.String(),
                        SpouseName = c.String(),
                        MobileNumber = c.String(),
                        EmailAddress = c.String(),
                        TelNumber = c.String(),
                        Profession = c.String(),
                        ChurchStatus = c.String(),
                        EmergencyContact = c.String(),
                        ChildFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.MetaLocalAssemblies", t => t.LocalAssemblyId, cascadeDelete: true)
                .ForeignKey("dbo.MetaProfessionCategories", t => t.ProfCatId, cascadeDelete: true)
                .Index(t => t.LocalAssemblyId)
                .Index(t => t.ProfCatId);
            
            CreateTable(
                "dbo.MetaLocalAssemblies",
                c => new
                    {
                        AssemblyId = c.Int(nullable: false, identity: true),
                        AssemblyName = c.String(nullable: false),
                        AssemblyAddress = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                        DistrictId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssemblyId)
                .ForeignKey("dbo.Addresses", t => t.AssemblyAddress, cascadeDelete: true)
                .Index(t => t.AssemblyAddress);
            
            CreateTable(
                "dbo.MetaProfessionCategories",
                c => new
                    {
                        ProfCatId = c.Int(nullable: false, identity: true),
                        ProfCatName = c.String(nullable: false),
                        ProfCatDesc = c.String(),
                    })
                .PrimaryKey(t => t.ProfCatId);
            
            CreateTable(
                "dbo.MemberUsers",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        MemberId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.MetaBlocks",
                c => new
                    {
                        BlockId = c.Int(nullable: false, identity: true),
                        BlockName = c.String(nullable: false),
                        BlockDescription = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BlockId);
            
            CreateTable(
                "dbo.MetaCommunityZones",
                c => new
                    {
                        ZoneId = c.Int(nullable: false, identity: true),
                        ZoneName = c.String(nullable: false),
                        ZoneAddress = c.Int(nullable: false),
                        ZoneLeader = c.Int(nullable: false),
                        localAssembly = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ZoneId)
                .ForeignKey("dbo.Addresses", t => t.ZoneAddress, cascadeDelete: true)
                .Index(t => t.ZoneAddress);
            
            CreateTable(
                "dbo.MetaCountries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false),
                        CountryDescription = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                        BlockId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CountryId)
                .ForeignKey("dbo.MetaBlocks", t => t.BlockId, cascadeDelete: true)
                .Index(t => t.BlockId);
            
            CreateTable(
                "dbo.MetaDistricts",
                c => new
                    {
                        DistrictId = c.Int(nullable: false, identity: true),
                        DistrictName = c.String(nullable: false),
                        DistrictAddress = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                        AreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DistrictId)
                .ForeignKey("dbo.Addresses", t => t.DistrictAddress, cascadeDelete: true)
                .ForeignKey("dbo.MetaAreas", t => t.AreaId, cascadeDelete: true)
                .Index(t => t.DistrictAddress)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.MetaAreas",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        AreaName = c.String(nullable: false),
                        AreaAddress = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                        RccId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AreaId)
                .ForeignKey("dbo.Addresses", t => t.AreaAddress, cascadeDelete: true)
                .ForeignKey("dbo.MetaRccs", t => t.RccId, cascadeDelete: true)
                .Index(t => t.AreaAddress)
                .Index(t => t.RccId);
            
            CreateTable(
                "dbo.MetaRccs",
                c => new
                    {
                        RccId = c.Int(nullable: false, identity: true),
                        RccName = c.String(nullable: false),
                        RccDescription = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RccId)
                .ForeignKey("dbo.MetaCountries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.MetaEventTypes",
                c => new
                    {
                        EventTypeId = c.Int(nullable: false, identity: true),
                        EventTypeName = c.String(),
                        EventTypeDesc = c.String(),
                    })
                .PrimaryKey(t => t.EventTypeId);
            
            CreateTable(
                "dbo.MetaGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                        GroupDeasc = c.String(),
                        GroupLeader = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.MetaHomeCells",
                c => new
                    {
                        HomeCellId = c.Int(nullable: false, identity: true),
                        HomeCellName = c.String(nullable: false),
                        CellAddress = c.Int(nullable: false),
                        CellLeader = c.Int(nullable: false),
                        ZoneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HomeCellId)
                .ForeignKey("dbo.Addresses", t => t.CellAddress, cascadeDelete: true)
                .Index(t => t.CellAddress);
            
            CreateTable(
                "dbo.MetaPastoralCareTypes",
                c => new
                    {
                        PcId = c.Int(nullable: false, identity: true),
                        PcEventTypeName = c.String(),
                        PcEventTypeDesc = c.String(),
                    })
                .PrimaryKey(t => t.PcId);
            
            CreateTable(
                "dbo.MetaPostings",
                c => new
                    {
                        PostingId = c.Int(nullable: false, identity: true),
                        PostingType = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PostingId);
            
            CreateTable(
                "dbo.Postings",
                c => new
                    {
                        PostingId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        PostingType = c.Int(nullable: false),
                        Date = c.String(),
                        OriginAssemblyId = c.Int(nullable: false),
                        DestinationAssemblyId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PostingId);
            
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
                "dbo.SalvationStatus",
                c => new
                    {
                        AssuranceId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        LocalAssemblyId = c.Int(nullable: false),
                        OfficiatingMinister = c.Int(nullable: false),
                        SalvationStatusText = c.String(),
                        Date = c.String(),
                        Communicant = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssuranceId);
            
            CreateTable(
                "dbo.SocialMediaHandlers",
                c => new
                    {
                        SocialMediaId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        SocialMediaText = c.String(),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.SocialMediaId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Level = c.Byte(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        MemberId = c.String(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MetaHomeCells", "CellAddress", "dbo.Addresses");
            DropForeignKey("dbo.MetaDistricts", "AreaId", "dbo.MetaAreas");
            DropForeignKey("dbo.MetaAreas", "RccId", "dbo.MetaRccs");
            DropForeignKey("dbo.MetaRccs", "CountryId", "dbo.MetaCountries");
            DropForeignKey("dbo.MetaAreas", "AreaAddress", "dbo.Addresses");
            DropForeignKey("dbo.MetaDistricts", "DistrictAddress", "dbo.Addresses");
            DropForeignKey("dbo.MetaCountries", "BlockId", "dbo.MetaBlocks");
            DropForeignKey("dbo.MetaCommunityZones", "ZoneAddress", "dbo.Addresses");
            DropForeignKey("dbo.Members", "ProfCatId", "dbo.MetaProfessionCategories");
            DropForeignKey("dbo.Members", "LocalAssemblyId", "dbo.MetaLocalAssemblies");
            DropForeignKey("dbo.MetaLocalAssemblies", "AssemblyAddress", "dbo.Addresses");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MetaHomeCells", new[] { "CellAddress" });
            DropIndex("dbo.MetaRccs", new[] { "CountryId" });
            DropIndex("dbo.MetaAreas", new[] { "RccId" });
            DropIndex("dbo.MetaAreas", new[] { "AreaAddress" });
            DropIndex("dbo.MetaDistricts", new[] { "AreaId" });
            DropIndex("dbo.MetaDistricts", new[] { "DistrictAddress" });
            DropIndex("dbo.MetaCountries", new[] { "BlockId" });
            DropIndex("dbo.MetaCommunityZones", new[] { "ZoneAddress" });
            DropIndex("dbo.MetaLocalAssemblies", new[] { "AssemblyAddress" });
            DropIndex("dbo.Members", new[] { "ProfCatId" });
            DropIndex("dbo.Members", new[] { "LocalAssemblyId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SocialMediaHandlers");
            DropTable("dbo.SalvationStatus");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Postings");
            DropTable("dbo.MetaPostings");
            DropTable("dbo.MetaPastoralCareTypes");
            DropTable("dbo.MetaHomeCells");
            DropTable("dbo.MetaGroups");
            DropTable("dbo.MetaEventTypes");
            DropTable("dbo.MetaRccs");
            DropTable("dbo.MetaAreas");
            DropTable("dbo.MetaDistricts");
            DropTable("dbo.MetaCountries");
            DropTable("dbo.MetaCommunityZones");
            DropTable("dbo.MetaBlocks");
            DropTable("dbo.MemberUsers");
            DropTable("dbo.MetaProfessionCategories");
            DropTable("dbo.MetaLocalAssemblies");
            DropTable("dbo.Members");
            DropTable("dbo.GroupMemberRelations");
            DropTable("dbo.BaptistStatus");
            DropTable("dbo.Addresses");
        }
    }
}
