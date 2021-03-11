using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetIdentity.WebApi.Models
{
    public class MetaBlock
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Block Id")]
        public int BlockId { get; set; }

        [Required]
        [Display(Name = "Block Name")]
        public string BlockName { get; set; }

        [Required]
        [Display(Name = "Block Description")]
        public string BlockDescription { get; set; }
        
        [Required]
        [Display(Name = "Block Coordinator")]
        public string UserId { get; set; }
    }

    public class MetaCountry
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Country Id")]
        public int CountryId { get; set; }

        [Required]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        [Required]
        [Display(Name = "Country Description")]
        public string CountryDescription { get; set; }

        [Required]
        [Display(Name = "Country Coordinator")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "BlockId")]
        public int BlockId { get; set; }

        [ForeignKey("BlockId")]
        public MetaBlock BlockFId { get; set; }
    }

    public class MetaRcc
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Rcc Id")]
        public int RccId { get; set; }

        [Required]
        [Display(Name = "Rcc Name")]
        public string RccName { get; set; }

        [Required]
        [Display(Name = "Rcc Description")]
        public string RccDescription { get; set; }

        [Required]
        [Display(Name = "Rcc Chairman")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "CountryId")]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public MetaCountry CountryFId { get; set; }
    }

    public class Address
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Address Id")]
        public int AddressId { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Locality")]
        public string Locality { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Region")]
        public string Region { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public int PostalCode { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }
    }

    public class MetaArea
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Area Id")]
        public int AreaId { get; set; }

        [Required]
        [Display(Name = "Area Name")]
        public string AreaName { get; set; }

        [Required]
        [Display(Name = "Area Address")]
        public int AreaAddress { get; set; }

        [ForeignKey("AreaAddress")]
        public Address AddrFId { get; set; }


        [Required]
        [Display(Name = "Area Head")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "RccId")]
        public int RccId { get; set; }

        [ForeignKey("RccId")]
        public MetaRcc RccFId { get; set; }
    }

    public class MetaDistrict
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "District Id")]
        public int DistrictId { get; set; }

        [Required]
        [Display(Name = "District Name")]
        public string DistrictName { get; set; }

        [Required]
        [Display(Name = "District Address")]
        public int DistrictAddress { get; set; }

        [ForeignKey("DistrictAddress")]
        public Address AddrFId { get; set; }

        [Required]
        [Display(Name = "District Pastor")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "AreaId")]
        public int AreaId { get; set; }

        [ForeignKey("AreaId")]
        public MetaArea AreaFId { get; set; }
    }

    public class MetaLocalAssembly
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Assembly Id")]
        public int AssemblyId { get; set; }

        [Required]
        [Display(Name = "Assemly Name")]
        public string AssemblyName { get; set; }

        [Required]
        [Display(Name = "Assembly Address")]
        public int AssemblyAddress { get; set; }

        [ForeignKey("AssemblyAddress")]
        public Address AddrFId { get; set; }

        [Required]
        [Display(Name = "Presiding Elder")]
        public string UserId { get; set; }

        [Display(Name = "DistrictId")]
        public int DistrictId { get; set; }

    }
}