namespace ControlStock
{
    internal static class clsSesion
    {
        public static long? IdUsuario { get; private set; }
        public static string Usuario { get; private set; }
        public static string Rol { get; private set; }
        public static void Iniciar(long idUsuario, string usuario, string rol)
        {
            IdUsuario = idUsuario;
            Usuario = usuario;
            Rol = rol;
        }
        public static void Cerrar()
        {
            IdUsuario = null;
            Usuario = null;
            Rol = null;
        }
    }
}