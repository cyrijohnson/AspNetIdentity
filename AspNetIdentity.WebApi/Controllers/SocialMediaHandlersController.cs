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
    public class SocialMediaHandlersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //public IQueryable<SocialMediaHandler> GetSocialMediaHandlers()
        //{
           // return db.SocialMediaHandlers;
        //}

        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead, DistPastor, PresElder")]
        [Route("getSmhByMemberId")]
        [ResponseType(typeof(SocialMediaHandler))]
        public IHttpActionResult GetSocialMediaHandler(int memberId)
        {
            List<SocialMediaHandler> response = new List<SocialMediaHandler>();
            LocalValidationHelper validationHelper = new LocalValidationHelper();
            if (validationHelper.checkUserValidity(User.Identity.Name, memberId))
            {
                var socialMediaHandler = db.SocialMediaHandlers.SqlQuery("SELECT * FROM SocialMediaHandlers where MemberId=@p0", memberId);
                foreach (SocialMediaHandler data in socialMediaHandler)
                {
                    response.Add(new SocialMediaHandler
                    {
                        SocialMediaId = data.SocialMediaId,
                        SocialMediaText = data.SocialMediaText,
                        MemberId = data.MemberId,
                        Link = data.Link
                    });
                }
            }
            else
            {
                return NotFound();
            }

            return Ok(response);
        }

        [Authorize(Roles = "PresElder")]
        [Route("updateSmhById")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSocialMediaHandler(int id, SocialMediaHandler socialMediaHandler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socialMediaHandler.SocialMediaId)
            {
                return BadRequest();
            }
            LocalValidationHelper validationHelper = new LocalValidationHelper();
            if (validationHelper.checkWriteAccess(User.Identity.Name, socialMediaHandler.MemberId))
            {
                return NotFound();
            }
            db.Entry(socialMediaHandler).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialMediaHandlerExists(id))
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

        // POST: api/SocialMediaHandlers
        [Authorize(Roles = "PresElder")]
        [Route("AddSmh")]
        [ResponseType(typeof(SocialMediaHandler))]
        public IHttpActionResult PostSocialMediaHandler(SocialMediaHandler socialMediaHandler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LocalValidationHelper validationHelper = new LocalValidationHelper();
            if (validationHelper.checkWriteAccess(User.Identity.Name, socialMediaHandler.MemberId))
            {
                return NotFound();
            }

            db.SocialMediaHandlers.Add(socialMediaHandler);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = socialMediaHandler.SocialMediaId }, socialMediaHandler);
        }

        // DELETE: api/SocialMediaHandlers/5
        [Authorize(Roles = "PresElder")]
        [Route("deleteSmhById")]
        [ResponseType(typeof(SocialMediaHandler))]
        public IHttpActionResult DeleteSocialMediaHandler(int id)
        {
            SocialMediaHandler socialMediaHandler = db.SocialMediaHandlers.Find(id);
            if (socialMediaHandler == null)
            {
                return NotFound();
            }
            LocalValidationHelper validationHelper = new LocalValidationHelper();
            if (validationHelper.checkWriteAccess(User.Identity.Name, socialMediaHandler.MemberId))
            {
                return NotFound();
            }
            db.SocialMediaHandlers.Remove(socialMediaHandler);
            db.SaveChanges();

            return Ok(socialMediaHandler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SocialMediaHandlerExists(int id)
        {
            return db.SocialMediaHandlers.Count(e => e.SocialMediaId == id) > 0;
        }
    }
}