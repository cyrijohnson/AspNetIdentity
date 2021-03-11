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
    public class MetaCountriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getAllCountries")]
        public IQueryable<MetaCountry> GetMetaCountries()
        {
            return db.MetaCountries;
        }

        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getCountryById")]
        [ResponseType(typeof(MetaCountry))]
        public IHttpActionResult GetMetaCountry(int id)
        {
            MetaCountry metaCountry = db.MetaCountries.Find(id);
            if (metaCountry == null)
            {
                return NotFound();
            }

            return Ok(metaCountry);
        }

        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getCountryByBlock")]
        [ResponseType(typeof(MetaCountry))]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetMetaCountryByBlockAsync(int id)
        {
            if (id==0)
            {
                return BadRequest(ModelState);
            }
            String query = "SELECT * FROM MetaCountries WHERE BlockId = @p0";
            MetaCountry metaCountry = await db.MetaCountries.SqlQuery(query, id).SingleOrDefaultAsync();
            if (metaCountry == null)
            {
                return NotFound();
            }

            return Ok(metaCountry);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [Route("updateCountryById")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaCountry(int id, MetaCountry metaCountry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaCountry.CountryId)
            {
                return BadRequest();
            }

            db.Entry(metaCountry).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaCountryExists(id))
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
        [Route("createCountry")]
        [ResponseType(typeof(MetaCountry))]
        public IHttpActionResult PostMetaCountry(MetaCountry metaCountry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MetaCountries.Add(metaCountry);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = metaCountry.CountryId }, metaCountry);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [Route("deleteCountryById")]
        [ResponseType(typeof(MetaCountry))]
        public IHttpActionResult DeleteMetaCountry(int id)
        {
            MetaCountry metaCountry = db.MetaCountries.Find(id);
            if (metaCountry == null)
            {
                return NotFound();
            }

            db.MetaCountries.Remove(metaCountry);
            db.SaveChanges();

            return Ok(metaCountry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaCountryExists(int id)
        {
            return db.MetaCountries.Count(e => e.CountryId == id) > 0;
        }
    }
}