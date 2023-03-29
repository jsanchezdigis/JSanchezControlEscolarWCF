using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAlumno" in both code and config file together.
    [ServiceContract]
    public interface IAlumno
    {
        [OperationContract]
        ML.Result Add(ML.Alumno alumno);
        [OperationContract]
        ML.Result Update(ML.Alumno alumno);
        [OperationContract]
        ML.Result Delete(ML.Alumno alumno);
        [OperationContract]
        [ServiceKnownType(typeof(ML.Alumno))]
        ML.Result GetAll();
        [OperationContract]
        [ServiceKnownType(typeof(ML.Alumno))]
        ML.Result GetById(int IdAlumno);
    }
}
