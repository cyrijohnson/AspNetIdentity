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
    public class MetaProfessionCategoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MetaProfessionCategories
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getAllProfessionCategories")]
        public IQueryable<MetaProfessionCategory> GetMetaProfessionCategories()
        {
            return db.MetaProfessionCategories;
        }

        // GET: api/MetaProfessionCategories/5
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getProfessionCategoryById")]
        [ResponseType(typeof(MetaProfessionCategory))]
        public IHttpActionResult GetMetaProfessionCategory(int id)
        {
            MetaProfessionCategory metaProfessionCategory = db.MetaProfessionCategories.Find(id);
            if (metaProfessionCategory == null)
            {
                return NotFound();
            }

            return Ok(metaProfessionCategory);
        }

        // PUT: api/MetaProfessionCategories/5
        [Authorize(Roles = "Admin,SuperAdmin")]
        [Route("updateProfessionCategory")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaProfessionCategory(int id, MetaProfessionCategory metaProfessionCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaProfessionCategory.ProfCatId)
            {
                return BadRequest();
            }

            db.Entry(metaProfessionCategory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaProfessionCategoryExists(id))
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
        [Route("createProfessionCategory")]
        // POST: api/MetaProfessionCategories
        [ResponseType(typeof(MetaProfessionCategory))]
        public IHttpActionResult PostMetaProfessionCategory(MetaProfessionCategory metaProfessionCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MetaProfessionCategories.Add(metaProfessionCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = metaProfessionCategory.ProfCatId }, metaProfessionCategory);
        }

        // DELETE: api/MetaProfessionCategories/5
        [Authorize(Roles = "Admin,SuperAdmin")]
        [Route("DeleteProfessionCategory")]
        [ResponseType(typeof(MetaProfessionCategory))]
        public IHttpActionResult DeleteMetaProfessionCategory(int id)
        {
            MetaProfessionCategory metaProfessionCategory = db.MetaProfessionCategories.Find(id);
            if (metaProfessionCategory == null)
            {
                return NotFound();
            }

            db.MetaProfessionCategories.Remove(metaProfessionCategory);
            db.SaveChanges();

            return Ok(metaProfessionCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaProfessionCategoryExists(int id)
        {
            return db.MetaProfessionCategories.Count(e => e.ProfCatId == id) > 0;
        }
    }
}