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
    [RoutePrefix("api/metadata")]
    public class MetaLocalAssembliesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        [Route("getAllAssembliesByDistrict")]
        public async System.Threading.Tasks.Task<IHttpActionResult> getAssembliesByDistrict(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            string query = "SELECT * FROM MetaLocalAssemblies where DistrictId=@p0";
            var metaAssemblies = db.MetaLocalAssemblies.SqlQuery(query, id).ToList();
            List<MetaLocalAssemblyDTO> response = new List<MetaLocalAssemblyDTO>();
            foreach (MetaLocalAssembly data in metaAssemblies)
            {
                response.Add(new MetaLocalAssemblyDTO
                {
                    AssemblyId = data.AssemblyId,
                    AssemblyName = data.AssemblyName,
                    UserId = data.UserId,
                    DistrictId = data.DistrictId,
                    AssemblyAddress = db.Addresses.Find(data.AssemblyAddress)
                });
            }
            if (metaAssemblies == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        public List<MetaLocalAssemblyDTO> getAssembliesByDistrictArray(int id)
        {
            if (id == 0)
            {
                return null;
            }

            string query = "SELECT * FROM MetaLocalAssemblies where DistrictId=@p0";
            var metaAssemblies = db.MetaLocalAssemblies.SqlQuery(query, id).ToList();
            List<MetaLocalAssemblyDTO> response = new List<MetaLocalAssemblyDTO>();
            foreach (MetaLocalAssembly data in metaAssemblies)
            {
                response.Add(new MetaLocalAssemblyDTO
                {
                    AssemblyId = data.AssemblyId,
                    AssemblyName = data.AssemblyName,
                    UserId = data.UserId,
                    DistrictId = data.DistrictId,
                    AssemblyAddress = db.Addresses.Find(data.AssemblyAddress)
                });
            }
            if (metaAssemblies == null)
            {
                return null;
            }

            return response;
        }

        [Authorize]
        [Route("getAllLocalAssemblies")]
        // GET: api/MetaLocalAssemblies
        public async System.Threading.Tasks.Task<IHttpActionResult> GetMetaLocalAssemblies()
        {
            
            var metaLA = db.MetaLocalAssemblies.ToList();
            List<MetaLocalAssemblyDTO> response = new List<MetaLocalAssemblyDTO>();
            foreach (MetaLocalAssembly data in metaLA)
            {
                response.Add(new MetaLocalAssemblyDTO
                {
                    AssemblyName = data.AssemblyName,
                    AssemblyId = data.AssemblyId,
                    UserId = data.UserId,
                    DistrictId = data.DistrictId,
                    AssemblyAddress = db.Addresses.Find(data.AssemblyAddress)
                });
            }
            if (metaLA == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [Authorize]
        [Route("getLocalAssembliesById")]
        // GET: api/MetaLocalAssemblies/5
        [ResponseType(typeof(MetaLocalAssembly))]
        public IHttpActionResult GetMetaLocalAssembly(int id)
        {
            MetaLocalAssembly metaLocalAssembly = db.MetaLocalAssemblies.Find(id);
            if (metaLocalAssembly == null)
            {
                return NotFound();
            }
            MetaLocalAssembly metaAssembly = db.MetaLocalAssemblies.Find(id);
            MetaLocalAssemblyDTO response = new MetaLocalAssemblyDTO();
            response.AssemblyName = metaAssembly.AssemblyName;
            response.AssemblyId = metaAssembly.AssemblyId;
            response.UserId = metaAssembly.UserId;
            response.AssemblyAddress = db.Addresses.Find(metaAssembly.AssemblyAddress);
            response.DistrictId = metaAssembly.DistrictId;
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        public MetaLocalAssemblyDTO GetMetaLocalAssemblyLocal(int id)
        {
            MetaLocalAssembly metaLocalAssembly = db.MetaLocalAssemblies.Find(id);
            if (metaLocalAssembly == null)
            {
                return null;
            }
            MetaLocalAssembly metaAssembly = db.MetaLocalAssemblies.Find(id);
            MetaLocalAssemblyDTO response = new MetaLocalAssemblyDTO();
            response.AssemblyName = metaAssembly.AssemblyName;
            response.AssemblyId = metaAssembly.AssemblyId;
            response.UserId = metaAssembly.UserId;
            response.AssemblyAddress = db.Addresses.Find(metaAssembly.AssemblyAddress);
            response.DistrictId = metaAssembly.DistrictId;
            return response;
        }

        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead, DistPastor, PresElder")]
        [Route("updateLocalAssebly")]
        // PUT: api/MetaLocalAssemblies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaLocalAssembly(int id, localAssemblyAddressUpdateDTO data)
        {
            MetaLocalAssembly metaLocalAssembly = data.assembly;
            Address address = data.address;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaLocalAssembly.AssemblyId)
            {
                return BadRequest();
            }

            db.Entry(metaLocalAssembly).State = EntityState.Modified;
            AddressUtility serviceObject = new AddressUtility();
            serviceObject.updateAddress(address);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaLocalAssemblyExists(id))
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

        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead, DistPastor, PresElder")]
        [Route("createLocalAssembly")]
        // POST: api/MetaLocalAssemblies
        [ResponseType(typeof(MetaLocalAssembly))]
        public IHttpActionResult PostMetaLocalAssembly(MetaLocalAssemblyDTO[] metaLocalAssembly)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AddressUtility serviceObject = new AddressUtility();
            foreach (MetaLocalAssemblyDTO i in metaLocalAssembly)
            {
                MetaLocalAssembly assembly = new MetaLocalAssembly();
                assembly.AssemblyName = i.AssemblyName;
                assembly.UserId = i.UserId;
                assembly.DistrictId = i.DistrictId;
                assembly.AssemblyAddress = serviceObject.addAddress(i.AssemblyAddress);
                db.MetaLocalAssemblies.Add(assembly);
            }
            db.SaveChanges();

            return Ok();
        }

        [Authorize(Roles = "SuperAdmin, TopMgmt, BlkCoor, NatHead, RccHead,  AreaHead, DistPastor")]
        [Route("deleteLocalAssembly")]
        // DELETE: api/MetaLocalAssemblies/5
        [ResponseType(typeof(MetaLocalAssembly))]
        public IHttpActionResult DeleteMetaLocalAssembly(int id)
        {
            MetaLocalAssembly metaLocalAssembly = db.MetaLocalAssemblies.Find(id);
            if (metaLocalAssembly == null)
            {
                return NotFound();
            }

            db.MetaLocalAssemblies.Remove(metaLocalAssembly);
            db.SaveChanges();

            return Ok(metaLocalAssembly);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaLocalAssemblyExists(int id)
        {
            return db.MetaLocalAssemblies.Count(e => e.AssemblyId == id) > 0;
        }
    }
}