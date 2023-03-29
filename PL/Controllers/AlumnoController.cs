using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult GetAll()
        {
            AlumnoService.AlumnoClient alumnoClient = new AlumnoService.AlumnoClient();//WCF
            ML.Alumno alumno = new ML.Alumno();
            //ML.Result result = BL.Alumno.GetAll();
            ML.Result result = alumnoClient.GetAll();//WCF

            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
                return View(alumno);
            }
            else
            {
                return View(alumno);
            }
        }

        [HttpGet]
        //Formulario
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            if (IdAlumno == null)
            {
                //Add Formulario vacio
                return View(alumno);
            }
            else
            {
                //GetById
                AlumnoService.AlumnoClient alumnoClient = new AlumnoService.AlumnoClient();//WCF
                //ML.Result result = BL.Alumno.GetById(IdAlumno.Value);
                ML.Result result = alumnoClient.GetById(IdAlumno.Value);//WCF

                if (result.Correct)
                {
                    alumno = (ML.Alumno)result.Object;
                    return View(alumno);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar la informacion";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            AlumnoService.AlumnoClient alumnoClient = new AlumnoService.AlumnoClient();//WCF

            if (alumno.IdAlumno == 0)
            {
                //Add
                //result = BL.Alumno.Add(alumno);
                result = alumnoClient.Add(alumno);//WCF

                if (result.Correct)
                {
                    ViewBag.Message = "Se completo el registro satisfactoriamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al insertar el registro";
                }
                return View("Modal");
            }
            else
            {
                //Update
                //result = BL.Alumno.Update(alumno);
                result = alumnoClient.Update(alumno);//WCF
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo el registro satisfactoriamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar el registro";
                }
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult Delete(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            AlumnoService.AlumnoClient alumnoClient = new AlumnoService.AlumnoClient();//WCF

            //result = BL.Alumno.Delete(alumno);
            result = alumnoClient.Delete(alumno);//WCF
            if (result.Correct)
            {
                ViewBag.Message = "Se elimino el registro satisfactoriamente";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar el registro";
            }
            return View("Modal");
        }
    }
}