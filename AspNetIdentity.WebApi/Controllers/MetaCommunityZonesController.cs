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
    public class MetaCommunityZonesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead, DistPastor, PresElder")]
        [Route("getAllCommunityZones")]
        // GET: api/MetaCommunityZones
        public async System.Threading.Tasks.Task<IHttpActionResult> GetMetaCommunityZones()
        {
            var metaCZ = db.MetaCommunityZones.ToList();
            List<MetaCommunityZoneDTO> response = new List<MetaCommunityZoneDTO>();
            foreach (MetaCommunityZone data in metaCZ)
            {
                response.Add(new MetaCommunityZoneDTO
                {
                    ZoneId = data.ZoneId,
                    ZoneName = data.ZoneName,
                    ZoneLeader = data.ZoneLeader,
                    ZoneAddress = db.Addresses.Find(data.ZoneAddress),
                });
            }
            if (metaCZ == null)
            {
                return NotFound();
            }

            return Ok(metaCZ);
        }

        // GET: api/MetaCommunityZones/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getCommunityZoneById")]
        [ResponseType(typeof(MetaCommunityZone))]
        public IHttpActionResult GetMetaCommunityZone(int id)
        {
            MetaCommunityZone metaCommunityZone = db.MetaCommunityZones.Find(id);
            if (metaCommunityZone == null)
            {
                return NotFound();
            }
            List<MetaCommunityZoneDTO> response = new List<MetaCommunityZoneDTO>();
            response.Add(new MetaCommunityZoneDTO
            {
                ZoneId = metaCommunityZone.ZoneId,
                ZoneName = metaCommunityZone.ZoneName,
                ZoneLeader = metaCommunityZone.ZoneLeader,
                ZoneAddress = db.Addresses.Find(metaCommunityZone.ZoneAddress),
            });

            return Ok(response);
        }

        // PUT: api/MetaCommunityZones/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("updateCommunityZoneById")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaCommunityZone(int id, CommunityZoneAddressUpdateDTO metaCommunityZoneUpdate)
        {
            MetaCommunityZone metaCommunityZone = metaCommunityZoneUpdate.CommunityZone;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaCommunityZone.ZoneId)
            {
                return BadRequest();
            }

            db.Entry(metaCommunityZone).State = EntityState.Modified;
            AddressUtility serviceObject = new AddressUtility();
            serviceObject.updateAddress(metaCommunityZoneUpdate.CommunityZoneAddress);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaCommunityZoneExists(id))
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

        // POST: api/MetaCommunityZones
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("createCommunityZone")]
        [ResponseType(typeof(MetaCommunityZone))]
        public IHttpActionResult PostMetaCommunityZone(MetaCommunityZoneDTO data)
        {
            MetaCommunityZone metaCommunityZone = new MetaCommunityZone();
            AddressUtility serviceObject = new AddressUtility();
            metaCommunityZone.ZoneName = data.ZoneName;
            metaCommunityZone.ZoneLeader = data.ZoneLeader;
            metaCommunityZone.ZoneAddress = serviceObject.addAddress(data.ZoneAddress);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MetaCommunityZones.Add(metaCommunityZone);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = metaCommunityZone.ZoneId }, metaCommunityZone);
        }

        // DELETE: api/MetaCommunityZones/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("deleteCommunityZone")]
        [ResponseType(typeof(MetaCommunityZone))]
        public IHttpActionResult DeleteMetaCommunityZone(int id)
        {
            MetaCommunityZone metaCommunityZone = db.MetaCommunityZones.Find(id);
            if (metaCommunityZone == null)
            {
                return NotFound();
            }

            db.MetaCommunityZones.Remove(metaCommunityZone);
            db.SaveChanges();

            return Ok(metaCommunityZone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaCommunityZoneExists(int id)
        {
            return db.MetaCommunityZones.Count(e => e.ZoneId == id) > 0;
        }
    }
}