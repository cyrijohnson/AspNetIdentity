using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetIdentity.WebApi.Models
{
    public class AspNetRoles
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class AspNetUserRoles
    {
        [Required]
        [Key]
        [Index]
        [Display(Name = "UserId")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AspNetUsers UserFId { get; set; }

        [Required]
        [Index]
        [Display(Name = "RoleId")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public AspNetRoles RoleFId { get; set; }

    }

    public class AspNetUsers
    {
        [Required]
        [Key]
        [Display(Name = "Id")]
        public string Id { get; set; }
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Display(Name = "Level")]
        public byte Level { get; set; }
        [Display(Name = "JoinDate")]
        public DateTime JoinDate { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "EmailConfirmed")]
        public string EmailConfirmed { get; set; }
        [Display(Name = "PasswordHash")]
        public string PasswordHash { get; set; }
        [Display(Name = "SecurityStamp")]
        public string SecurityStamp { get; set; }
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Display(Name = "PhoneNumberConfirmed")]
        public bool PhoneNumberConfirmed { get; set; }
        [Display(Name = "TwoFactorEnabled")]
        public bool TwoFactorEnabled { get; set; }
        [Display(Name = "LockoutEndDateUtc")]
        public DateTime LockoutEndDateUtc { get; set; }
        [Display(Name = "LockoutEnabled")]
        public bool LockoutEnabled { get; set; }
        [Display(Name = "AccessFailedCount")]
        public int AccessFailedCount { get; set; }
        [Display(Name = "UserName")]
        [Index("UserNameIndex", IsUnique=true)]
        public string UserName { get; set; }
        [Display(Name = "MemberId")]
        public string MemberId { get; set; }
    }

    public class AspNetUserClaims
    {
        [Required]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "UserId")]
        [Index]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AspNetUsers UserFId { get; set; }
        [Display(Name = "ClaimType")]
        public string ClaimType { get; set; }
        [Display(Name = "ClaimValue")]
        public string ClaimValue { get; set; }
    }

    public class AspNetUserLogins
    {
        [Required]
        [Key]
        [Display(Name = "LoginProvider")]
        public string LoginProvider { get; set; }
        [Required]
        [Key]
        [Display(Name = "ProviderKey")]
        public string ProviderKey { get; set; }
        [Required]
        [Key]
        [Display(Name = "UserId")]
        [Index]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AspNetUsers UserFId { get; set; }
    }
}