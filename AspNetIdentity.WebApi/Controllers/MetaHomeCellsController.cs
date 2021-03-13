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
    public class MetaHomeCellsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getAllHomeCellsByZone")]
        public async System.Threading.Tasks.Task<IHttpActionResult> getHomeCellByZone(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            string query = "SELECT * FROM MetaHomeCells where ZoneId=@p0";
            var metaHomeCells = db.MetaHomeCells.SqlQuery(query, id).ToList();
            if (metaHomeCells == null)
            {
                return NotFound();
            }
            List<MetaHomeCellDTO> response = new List<MetaHomeCellDTO>();
            foreach (MetaHomeCell data in metaHomeCells)
            {
                response.Add(new MetaHomeCellDTO
                {
                    HomeCellId = data.HomeCellId,
                    HomeCellName = data.HomeCellName,
                    CellLeader = data.CellLeader,
                    CellAddress = db.Addresses.Find(data.CellAddress)
                });
            }
            

            return Ok(response);
        }

        // GET: api/MetaHomeCells
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getAllHomeCells")]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetMetaHomeCells()
        {
            var MetaHC = db.MetaHomeCells;
            if (MetaHC == null)
            {
                return NotFound();
            }
            List<MetaHomeCellDTO> response = new List<MetaHomeCellDTO>();
            foreach (MetaHomeCell data in MetaHC)
            {
                response.Add(new MetaHomeCellDTO
                {
                    HomeCellId = data.HomeCellId,
                    HomeCellName = data.HomeCellName,
                    CellLeader = data.CellLeader,
                    CellAddress = db.Addresses.Find(data.CellAddress)
                });
            }
            return Ok(response);
        }
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("getHomeCellById")]
        // GET: api/MetaHomeCells/5
        [ResponseType(typeof(MetaHomeCell))]
        public IHttpActionResult GetMetaHomeCell(int id)
        {
            MetaHomeCell metaHomeCell = db.MetaHomeCells.Find(id);
            if (metaHomeCell == null)
            {
                return NotFound();
            }
            
            MetaHomeCellDTO response = new MetaHomeCellDTO();

            response.HomeCellId = metaHomeCell.HomeCellId;
            response.HomeCellName = metaHomeCell.HomeCellName;
            response.CellLeader = metaHomeCell.CellLeader;
            response.CellAddress = db.Addresses.Find(metaHomeCell.CellAddress);
         
            return Ok(response);

        }
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("updateHomeCell")]
        // PUT: api/MetaHomeCells/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMetaHomeCell(int id, HomeCellAddressUpdateDTO homeCell)
        {
            MetaHomeCell metaHomeCell = homeCell.HomeCell;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != metaHomeCell.HomeCellId)
            {
                return BadRequest();
            }

            db.Entry(metaHomeCell).State = EntityState.Modified;
            AddressUtility serviceObject = new AddressUtility();
            serviceObject.updateAddress(homeCell.HomeCellAddress);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaHomeCellExists(id))
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
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("createHomeCell")]
        // POST: api/MetaHomeCells
        [ResponseType(typeof(MetaHomeCell))]
        public IHttpActionResult PostMetaHomeCell(MetaHomeCellDTO HomeCell)
        {
            MetaHomeCell metaHomeCell = new MetaHomeCell();
            AddressUtility serviceObject = new AddressUtility();
            metaHomeCell.HomeCellName = HomeCell.HomeCellName;
            metaHomeCell.CellLeader = HomeCell.CellLeader;
            metaHomeCell.CellAddress = serviceObject.addAddress(HomeCell.CellAddress);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MetaHomeCells.Add(metaHomeCell);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = metaHomeCell.HomeCellId }, metaHomeCell);
        }
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [Route("deleteHomeCell")]
        // DELETE: api/MetaHomeCells/5
        [ResponseType(typeof(MetaHomeCell))]
        public IHttpActionResult DeleteMetaHomeCell(int id)
        {
            MetaHomeCell metaHomeCell = db.MetaHomeCells.Find(id);
            if (metaHomeCell == null)
            {
                return NotFound();
            }

            db.MetaHomeCells.Remove(metaHomeCell);
            db.SaveChanges();

            return Ok(metaHomeCell);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MetaHomeCellExists(int id)
        {
            return db.MetaHomeCells.Count(e => e.HomeCellId == id) > 0;
        }
    }
}