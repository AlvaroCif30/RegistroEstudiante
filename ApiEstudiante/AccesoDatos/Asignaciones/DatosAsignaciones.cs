using ApiEstudiante.Entidades;
using ApiEstudiante.Entidades.Transversal;
using MySql.Data.MySqlClient;
using System.Data;

namespace ApiEstudiante.AccesoDatos.Asignaciones
{
    public class DatosAsignaciones
    {
        private readonly string InsertAsignaciones;
        private readonly string UpdateAsignaciones;
        private readonly string ConsulMateriaEstudiante;
        private readonly string ConsulCreditosMateria;

        public DatosAsignaciones()
        {
            InsertAsignaciones = @"InsertAsignaciones";
            UpdateAsignaciones = @"UpdateAsignaciones";
            ConsulMateriaEstudiante = @"ConsulMateriaEstudiante";
            ConsulCreditosMateria = @"ConsulCreditosMateria";
        }
        public void GuardarMateria(string id, int idmatpro)
        {
            using MySqlConnection conn = new(Conexion.GetDatosConexionMySQL());
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = InsertAsignaciones;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@Id", id));
                cmd.Parameters.Add(new MySqlParameter("@IdMateriaProfe", idmatpro));
                cmd.ExecuteNonQuery();

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
        public void UpdateMaterias(string id)
        {
            using MySqlConnection conn = new(Conexion.GetDatosConexionMySQL());
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = UpdateAsignaciones;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@Id", id));
                cmd.ExecuteNonQuery();

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

        public int ConsultarCreditosMateria(string id)
        {
            using MySqlConnection conn = new(Conexion.GetDatosConexionMySQL());
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = ConsulCreditosMateria;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@Id", id));
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

        public List<MateriaProfesor> ConsultarMateriaEstudiante(string id)
        {
            using MySqlConnection conn = new(Conexion.GetDatosConexionMySQL());
            List<MateriaProfesor> datos = new List<MateriaProfesor>();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = ConsulMateriaEstudiante;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("@Id", id));
                MySqlDataReader reader = cmd.ExecuteReader();
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
