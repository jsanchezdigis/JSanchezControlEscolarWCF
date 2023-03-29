using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Alumno" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Alumno.svc or Alumno.svc.cs at the Solution Explorer and start debugging.
    public class Alumno : IAlumno
    {
        public ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Add(alumno);

            if (result.Correct)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        public ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Update(alumno);

            if (result.Correct)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        public ML.Result Delete(ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Delete(alumno);

            if (result.Correct)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        public ML.Result GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();

            if (result.Correct)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        public ML.Result GetById(int IdAlumno)
        {
            ML.Result result = BL.Alumno.GetById(IdAlumno);

            if (result.Correct)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
