using ApiEstudiante.Entidades.Transversal;

namespace ApiEstudiante.AccesoDatos
{
    public static class Conexion
    {
        public static string GetDatosConexionMySQL()
        {
            Datos datos = new()
            {
                servidor = "127.0.0.1",
                baseDatos = "clases-estudiantes",
                user = "root",
                pass = ""
            };

            return $"server={datos.servidor}; uid={datos.user}; pwd={datos.pass}; database={datos.baseDatos}";
        }
    }

}
