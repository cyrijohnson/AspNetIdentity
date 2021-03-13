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
    public class MetaEventTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MetaEventTypes
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getAllEventTypes")]
        public IQueryable<MetaEventType> GetMetaEventTypes()
        {
            return db.MetaEventTypes;
        }

        // GET: api/MetaEventTypes/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getEventTypeById")]
        [ResponseType(typeof(MetaEventType))]
        public IHttpActionResult GetMetaEventType(int id)
        {
            MetaEventType metaEventType = db.MetaEventTypes.Find(id);
            if (metaEventType == null)
            {
                return NotFound();
            }

            return Ok(metaEventType);
        }

        // PUT: api/MetaEventTypes/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("updateEventType")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaEventType(int id, MetaEventType metaEventType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaEventType.EventTypeId)
            {
                return BadRequest();
            }

            db.Entry(metaEventType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaEventTypeExists(id))
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
        [Route("createEventType")]
        // POST: api/MetaEventTypes
        [ResponseType(typeof(MetaEventType))]
        public IHttpActionResult PostMetaEventType(MetaEventType metaEventType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MetaEventTypes.Add(metaEventType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = metaEventType.EventTypeId }, metaEventType);
        }

        // DELETE: api/MetaEventTypes/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("deleteEventType")]
        [ResponseType(typeof(MetaEventType))]
        public IHttpActionResult DeleteMetaEventType(int id)
        {
            MetaEventType metaEventType = db.MetaEventTypes.Find(id);
            if (metaEventType == null)
            {
                return NotFound();
            }

            db.MetaEventTypes.Remove(metaEventType);
            db.SaveChanges();

            return Ok(metaEventType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaEventTypeExists(int id)
        {
            return db.MetaEventTypes.Count(e => e.EventTypeId == id) > 0;
        }
    }
}