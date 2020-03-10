using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Alumnos.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            try
            {
                //int edad = 18;
                //string sql = @"select a.id, a.Nombres, a.Apellidos, a.Edad, a.Sexo, a.FechaRegistro, c.Nombre as NombreCiudad
                //from Alumno a
                //inner join Ciudad c on a.CodCiudad=c.id
                //where a.Edad> @edadAlumno";
                using (var db = new AlumnosContext())
                {
                    var data = from a in db.Alumno
                               join c in db.Ciudad
                               on a.CodCiudad equals c.id
                               select new AlumnoCE()
                               {
                                   id = a.id,
                                   Nombres = a.Nombres,
                                   Apellidos = a.Apellidos,
                                   Edad = a.Edad,
                                   Sexo = a.Sexo,
                                   FechaRegistro = a.FechaRegistro,
                                   NombreCiudad = c.Nombre,

                               };

                    return View(data.ToList());
                    //return View(db.Database.SqlQuery<AlumnoCE>(sql, new SqlParameter("@edadAlumno", edad).ToList());
                }
            }
            catch (Exception )
            {
                ModelState.AddModelError("", "Error al buscar los alumno");
                return View();
            }
            
        }
        public ActionResult Agregar()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                using (var db = new AlumnosContext())
                {
                    alumno.FechaRegistro = DateTime.Now;
                    db.Alumno.Add(alumno);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch(Exception )
            {
                ModelState.AddModelError("", "Error al agregar el alumno");
                return View();
            }
        }
        public ActionResult Editar(int id)
        {

            try
            {
                using (var db = new AlumnosContext())
                {
                    Alumno alumno = db.Alumno.Where(a => a.id == id).FirstOrDefault();
                    //Alumno alumno1 = db.Alumno.Find(id);

                    return View(alumno);
                }
            }
            catch (Exception )
            {
                ModelState.AddModelError("", "Error al traer al alumno");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Alumno al)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                using (var db = new AlumnosContext())
                {
                    Alumno alumno = db.Alumno.Where(a => a.id == al.id).FirstOrDefault();
                    //Alumno alumno1 = db.Alumno.Find(al.id);
                    alumno.Nombres = al.Nombres;
                    alumno.Apellidos = al.Apellidos;
                    alumno.Edad = al.Edad;
                    alumno.Sexo = al.Sexo;
                    db.SaveChanges();


                    return RedirectToAction("Index");
                }
            }
            catch (Exception )
            {
                ModelState.AddModelError("", "Error al editar al alumno");
                return View();
            }
            
            
        }
        public ActionResult Detallar(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Alumno alumno = db.Alumno.Where(a => a.id == id).FirstOrDefault();
                    //Alumno alumno1 = db.Alumno.Find(id);

                    return View(alumno);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error al traer al alumno");
                return View();
            }

            
        }

       
        public ActionResult Eliminar(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Alumno alumno = db.Alumno.Where(a => a.id == id).FirstOrDefault();
                    //Alumno alumno1 = db.Alumno.Find(id);
                    db.Alumno.Remove(alumno);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error al eliminar al alumno");
                return View();
            }

           
        }

        public ActionResult ListaCiudades()
        {
            try
            {
                using (var db = new AlumnosContext())
                {

                    return PartialView(db.Ciudad.ToList());
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error al trear lista de ciudades");
                return View();
            }


        }
        public static string NombreCiudad(int CodCiudad)
        {

            using (var db = new AlumnosContext())
            {
                return db.Ciudad.Find(CodCiudad).Nombre;
            }



        }
    }
}