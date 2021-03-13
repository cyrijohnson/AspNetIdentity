using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetIdentity.WebApi.Models
{
    public class MetaAreaDTO
    {
            public int AreaId { get; set; }
            public string AreaName { get; set; }
            public Address AreaAddress { get; set; }
            public string UserId { get; set; }
            public int RccId { get; set; }  
    }

    public class areaAddressUpdateDTO
    {
        public MetaArea area { get; set; }
        public Address address { get; set; }
    }

    public class districtAddressUpdateDTO
    {
        public MetaDistrict district { get; set; }
        public Address address { get; set; }
    }

    public class MetaDistrictDTO
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public Address DistrictAddress { get; set; }
        public string UserId { get; set; }
        public int AreaId { get; set; }
    }

    public class MetaLocalAssemblyDTO
    {
        public int AssemblyId { get; set; }
        public string AssemblyName { get; set; }
        public Address AssemblyAddress { get; set; }
        public string UserId { get; set; }
        public int DistrictId { get; set; }
    }

    public class localAssemblyAddressUpdateDTO
    {
        public MetaLocalAssembly assembly { get; set; }
        public Address address { get; set; }
    }

    public class MetaCommunityZoneDTO
    {
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public Address ZoneAddress { get; set; }
        public int ZoneLeader { get; set; }
    }

    public class CommunityZoneAddressUpdateDTO
    {
        public MetaCommunityZone CommunityZone { get; set; }
        public Address CommunityZoneAddress { get; set; }
    }

    public class MetaHomeCellDTO
    {
        public int HomeCellId { get; set; }    
        public string HomeCellName { get; set; }
        public Address CellAddress { get; set; }
        public int CellLeader { get; set; }     
        public int ZoneId { get; set; }

    }

    public class HomeCellAddressUpdateDTO
    {
        public MetaHomeCell HomeCell { get; set; }
        public Address HomeCellAddress { get; set; }
    }
}