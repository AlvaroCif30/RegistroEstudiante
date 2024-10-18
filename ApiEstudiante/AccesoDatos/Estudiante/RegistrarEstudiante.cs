using MySql.Data.MySqlClient;
using ApiEstudiante.Entidades;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data;

namespace ApiEstudiante.AccesoDatos.Estudiante
{
    public class RegistrarEstudiante
    {

        private readonly string InsertarEstudiante;
        private readonly string ConsulEstudiante;
        private readonly string ConsulCorreo;
        private readonly string RegistrosEstudiante;

        public RegistrarEstudiante() {

            InsertarEstudiante = @"InsertarEstudiante";
            ConsulEstudiante = @"ConsulEstudiante";
            ConsulCorreo = @"ConsulCorreo";
            RegistrosEstudiante = @"RegistrosEstudiante";
        }


        public int RegisEstudiante(ApiEstudiante.Entidades.Estudiante estudiante) {
            int idestudiante = 0;
            using MySqlConnection conn = new MySqlConnection(Conexion.GetDatosConexionMySQL());
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = InsertarEstudiante;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@Nombre", estudiante.nombre));
                cmd.Parameters.Add(new MySqlParameter("@Email", estudiante.email));
                cmd.Parameters.Add(new MySqlParameter("@Pass", estudiante.password));
                idestudiante = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return idestudiante;
        }

        public bool ConsultarEmail(ApiEstudiante.Entidades.Estudiante estudiante)
        {
            using MySqlConnection conn = new(Conexion.GetDatosConexionMySQL());
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = ConsulCorreo;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@Email", estudiante.email));
                var reader = cmd.ExecuteReader();
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public int ConsultarEstudiante(ApiEstudiante.Entidades.Estudiante estudiante) 
        {
            using MySqlConnection conn = new(Conexion.GetDatosConexionMySQL());
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = ConsulEstudiante;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@Email", estudiante.email));
                cmd.Parameters.Add(new MySqlParameter("@Pass", estudiante.password));
                return Convert.ToInt32(cmd.ExecuteScalar());
                
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<ApiEstudiante.Entidades.Estudiante> ConsultarRegisEstudiante() {
            List<ApiEstudiante.Entidades.Estudiante> datos = new List<ApiEstudiante.Entidades.Estudiante>();

            using MySqlConnection conn = new(Conexion.GetDatosConexionMySQL());
            try
            {

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText= RegistrosEstudiante;
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ApiEstudiante.Entidades.Estudiante resp = new ApiEstudiante.Entidades.Estudiante()
                        {
                            nombre = reader["nombre"].ToString(),
                            email = reader["correo_electronico"].ToString(),
                            password = ""

                        };
                        datos.Add(resp);
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return datos;
        }
    }
}
