using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {

                using (var client = new HttpClient())
                {
                    string urlApi = ConfigurationManager.AppSettings["urlApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Materia/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Materia resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    materia.Materias = result.Objects;
                }

            }
            catch (Exception ex)
            {
            }

            return View(materia);
            //Normal
            //ML.Materia materia = new ML.Materia();
            //ML.Result result = BL.Materia.GetAll();

            //if (result.Correct)
            //{
            //    materia.Materias = result.Objects;
            //    return View(materia);
            //}
            //else
            //{
            //    return View(materia);
            //}
        }

        [HttpGet]
        //Formulario
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia();
            if (IdMateria == null)
            {
                //Add Formulario vacio
                return View(materia);
            }
            else
            {
                //GetById
                ML.Result result = new ML.Result();

                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlApi"]);
                        //GET
                        var responseTask = client.GetAsync("Materia/GetById/" + IdMateria);
                        responseTask.Wait();

                        var resultService = responseTask.Result;

                        if (resultService.IsSuccessStatusCode)
                        {
                            var readTask = resultService.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();


                            ML.Materia resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;

                            materia = (ML.Materia)result.Object;
                            return View(materia);
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se puedo hacer la consulta";
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                    }
                    return View("Modal");
                }

                //Normal
                //ML.Result result = BL.Materia.GetById(IdMateria.Value);

                //if (result.Correct)
                //{
                //    materia = (ML.Materia)result.Object;
                //    return View(materia);
                //}
                //else
                //{
                //    ViewBag.Message = "Ocurrio un error al consultar la informacion";
                //    return View("Modal");
                //}
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlApi"]);
                if (materia.IdMateria == 0)
                {
                    //POST
                    var postTask = client.PostAsJsonAsync<ML.Materia>("Materia/Add", materia);
                    postTask.Wait();

                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
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
                    var postTask = client.PostAsJsonAsync<ML.Materia>("Materia/Update/"+ materia.IdMateria, materia);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se actualizo el registro satisfactoriamente";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al actualizar el registro";
                    }
                    return View("Modal");
                }
                return View("Modal");
            }

            //Normal
            //ML.Result result = new ML.Result();

            //if (materia.IdMateria == 0)
            //{
            //    //Add
            //    result = BL.Materia.Add(materia);

            //    if (result.Correct)
            //    {
            //        ViewBag.Message = "Se completo el registro satisfactoriamente";
            //    }
            //    else
            //    {
            //        ViewBag.Message = "Ocurrio un error al insertar el registro";
            //    }
            //    return View("Modal");
            //}
            //else
            //{
            //    //Update
            //    result = BL.Materia.Update(materia);
            //    if (result.Correct)
            //    {
            //        ViewBag.Message = "Se actualizo el registro satisfactoriamente";
            //    }
            //    else
            //    {
            //        ViewBag.Message = "Ocurrio un error al actualizar el registro";
            //    }
            //    return View("Modal");
            //}
        }

        [HttpGet]
        public ActionResult Delete(ML.Materia materia)
        {
            int IdMateria = materia.IdMateria;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlApi"]);

                //POST-Delete
                //var postTask = client.DeleteAsync("Usuario/Delete/" + IdUsuario);
                var postTask = client.DeleteAsync("Materia/Delete/" + IdMateria);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se elimino el registro satisfactoriamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al eliminar el registro";
                }
                return View("Modal");
            }

            //Normal
            //ML.Result result = new ML.Result();

            //result = BL.Materia.Delete(materia);
            //if (result.Correct)
            //{
            //    ViewBag.Message = "Se elimino el registro satisfactoriamente";
            //}
            //else
            //{
            //    ViewBag.Message = "Ocurrio un error al eliminar el registro";
            //}
            //return View("Modal");
        }
    }
}