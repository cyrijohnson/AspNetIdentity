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
    public class MetaLocalAssembliesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MetaLocalAssemblies
        public IQueryable<MetaLocalAssembly> GetMetaLocalAssemblies()
        {
            return db.MetaLocalAssemblies;
        }

        // GET: api/MetaLocalAssemblies/5
        [ResponseType(typeof(MetaLocalAssembly))]
        public IHttpActionResult GetMetaLocalAssembly(int id)
        {
            MetaLocalAssembly metaLocalAssembly = db.MetaLocalAssemblies.Find(id);
            if (metaLocalAssembly == null)
            {
                return NotFound();
            }

            return Ok(metaLocalAssembly);
        }

        // PUT: api/MetaLocalAssemblies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaLocalAssembly(int id, MetaLocalAssembly metaLocalAssembly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaLocalAssembly.AssemblyId)
            {
                return BadRequest();
            }

            db.Entry(metaLocalAssembly).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaLocalAssemblyExists(id))
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

        // POST: api/MetaLocalAssemblies
        [ResponseType(typeof(MetaLocalAssembly))]
        public IHttpActionResult PostMetaLocalAssembly(MetaLocalAssembly metaLocalAssembly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MetaLocalAssemblies.Add(metaLocalAssembly);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = metaLocalAssembly.AssemblyId }, metaLocalAssembly);
        }

        // DELETE: api/MetaLocalAssemblies/5
        [ResponseType(typeof(MetaLocalAssembly))]
        public IHttpActionResult DeleteMetaLocalAssembly(int id)
        {
            MetaLocalAssembly metaLocalAssembly = db.MetaLocalAssemblies.Find(id);
            if (metaLocalAssembly == null)
            {
                return NotFound();
            }

            db.MetaLocalAssemblies.Remove(metaLocalAssembly);
            db.SaveChanges();

            return Ok(metaLocalAssembly);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaLocalAssemblyExists(int id)
        {
            return db.MetaLocalAssemblies.Count(e => e.AssemblyId == id) > 0;
        }
    }
}