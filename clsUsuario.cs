using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace ControlStock
{
    internal class clsUsuario
    {
        public bool ValidarLogin(string usuario, string clave)
        {
            using (SQLiteConnection connection = clsDatabase.AbrirConexion())
            using (SQLiteCommand cmd = new SQLiteCommand(@"
                SELECT IdUsuario, Usuario, Rol
                FROM Usuarios
                WHERE Usuario = @Usuario AND ClaveHash = @ClaveHash AND Activo = 1
                LIMIT 1;", connection))
            {
                cmd.Parameters.AddWithValue("@Usuario", usuario.Trim());
                cmd.Parameters.AddWithValue("@ClaveHash", CalcularSha256(clave));

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return false;
                    }

                    clsSesion.Iniciar(Convert.ToInt64(reader["IdUsuario"]), reader["Usuario"].ToString(), reader["Rol"].ToString());
                    return true;
                }
            }
        }
        private static string CalcularSha256(string texto)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(texto ?? string.Empty));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}