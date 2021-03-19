﻿using System;
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
    public class PostingsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Postings
        public IQueryable<Posting> GetPostings()
        {
            return db.Postings;
        }

        // GET: api/Postings/5
        [ResponseType(typeof(Posting))]
        public IHttpActionResult GetPosting(int id)
        {
            Posting posting = db.Postings.Find(id);
            if (posting == null)
            {
                return NotFound();
            }

            return Ok(posting);
        }

        // PUT: api/Postings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPosting(int id, Posting posting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != posting.PostingId)
            {
                return BadRequest();
            }

            db.Entry(posting).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostingExists(id))
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

        // POST: api/Postings
        [ResponseType(typeof(Posting))]
        public IHttpActionResult PostPosting(Posting posting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Postings.Add(posting);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = posting.PostingId }, posting);
        }

        // DELETE: api/Postings/5
        [ResponseType(typeof(Posting))]
        public IHttpActionResult DeletePosting(int id)
        {
            Posting posting = db.Postings.Find(id);
            if (posting == null)
            {
                return NotFound();
            }

            db.Postings.Remove(posting);
            db.SaveChanges();

            return Ok(posting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostingExists(int id)
        {
            return db.Postings.Count(e => e.PostingId == id) > 0;
        }
    }
}