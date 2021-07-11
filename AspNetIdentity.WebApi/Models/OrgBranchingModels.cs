using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetIdentity.WebApi.Models
{
    public class ReturnDataModel
    {
        public List<Blocks> blocks = new List<Blocks>(); 
    }
    public class Blocks
    {
        public int BlockId { get; set; }
        public string BlockName { get; set; }
        public string BlockDescription { get; set; }
        public string UserId { get; set; }
        public List<Countries> countries { get; set; } 
    }

    public class Countries
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryDescription { get; set; }
        public string UserId { get; set; }
        public int BlockId { get; set; }
        public List<RCC> rccs { get; set; }
    }

    public class RCC
    {
        public int RccId { get; set; }
        public string RccName { get; set; }
        public string RccDescription { get; set; }
        public string UserId { get; set; }
        public int CountryId { get; set; }
        public List<Areas> areas { get; set; }
    }

    public class Areas
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public Address AreaAddress { get; set; }
        public string UserId { get; set; }
        public int RccId { get; set; }
        public List<District> districts { get; set; }
    }

    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public Address DistrictAddress { get; set; }
        public string UserId { get; set; }
        public int AreaId { get; set; }
        public List<Assembly> assemblies { get; set; }
    }

    public class Assembly
    {
        public int AssemblyId { get; set; }
        public string AssemblyName { get; set; }
        public Address AssemblyAddress { get; set; }
        public string UserId { get; set; }
        public int DistrictId { get; set; }
        public List<Member> members { get; set; }
    }
}