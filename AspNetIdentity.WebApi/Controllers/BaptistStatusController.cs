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
    public class BaptistStatusController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BaptistStatus
        public IQueryable<BaptistStatus> GetBaptistStatus()
        {
            return db.BaptistStatus;
        }

        // GET: api/BaptistStatus/5
        [ResponseType(typeof(BaptistStatus))]
        public IHttpActionResult GetBaptistStatus(int id)
        {
            BaptistStatus baptistStatus = db.BaptistStatus.Find(id);
            if (baptistStatus == null)
            {
                return NotFound();
            }

            return Ok(baptistStatus);
        }

        // PUT: api/BaptistStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBaptistStatus(int id, BaptistStatus baptistStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != baptistStatus.BaptismId)
            {
                return BadRequest();
            }

            db.Entry(baptistStatus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BaptistStatusExists(id))
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

        // POST: api/BaptistStatus
        [ResponseType(typeof(BaptistStatus))]
        public IHttpActionResult PostBaptistStatus(BaptistStatus baptistStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BaptistStatus.Add(baptistStatus);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = baptistStatus.BaptismId }, baptistStatus);
        }

        // DELETE: api/BaptistStatus/5
        [ResponseType(typeof(BaptistStatus))]
        public IHttpActionResult DeleteBaptistStatus(int id)
        {
            BaptistStatus baptistStatus = db.BaptistStatus.Find(id);
            if (baptistStatus == null)
            {
                return NotFound();
            }

            db.BaptistStatus.Remove(baptistStatus);
            db.SaveChanges();

            return Ok(baptistStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BaptistStatusExists(int id)
        {
            return db.BaptistStatus.Count(e => e.BaptismId == id) > 0;
        }
    }
}