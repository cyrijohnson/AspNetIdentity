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
using Newtonsoft.Json;

namespace AspNetIdentity.WebApi.Controllers
{
    [RoutePrefix("api/MemebershipManager")]
    public class MembersController : BaseApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead, DistPastor, PresElder")]
        [Route("getAllMemebers")]
        public ReturnDataModel GetMembers()
        {
            MemberUtility utilityObject = new MemberUtility();
            MemberUsersController controllerInstance = new MemberUsersController();
            MemberUser bridgeTable = controllerInstance.GetMemberUserLocal(User.Identity.Name);
            Member memberInfo = GetMemberById(int.Parse(bridgeTable.MemberId));
            if (User.IsInRole("SuperAdmin") || User.IsInRole("TopMgmt"))
            {
                return utilityObject.getMembersAdmin();
            }
            else if (User.IsInRole("BlkCoor"))
            {
                return utilityObject.getMembersBlockCoor(memberInfo.LocalAssemblyId, User.Identity.Name);
            }
            else if (User.IsInRole("NatHead"))
            {
                return utilityObject.getMembersNatHead(memberInfo.LocalAssemblyId, User.Identity.Name);
            }
            else if (User.IsInRole("RccHead"))
            {
                return utilityObject.getMembersRccCoor(memberInfo.LocalAssemblyId, User.Identity.Name);
            }
            else if (User.IsInRole("AreaHead"))
            {
                return utilityObject.getMembersAreaHead(memberInfo.LocalAssemblyId, User.Identity.Name);
            }
            else if (User.IsInRole("DistPastor"))
            {
                return utilityObject.getMembersDistrictHead(memberInfo.LocalAssemblyId, User.Identity.Name);
            }
            else if (User.IsInRole("PresElder"))
            {
                return utilityObject.getMembersAssemblyHead(memberInfo.LocalAssemblyId, User.Identity.Name);
            }
            else
            {
                return null;
            }
            
        }

        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead, DistPastor, PresElder")]
        [Route("getAllMemebers")]
        [ResponseType(typeof(Member))]
        public IHttpActionResult GetMember(int id)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        public Member GetMemberById(int id)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return null;
            }

            return member;
        }

        public List<Member> GetMemberArrayByAssembly(int id)
        {
            string query = "SELECT * FROM Members where LocalAssemblyId=@p0";
            List<Member> memberList = db.Members.SqlQuery(query, id).ToList();
            return memberList;
        }

        // PUT: api/Members/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMember(int id, Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != member.MemberId)
            {
                return BadRequest();
            }

            db.Entry(member).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // POST: api/Members
        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead, DistPastor, PresElder")]
        [Route("addMemeber")]
        [ResponseType(typeof(Member))]
        public IHttpActionResult PostMember(Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Members.Add(member);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = member.MemberId }, member);
        }

        // DELETE: api/Members/5
        [ResponseType(typeof(Member))]
        public IHttpActionResult DeleteMember(int id)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return NotFound();
            }

            db.Members.Remove(member);
            db.SaveChanges();

            return Ok(member);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MemberExists(int id)
        {
            return db.Members.Count(e => e.MemberId == id) > 0;
        }
    }
}