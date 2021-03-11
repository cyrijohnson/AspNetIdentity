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
    public class MetaAreasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getAllAreasByRcc")]
        public async System.Threading.Tasks.Task<IHttpActionResult> getAreasByRcc(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            string query = "SELECT * FROM MetaAreas where RccId=@p0";
            var metaArea = db.MetAreas.SqlQuery(query, id).ToList();
            List<MetaAreaDTO> response = new List<MetaAreaDTO>();
            foreach(MetaArea data in metaArea)
            {
                response.Add(new MetaAreaDTO
                {
                    AreaId = data.AreaId,
                    AreaName = data.AreaName,
                    UserId = data.UserId,
                    RccId = data.RccId,
                    AreaAddress = db.Addresses.Find(data.AreaAddress)
                });
            }
            if (metaArea == null)
            {
                return NotFound();
            }

            return Ok(metaArea);
        }
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getAllAreas")]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetMetAreas()
        {
            var metaArea = db.MetAreas.ToList();
            List<MetaAreaDTO> response = new List<MetaAreaDTO>();
            foreach (MetaArea data in metaArea)
            {
                response.Add(new MetaAreaDTO
                {
                    AreaId = data.AreaId,
                    AreaName = data.AreaName,
                    UserId = data.UserId,
                    RccId = data.RccId,
                    AreaAddress = db.Addresses.Find(data.AreaAddress)
                });
            }
            if (metaArea == null)
            {
                return NotFound();
            }

            return Ok(metaArea);
        }

        // GET: api/MetaAreas/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getAllAreasById")]
        [ResponseType(typeof(MetaArea))]
        public IHttpActionResult GetMetaArea(int id)
        {
            var metaArea = db.MetAreas.Find(id);
            MetaAreaDTO response = new MetaAreaDTO();
            response.AreaId = metaArea.AreaId;
            response.AreaName = metaArea.AreaName;
            response.UserId = metaArea.UserId;
            response.AreaAddress = db.Addresses.Find(metaArea.AreaAddress);
            response.RccId = metaArea.RccId;
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [Route("updateAreaById")]
        // PUT: api/MetaAreas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaArea(int id, areaAddressUpdateDTO data)
        {
            MetaArea metaArea = data.area;
            Address address = data.address;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaArea.AreaId)
            {
                return BadRequest();
            }

            db.Entry(metaArea).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                AddressUtility serviceObject = new AddressUtility();
                serviceObject.updateAddress(address);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaAreaExists(id))
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

        [Authorize(Roles = "Admin,SuperAdmin")]
        [Route("createArea")]
        // POST: api/MetaAreas
        [ResponseType(typeof(MetaArea))]
        public IHttpActionResult PostMetaArea(MetaAreaDTO metaAreaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AddressUtility serviceObject = new AddressUtility();
            MetaArea metaArea = new MetaArea();
            metaArea.AreaName = metaAreaDTO.AreaName;
            metaArea.UserId = metaAreaDTO.UserId;
            metaArea.RccId = metaAreaDTO.RccId;
            metaArea.AreaAddress = serviceObject.addAddress(metaAreaDTO.AreaAddress);
            db.MetAreas.Add(metaArea);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = metaArea.AreaId }, metaArea);
        }

        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("deleteAreaById")]
        // DELETE: api/MetaAreas/5
        [ResponseType(typeof(MetaArea))]
        public IHttpActionResult DeleteMetaArea(int id)
        {
            MetaArea metaArea = db.MetAreas.Find(id);
            if (metaArea == null)
            {
                return NotFound();
            }

            db.MetAreas.Remove(metaArea);
            db.SaveChanges();

            return Ok(metaArea);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaAreaExists(int id)
        {
            return db.MetAreas.Count(e => e.AreaId == id) > 0;
        }
    }
}