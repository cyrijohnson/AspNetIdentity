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
    public class SocialMediaHandlersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SocialMediaHandlers
        public IQueryable<SocialMediaHandler> GetSocialMediaHandlers()
        {
            return db.SocialMediaHandlers;
        }

        // GET: api/SocialMediaHandlers/5
        [ResponseType(typeof(SocialMediaHandler))]
        public IHttpActionResult GetSocialMediaHandler(int id)
        {
            SocialMediaHandler socialMediaHandler = db.SocialMediaHandlers.Find(id);
            if (socialMediaHandler == null)
            {
                return NotFound();
            }

            return Ok(socialMediaHandler);
        }

        // PUT: api/SocialMediaHandlers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSocialMediaHandler(int id, SocialMediaHandler socialMediaHandler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socialMediaHandler.SocialMediaId)
            {
                return BadRequest();
            }

            db.Entry(socialMediaHandler).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialMediaHandlerExists(id))
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

        // POST: api/SocialMediaHandlers
        [ResponseType(typeof(SocialMediaHandler))]
        public IHttpActionResult PostSocialMediaHandler(SocialMediaHandler socialMediaHandler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SocialMediaHandlers.Add(socialMediaHandler);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = socialMediaHandler.SocialMediaId }, socialMediaHandler);
        }

        // DELETE: api/SocialMediaHandlers/5
        [ResponseType(typeof(SocialMediaHandler))]
        public IHttpActionResult DeleteSocialMediaHandler(int id)
        {
            SocialMediaHandler socialMediaHandler = db.SocialMediaHandlers.Find(id);
            if (socialMediaHandler == null)
            {
                return NotFound();
            }

            db.SocialMediaHandlers.Remove(socialMediaHandler);
            db.SaveChanges();

            return Ok(socialMediaHandler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SocialMediaHandlerExists(int id)
        {
            return db.SocialMediaHandlers.Count(e => e.SocialMediaId == id) > 0;
        }
    }
}