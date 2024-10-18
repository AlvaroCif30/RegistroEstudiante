using MySql.Data.MySqlClient;
using ApiEstudiante.Entidades;
using System.Data;

namespace ApiEstudiante.AccesoDatos.DatosMateriaProfesor
{
    public class DatosMateriaProfesor
    {
        private readonly string ConsulMateriaProfesor;

        public DatosMateriaProfesor()
        {
            ConsulMateriaProfesor = @"ConsulMateriaProfesor";
        }

        public List<MateriaProfesor> ConsultarMateriaProfesor()
        {
            List<MateriaProfesor> datos = new List<MateriaProfesor>();
            using MySqlConnection conn = new MySqlConnection(Conexion.GetDatosConexionMySQL());
            try
            {
                
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                MySqlCommand cmd = conn.CreateCommand ();
                cmd.CommandText = ConsulMateriaProfesor;
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MateriaProfesor resp = new MateriaProfesor()
                        {
                            id_materia_profesor = Convert.ToInt32(reader["id_materia_profesor"].ToString()),
                            nombre = reader["nombre"].ToString()
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

