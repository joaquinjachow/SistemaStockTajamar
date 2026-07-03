namespace ControlStock
{
    internal static class clsSesion
    {
        public const string RolAdministrador = "Administrador";
        public const string RolOperario = "Operario";
        public const string RolCompras = "Compras";

        public static long? IdUsuario { get; private set; }
        public static string Usuario { get; private set; }
        public static string Rol { get; private set; }
        public static bool EsAdministrador
        {
            get { return TieneRol(RolAdministrador); }
        }
        public static bool EsOperario
        {
            get { return TieneRol(RolOperario); }
        }
        public static bool EsCompras
        {
            get { return TieneRol(RolCompras); }
        }
        public static bool PuedeAgregar
        {
            get { return EsAdministrador || EsOperario; }
        }
        public static bool PuedeEditar
        {
            get { return EsAdministrador; }
        }
        public static bool PuedeVerReportes
        {
            get { return EsAdministrador || EsCompras; }
        }
        public static bool PuedeExportar
        {
            get { return EsAdministrador || EsCompras; }
        }
        public static bool PuedeVerEstadisticas
        {
            get { return EsAdministrador || EsCompras; }
        }

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
        private static bool TieneRol(string rol)
        {
            return string.Equals(Rol, rol, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}
