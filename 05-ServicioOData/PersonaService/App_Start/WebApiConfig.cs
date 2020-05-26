using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PersonaService.Models;
using System.Web.Http.OData.Builder;

namespace PersonaService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Persona>("Personas");
            config.Routes.MapODataRoute("odata","odata", builder.GetEdmModel());
        }
    }
}
