using System;
using System.Data;
using System.Data.SQLite;

namespace ControlStock
{
    internal class clsCliente
    {
        public void AgregarCliente(string empresa, string direccion, string telefono, string cuit, string email, string cuentaBancaria, string observaciones)
        {
            ValidarDatosCliente(empresa, direccion, telefono, cuit, email);
            const string sql = "INSERT INTO Clientes (Empresa, Direccion, Telefono, Cuit, Email, CuentaBancaria, Observaciones) VALUES (@Empresa, @Direccion, @Telefono, @Cuit, @Email, @CuentaBancaria, @Observaciones)";

            using (SQLiteConnection connection = clsDatabase.AbrirConexion())
            using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@Empresa", empresa.Trim());
                cmd.Parameters.AddWithValue("@Direccion", string.IsNullOrWhiteSpace(direccion) ? (object)DBNull.Value : direccion.Trim());
                cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrWhiteSpace(telefono) ? (object)DBNull.Value : telefono.Trim());
                cmd.Parameters.AddWithValue("@Cuit", string.IsNullOrWhiteSpace(cuit) ? (object)DBNull.Value : cuit.Trim());
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email.Trim());
                cmd.Parameters.AddWithValue("@CuentaBancaria", string.IsNullOrWhiteSpace(cuentaBancaria) ? (object)DBNull.Value : cuentaBancaria.Trim());
                cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrWhiteSpace(observaciones) ? (object)DBNull.Value : observaciones.Trim());
                cmd.ExecuteNonQuery();
            }
        }
        public DataTable ListarClientes()
        {
            return BuscarClientes(string.Empty);
        }
        public DataTable ListarClientesConId()
        {
            const string sql = "SELECT IdCliente, Empresa, Direccion, Telefono, Cuit, Email, CuentaBancaria, Observaciones FROM Clientes ORDER BY Empresa";
            return clsStockRepository.Consultar(sql);
        }
        public DataTable BuscarClientes(string textoBusqueda)
        {
            DataTable tabla = new DataTable();
            string filtro = "%" + textoBusqueda + "%";
            const string sql = "SELECT IdCliente, Empresa, Direccion, Telefono, Cuit, Email, CuentaBancaria, Observaciones FROM Clientes WHERE Empresa LIKE @Filtro OR Direccion LIKE @Filtro OR Telefono LIKE @Filtro OR Cuit LIKE @Filtro OR Email LIKE @Filtro OR CuentaBancaria LIKE @Filtro ORDER BY Empresa";

            using (SQLiteConnection connection = clsDatabase.AbrirConexion())
            using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@Filtro", filtro);
                adapter.Fill(tabla);
            }
            return tabla;
        }
        public void EditarCliente(long idCliente, string empresa, string direccion, string telefono, string cuit, string email, string cuentaBancaria, string observaciones)
        {
            ValidarDatosCliente(empresa, direccion, telefono, cuit, email);
            const string sql = "UPDATE Clientes SET Empresa = @Empresa, Direccion = @Direccion, Telefono = @Telefono, Cuit = @Cuit, Email = @Email, CuentaBancaria = @CuentaBancaria, Observaciones = @Observaciones WHERE IdCliente = @IdCliente";

            using (SQLiteConnection connection = clsDatabase.AbrirConexion())
            using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                cmd.Parameters.AddWithValue("@Empresa", empresa.Trim());
                cmd.Parameters.AddWithValue("@Direccion", string.IsNullOrWhiteSpace(direccion) ? (object)DBNull.Value : direccion.Trim());
                cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrWhiteSpace(telefono) ? (object)DBNull.Value : telefono.Trim());
                cmd.Parameters.AddWithValue("@Cuit", string.IsNullOrWhiteSpace(cuit) ? (object)DBNull.Value : cuit.Trim());
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email.Trim());
                cmd.Parameters.AddWithValue("@CuentaBancaria", string.IsNullOrWhiteSpace(cuentaBancaria) ? (object)DBNull.Value : cuentaBancaria.Trim());
                cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrWhiteSpace(observaciones) ? (object)DBNull.Value : observaciones.Trim());
                cmd.ExecuteNonQuery();
            }
        }
        private static void ValidarDatosCliente(string empresa, string direccion, string telefono, string cuit, string email)
        {
            if (string.IsNullOrWhiteSpace(empresa))
            {
                throw new InvalidOperationException("La empresa es obligatoria.");
            }

            if (string.IsNullOrWhiteSpace(direccion))
            {
                throw new InvalidOperationException("La direccion de envio es obligatoria.");
            }

            if (string.IsNullOrWhiteSpace(telefono))
            {
                throw new InvalidOperationException("El telefono es obligatorio.");
            }

            if (!TelefonoValido(telefono))
            {
                throw new InvalidOperationException("El telefono solo puede contener numeros, espacios, guiones, parentesis o el signo +.");
            }

            if (!string.IsNullOrWhiteSpace(cuit) && !CuitValido(cuit))
            {
                throw new InvalidOperationException("El CUIT debe tener 11 numeros.");
            }

            if (!string.IsNullOrWhiteSpace(email) && !EmailValido(email))
            {
                throw new InvalidOperationException("El email no tiene un formato valido.");
            }
        }
        private static bool TelefonoValido(string telefono)
        {
            foreach (char caracter in telefono.Trim())
            {
                if (!char.IsDigit(caracter) && caracter != ' ' && caracter != '-' && caracter != '(' && caracter != ')' && caracter != '+')
                {
                    return false;
                }
            }
            return true;
        }
        private static bool CuitValido(string cuit)
        {
            string numeros = string.Empty;
            foreach (char caracter in cuit)
            {
                if (char.IsDigit(caracter))
                {
                    numeros += caracter;
                }
                else if (caracter != '-' && caracter != ' ')
                {
                    return false;
                }
            }
            return numeros.Length == 11;
        }
        private static bool EmailValido(string email)
        {
            string valor = email.Trim();
            int arroba = valor.IndexOf('@');
            int punto = valor.LastIndexOf('.');
            return arroba > 0 && punto > arroba + 1 && punto < valor.Length - 1;
        }
        public void EliminarCliente(long idCliente)
        {
            using (SQLiteConnection connection = clsDatabase.AbrirConexion())
            using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Clientes WHERE IdCliente = @IdCliente", connection))
            {
                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                cmd.ExecuteNonQuery();
            }
        }
    }
}