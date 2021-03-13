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
    public class MetaGroupsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MetaGroups
        public IQueryable<MetaGroup> GetMetaGroups()
        {
            return db.MetaGroups;
        }

        // GET: api/MetaGroups/5
        [ResponseType(typeof(MetaGroup))]
        public IHttpActionResult GetMetaGroup(int id)
        {
            MetaGroup metaGroup = db.MetaGroups.Find(id);
            if (metaGroup == null)
            {
                return NotFound();
            }

            return Ok(metaGroup);
        }

        // PUT: api/MetaGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaGroup(int id, MetaGroup metaGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaGroup.GroupId)
            {
                return BadRequest();
            }

            db.Entry(metaGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaGroupExists(id))
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

        // POST: api/MetaGroups
        [ResponseType(typeof(MetaGroup))]
        public IHttpActionResult PostMetaGroup(MetaGroup metaGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MetaGroups.Add(metaGroup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = metaGroup.GroupId }, metaGroup);
        }

        // DELETE: api/MetaGroups/5
        [ResponseType(typeof(MetaGroup))]
        public IHttpActionResult DeleteMetaGroup(int id)
        {
            MetaGroup metaGroup = db.MetaGroups.Find(id);
            if (metaGroup == null)
            {
                return NotFound();
            }

            db.MetaGroups.Remove(metaGroup);
            db.SaveChanges();

            return Ok(metaGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaGroupExists(int id)
        {
            return db.MetaGroups.Count(e => e.GroupId == id) > 0;
        }
    }
}