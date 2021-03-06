using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetIdentity.WebApi.Models
{
    public class Member
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Member Id")]
        public int MemberId { get; set; }

        [Display(Name = "Local Assembly Id")]
        public int LocalAssemblyId { get; set; }

        [ForeignKey("LocalAssemblyId")]
        public MetaLocalAssembly LocalAssemblyFKId { get; set; }

        [Display(Name = "Professional Category Id")]
        public int ProfCatId { get; set; }

        [ForeignKey("ProfCatId ")]
        public MetaProfessionCategory ProfCatFKId { get; set; }

        [Display(Name = "Address Id")]
        public int MemberAddress { get; set; }

        [Display(Name ="AssuranceId")]
        public int AssuranceId { get; set; }

        [Display(Name = "BaptismStatusId")]
        public int BaptismStatusId { get; set; }

        [Display(Name ="Home Cell Id")]
        public int HomeCellId { get; set; }

        [Display(Name ="Title")]
        public string Title { get; set; }
        
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name ="Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name ="Date Of Birth")]
        public string DOB { get; set; }
        
        [Display(Name ="Gender")]
        public string  Gender{ get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Display(Name ="Counrey Of Birth")]
        public string  CountryOfBirth{ get; set; }

        [Display(Name ="Country Of Residence")]
        public string  CountryOfResidence{ get; set; }

        [Display(Name ="Marital Status")]
        public string  MaritalStatus{ get; set; }

        [Display(Name ="Spuse Name")]
        public string  SpouseName{ get; set; }

        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name ="Email Address")]
        public string  EmailAddress{ get; set; }

        [Display(Name ="Telephone Number")]
        public string  TelNumber{ get; set; }

        [Display(Name ="Profession")]
        public string  Profession{ get; set; }

        [Display(Name ="Church Status")]
        public string  ChurchStatus{ get; set; }

        [Display(Name ="Emergency Contact")]
        public string  EmergencyContact{ get; set; }

        [Display(Name ="Child Flag")]
        public bool ChildFlag{ get; set; }

    }

    public class SalvationStatus
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Assurance Id")]
        public int AssuranceId { get; set; }
        [Required]
        [Display(Name = "Member Id")]
        public int MemberId { get; set; }

        [Display(Name = "Local Assembly Id")]
        public int LocalAssemblyId { get; set; }

        [Display(Name = "Officiating Minister")]
        public int OfficiatingMinister { get; set; }

        [Display(Name = "Salvation Status")]
        public string SalvationStatusText{ get; set; }

        [Display(Name = "Date Of Salvation")]
        public string Date { get; set; }

        [Display(Name = "Communicant")]
        public int Communicant { get; set; }
    }

    public class SocialMediaHandler {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Social Media Id")]
        public int SocialMediaId { get; set; }

        [Required]
        [Display(Name = "Member Id")]
        public int MemberId { get; set; }
        
        [Display(Name = "Social Media Text")]
        public string SocialMediaText { get; set; }

        [Display(Name = "Social Media Link")]
        public string Link { get; set; }

    }

    public class BaptistStatus
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Baptism Status Id")]
        public int BaptismId { get; set; }
        [Required]
        [Display(Name = "Member Id")]
        public int MemberId { get; set; }

        [Display(Name = "Local Assembly Id")]
        public int LocalAssemblyId { get; set; }

        [Display(Name = "Officiating Minister")]
        public int Minister { get; set; }

        [Display(Name = "Type Of Baptism")]
        public string Type { get; set; }

        [Display(Name = "Date Of Baptism")]
        public string Date { get; set; }
    }

    public class GroupMemberRelation
    {
        [Required]
        [Display(Name = "Group Id")]
        [Key]
        [Column(Order = 1)]
        public int GroupId { get; set; }

        [Required]
        [Display(Name = "Member Id")]
        [Key]
        [Column(Order = 2)]
        public int MemberId { get; set; }
    }

    public class Posting
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Posting Id")]
        public int PostingId { get; set; }

        [Required]
        [Display(Name = "Member Id")]
        public int MemberId { get; set; }

        [Required]
        [Display(Name = "Posting Type")]
        public int PostingType { get; set; }

        [Display(Name = "Date")]
        public string Date { get; set; }

        [Display(Name = "OriginAssemblyId")]
        public int OriginAssemblyId { get; set; }

        [Display(Name = "DestinationAssemblyId")]
        public int DestinationAssemblyId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    public class MemberUser
    {
        [Required]
        [Key]
        [Display(Name = "UserName")]
        public string Username { get; set; }
        
        [Required]
        [Display(Name = "MemberId")]
        public string MemberId { get; set; }
    }

}