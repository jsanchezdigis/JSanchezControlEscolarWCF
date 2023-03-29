using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {
        public static ML.Result Add(ML.Alumno alumno)

        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "AlumnoAdd";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[3];

                        collection[0] = new SqlParameter("Nombre", System.Data.SqlDbType.VarChar);
                        collection[0].Value = alumno.Nombre;

                        collection[1] = new SqlParameter("ApellidoPaterno", System.Data.SqlDbType.VarChar);
                        collection[1].Value = alumno.ApellidoPaterno;

                        collection[2] = new SqlParameter("ApellidoMaterno", System.Data.SqlDbType.VarChar);
                        collection[2].Value = alumno.ApellidoMaterno;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        int RowAffected = cmd.ExecuteNonQuery();
                        if (RowAffected > 0)
                        {
                            result.Correct = true;
                            result.ErrorMessage = "Se inserto el regristro";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Correct = false;
                result.ErrorMessage = "Error al agregar el registro";

            }
            return result;
        }

        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "AlumnoUpdate";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[4];

                        collection[0] = new SqlParameter("IdAlumno", System.Data.SqlDbType.VarChar);
                        collection[0].Value = alumno.IdAlumno;

                        collection[1] = new SqlParameter("Nombre", System.Data.SqlDbType.VarChar);
                        collection[1].Value = alumno.Nombre;

                        collection[2] = new SqlParameter("ApellidoPaterno", System.Data.SqlDbType.VarChar);
                        collection[2].Value = alumno.ApellidoPaterno;

                        collection[3] = new SqlParameter("ApellidoMaterno", System.Data.SqlDbType.VarChar);
                        collection[3].Value = alumno.ApellidoMaterno;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        int RowAffected = cmd.ExecuteNonQuery();
                        if (RowAffected > 0)
                        {
                            result.Correct = true;
                            result.ErrorMessage = "Se actualizo el regristro";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Correct = false;
                result.ErrorMessage = "Error al actualizar el registro";

            }
            return result;
        }

        public static ML.Result Delete(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "AlumnoDelete";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdAlumno", System.Data.SqlDbType.VarChar);
                        collection[0].Value = alumno.IdAlumno;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        int RowAffected = cmd.ExecuteNonQuery();
                        if (RowAffected > 0)
                        {
                            result.Correct = true;
                            result.ErrorMessage = "Se actualizo el regristro";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Correct = false;
                result.ErrorMessage = "Error al actualizar el registro";

            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    var query = "AlumnoGetAll";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        DataTable TablaAlumno = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(TablaAlumno);

                        if (TablaAlumno.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in TablaAlumno.Rows)
                            {
                                ML.Alumno alumno = new ML.Alumno();

                                alumno.IdAlumno = Convert.ToInt32(row[0]);
                                alumno.Nombre = row[1].ToString();
                                alumno.ApellidoPaterno = row[2].ToString();
                                alumno.ApellidoMaterno = row[3].ToString();

                                result.Objects.Add(alumno);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Exception = ex;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }

        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "AlumnoGetById";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdAlumno", System.Data.SqlDbType.VarChar);
                        collection[0].Value = IdAlumno;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        DataTable TablaAlumno = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(TablaAlumno);

                        if (TablaAlumno.Rows.Count > 0)
                        {
                            result.Object = TablaAlumno.Rows[0];
                            foreach (DataRow row in TablaAlumno.Rows)
                            {
                                ML.Alumno alumno = new ML.Alumno();

                                alumno.Nombre = row[0].ToString();
                                alumno.ApellidoPaterno = row[1].ToString();
                                alumno.ApellidoMaterno = row[2].ToString();

                                result.Object = alumno;
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Correct = false;
                result.ErrorMessage = "Error al actualizar el registro";

            }
            return result;
        }
    }
}
