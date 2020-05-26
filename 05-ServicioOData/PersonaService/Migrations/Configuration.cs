namespace PersonaService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PersonaService.Models.PersonaServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PersonaService.Models.PersonaServiceContext context)
        {
            context.Personas.AddOrUpdate(new Models.Persona[]
                {
                    new Models.Persona() { ID = 1, Nombre = "Carlos" },
                    new Models.Persona() { ID = 2, Nombre = "Pablo" },
                    new Models.Persona() { ID = 3, Nombre = "Mauricio" },
                }
                );
        }
    }
}
