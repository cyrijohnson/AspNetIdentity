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
using AspNetIdentity.WebApi.Services;

namespace AspNetIdentity.WebApi.Controllers
{
    [RoutePrefix("api/MemberStatus")]
    public class BaptistStatusController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BaptistStatus
        public IQueryable<BaptistStatus> GetBaptistStatus()
        {
            return db.BaptistStatus;
        }

        // GET: api/BaptistStatus/5
        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead, DistPastor, PresElder")]
        [Route("getBaptismStatusById")]
        [ResponseType(typeof(BaptistStatus))]
        public IHttpActionResult GetBaptistStatus(int memberId)
        {
            int id = 0;
            LocalValidationHelper validationHelper = new LocalValidationHelper();
            if (validationHelper.checkUserValidity(User.Identity.Name, memberId))
            {
                id = new MemberUtility().getMemberInfo(memberId).BaptismStatusId;
            }
            BaptistStatus baptistStatus = db.BaptistStatus.Find(id);
            if (baptistStatus == null)
            {
                return NotFound();
            }

            return Ok(baptistStatus);
        }

        // PUT: api/BaptistStatus/5
        [Authorize(Roles = "PresElder")]
        [Route("updateBaptismStatus")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBaptistStatus(BaptistStatus baptistStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = 0;
            LocalValidationHelper validationHelper = new LocalValidationHelper();
            if (validationHelper.checkWriteAccess(User.Identity.Name, baptistStatus.MemberId))
            {
                Member memberInfo = new MemberUtility().getMemberInfo(baptistStatus.MemberId);
                if (memberInfo == null)
                {
                    return NotFound();
                }
                id = memberInfo.BaptismStatusId;
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
        [Authorize(Roles = "PresElder")]
        [Route("addBaptismStatus")]
        [ResponseType(typeof(BaptistStatus))]
        public IHttpActionResult PostBaptistStatus(BaptistStatus baptistStatus)
        {
            LocalValidationHelper validationHelper = new LocalValidationHelper();
            if (validationHelper.checkWriteAccess(User.Identity.Name, baptistStatus.MemberId))
            {
                StatusUtility statusUtil = new StatusUtility();
                db.BaptistStatus.Add(baptistStatus);
                db.SaveChanges();
                string query = "SELECT * FROM BaptistStatus where MemberId=@p0";
                BaptistStatus baptismStatus = db.BaptistStatus.SqlQuery(query, baptistStatus.MemberId).Single();
                statusUtil.updateMemberInfo(baptismStatus.BaptismId, baptistStatus.MemberId, "BapStatus");
            }
            else
            {
                return Unauthorized();
            }

            

            return CreatedAtRoute("DefaultApi", new { id = baptistStatus.BaptismId }, baptistStatus);
        }

        // DELETE: api/BaptistStatus/5
        [Authorize(Roles = "PresElder")]
        [Route("deleteBaptismStatus")]
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