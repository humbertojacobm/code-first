using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CursoASPNETMVC.Models;

namespace CursoASPNETMVC.Controllers
{
    public class PersonasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Personas
        public ActionResult Index()
        {
            //var listadoPersonasTodasLasColumnas = db.Persona.ToList();

            ////listado de string
            //var listadoNombres = db.Persona.Select(x => x.Nombre).ToList();

            //var listadoPersonasVariasColumnasAnonimo = db.Persona.Select(x => new { Nomre = x.Nombre, Edad = x.Edad }).ToList();

            //var listadoPersonasVariasColumnas = db.Persona.Select(x => new { Nombre = x.Nombre, Edad = x.Edad }).ToList()
            //    .Select(x => new Persona() { Nombre = x.Nombre, Edad = x.Edad }).ToList();

            //db.Direccion.Add(new Direccion() { Calle = "ejemplo", CodigoDireccion = "", Persona = new Persona() {Id=2 } });

            //insertar persona y dirección a la vez
            //var persona = new Persona() { Id=2 };
            //db.Direccion.Add(new Direccion() { Calle = "ejemplo", Persona= persona });

            //var persona = new Persona() { Id=2 };
            //db.Persona.Attach(persona);//escucha
            //db.Direccion.Add(new Direccion() { Calle="Ejemplo", Persona=persona });
            //db.SaveChanges();

            //var persona = db.Persona.Include("Direcciones").FirstOrDefault(x => x.Id == 2);
            //var direcciones = persona.Direcciones;

            //var direccion = db.Direccion.FirstOrDefault(x => x.CodigoDireccion == 1);
            //var nombre = direccion.Persona.Nombre;

            

            return View(db.Persona.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Nacimiento,Edad")] Persona persona)
        {




            if (ModelState.IsValid)
            {

                var personas = new List<Persona>() { persona };

                //personas.Add(new Persona() { Nombre="Agregado", Edad=454, Nacimiento=new DateTime(2017,12,01) });

                //db.Persona.AddRange(personas);
                db.Persona.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(persona);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Nacimiento,Edad")] Persona persona)
        {

            //
            var personaEditar = db.Persona.FirstOrDefault(x => x.Id == 2);
            personaEditar.Nombre = "Editado método 1";
            personaEditar.Edad = personaEditar.Edad + 1;
            db.SaveChanges();


            var personaEditar2 = new Persona();
            personaEditar2.Id = 3;
            personaEditar2.Nombre = "Editado metodo 2";
            personaEditar2.Edad = 54;
            db.Persona.Attach(personaEditar2);//escucha a este objeto
            db.Entry(personaEditar2).Property(x => x.Nombre).IsModified = true;
            db.SaveChanges();

            db.Entry(persona).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Persona.Find(id);
            db.Persona.Remove(persona);
            //db.Persona.RemoveRange();
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
