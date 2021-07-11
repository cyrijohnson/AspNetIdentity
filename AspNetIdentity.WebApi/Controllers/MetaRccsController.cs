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
    public class MetaRccsController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MetaRccs
        [Authorize]
        [Route("getAllRccs")]
        public IQueryable<MetaRcc> GetMetaRccs()
        {
            return db.MetaRccs;
        }

        // GET: api/MetaRccs/5
        [Authorize]
        [Route("getAllRccById")]
        [ResponseType(typeof(MetaRcc))]
        public IHttpActionResult GetMetaRcc(int id)
        {
            MetaRcc metaRcc = db.MetaRccs.Find(id);
            if (metaRcc == null)
            {
                return NotFound();
            }

            return Ok(metaRcc);
        }

        public MetaRcc GetMetaRccLocal(int id)
        {
            MetaRcc metaRcc = db.MetaRccs.Find(id);
            if (metaRcc == null)
            {
                return null;
            }

            return metaRcc;
        }

        // PUT: api/MetaRccs/5
        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead")]
        [Route("updateRcc")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaRcc(int id, MetaRcc metaRcc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaRcc.RccId)
            {
                return BadRequest();
            }

            db.Entry(metaRcc).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaRccExists(id))
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

        // POST: api/MetaRccs
        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead")]
        [Route("createRCC")]
        [ResponseType(typeof(MetaRcc))]
        public IHttpActionResult PostMetaRcc(MetaRcc[] metaRcc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach(MetaRcc record in metaRcc)
            {
                db.MetaRccs.Add(record);
            }
            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/MetaRccs/5
        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor")]
        [Route("deleteRccById")]
        [ResponseType(typeof(MetaRcc))]
        public IHttpActionResult DeleteMetaRcc(int id)
        {
            MetaRcc metaRcc = db.MetaRccs.Find(id);
            if (metaRcc == null)
            {
                return NotFound();
            }

            db.MetaRccs.Remove(metaRcc);
            db.SaveChanges();

            return Ok(metaRcc);
        }
        [Authorize]
        [Route("getAllRccsByCountry")]
        public async System.Threading.Tasks.Task<IHttpActionResult> getRccByCountry(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            string query = "SELECT * FROM MetaRccs where CountryId=@p0";
            MetaRcc metaRcc = await db.MetaRccs.SqlQuery(query, id).SingleOrDefaultAsync();
            if (metaRcc == null)
            {
                return NotFound();
            }

            return Ok(metaRcc);
        }

        public MetaRcc[] getRccByCountryArray(int id)
        {
            if (id == 0)
            {
                return null;
            }

            string query = "SELECT * FROM MetaRccs where CountryId=@p0";
            MetaRcc[] metaRcc = db.MetaRccs.SqlQuery(query, id).ToArray();
            if (metaRcc == null)
            {
                return null; ;
            }

            return metaRcc;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaRccExists(int id)
        {
            return db.MetaRccs.Count(e => e.RccId == id) > 0;
        }
    }
}