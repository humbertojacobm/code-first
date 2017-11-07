using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace CursoASPNETMVC.Models
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext()
            : base("name=DefaultConnection")
        {
                
        }

        //interceptar query en nuestra base de datos

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(x => x.HasColumnType("datetime2"));
            modelBuilder.Properties<int>().Where(p => p.Name.StartsWith("Codigo")).Configure(p => p.IsKey());

            modelBuilder.Entity<Direccion>().HasRequired(x => x.Persona).WithMany().HasForeignKey(x=>x.Persona_Id);//create foreign key

            base.OnModelCreating(modelBuilder);
        }

        protected override bool ShouldValidateEntity(DbEntityEntry entityEntry)
        {
            //si se va a borrar algo, devuelvo "true" para que no valide nada
            if (entityEntry.State==EntityState.Deleted)
            {
                return true;
            }
            return base.ShouldValidateEntity(entityEntry);
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            if (entityEntry.Entity is Persona && entityEntry.State==EntityState.Deleted)
            {
                var entidad = entityEntry.Entity as Persona;
                if (entidad.Edad<18)
                {
                    return new DbEntityValidationResult(entityEntry, new DbValidationError[] {
                        new DbValidationError("Edad","No se puede borrar a un menor de edad")
                        });
                }

            }
            return base.ValidateEntity(entityEntry, items);
        }

        public override int SaveChanges()
        {
            var entidades = ChangeTracker.Entries();

            if (entidades!=null)
            {
                foreach (var entidad in entidades.Where(c=>c.State!=EntityState.Unchanged))
                {
                    Auditar(entidad);
                }
            }

            return base.SaveChanges();
        }

        private void Auditar(DbEntityEntry entidad)
        {

        }

    }
}