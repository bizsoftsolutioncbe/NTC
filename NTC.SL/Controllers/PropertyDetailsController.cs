using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using NTC.DAL;

namespace NTC.SL.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PropertyDetailsController : ApiController
    {
        private DBNTCEntities db = new DBNTCEntities();

        // GET: api/PropertyDetails
        public IQueryable<PropertyDetail> GetPropertyDetails()
        {
            return db.PropertyDetails;
        }

        // GET: api/PropertyDetails/5
        [ResponseType(typeof(PropertyDetail))]
        public IHttpActionResult GetPropertyDetail(int id)
        {
            PropertyDetail propertyDetail = db.PropertyDetails.Find(id);
            if (propertyDetail == null)
            {
                return NotFound();
            }

            return Ok(propertyDetail);
        }

        // PUT: api/PropertyDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropertyDetail(int id, PropertyDetail propertyDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propertyDetail.Id)
            {
                return BadRequest();
            }

            db.Entry(propertyDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyDetailExists(id))
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

        // POST: api/PropertyDetails
        [ResponseType(typeof(PropertyDetail))]
        public IHttpActionResult PostPropertyDetail(PropertyDetail propertyDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PropertyDetails.Add(propertyDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = propertyDetail.Id }, propertyDetail);
        }

        // DELETE: api/PropertyDetails/5
        [ResponseType(typeof(PropertyDetail))]
        public IHttpActionResult DeletePropertyDetail(int id)
        {
            PropertyDetail propertyDetail = db.PropertyDetails.Find(id);
            if (propertyDetail == null)
            {
                return NotFound();
            }

            db.PropertyDetails.Remove(propertyDetail);
            db.SaveChanges();

            return Ok(propertyDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyDetailExists(int id)
        {
            return db.PropertyDetails.Count(e => e.Id == id) > 0;
        }
    }
}