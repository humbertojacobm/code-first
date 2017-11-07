namespace IntroCodeFirst.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IntroCodeFirst.Models.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;//solamente por probar, pero no debería estar en false, es peligroso
            
        }

        protected override void Seed(IntroCodeFirst.Models.BlogContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Comentarios.AddOrUpdate(x => x.Id, new Models.Comentario()
            {
                Id = 1,
                Autor = "Pablo",
                BlogPostId = 1,
                Contenido = "Ejemplo de contenido",
            });

        }
    }
}
