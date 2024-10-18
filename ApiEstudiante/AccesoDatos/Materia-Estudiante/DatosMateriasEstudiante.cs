using ApiEstudiante.Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace ApiEstudiante.AccesoDatos.Materia_Estudiante
{
    public class DatosMateriasEstudiante
    {
        private readonly string ConsulMateriaEstudiante;
        
        public DatosMateriasEstudiante()
        {
            ConsulMateriaEstudiante = "ConsulMateriaEstudiante";
        }


        public List<string> ConsultarMateriasEstudiante(string id)
        {
            List<string> datos = new List<string>();
            using MySqlConnection conn = new MySqlConnection(Conexion.GetDatosConexionMySQL());
            try
            {
                
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = ConsulMateriaEstudiante;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@Id", id));
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string resp = reader["nombre"].ToString();
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
