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
    public class GroupMemberRelationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/GroupMemberRelations
        public IQueryable<GroupMemberRelation> GetGroupMemberRelations()
        {
            return db.GroupMemberRelations;
        }

        // GET: api/GroupMemberRelations/5
        [ResponseType(typeof(GroupMemberRelation))]
        public IHttpActionResult GetGroupMemberRelation(int id)
        {
            GroupMemberRelation groupMemberRelation = db.GroupMemberRelations.Find(id);
            if (groupMemberRelation == null)
            {
                return NotFound();
            }

            return Ok(groupMemberRelation);
        }

        // PUT: api/GroupMemberRelations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGroupMemberRelation(int id, GroupMemberRelation groupMemberRelation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != groupMemberRelation.GroupId)
            {
                return BadRequest();
            }

            db.Entry(groupMemberRelation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupMemberRelationExists(id))
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

        // POST: api/GroupMemberRelations
        [ResponseType(typeof(GroupMemberRelation))]
        public IHttpActionResult PostGroupMemberRelation(GroupMemberRelation groupMemberRelation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GroupMemberRelations.Add(groupMemberRelation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GroupMemberRelationExists(groupMemberRelation.GroupId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = groupMemberRelation.GroupId }, groupMemberRelation);
        }

        // DELETE: api/GroupMemberRelations/5
        [ResponseType(typeof(GroupMemberRelation))]
        public IHttpActionResult DeleteGroupMemberRelation(int id)
        {
            GroupMemberRelation groupMemberRelation = db.GroupMemberRelations.Find(id);
            if (groupMemberRelation == null)
            {
                return NotFound();
            }

            db.GroupMemberRelations.Remove(groupMemberRelation);
            db.SaveChanges();

            return Ok(groupMemberRelation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupMemberRelationExists(int id)
        {
            return db.GroupMemberRelations.Count(e => e.GroupId == id) > 0;
        }
    }
}