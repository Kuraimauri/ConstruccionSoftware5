using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using PersonaService.Models;

namespace PersonaService.Controllers
{
    /*
    Puede que la clase WebApiConfig requiera cambios adicionales para agregar una ruta para este controlador. Combine estas instrucciones en el método Register de la clase WebApiConfig según corresponda. Tenga en cuenta que las direcciones URL de OData distinguen mayúsculas de minúsculas.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using PersonaService.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Persona>("Personas");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PersonasController : ODataController
    {
        private PersonaServiceContext db = new PersonaServiceContext();

        // GET: odata/Personas
        [EnableQuery]
        public IQueryable<Persona> GetPersonas()
        {
            return db.Personas;
        }

        // GET: odata/Personas(5)
        [EnableQuery]
        public SingleResult<Persona> GetPersona([FromODataUri] int key)
        {
            return SingleResult.Create(db.Personas.Where(persona => persona.ID == key));
        }

        // PUT: odata/Personas(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Persona> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Persona persona = await db.Personas.FindAsync(key);
            if (persona == null)
            {
                return NotFound();
            }

            patch.Put(persona);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(persona);
        }

        // POST: odata/Personas
        public async Task<IHttpActionResult> Post(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Personas.Add(persona);
            await db.SaveChangesAsync();

            return Created(persona);
        }

        // PATCH: odata/Personas(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Persona> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Persona persona = await db.Personas.FindAsync(key);
            if (persona == null)
            {
                return NotFound();
            }

            patch.Patch(persona);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(persona);
        }

        // DELETE: odata/Personas(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Persona persona = await db.Personas.FindAsync(key);
            if (persona == null)
            {
                return NotFound();
            }

            db.Personas.Remove(persona);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonaExists(int key)
        {
            return db.Personas.Count(e => e.ID == key) > 0;
        }
    }
}
