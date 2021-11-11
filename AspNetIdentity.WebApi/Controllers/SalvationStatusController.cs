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
    public class SalvationStatusController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SalvationStatus
        public IQueryable<SalvationStatus> GetSalvationStatus()
        {
            return db.SalvationStatus;
        }

        // GET: api/SalvationStatus/5
        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead, DistPastor, PresElder")]
        [Route("getSalavationStatusById")]
        [ResponseType(typeof(SalvationStatus))]
        public IHttpActionResult GetSalvationStatus(int memberId)
        {
            int id = 0;
            LocalValidationHelper validationHelper = new LocalValidationHelper();
            if(validationHelper.checkUserValidity(User.Identity.Name, memberId))
            {
                id = new MemberUtility().getMemberInfo(memberId).BaptismStatusId;
            }
            SalvationStatus salvationStatus = db.SalvationStatus.Find(id);
            if (salvationStatus == null)
            {
                return NotFound();
            }

            return Ok(salvationStatus);
        }

        // PUT: api/SalvationStatus/5
        [Authorize(Roles = "PresElder")]
        [Route("updateSalvationStatusById")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalvationStatus(SalvationStatus salvationStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int id = 0;
            LocalValidationHelper validationHelper = new LocalValidationHelper();
            if (validationHelper.checkWriteAccess(User.Identity.Name, salvationStatus.MemberId))
            {
                Member memberInfo = new MemberUtility().getMemberInfo(salvationStatus.MemberId);
                if(memberInfo == null)
                {
                    return NotFound();
                } 
                id = memberInfo.AssuranceId;
            }

            if (id != salvationStatus.AssuranceId)
            {
                return BadRequest();
            }

            db.Entry(salvationStatus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalvationStatusExists(id))
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

        // POST: api/SalvationStatus
        [Authorize(Roles = "PresElder")]
        [Route("addSalvationStatus")]
        [ResponseType(typeof(SalvationStatus))]
        public IHttpActionResult PostSalvationStatus(SalvationStatus salvationStatus)
        {
            LocalValidationHelper validationHelper = new LocalValidationHelper();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (validationHelper.checkWriteAccess(User.Identity.Name, salvationStatus.MemberId))
            {
                StatusUtility statusUtil = new StatusUtility();
                db.SalvationStatus.Add(salvationStatus);
                db.SaveChanges();
                string query = "SELECT * FROM SalvationStatus where MemberId=@p0";
                SalvationStatus salStatus = db.SalvationStatus.SqlQuery(query, salvationStatus.MemberId).Single();
                statusUtil.updateMemberInfo(salStatus.AssuranceId, salStatus.MemberId, "SalStatus");
            }
            else 
            {
                return Unauthorized();
            }
            return CreatedAtRoute("DefaultApi", new { id = salvationStatus.AssuranceId }, salvationStatus);
        }

        // DELETE: api/SalvationStatus/5
        [Authorize(Roles = "PresElder")]
        [Route("removeSalvationStatus")]
        [ResponseType(typeof(SalvationStatus))]
        public IHttpActionResult DeleteSalvationStatus(int id)
        {
            SalvationStatus salvationStatus = db.SalvationStatus.Find(id);
            if (salvationStatus == null)
            {
                return NotFound();
            }

            db.SalvationStatus.Remove(salvationStatus);
            db.SaveChanges();

            return Ok(salvationStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalvationStatusExists(int id)
        {
            return db.SalvationStatus.Count(e => e.AssuranceId == id) > 0;
        }
    }
}