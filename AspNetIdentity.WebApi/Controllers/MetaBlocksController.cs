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
    public class MetaBlocksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getAllBlocks")]
        public IQueryable<MetaBlock> GetMetaBlocks()
        {
            return db.MetaBlocks;
        }

        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getBlockById")]
        [ResponseType(typeof(MetaBlock))]
        public IHttpActionResult GetMetaBlock(int id)
        {
            MetaBlock metaBlock = db.MetaBlocks.Find(id);
            if (metaBlock == null)
            {
                return NotFound();
            }

            return Ok(metaBlock);
        }

        // PUT: api/MetaBlocks/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("UpdateBlockById")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaBlock(int id, MetaBlock metaBlock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaBlock.BlockId)
            {
                return BadRequest();
            }

            db.Entry(metaBlock).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaBlockExists(id))
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
        [Route("createBlock", Name ="DefaultApi")]

        [ResponseType(typeof(MetaBlock))]
        public IHttpActionResult PostMetaBlock(MetaBlock metaBlock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;
            db.MetaBlocks.Add(metaBlock);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = metaBlock.BlockId }, metaBlock);
        }

        // DELETE: api/MetaBlocks/5
        [Authorize(Roles = "Admin,SuperAdmin")]
        [Route("deleteBlock")]
        [ResponseType(typeof(MetaBlock))]
        public IHttpActionResult DeleteMetaBlock(int id)
        {
            MetaBlock metaBlock = db.MetaBlocks.Find(id);
            if (metaBlock == null)
            {
                return NotFound();
            }

            db.MetaBlocks.Remove(metaBlock);
            db.SaveChanges();

            return Ok(metaBlock);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaBlockExists(int id)
        {
            return db.MetaBlocks.Count(e => e.BlockId == id) > 0;
        }
    }
}