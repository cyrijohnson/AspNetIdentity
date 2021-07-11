using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AspNetIdentity.WebApi.Controllers;
using AspNetIdentity.WebApi.Infrastructure;
using AspNetIdentity.WebApi.Models;
using AspNetIdentity.WebApi.Services;

namespace AspNetIdentity.WebApi.Services
{
    public class MemberUtility : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public int getUserLocalAssembly(String userId)
        {
            return 0;
        }
        //ADMIN Return Functions
        public ReturnDataModel getMembersAdmin()
        {
            MetaBlocksController blockController = new MetaBlocksController();
            ReturnDataModel responseModel = new ReturnDataModel();
            MetaBlock[] blocks = blockController.GetMetaBlocksArray();
            foreach(MetaBlock i in blocks)
            {
                Blocks block = new Blocks();
                block.BlockId = i.BlockId;
                block.BlockName = i.BlockName;
                block.BlockDescription = i.BlockDescription;
                block.UserId = i.UserId;
                block.countries = getBlockCountries(i.BlockId);
                responseModel.blocks.Add(block);
            }
            return responseModel;
        }

        public List<Countries> getBlockCountries(int blockId)
        {
            List<Countries> countryList = new List<Countries>();
            MetaCountriesController coutriesController = new MetaCountriesController();
            MetaCountry[] countries = coutriesController.GetMetaCountryByBlockArray(blockId);
            foreach(MetaCountry i in countries)
            {
                countryList.Add(new Countries
                {
                    CountryId = i.CountryId,
                    CountryName = i.CountryName,
                    CountryDescription = i.CountryDescription,
                    UserId = i.UserId,
                    BlockId = i.BlockId,
                    rccs = getCountryRccs(i.CountryId)
                });
            }
            return countryList;
        }

        public List<RCC> getCountryRccs(int countryId)
        {
            List<RCC> RccList = new List<RCC>();
            MetaRccsController rccController = new MetaRccsController();
            MetaRcc[] rccs = rccController.getRccByCountryArray(countryId);
            foreach (MetaRcc i in rccs)
            {
                RccList.Add(new RCC
                {
                    RccId = i.RccId,
                    RccName = i.RccName,
                    RccDescription = i.RccDescription,
                    UserId = i.UserId,
                    CountryId = i.CountryId,
                    areas = getRccAreas(i.RccId)
                });
            }
            return RccList;
        }

        public List<Areas> getRccAreas(int rccId)
        {
            List<Areas> AreaList = new List<Areas>();
            MetaAreasController areaController = new MetaAreasController();
            List<MetaAreaDTO> areas = areaController.getAreasByRccArray(rccId);
            foreach (MetaAreaDTO i in areas)
            {
                AreaList.Add(new Areas
                {
                    AreaId = i.AreaId,
                    AreaName = i.AreaName,
                    AreaAddress = i.AreaAddress,
                    RccId = i.RccId,
                    UserId = i.UserId,
                    districts = getAreaDistricts(i.AreaId)
                });
            }
            return AreaList;
        }

        public List<District> getAreaDistricts(int areaId)
        {
            List<District> DistrictList = new List<District>();
            MetaDistrictsController districtController = new MetaDistrictsController();
            List<MetaDistrictDTO> districts = districtController.getDistrictsByAreaArray(areaId);
            foreach (MetaDistrictDTO i in districts)
            {
                DistrictList.Add(new District
                {
                    DistrictId = i.DistrictId,
                    DistrictName = i.DistrictName,
                    DistrictAddress = i.DistrictAddress,
                    UserId = i.UserId,
                    AreaId = i.AreaId,
                    assemblies = getDistrictAssemblies(i.DistrictId)
                });
            }
            return DistrictList;
        }

        public List<Assembly> getDistrictAssemblies(int distId)
        {
            List<Assembly> AssemblyList = new List<Assembly>();
            MetaLocalAssembliesController assemblyController = new MetaLocalAssembliesController();
            List<MetaLocalAssemblyDTO> assemblies = assemblyController.getAssembliesByDistrictArray(distId);
            foreach (MetaLocalAssemblyDTO i in assemblies)
            {
                AssemblyList.Add(new Assembly
                {
                   AssemblyId = i.AssemblyId,
                   AssemblyName = i.AssemblyName,
                   AssemblyAddress = i.AssemblyAddress,
                   DistrictId = i.DistrictId,
                   UserId = i.UserId,
                   members = getAssemblyMembers(i.AssemblyId)
                });
            }
            return AssemblyList;
        }

        public List<Member> getAssemblyMembers(int assemblyId)
        {
            MembersController membersController = new MembersController();
            List<Member> memberList = membersController.GetMemberArrayByAssembly(assemblyId);
            return memberList;
        }
        //END of ADMIN Return Functions

        //Get members block coordintor
        public ReturnDataModel getMembersBlockCoor(int assemblyId, string userName)
        {
            int BlockId = getUserMetaId(assemblyId, "block");
            MetaBlocksController blockController = new MetaBlocksController();
            ReturnDataModel responseModel = new ReturnDataModel();
            MetaBlock[] blocks = blockController.GetMetaBlockByIdArray(BlockId);
            if (blocks.Count() != 0)
            {
                if (blocks[0].UserId != userName)
                {
                    return null;
                }
            }
            foreach (MetaBlock i in blocks)
            {
                Blocks block = new Blocks();
                block.BlockId = i.BlockId;
                block.BlockName = i.BlockName;
                block.BlockDescription = i.BlockDescription;
                block.UserId = i.UserId;
                block.countries = getBlockCountries(i.BlockId);
                responseModel.blocks.Add(block);
            }
            return responseModel;
        }
        //End of get members block coordinator

        //Get members Country Head
        
        public ReturnDataModel getMembersNatHead(int assemblyId, string userName)
        {
            int CountryId = getUserMetaId(assemblyId, "nat");
            int BlockId = getUserMetaId(assemblyId, "block");
            MetaCountriesController countryController = new MetaCountriesController();
            MetaBlocksController blockController = new MetaBlocksController();
            ReturnDataModel responseModel = new ReturnDataModel();
            MetaBlock[] blocks = blockController.GetMetaBlockByIdArray(BlockId);
            MetaCountry country = countryController.GetMetaCountryLocal(CountryId);
            if (country != null)
            {
                if (country.UserId != userName)
                {
                    return null;
                }
            }
            Blocks block = new Blocks();
            block.BlockId = blocks[0].BlockId;
            block.BlockName = blocks[0].BlockName;
            block.BlockDescription = blocks[0].BlockDescription;
            block.UserId = blocks[0].UserId;
            block.countries.Add(new Countries {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
                CountryDescription = country.CountryDescription,
                BlockId = country.BlockId,
                UserId = country.UserId,
                rccs = getCountryRccs(country.CountryId)
            });
            responseModel.blocks.Add(block);
            return responseModel;
        }

      

        //End of get memebers country head

        //Get Member RCC Coor
        public ReturnDataModel getMembersRccCoor(int assemblyId, string userName)
        {
            int CountryId = getUserMetaId(assemblyId, "nat");
            int BlockId = getUserMetaId(assemblyId, "block");
            int RccId = getUserMetaId(assemblyId, "rcc");
            ReturnDataModel responseModel = new ReturnDataModel();
            MetaCountriesController countryController = new MetaCountriesController();
            MetaBlocksController blockController = new MetaBlocksController();
            MetaRccsController rccController = new MetaRccsController();
            MetaBlock[] blocks = blockController.GetMetaBlockByIdArray(BlockId);
            MetaCountry country = countryController.GetMetaCountryLocal(CountryId);
            MetaRcc rcc = rccController.GetMetaRccLocal(RccId);
            if (rcc != null)
            {
                if (rcc.UserId != userName)
                {
                    return null;
                }
            }
            List<RCC> rccList = new List<RCC>();
            Blocks block = new Blocks();
            rccList.Add(new RCC
            {
                RccId = rcc.RccId,
                RccName = rcc.RccName,
                RccDescription = rcc.RccDescription,
                CountryId = rcc.CountryId,
                UserId = rcc.UserId,
                areas = getRccAreas(rcc.RccId)
            });
            block.BlockId = blocks[0].BlockId;
            block.BlockName = blocks[0].BlockName;
            block.BlockDescription = blocks[0].BlockDescription;
            block.UserId = blocks[0].UserId;
            block.countries.Add(new Countries
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
                CountryDescription = country.CountryDescription,
                BlockId = country.BlockId,
                UserId = country.UserId,
                rccs = rccList
            });
            responseModel.blocks.Add(block);
            return responseModel;

        }
        //End of Get Member RCC Coor

        //Get Member Area Head
        public ReturnDataModel getMembersAreaHead(int assemblyId, string userName)
        {
            int CountryId = getUserMetaId(assemblyId, "nat");
            int BlockId = getUserMetaId(assemblyId, "block");
            int RccId = getUserMetaId(assemblyId, "rcc");
            int AreaId = getUserMetaId(assemblyId, "area");
            ReturnDataModel responseModel = new ReturnDataModel();
            MetaCountriesController countryController = new MetaCountriesController();
            MetaBlocksController blockController = new MetaBlocksController();
            MetaRccsController rccController = new MetaRccsController();
            MetaAreasController areaController = new MetaAreasController();
            MetaBlock[] blocks = blockController.GetMetaBlockByIdArray(BlockId);
            MetaCountry country = countryController.GetMetaCountryLocal(CountryId);
            MetaRcc rcc = rccController.GetMetaRccLocal(RccId);
            MetaAreaDTO area = areaController.GetMetaAreaLocal(AreaId);
            if (area != null)
            {
                if (area.UserId != userName)
                {
                    return null;
                }
            }
            List<Areas> areasList = new List<Areas>();
            List<RCC> rccList = new List<RCC>();
            Blocks block = new Blocks();
            areasList.Add(new Areas{
                AreaId = area.AreaId,
                AreaName = area.AreaName,
                AreaAddress = area.AreaAddress,
                RccId = area.RccId,
                UserId = area.UserId,
                districts = getAreaDistricts(area.AreaId)
            });
            rccList.Add(new RCC
            {
                RccId = rcc.RccId,
                RccName = rcc.RccName,
                RccDescription = rcc.RccDescription,
                CountryId = rcc.CountryId,
                UserId = rcc.UserId,
                areas = areasList
            });
            block.BlockId = blocks[0].BlockId;
            block.BlockName = blocks[0].BlockName;
            block.BlockDescription = blocks[0].BlockDescription;
            block.UserId = blocks[0].UserId;
            block.countries.Add(new Countries
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
                CountryDescription = country.CountryDescription,
                BlockId = country.BlockId,
                UserId = country.UserId,
                rccs = rccList
            });
            responseModel.blocks.Add(block);
            return responseModel;

        }
        //End of Get Member Area Head

        //Get Member District Head
        public ReturnDataModel getMembersDistrictHead(int assemblyId, string userName)
        {
            int CountryId = getUserMetaId(assemblyId, "nat");
            int BlockId = getUserMetaId(assemblyId, "block");
            int RccId = getUserMetaId(assemblyId, "rcc");
            int AreaId = getUserMetaId(assemblyId, "area");
            int DistrictId = getUserMetaId(assemblyId, "district");
            ReturnDataModel responseModel = new ReturnDataModel();
            MetaCountriesController countryController = new MetaCountriesController();
            MetaBlocksController blockController = new MetaBlocksController();
            MetaRccsController rccController = new MetaRccsController();
            MetaAreasController areaController = new MetaAreasController();
            MetaDistrictsController districtController = new MetaDistrictsController();
            MetaBlock[] blocks = blockController.GetMetaBlockByIdArray(BlockId);
            MetaCountry country = countryController.GetMetaCountryLocal(CountryId);
            MetaRcc rcc = rccController.GetMetaRccLocal(RccId);
            MetaAreaDTO area = areaController.GetMetaAreaLocal(AreaId);
            MetaDistrictDTO district = districtController.GetMetaDistrictLocal(DistrictId);
            if (district != null)
            {
                if (district.UserId != userName)
                {
                    return null;
                }
            }
            List<Areas> areasList = new List<Areas>();
            List<RCC> rccList = new List<RCC>();
            List<District> districtList = new List<District>();
            Blocks block = new Blocks();
            districtList.Add(new District
            {
                DistrictId = district.DistrictId,
                DistrictName = district.DistrictName,
                DistrictAddress = district.DistrictAddress,
                AreaId = district.AreaId,
                UserId = district.UserId,
                assemblies = getDistrictAssemblies(district.DistrictId)
            });
            areasList.Add(new Areas
            {
                AreaId = area.AreaId,
                AreaName = area.AreaName,
                AreaAddress = area.AreaAddress,
                RccId = area.RccId,
                UserId = area.UserId,
                districts = districtList
            });
            rccList.Add(new RCC
            {
                RccId = rcc.RccId,
                RccName = rcc.RccName,
                RccDescription = rcc.RccDescription,
                CountryId = rcc.CountryId,
                UserId = rcc.UserId,
                areas = areasList
            });
            block.BlockId = blocks[0].BlockId;
            block.BlockName = blocks[0].BlockName;
            block.BlockDescription = blocks[0].BlockDescription;
            block.UserId = blocks[0].UserId;
            block.countries.Add(new Countries
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
                CountryDescription = country.CountryDescription,
                BlockId = country.BlockId,
                UserId = country.UserId,
                rccs = rccList
            });
            responseModel.blocks.Add(block);
            return responseModel;
        }
        //End of Get Member District Head

        //Get Members Assembly Head
        public ReturnDataModel getMembersAssemblyHead(int assemblyId, string userName)
        {
            int CountryId = getUserMetaId(assemblyId, "nat");
            int BlockId = getUserMetaId(assemblyId, "block");
            int RccId = getUserMetaId(assemblyId, "rcc");
            int AreaId = getUserMetaId(assemblyId, "area");
            int DistrictId = getUserMetaId(assemblyId, "district");
            ReturnDataModel responseModel = new ReturnDataModel();
            MetaCountriesController countryController = new MetaCountriesController();
            MetaBlocksController blockController = new MetaBlocksController();
            MetaRccsController rccController = new MetaRccsController();
            MetaAreasController areaController = new MetaAreasController();
            MetaDistrictsController districtController = new MetaDistrictsController();
            MetaLocalAssembliesController assemblyController = new MetaLocalAssembliesController();
            MetaBlock[] blocks = blockController.GetMetaBlockByIdArray(BlockId);
            MetaCountry country = countryController.GetMetaCountryLocal(CountryId);
            MetaRcc rcc = rccController.GetMetaRccLocal(RccId);
            MetaAreaDTO area = areaController.GetMetaAreaLocal(AreaId);
            MetaDistrictDTO district = districtController.GetMetaDistrictLocal(DistrictId);
            MetaLocalAssemblyDTO assembly = assemblyController.GetMetaLocalAssemblyLocal(assemblyId);
            if (assembly != null)
            {
                if (assembly.UserId != userName)
                {
                    return null;
                }
            }
            List<Areas> areasList = new List<Areas>();
            List<RCC> rccList = new List<RCC>();
            List<District> districtList = new List<District>();
            List<Assembly> assemblyList = new List<Assembly>();
            Blocks block = new Blocks();
            assemblyList.Add(new Assembly
            {
                AssemblyId = assembly.AssemblyId,
                AssemblyName = assembly.AssemblyName,
                AssemblyAddress = assembly.AssemblyAddress,
                DistrictId = assembly.DistrictId,
                UserId = assembly.UserId,
                members = getAssemblyMembers(assembly.AssemblyId)
            });
            districtList.Add(new District
            {
                DistrictId = district.DistrictId,
                DistrictName = district.DistrictName,
                DistrictAddress = district.DistrictAddress,
                AreaId = district.AreaId,
                UserId = district.UserId,
                assemblies = assemblyList
            });
            areasList.Add(new Areas
            {
                AreaId = area.AreaId,
                AreaName = area.AreaName,
                AreaAddress = area.AreaAddress,
                RccId = area.RccId,
                UserId = area.UserId,
                districts = districtList
            });
            rccList.Add(new RCC
            {
                RccId = rcc.RccId,
                RccName = rcc.RccName,
                RccDescription = rcc.RccDescription,
                CountryId = rcc.CountryId,
                UserId = rcc.UserId,
                areas = areasList
            });
            block.BlockId = blocks[0].BlockId;
            block.BlockName = blocks[0].BlockName;
            block.BlockDescription = blocks[0].BlockDescription;
            block.UserId = blocks[0].UserId;
            block.countries.Add(new Countries
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
                CountryDescription = country.CountryDescription,
                BlockId = country.BlockId,
                UserId = country.UserId,
                rccs = rccList
            });
            responseModel.blocks.Add(block);
            return responseModel;
        }
        //End of Get Members Assembly Head

        //Get user meta Id's
        public int getUserMetaId(int assemblyId, string type)
        {
            MetaLocalAssembly assembly = db.MetaLocalAssemblies.Find(assemblyId);
            MetaDistrict district = db.MetaDistricts.Find(assembly.DistrictId);
            MetaArea area = db.MetAreas.Find(district.AreaId);
            MetaRcc rcc = db.MetaRccs.Find(area.RccId);
            MetaCountry country = db.MetaCountries.Find(rcc.CountryId);
            MetaBlock block = db.MetaBlocks.Find(country.BlockId);
            switch (type)
            {
                case "ass":
                    return assembly.AssemblyId;
                    break;
                case "district":
                    return district.DistrictId;
                    break;
                case "area":
                    return area.AreaId;
                    break;
                case "rcc":
                    return rcc.RccId;
                    break;
                case "nat":
                    return country.CountryId;
                    break;
                case "block":
                    return block.BlockId;
                    break;
                default:
                    return 0;
                    break;
            }
        }
        //End of Get user meta Id's
    }
}