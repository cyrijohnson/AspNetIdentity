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
using AspNetIdentity.WebApi.Infrastructure;
using AspNetIdentity.WebApi.Models;
using AspNetIdentity.WebApi.Services;

namespace AspNetIdentity.WebApi.Controllers
{
    [RoutePrefix("api/metadata")]
    public class MetaDistrictsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        [Route("getAllDistrictsByArea")]
        public async System.Threading.Tasks.Task<IHttpActionResult> getDistrictsByArea(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            string query = "SELECT * FROM MetaDistricts where AreaId=@p0";
            var metaDistrict = db.MetaDistricts.SqlQuery(query, id).ToList();
            List<MetaDistrictDTO> response = new List<MetaDistrictDTO>();
            foreach (MetaDistrict data in metaDistrict)
            {
                response.Add(new MetaDistrictDTO
                {
                    DistrictId = data.DistrictId,
                    DistrictName = data.DistrictName,
                    UserId = data.UserId,
                    AreaId = data.AreaId,
                    DistrictAddress = db.Addresses.Find(data.DistrictAddress)
                });
            }
            if (metaDistrict == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        public  List<MetaDistrictDTO> getDistrictsByAreaArray(int id)
        {
            if (id == 0)
            {
                return null;
            }

            string query = "SELECT * FROM MetaDistricts where AreaId=@p0";
            var metaDistrict = db.MetaDistricts.SqlQuery(query, id).ToList();
            List<MetaDistrictDTO> response = new List<MetaDistrictDTO>();
            foreach (MetaDistrict data in metaDistrict)
            {
                response.Add(new MetaDistrictDTO
                {
                    DistrictId = data.DistrictId,
                    DistrictName = data.DistrictName,
                    UserId = data.UserId,
                    AreaId = data.AreaId,
                    DistrictAddress = db.Addresses.Find(data.DistrictAddress)
                });
            }
            if (metaDistrict == null)
            {
                return null;
            }

            return response;
        }

        // GET: api/MetaDistricts
        [Authorize]
        [Route("getAllDistricts")]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetMetaDistricts()
        {
            var metaDist = db.MetaDistricts.ToList();
            List<MetaDistrictDTO> response = new List<MetaDistrictDTO>();
            foreach (MetaDistrict data in metaDist)
            {
                response.Add(new MetaDistrictDTO
                {
                    DistrictId = data.DistrictId,
                    DistrictName = data.DistrictName,
                    UserId = data.UserId,
                    AreaId = data.AreaId,
                    DistrictAddress = db.Addresses.Find(data.DistrictAddress)
                });
            }
            if (metaDist == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [Authorize]
        [Route("getDistrictsById")]
        // GET: api/MetaDistricts/5
        [ResponseType(typeof(MetaDistrict))]
        public IHttpActionResult GetMetaDistrict(int id)
        {
            MetaDistrict metaDistrict = db.MetaDistricts.Find(id);
            MetaDistrictDTO response = new MetaDistrictDTO();
            response.DistrictName = metaDistrict.DistrictName;
            response.DistrictId = metaDistrict.DistrictId;
            response.UserId = metaDistrict.UserId;
            response.DistrictAddress = db.Addresses.Find(metaDistrict.DistrictAddress);
            response.AreaId = metaDistrict.AreaId;
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response); 
        }

        public MetaDistrictDTO GetMetaDistrictLocal(int id)
        {
            MetaDistrict metaDistrict = db.MetaDistricts.Find(id);
            MetaDistrictDTO response = new MetaDistrictDTO();
            response.DistrictName = metaDistrict.DistrictName;
            response.DistrictId = metaDistrict.DistrictId;
            response.UserId = metaDistrict.UserId;
            response.DistrictAddress = db.Addresses.Find(metaDistrict.DistrictAddress);
            response.AreaId = metaDistrict.AreaId;
            if (response == null)
            {
                return null;
            }

            return response;
        }

        // PUT: api/MetaDistricts/5
        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead, DistPastor")]
        [Route("updateDistrict")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaDistrict(int id, districtAddressUpdateDTO data)
        {
            MetaDistrict metaDistrict = data.district;
            Address address = data.address;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaDistrict.DistrictId)
            {
                return BadRequest();
            }

            db.Entry(metaDistrict).State = EntityState.Modified;
            AddressUtility serviceObject = new AddressUtility();
            serviceObject.updateAddress(address);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaDistrictExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MetaDistricts
        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead, DistPastor")]
        [Route("createDistrict")]
        [ResponseType(typeof(MetaDistrict))]
        public IHttpActionResult PostMetaDistrict(MetaDistrictDTO[] metaDistrict)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AddressUtility serviceObject = new AddressUtility();
            foreach(MetaDistrictDTO i in metaDistrict)
            {
                MetaDistrict distict = new MetaDistrict();
                distict.DistrictName = i.DistrictName;
                distict.UserId = i.UserId;
                distict.AreaId = i.AreaId;
                distict.DistrictAddress = serviceObject.addAddress(i.DistrictAddress);
                db.MetaDistricts.Add(distict);
            }
            db.SaveChanges();
            return Ok();
        }

        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead")]
        [Route("deleteDistrict")]
        // DELETE: api/MetaDistricts/5
        [ResponseType(typeof(MetaDistrict))]
        public IHttpActionResult DeleteMetaDistrict(int id)
        {
            MetaDistrict metaDistrict = db.MetaDistricts.Find(id);
            if (metaDistrict == null)
            {
                return NotFound();
            }

            db.MetaDistricts.Remove(metaDistrict);
            db.SaveChanges();

            return Ok(metaDistrict);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaDistrictExists(int id)
        {
            return db.MetaDistricts.Count(e => e.DistrictId == id) > 0;
        }
    }
}