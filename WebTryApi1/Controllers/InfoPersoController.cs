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
using WebTryApi1.DataAccess;

namespace WebTryApi1.Controllers
{
    public class InfoPersoController : ApiController
    {
        private Database1Entities1 db = new Database1Entities1();

        // GET: api/InfoPerso
        public IQueryable<infoperso> Getinfopersoes()
        {
            return db.infopersoes;
        }

        // GET: api/InfoPerso/5
        [ResponseType(typeof(infoperso))]
        public IHttpActionResult Getinfoperso(int id)
        {
            infoperso infoperso = db.infopersoes.Find(id);
            if (infoperso == null)
            {
                return NotFound();
            }

            return Ok(infoperso);
        }

        // PUT: api/InfoPerso/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putinfoperso(int id, infoperso infoperso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infoperso.Id)
            {
                return BadRequest();
            }

            db.Entry(infoperso).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!infopersoExists(id))
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

        // POST: api/InfoPerso
        [ResponseType(typeof(infoperso))]
        public IHttpActionResult Postinfoperso(infoperso infoperso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.infopersoes.Add(infoperso);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (infopersoExists(infoperso.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = infoperso.Id }, infoperso);
        }

        // DELETE: api/InfoPerso/5
        [ResponseType(typeof(infoperso))]
        public IHttpActionResult Deleteinfoperso(int id)
        {
            infoperso infoperso = db.infopersoes.Find(id);
            if (infoperso == null)
            {
                return NotFound();
            }

            db.infopersoes.Remove(infoperso);
            db.SaveChanges();

            return Ok(infoperso);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool infopersoExists(int id)
        {
            return db.infopersoes.Count(e => e.Id == id) > 0;
        }
    }
}