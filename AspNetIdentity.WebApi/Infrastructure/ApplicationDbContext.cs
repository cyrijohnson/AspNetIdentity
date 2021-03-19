using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AspNetIdentity.WebApi.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new NullDatabaseInitializer<ApplicationDbContext>());
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaBlock> MetaBlocks { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaCountry> MetaCountries { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaRcc> MetaRccs { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaArea> MetAreas { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.Address> Addresses { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaDistrict> MetaDistricts { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaLocalAssembly> MetaLocalAssemblies { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaProfessionCategory> MetaProfessionCategories { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaCommunityZone> MetaCommunityZones { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaHomeCell> MetaHomeCells { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaGroup> MetaGroups { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaPostings> MetaPostings { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaPastoralCareType> MetaPastoralCareTypes { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.MetaEventType> MetaEventTypes { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.Member> Members { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.SalvationStatus> SalvationStatus { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.SocialMediaHandler> SocialMediaHandlers { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.BaptistStatus> BaptistStatus { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.GroupMemberRelation> GroupMemberRelations { get; set; }

        public System.Data.Entity.DbSet<AspNetIdentity.WebApi.Models.Posting> Postings { get; set; }
    }
}