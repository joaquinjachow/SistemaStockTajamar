using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace ControlStock
{
    internal static class clsDatabase
    {
        private const string DatabaseFileName = "ControlStock.db";
        public const string SedeCordoba = "Cordoba";
        public const string SedeMisiones = "Misiones";
        private static bool inicializada;

        public static string DatabasePath
        {
            get
            {
                string assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return Path.Combine(assemblyDir, DatabaseFileName);
            }
        }
        public static string ConnectionString
        {
            get
            {
                return $"Data Source={DatabasePath};Version=3;Foreign Keys=True;";
            }
        }
        public static SQLiteConnection AbrirConexion()
        {
            AsegurarBaseDatos();
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            Ejecutar(connection, "PRAGMA foreign_keys = ON;");
            return connection;
        }
        public static void AsegurarBaseDatos()
        {
            if (inicializada && File.Exists(DatabasePath))
            {
                return;
            }
            bool debeSembrar = !File.Exists(DatabasePath);
            Directory.CreateDirectory(Path.GetDirectoryName(DatabasePath));
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                Ejecutar(connection, "PRAGMA foreign_keys = ON;");
                CrearTablas(connection);

                if (debeSembrar || EstaVacia(connection))
                {
                    SembrarDatosMinimos(connection);
                }
            }
            inicializada = true;
        }

        private static bool EstaVacia(SQLiteConnection connection)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM Sedes;", connection))
            {
                return Convert.ToInt32(cmd.ExecuteScalar()) == 0;
            }
        }
        private static void CrearTablas(SQLiteConnection connection)
        {
            Ejecutar(connection, @"
                CREATE TABLE IF NOT EXISTS Sedes (
                    IdSede INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL UNIQUE
                );");
                            Ejecutar(connection, @"
                CREATE TABLE IF NOT EXISTS Productos (
                    IdProducto INTEGER PRIMARY KEY AUTOINCREMENT,
                    Rubro TEXT NOT NULL,
                    Medida TEXT NULL,
                    Secado TEXT NULL,
                    Especie TEXT NULL,
                    Calidad TEXT NULL,
                    Espesor TEXT NULL,
                    CantidadPorUnidad INTEGER NOT NULL DEFAULT 1 CHECK (CantidadPorUnidad > 0),
                    Unidad TEXT NOT NULL,
                    Activo INTEGER NOT NULL DEFAULT 1 CHECK (Activo IN (0, 1)),
                    FechaAlta TEXT NOT NULL DEFAULT (datetime('now', 'localtime'))
                );");
                            Ejecutar(connection, @"
                CREATE TABLE IF NOT EXISTS StockSede (
                    IdStock INTEGER PRIMARY KEY AUTOINCREMENT,
                    IdProducto INTEGER NOT NULL,
                    IdSede INTEGER NOT NULL,
                    Cantidad INTEGER NOT NULL DEFAULT 0 CHECK (Cantidad >= 0),
                    StockMinimo INTEGER NOT NULL DEFAULT 0 CHECK (StockMinimo >= 0),
                    UNIQUE (IdProducto, IdSede),
                    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto),
                    FOREIGN KEY (IdSede) REFERENCES Sedes(IdSede)
                );");
                            Ejecutar(connection, @"
                CREATE TABLE IF NOT EXISTS Clientes (
                    IdCliente INTEGER PRIMARY KEY AUTOINCREMENT,
                    Empresa TEXT NOT NULL,
                    Direccion TEXT NULL,
                    Telefono TEXT NULL,
                    Cuit TEXT NULL,
                    Email TEXT NULL,
                    CuentaBancaria TEXT NULL,
                    Observaciones TEXT NULL,
                    FechaAlta TEXT NOT NULL DEFAULT (datetime('now', 'localtime'))
                );");
                            AgregarColumnaSiNoExiste(connection, "Clientes", "Cuit", "TEXT NULL");
                            AgregarColumnaSiNoExiste(connection, "Clientes", "Email", "TEXT NULL");
                            AgregarColumnaSiNoExiste(connection, "Clientes", "CuentaBancaria", "TEXT NULL");
                            AgregarColumnaSiNoExiste(connection, "Clientes", "Observaciones", "TEXT NULL");
                            Ejecutar(connection, @"
                CREATE TABLE IF NOT EXISTS Usuarios (
                    IdUsuario INTEGER PRIMARY KEY AUTOINCREMENT,
                    Usuario TEXT NOT NULL UNIQUE,
                    ClaveHash TEXT NOT NULL,
                    Rol TEXT NOT NULL DEFAULT 'Operador',
                    Activo INTEGER NOT NULL DEFAULT 1 CHECK (Activo IN (0, 1)),
                    FechaAlta TEXT NOT NULL DEFAULT (datetime('now', 'localtime'))
                );");
                            Ejecutar(connection, @"
                CREATE TABLE IF NOT EXISTS Movimientos (
                    IdMovimiento INTEGER PRIMARY KEY AUTOINCREMENT,
                    Fecha TEXT NOT NULL DEFAULT (datetime('now', 'localtime')),
                    IdProducto INTEGER NOT NULL,
                    IdSede INTEGER NOT NULL,
                    IdSedeDestino INTEGER NULL,
                    IdCliente INTEGER NULL,
                    Tipo TEXT NOT NULL CHECK (Tipo IN ('Alta', 'Ingreso', 'Egreso', 'Ajuste', 'Transferencia')),
                    Cantidad INTEGER NOT NULL CHECK (Cantidad > 0),
                    Detalle TEXT NULL,
                    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto),
                    FOREIGN KEY (IdSede) REFERENCES Sedes(IdSede),
                    FOREIGN KEY (IdSedeDestino) REFERENCES Sedes(IdSede),
                    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente)
                );");
            if (!ExisteColumna(connection, "Movimientos", "IdUsuario"))
            {
                Ejecutar(connection, "ALTER TABLE Movimientos ADD COLUMN IdUsuario INTEGER NULL;");
            }
            Ejecutar(connection, @"
            INSERT OR IGNORE INTO Usuarios (Usuario, ClaveHash, Rol)
            VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'Administrador');");
            Ejecutar(connection, "CREATE INDEX IF NOT EXISTS IX_Productos_Rubro ON Productos(Rubro, Activo);");
            Ejecutar(connection, "CREATE INDEX IF NOT EXISTS IX_StockSede_Sede ON StockSede(IdSede);");
            Ejecutar(connection, "CREATE INDEX IF NOT EXISTS IX_Movimientos_ProductoFecha ON Movimientos(IdProducto, Fecha);");
        }
        private static void SembrarDatosMinimos(SQLiteConnection connection)
        {
            using (SQLiteTransaction tx = connection.BeginTransaction())
            {
                Ejecutar(connection, "INSERT OR IGNORE INTO Sedes (Nombre) VALUES ('Cordoba'), ('Misiones');", tx);
                tx.Commit();
            }
        }
        private static void Ejecutar(SQLiteConnection connection, string sql, SQLiteTransaction tx = null)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(sql, connection, tx))
            {
                cmd.ExecuteNonQuery();
            }
        }
        private static bool ExisteColumna(SQLiteConnection connection, string tabla, string columna)
        {
            using (SQLiteCommand cmd = new SQLiteCommand($"PRAGMA table_info({tabla});", connection))
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (string.Equals(reader["name"].ToString(), columna, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private static void AgregarColumnaSiNoExiste(SQLiteConnection connection, string tabla, string columna, string definicion)
        {
            if (!ExisteColumna(connection, tabla, columna))
            {
                Ejecutar(connection, $"ALTER TABLE {tabla} ADD COLUMN {columna} {definicion};");
            }
        }
    }
}