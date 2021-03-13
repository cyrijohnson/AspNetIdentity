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

namespace AspNetIdentity.WebApi.Controllers
{
    [RoutePrefix("api/metadata")]
    public class MetaPastoralCareTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MetaPastoralCareTypes
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getAllPcEventTypes")]
        public IQueryable<MetaPastoralCareType> GetMetaPastoralCareTypes()
        {
            return db.MetaPastoralCareTypes;
        }

        // GET: api/MetaPastoralCareTypes/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getPcEventTypeById")]
        [ResponseType(typeof(MetaPastoralCareType))]
        public IHttpActionResult GetMetaPastoralCareType(int id)
        {
            MetaPastoralCareType metaPastoralCareType = db.MetaPastoralCareTypes.Find(id);
            if (metaPastoralCareType == null)
            {
                return NotFound();
            }

            return Ok(metaPastoralCareType);
        }
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("updatePcEventType")]
        // PUT: api/MetaPastoralCareTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaPastoralCareType(int id, MetaPastoralCareType metaPastoralCareType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaPastoralCareType.PcId)
            {
                return BadRequest();
            }

            db.Entry(metaPastoralCareType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaPastoralCareTypeExists(id))
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
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("createPcEventType")]
        // POST: api/MetaPastoralCareTypes
        [ResponseType(typeof(MetaPastoralCareType))]
        public IHttpActionResult PostMetaPastoralCareType(MetaPastoralCareType metaPastoralCareType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MetaPastoralCareTypes.Add(metaPastoralCareType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = metaPastoralCareType.PcId }, metaPastoralCareType);
        }

        // DELETE: api/MetaPastoralCareTypes/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("deletePcEventType")]
        [ResponseType(typeof(MetaPastoralCareType))]
        public IHttpActionResult DeleteMetaPastoralCareType(int id)
        {
            MetaPastoralCareType metaPastoralCareType = db.MetaPastoralCareTypes.Find(id);
            if (metaPastoralCareType == null)
            {
                return NotFound();
            }

            db.MetaPastoralCareTypes.Remove(metaPastoralCareType);
            db.SaveChanges();

            return Ok(metaPastoralCareType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaPastoralCareTypeExists(int id)
        {
            return db.MetaPastoralCareTypes.Count(e => e.PcId == id) > 0;
        }
    }
}