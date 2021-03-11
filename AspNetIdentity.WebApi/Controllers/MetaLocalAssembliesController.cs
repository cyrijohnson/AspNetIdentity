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

        [Authorize(Roles = "User,Admin,SuperAdmin")]
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

            return Ok(metaAssemblies);
        }

        [Authorize(Roles = "User,Admin,SuperAdmin")]
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

            return Ok(metaLA);
        }

        [Authorize(Roles = "User,Admin,SuperAdmin")]
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

            return Ok(metaLocalAssembly);
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

        [Authorize(Roles = "Admin,SuperAdmin")]
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

        [Authorize(Roles = "Admin,SuperAdmin")]
        [Route("createLocalAssembly")]
        // POST: api/MetaLocalAssemblies
        [ResponseType(typeof(MetaLocalAssembly))]
        public IHttpActionResult PostMetaLocalAssembly(MetaLocalAssemblyDTO metaLocalAssembly)
        {
            MetaLocalAssembly assembly = new MetaLocalAssembly();
            AddressUtility serviceObject = new AddressUtility();
            assembly.AssemblyName = metaLocalAssembly.AssemblyName;
            assembly.UserId = metaLocalAssembly.UserId;
            assembly.DistrictId = metaLocalAssembly.DistrictId;
            assembly.AssemblyAddress = serviceObject.addAddress(metaLocalAssembly.AssemblyAddress);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MetaLocalAssemblies.Add(assembly);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = metaLocalAssembly.AssemblyId }, metaLocalAssembly);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
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