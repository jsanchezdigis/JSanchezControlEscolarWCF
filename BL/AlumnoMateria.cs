using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result Add(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JSanchezControlEscolarWCFEntities context = new DL_EF.JSanchezControlEscolarWCFEntities())
                {
                    int query = context.AlumnoMateriaAdd(
                        alumnoMateria.Alumno.IdAlumno,
                        alumnoMateria.Materia.IdMateria);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JSanchezControlEscolarWCFEntities context = new DL_EF.JSanchezControlEscolarWCFEntities())
                {
                    int query = context.AlumnoMateriaDelete(alumnoMateria.IdAlumnoMateria);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Elimino el registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAll(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JSanchezControlEscolarWCFEntities context = new DL_EF.JSanchezControlEscolarWCFEntities())
                {
                    var query = context.AlumnoMateriaGetAll(alumnoMateria.Alumno.IdAlumno).ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            alumnoMateria = new ML.AlumnoMateria();

                            alumnoMateria.IdAlumnoMateria = obj.IdAlumnoMateria;
                            alumnoMateria.Alumno = new ML.Alumno();
                            alumnoMateria.Alumno.IdAlumno = obj.IdAlumno;
                            alumnoMateria.Alumno.Nombre = obj.NombreAlumno;
                            alumnoMateria.Alumno.ApellidoPaterno = obj.ApellidoPaterno;
                            alumnoMateria.Alumno.ApellidoMaterno = obj.ApellidoMaterno;

                            alumnoMateria.Materia = new ML.Materia();
                            alumnoMateria.Materia.IdMateria = obj.IdMateria;
                            alumnoMateria.Materia.Nombre = obj.Materia;
                            alumnoMateria.Materia.Costo = obj.Costo.Value;

                            result.Objects.Add(alumnoMateria);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int IdAlumnoMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JSanchezControlEscolarWCFEntities context = new DL_EF.JSanchezControlEscolarWCFEntities())
                {
                    var query = context.AlumnoMateriaGetById(IdAlumnoMateria).FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();
                        var obj = query;
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.IdAlumnoMateria = obj.IdAlumnoMateria;

                            alumnoMateria.Alumno = new ML.Alumno();
                            alumnoMateria.Alumno.Nombre = obj.NombreAlumno;
                            alumnoMateria.Alumno.ApellidoPaterno = obj.ApellidoPaterno;
                            alumnoMateria.Alumno.ApellidoMaterno = obj.ApellidoMaterno;

                            alumnoMateria.Materia = new ML.Materia();
                            alumnoMateria.Materia.Nombre = obj.Materia;
                            alumnoMateria.Materia.Costo = obj.Costo.Value;

                            result.Object = alumnoMateria;
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result AlumnoMateriasDisponibles(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JSanchezControlEscolarWCFEntities context = new DL_EF.JSanchezControlEscolarWCFEntities())
                {
                    var query = context.AlumnoMateriasDisponibles(alumnoMateria.Alumno.IdAlumno).ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            alumnoMateria = new ML.AlumnoMateria();

                            alumnoMateria.Materia = new ML.Materia();
                            alumnoMateria.Materia.IdMateria = obj.IdMateria;
                            alumnoMateria.Materia.Nombre = obj.Nombre;
                            alumnoMateria.Materia.Costo = obj.Costo.Value;

                            result.Objects.Add(alumnoMateria);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
