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
    public class MetaPostingsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MetaPostings
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getAllPostingTypes")]
        public IQueryable<MetaPostings> GetMetaPostings()
        {
            return db.MetaPostings;
        }

        // GET: api/MetaPostings/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getPostingTypeById")]
        [ResponseType(typeof(MetaPostings))]
        public IHttpActionResult GetMetaPostings(int id)
        {
            MetaPostings metaPostings = db.MetaPostings.Find(id);
            if (metaPostings == null)
            {
                return NotFound();
            }

            return Ok(metaPostings);
        }

        // PUT: api/MetaPostings/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("updatePostingType")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaPostings(int id, MetaPostings metaPostings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaPostings.PostingId)
            {
                return BadRequest();
            }

            db.Entry(metaPostings).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaPostingsExists(id))
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

        // POST: api/MetaPostings
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("createPostingType")]
        [ResponseType(typeof(MetaPostings))]
        public IHttpActionResult PostMetaPostings(MetaPostings metaPostings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MetaPostings.Add(metaPostings);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = metaPostings.PostingId }, metaPostings);
        }

        // DELETE: api/MetaPostings/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("deletePostingType")]
        [ResponseType(typeof(MetaPostings))]
        public IHttpActionResult DeleteMetaPostings(int id)
        {
            MetaPostings metaPostings = db.MetaPostings.Find(id);
            if (metaPostings == null)
            {
                return NotFound();
            }

            db.MetaPostings.Remove(metaPostings);
            db.SaveChanges();

            return Ok(metaPostings);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaPostingsExists(int id)
        {
            return db.MetaPostings.Count(e => e.PostingId == id) > 0;
        }
    }
}