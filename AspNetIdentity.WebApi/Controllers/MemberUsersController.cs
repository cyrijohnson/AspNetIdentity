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
    [RoutePrefix("api/MemebershipManager")]
    public class MemberUsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MemberUsers
        public IQueryable<MemberUser> GetMemberUsers()
        {
            return db.MemberUsers;
        }

        // GET: api/MemberUsers/5
        [ResponseType(typeof(MemberUser))]
        public IHttpActionResult GetMemberUser(string id)
        {
            MemberUser memberUser = db.MemberUsers.Find(id);
            if (memberUser == null)
            {
                return NotFound();
            }

            return Ok(memberUser);
        }

        public MemberUser GetMemberUserLocal(string id)
        {
            MemberUser memberUser = db.MemberUsers.Find(id);
            if (memberUser == null)
            {
                return null;
            }

            return memberUser;
        }

        // PUT: api/MemberUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMemberUser(string id, MemberUser memberUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != memberUser.Username)
            {
                return BadRequest();
            }

            db.Entry(memberUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberUserExists(id))
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

        [Route("updateMemberId")]
        [ResponseType(typeof(MemberUser))]
        public IHttpActionResult PostMemberUser(MemberUser memberUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MemberUsers.Add(memberUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MemberUserExists(memberUser.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = memberUser.Username }, memberUser);
        }

        // DELETE: api/MemberUsers/5
        [ResponseType(typeof(MemberUser))]
        public IHttpActionResult DeleteMemberUser(string id)
        {
            MemberUser memberUser = db.MemberUsers.Find(id);
            if (memberUser == null)
            {
                return NotFound();
            }

            db.MemberUsers.Remove(memberUser);
            db.SaveChanges();

            return Ok(memberUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MemberUserExists(string id)
        {
            return db.MemberUsers.Count(e => e.Username == id) > 0;
        }
    }
}