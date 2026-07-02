using System;
using System.Data;
using System.Data.SQLite;

namespace ControlStock
{
    internal static class clsStockRepository
    {
        public const string SedeTotal = "Total";
        public static DataTable ObtenerSedesConTotal()
        {
            DataTable sedes = ObtenerSedes();
            sedes.Rows.InsertAt(sedes.NewRow(), 0);
            sedes.Rows[0]["Nombre"] = SedeTotal;
            return sedes;
        }

        public static DataTable ObtenerSedes()
        {
            return Consultar("SELECT Nombre FROM Sedes ORDER BY Nombre;");
        }
        public static DataTable ObtenerValores(string rubro, string campo)
        {
            string sql = $"SELECT DISTINCT {campo} FROM Productos WHERE Rubro = @Rubro AND Activo = 1 AND {campo} IS NOT NULL ORDER BY {campo};";
            return Consultar(sql, Param("@Rubro", rubro));
        }
        public static DataTable ObtenerValoresFiltrados(string rubro, string campoResultado, string campoFiltro, string valorFiltro)
        {
            string sql = $"SELECT DISTINCT {campoResultado} FROM Productos WHERE Rubro = @Rubro AND Activo = 1 AND {campoFiltro} = @Filtro AND {campoResultado} IS NOT NULL ORDER BY {campoResultado};";
            return Consultar(sql, Param("@Rubro", rubro), Param("@Filtro", valorFiltro));
        }
        public static void AgregarProducto(string rubro, string medida, string secado, string especie, string calidad, string espesor, int cantidadPorUnidad, string unidad, int cantidadInicial, string sedeInicial = clsDatabase.SedeCordoba)
        {
            if (cantidadPorUnidad <= 0)
            {
                throw new InvalidOperationException("La cantidad por unidad debe ser mayor a cero.");
            }
            if (cantidadInicial < 0)
            {
                throw new InvalidOperationException("La cantidad inicial no puede ser negativa.");
            }
            string sede = NormalizarSede(sedeInicial);
            if (sede == SedeTotal)
            {
                throw new InvalidOperationException("Seleccione Cordoba o Misiones para el stock inicial.");
            }
            using (SQLiteConnection connection = clsDatabase.AbrirConexion())
            using (SQLiteTransaction tx = connection.BeginTransaction())
            {
                if (ExisteProducto(connection, tx, rubro, medida, secado, especie, calidad, espesor))
                {
                    throw new InvalidOperationException("Ya existe un producto activo con esos datos.");
                }
                long productoId;
                using (SQLiteCommand cmd = new SQLiteCommand(@"
                INSERT INTO Productos (Rubro, Medida, Secado, Especie, Calidad, Espesor, CantidadPorUnidad, Unidad)
                VALUES (@Rubro, @Medida, @Secado, @Especie, @Calidad, @Espesor, @CantidadPorUnidad, @Unidad);
                SELECT last_insert_rowid();", connection, tx))
                {
                    cmd.Parameters.AddWithValue("@Rubro", rubro);
                    cmd.Parameters.AddWithValue("@Medida", ValorDb(medida));
                    cmd.Parameters.AddWithValue("@Secado", ValorDb(secado));
                    cmd.Parameters.AddWithValue("@Especie", ValorDb(especie));
                    cmd.Parameters.AddWithValue("@Calidad", ValorDb(calidad));
                    cmd.Parameters.AddWithValue("@Espesor", ValorDb(espesor));
                    cmd.Parameters.AddWithValue("@CantidadPorUnidad", cantidadPorUnidad);
                    cmd.Parameters.AddWithValue("@Unidad", unidad);
                    productoId = (long)cmd.ExecuteScalar();
                }
                InsertarStockInicial(connection, tx, productoId, clsDatabase.SedeCordoba, sede == clsDatabase.SedeCordoba ? cantidadInicial : 0);
                InsertarStockInicial(connection, tx, productoId, clsDatabase.SedeMisiones, sede == clsDatabase.SedeMisiones ? cantidadInicial : 0);
                RegistrarMovimiento(connection, tx, productoId, sede, "Alta", Math.Max(cantidadInicial, 1), "Alta de producto desde el sistema");
                tx.Commit();
            }
        }
        public static DataTable ListarPino(string sede = SedeTotal, string filtro = "")
        {
            return Consultar(@"
                SELECT
                    P.Secado,
                    P.Medida,
                    SUM(S.Cantidad) AS Cantidad,
                    P.CantidadPorUnidad,
                    SUM(S.Cantidad) * P.CantidadPorUnidad AS Total
                FROM Productos P
                INNER JOIN StockSede S ON S.IdProducto = P.IdProducto
                INNER JOIN Sedes SE ON SE.IdSede = S.IdSede
                WHERE P.Rubro = 'Pino' AND P.Activo = 1
                  AND (@Sede = 'Total' OR SE.Nombre = @Sede)
                  AND (@Filtro = '' OR P.Secado LIKE @FiltroLike OR P.Medida LIKE @FiltroLike)
                GROUP BY P.IdProducto, P.Secado, P.Medida, P.CantidadPorUnidad
                ORDER BY P.Secado, P.Medida;", Param("@Sede", NormalizarSede(sede)), Param("@Filtro", NormalizarFiltro(filtro)), Param("@FiltroLike", FiltroLike(filtro)));
        }
        public static DataTable ListarMaderaDura(string sede = SedeTotal, string filtro = "")
        {
            return Consultar(@"
            SELECT
                P.Especie,
                P.Medida,
                SUM(S.Cantidad) AS Cantidad,
                P.CantidadPorUnidad,
                SUM(S.Cantidad) * P.CantidadPorUnidad AS Total
            FROM Productos P
            INNER JOIN StockSede S ON S.IdProducto = P.IdProducto
            INNER JOIN Sedes SE ON SE.IdSede = S.IdSede
            WHERE P.Rubro = 'MaderaDura' AND P.Activo = 1
              AND (@Sede = 'Total' OR SE.Nombre = @Sede)
              AND (@Filtro = '' OR P.Especie LIKE @FiltroLike OR P.Medida LIKE @FiltroLike)
            GROUP BY P.IdProducto, P.Especie, P.Medida, P.CantidadPorUnidad
            ORDER BY P.Especie, P.Medida;", Param("@Sede", NormalizarSede(sede)), Param("@Filtro", NormalizarFiltro(filtro)), Param("@FiltroLike", FiltroLike(filtro)));
                    }
        public static DataTable ListarMachimbre(string sede = SedeTotal, string filtro = "")
        {
            return Consultar(@"
            SELECT
                P.Calidad,
                P.Medida,
                SUM(S.Cantidad) AS Cantidad,
                P.CantidadPorUnidad,
                SUM(S.Cantidad) * P.CantidadPorUnidad AS Total
            FROM Productos P
            INNER JOIN StockSede S ON S.IdProducto = P.IdProducto
            INNER JOIN Sedes SE ON SE.IdSede = S.IdSede
            WHERE P.Rubro = 'Machimbre' AND P.Activo = 1
              AND (@Sede = 'Total' OR SE.Nombre = @Sede)
              AND (@Filtro = '' OR P.Calidad LIKE @FiltroLike OR P.Medida LIKE @FiltroLike)
            GROUP BY P.IdProducto, P.Calidad, P.Medida, P.CantidadPorUnidad
            ORDER BY P.Calidad, P.Medida;", Param("@Sede", NormalizarSede(sede)), Param("@Filtro", NormalizarFiltro(filtro)), Param("@FiltroLike", FiltroLike(filtro)));
        }
        public static DataTable ListarFenolicos(string sede = SedeTotal, string filtro = "")
        {
            return Consultar(@"
            SELECT
                P.Calidad,
                P.Espesor,
                P.CantidadPorUnidad,
                SUM(S.Cantidad) AS Cantidad
            FROM Productos P
            INNER JOIN StockSede S ON S.IdProducto = P.IdProducto
            INNER JOIN Sedes SE ON SE.IdSede = S.IdSede
            WHERE P.Rubro = 'Fenolicos' AND P.Activo = 1
              AND (@Sede = 'Total' OR SE.Nombre = @Sede)
              AND (@Filtro = '' OR P.Calidad LIKE @FiltroLike OR P.Espesor LIKE @FiltroLike)
            GROUP BY P.IdProducto, P.Calidad, P.Espesor, P.CantidadPorUnidad
            ORDER BY P.Calidad, P.Espesor;", Param("@Sede", NormalizarSede(sede)), Param("@Filtro", NormalizarFiltro(filtro)), Param("@FiltroLike", FiltroLike(filtro)));
        }
        public static DataTable ObtenerStockPorMedida(string rubro)
        {
            return Consultar(@"
            SELECT
                COALESCE(P.Medida, P.Calidad, P.Especie, P.Espesor) AS Medida,
                SUM(S.Cantidad) AS StockTotal
            FROM Productos P
            INNER JOIN StockSede S ON S.IdProducto = P.IdProducto
            WHERE P.Rubro = @Rubro AND P.Activo = 1
            GROUP BY COALESCE(P.Medida, P.Calidad, P.Especie, P.Espesor)
            ORDER BY Medida;", Param("@Rubro", rubro));
        }
        public static DataTable ObtenerStockFenolicosParaGrafico()
        {
            return Consultar(@"
            SELECT
                P.Calidad,
                SUM(S.Cantidad) AS StockTotal
            FROM Productos P
            INNER JOIN StockSede S ON S.IdProducto = P.IdProducto
            WHERE P.Rubro = 'Fenolicos' AND P.Activo = 1
            GROUP BY P.Calidad
            ORDER BY P.Calidad;");
        }
        public static void SumarStock(string rubro, string medida, string secado, string especie, string calidad, string espesor, int cantidad, string sede = clsDatabase.SedeCordoba)
        {
            ActualizarStock(rubro, medida, secado, especie, calidad, espesor, cantidad, "Ingreso", sede, "Ingreso de stock", null);
        }
        public static void RestarStock(string rubro, string medida, string secado, string especie, string calidad, string espesor, int cantidad, string sede = clsDatabase.SedeCordoba, string detalle = "Egreso de stock", long? clienteId = null)
        {
            ActualizarStock(rubro, medida, secado, especie, calidad, espesor, -cantidad, "Egreso", sede, detalle, clienteId);
        }
        public static void TransferirStock(string rubro, string medida, string secado, string especie, string calidad, string espesor, int cantidad, string sedeOrigen, string sedeDestino, string detalle)
        {
            if (cantidad <= 0)
            {
                throw new InvalidOperationException("La cantidad a transferir debe ser mayor a cero.");
            }
            if (NormalizarSede(sedeOrigen) == NormalizarSede(sedeDestino))
            {
                throw new InvalidOperationException("La sede origen y destino deben ser distintas.");
            }
            using (SQLiteConnection connection = clsDatabase.AbrirConexion())
            using (SQLiteTransaction tx = connection.BeginTransaction())
            {
                long productoId = ObtenerProductoId(connection, rubro, medida, secado, especie, calidad, espesor, tx);
                long origenId = ObtenerSedeId(connection, NormalizarSede(sedeOrigen), tx);
                long destinoId = ObtenerSedeId(connection, NormalizarSede(sedeDestino), tx);
                int stockOrigen = ObtenerStockActual(connection, productoId, origenId, tx);
                if (stockOrigen < cantidad)
                {
                    throw new InvalidOperationException("No hay stock suficiente en la sede origen para transferir.");
                }
                GuardarStock(connection, tx, productoId, origenId, stockOrigen - cantidad);
                GuardarStock(connection, tx, productoId, destinoId, ObtenerStockActual(connection, productoId, destinoId, tx) + cantidad);
                string descripcion = string.IsNullOrWhiteSpace(detalle) ? $"Transferencia de {sedeOrigen} a {sedeDestino}" : detalle.Trim();
                RegistrarMovimiento(connection, tx, productoId, NormalizarSede(sedeOrigen), "Transferencia", cantidad, "Salida: " + descripcion, null, NormalizarSede(sedeDestino));
                RegistrarMovimiento(connection, tx, productoId, NormalizarSede(sedeDestino), "Transferencia", cantidad, "Entrada: " + descripcion, null, NormalizarSede(sedeOrigen));
                tx.Commit();
            }
        }
        public static DataTable ListarMovimientos(string filtro = "")
        {
            return ListarMovimientos(filtro, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }
        public static DataTable ListarMovimientos(string filtro, string fechaDesde, string fechaHasta, string tipo, string sede, string rubro, string cliente = "")
        {
            return Consultar(@"
                SELECT
                    M.Fecha,
                    P.Rubro,
                    TRIM(COALESCE(P.Secado || ' ', '') || COALESCE(P.Especie || ' ', '') || COALESCE(P.Calidad || ' ', '') || COALESCE(P.Medida || ' ', '') || COALESCE(P.Espesor, '')) AS Producto,
                    SE.Nombre AS Sede,
                    M.Tipo,
                    M.Cantidad,
                    COALESCE(C.Empresa, '') AS Cliente,
                    COALESCE(U.Usuario, '') AS Usuario,
                    M.Detalle
                FROM Movimientos M
                INNER JOIN Productos P ON P.IdProducto = M.IdProducto
                INNER JOIN Sedes SE ON SE.IdSede = M.IdSede
                LEFT JOIN Clientes C ON C.IdCliente = M.IdCliente
                LEFT JOIN Usuarios U ON U.IdUsuario = M.IdUsuario
                WHERE (@FechaDesde = '' OR date(M.Fecha) >= date(@FechaDesde))
                  AND (@FechaHasta = '' OR date(M.Fecha) <= date(@FechaHasta))
                  AND (@Tipo = '' OR M.Tipo = @Tipo)
                  AND (@Sede = '' OR @Sede = 'Total' OR SE.Nombre = @Sede)
                  AND (@Rubro = '' OR @Rubro = 'Todos' OR P.Rubro = @Rubro)
                  AND (@Cliente = '' OR C.Empresa LIKE @ClienteLike)
                  AND (@Filtro = ''
                       OR P.Rubro LIKE @FiltroLike
                       OR Producto LIKE @FiltroLike
                       OR SE.Nombre LIKE @FiltroLike
                       OR M.Tipo LIKE @FiltroLike
                       OR M.Detalle LIKE @FiltroLike
                       OR C.Empresa LIKE @FiltroLike
                       OR U.Usuario LIKE @FiltroLike)
                ORDER BY M.Fecha DESC, M.IdMovimiento DESC;", Param("@Filtro", NormalizarFiltro(filtro)), Param("@FiltroLike", FiltroLike(filtro)), Param("@FechaDesde", NormalizarFiltro(fechaDesde)), Param("@FechaHasta", NormalizarFiltro(fechaHasta)), Param("@Tipo", NormalizarFiltro(tipo)), Param("@Sede", NormalizarFiltro(sede)), Param("@Rubro", NormalizarFiltro(rubro)), Param("@Cliente", NormalizarFiltro(cliente)), Param("@ClienteLike", FiltroLike(cliente)));
                        }
        public static DataTable ListarStockBajo()
        {
            return Consultar(@"
            SELECT
                P.Rubro,
                TRIM(COALESCE(P.Secado || ' ', '') || COALESCE(P.Especie || ' ', '') || COALESCE(P.Calidad || ' ', '') || COALESCE(P.Medida || ' ', '') || COALESCE(P.Espesor, '')) AS Producto,
                SE.Nombre AS Sede,
                S.Cantidad AS Stock,
                S.StockMinimo
            FROM Productos P
            INNER JOIN StockSede S ON S.IdProducto = P.IdProducto
            INNER JOIN Sedes SE ON SE.IdSede = S.IdSede
            WHERE P.Activo = 1 AND S.Cantidad <= S.StockMinimo
            ORDER BY P.Rubro, Producto, SE.Nombre;");
        }

        public static DataTable ListarStockMinimo(string filtro = "")
        {
            return Consultar(@"
            SELECT
                S.IdStock,
                P.Rubro,
                TRIM(COALESCE(P.Secado || ' ', '') || COALESCE(P.Especie || ' ', '') || COALESCE(P.Calidad || ' ', '') || COALESCE(P.Medida || ' ', '') || COALESCE(P.Espesor, '')) AS Producto,
                SE.Nombre AS Sede,
                S.Cantidad AS Stock,
                S.StockMinimo
            FROM Productos P
            INNER JOIN StockSede S ON S.IdProducto = P.IdProducto
            INNER JOIN Sedes SE ON SE.IdSede = S.IdSede
            WHERE P.Activo = 1
              AND (@Filtro = ''
                   OR P.Rubro LIKE @FiltroLike
                   OR Producto LIKE @FiltroLike
                   OR SE.Nombre LIKE @FiltroLike)
            ORDER BY P.Rubro, Producto, SE.Nombre;", Param("@Filtro", NormalizarFiltro(filtro)), Param("@FiltroLike", FiltroLike(filtro)));
                    }
        public static void ActualizarStockMinimo(long idStock, int stockMinimo)
        {
            if (stockMinimo < 0)
            {
                throw new InvalidOperationException("El stock minimo no puede ser negativo.");
            }
            using (SQLiteConnection connection = clsDatabase.AbrirConexion())
            using (SQLiteCommand cmd = new SQLiteCommand("UPDATE StockSede SET StockMinimo = @StockMinimo WHERE IdStock = @IdStock;", connection))
            {
                cmd.Parameters.AddWithValue("@IdStock", idStock);
                cmd.Parameters.AddWithValue("@StockMinimo", stockMinimo);
                cmd.ExecuteNonQuery();
            }
        }
        public static DataTable ObtenerResumenDashboard()
        {
            return Consultar(@"
                SELECT
                    P.Rubro,
                    SE.Nombre AS Sede,
                    COUNT(DISTINCT P.IdProducto) AS Productos,
                    SUM(S.Cantidad) AS Stock
                FROM Productos P
                INNER JOIN StockSede S ON S.IdProducto = P.IdProducto
                INNER JOIN Sedes SE ON SE.IdSede = S.IdSede
                WHERE P.Activo = 1
                GROUP BY P.Rubro, SE.Nombre
                ORDER BY P.Rubro, SE.Nombre;");
        }
        public static void EliminarProducto(string rubro, string medida, string secado, string especie, string calidad, string espesor)
        {
            using (SQLiteConnection connection = clsDatabase.AbrirConexion())
            using (SQLiteCommand cmd = new SQLiteCommand(@"
            UPDATE Productos
            SET Activo = 0
            WHERE IdProducto = @IdProducto;", connection))
            {
                cmd.Parameters.AddWithValue("@IdProducto", ObtenerProductoId(connection, rubro, medida, secado, especie, calidad, espesor));
                cmd.ExecuteNonQuery();
            }
        }
        public static DataTable ListarProductos(string filtro = "")
        {
            return Consultar(@"
            SELECT
                IdProducto,
                Rubro,
                Medida,
                Secado,
                Especie,
                Calidad,
                Espesor,
                CantidadPorUnidad,
                Unidad
            FROM Productos
            WHERE Activo = 1
              AND (@Filtro = ''
                   OR Rubro LIKE @FiltroLike
                   OR Medida LIKE @FiltroLike
                   OR Secado LIKE @FiltroLike
                   OR Especie LIKE @FiltroLike
                   OR Calidad LIKE @FiltroLike
                   OR Espesor LIKE @FiltroLike)
            ORDER BY Rubro, Secado, Especie, Calidad, Medida, Espesor;", Param("@Filtro", NormalizarFiltro(filtro)), Param("@FiltroLike", FiltroLike(filtro)));
        }
        public static void EditarProducto(long idProducto, string medida, string secado, string especie, string calidad, string espesor, int cantidadPorUnidad, string unidad)
        {
            if (idProducto <= 0)
            {
                throw new InvalidOperationException("Seleccione un producto valido.");
            }
            if (cantidadPorUnidad <= 0)
            {
                throw new InvalidOperationException("La cantidad por unidad debe ser mayor a cero.");
            }
            if (string.IsNullOrWhiteSpace(unidad))
            {
                throw new InvalidOperationException("La unidad es obligatoria.");
            }
            if (string.IsNullOrWhiteSpace(medida) && string.IsNullOrWhiteSpace(secado) && string.IsNullOrWhiteSpace(especie) && string.IsNullOrWhiteSpace(calidad) && string.IsNullOrWhiteSpace(espesor))
            {
                throw new InvalidOperationException("El producto debe tener al menos un dato descriptivo.");
            }
            using (SQLiteConnection connection = clsDatabase.AbrirConexion())
            using (SQLiteCommand cmd = new SQLiteCommand(@"
                UPDATE Productos
                SET Medida = @Medida,
                    Secado = @Secado,
                    Especie = @Especie,
                    Calidad = @Calidad,
                    Espesor = @Espesor,
                    CantidadPorUnidad = @CantidadPorUnidad,
                    Unidad = @Unidad
                WHERE IdProducto = @IdProducto;", connection))
            {
                cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                cmd.Parameters.AddWithValue("@Medida", ValorDb(medida));
                cmd.Parameters.AddWithValue("@Secado", ValorDb(secado));
                cmd.Parameters.AddWithValue("@Especie", ValorDb(especie));
                cmd.Parameters.AddWithValue("@Calidad", ValorDb(calidad));
                cmd.Parameters.AddWithValue("@Espesor", ValorDb(espesor));
                cmd.Parameters.AddWithValue("@CantidadPorUnidad", cantidadPorUnidad);
                cmd.Parameters.AddWithValue("@Unidad", unidad.Trim());
                cmd.ExecuteNonQuery();
            }
        }
        public static DataTable Consultar(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = clsDatabase.AbrirConexion())
            using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }
        private static void ActualizarStock(string rubro, string medida, string secado, string especie, string calidad, string espesor, int delta, string tipo, string sede, string detalle, long? clienteId)
        {
            if (delta == 0)
            {
                return;
            }
            if (NormalizarSede(sede) == SedeTotal)
            {
                throw new InvalidOperationException("Seleccione Cordoba o Misiones para modificar stock.");
            }
            using (SQLiteConnection connection = clsDatabase.AbrirConexion())
            using (SQLiteTransaction tx = connection.BeginTransaction())
            {
                long productoId = ObtenerProductoId(connection, rubro, medida, secado, especie, calidad, espesor, tx);
                long sedeId = ObtenerSedeId(connection, NormalizarSede(sede), tx);
                int stockActual = ObtenerStockActual(connection, productoId, sedeId, tx);
                int nuevoStock = stockActual + delta;

                if (nuevoStock < 0)
                {
                    throw new InvalidOperationException($"No hay stock suficiente en {NormalizarSede(sede)} para registrar el egreso.");
                }

                GuardarStock(connection, tx, productoId, sedeId, nuevoStock);

                RegistrarMovimiento(connection, tx, productoId, NormalizarSede(sede), tipo, Math.Abs(delta), detalle, clienteId, null);
                tx.Commit();
            }
        }
        private static bool ExisteProducto(SQLiteConnection connection, SQLiteTransaction tx, string rubro, string medida, string secado, string especie, string calidad, string espesor)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(@"
            SELECT COUNT(*)
            FROM Productos
            WHERE Rubro = @Rubro
              AND Activo = 1
              AND COALESCE(Medida, '') = COALESCE(@Medida, '')
              AND COALESCE(Secado, '') = COALESCE(@Secado, '')
              AND COALESCE(Especie, '') = COALESCE(@Especie, '')
              AND COALESCE(Calidad, '') = COALESCE(@Calidad, '')
              AND COALESCE(Espesor, '') = COALESCE(@Espesor, '');", connection, tx))
            {
                cmd.Parameters.AddWithValue("@Rubro", rubro);
                cmd.Parameters.AddWithValue("@Medida", ValorDb(medida));
                cmd.Parameters.AddWithValue("@Secado", ValorDb(secado));
                cmd.Parameters.AddWithValue("@Especie", ValorDb(especie));
                cmd.Parameters.AddWithValue("@Calidad", ValorDb(calidad));
                cmd.Parameters.AddWithValue("@Espesor", ValorDb(espesor));
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        private static long ObtenerProductoId(SQLiteConnection connection, string rubro, string medida, string secado, string especie, string calidad, string espesor, SQLiteTransaction tx = null)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(@"
            SELECT IdProducto
            FROM Productos
            WHERE Rubro = @Rubro
              AND Activo = 1
              AND COALESCE(Medida, '') = COALESCE(@Medida, '')
              AND COALESCE(Secado, '') = COALESCE(@Secado, '')
              AND COALESCE(Especie, '') = COALESCE(@Especie, '')
              AND COALESCE(Calidad, '') = COALESCE(@Calidad, '')
              AND COALESCE(Espesor, '') = COALESCE(@Espesor, '')
            LIMIT 1;", connection, tx))
            {
                cmd.Parameters.AddWithValue("@Rubro", rubro);
                cmd.Parameters.AddWithValue("@Medida", ValorDb(medida));
                cmd.Parameters.AddWithValue("@Secado", ValorDb(secado));
                cmd.Parameters.AddWithValue("@Especie", ValorDb(especie));
                cmd.Parameters.AddWithValue("@Calidad", ValorDb(calidad));
                cmd.Parameters.AddWithValue("@Espesor", ValorDb(espesor));
                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    throw new InvalidOperationException("No se encontro el producto seleccionado.");
                }

                return Convert.ToInt64(result);
            }
        }

        private static long ObtenerSedeId(SQLiteConnection connection, string sede, SQLiteTransaction tx)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT IdSede FROM Sedes WHERE Nombre = @Sede;", connection, tx))
            {
                cmd.Parameters.AddWithValue("@Sede", sede);
                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }
        private static int ObtenerStockActual(SQLiteConnection connection, long productoId, long sedeId, SQLiteTransaction tx)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT Cantidad FROM StockSede WHERE IdProducto = @IdProducto AND IdSede = @IdSede;", connection, tx))
            {
                cmd.Parameters.AddWithValue("@IdProducto", productoId);
                cmd.Parameters.AddWithValue("@IdSede", sedeId);
                object result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
        }
        private static void GuardarStock(SQLiteConnection connection, SQLiteTransaction tx, long productoId, long sedeId, int cantidad)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(@"
                UPDATE StockSede
                SET Cantidad = @Cantidad
                WHERE IdProducto = @IdProducto AND IdSede = @IdSede;", connection, tx))
            {
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@IdProducto", productoId);
                cmd.Parameters.AddWithValue("@IdSede", sedeId);
                cmd.ExecuteNonQuery();
            }
        }
        private static void InsertarStockInicial(SQLiteConnection connection, SQLiteTransaction tx, long productoId, string sede, int cantidad)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(@"
            INSERT INTO StockSede (IdProducto, IdSede, Cantidad)
            SELECT @IdProducto, IdSede, @Cantidad FROM Sedes WHERE Nombre = @Sede;", connection, tx))
            {
                cmd.Parameters.AddWithValue("@IdProducto", productoId);
                cmd.Parameters.AddWithValue("@Sede", sede);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.ExecuteNonQuery();
            }
        }
        private static void RegistrarMovimiento(SQLiteConnection connection, SQLiteTransaction tx, long productoId, string sede, string tipo, int cantidad, string detalle)
        {
            RegistrarMovimiento(connection, tx, productoId, sede, tipo, cantidad, detalle, null, null);
        }
        private static void RegistrarMovimiento(SQLiteConnection connection, SQLiteTransaction tx, long productoId, string sede, string tipo, int cantidad, string detalle, long? clienteId, string sedeDestino)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(@"
            INSERT INTO Movimientos (IdProducto, IdSede, IdSedeDestino, IdCliente, IdUsuario, Tipo, Cantidad, Detalle)
            SELECT @IdProducto, S.IdSede, SD.IdSede, @IdCliente, @IdUsuario, @Tipo, @Cantidad, @Detalle
            FROM Sedes S
            LEFT JOIN Sedes SD ON SD.Nombre = @SedeDestino
            WHERE S.Nombre = @Sede;", connection, tx))
            {
                cmd.Parameters.AddWithValue("@IdProducto", productoId);
                cmd.Parameters.AddWithValue("@Sede", sede);
                cmd.Parameters.AddWithValue("@SedeDestino", string.IsNullOrWhiteSpace(sedeDestino) ? (object)DBNull.Value : sedeDestino);
                cmd.Parameters.AddWithValue("@IdCliente", clienteId.HasValue ? (object)clienteId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@IdUsuario", clsSesion.IdUsuario.HasValue ? (object)clsSesion.IdUsuario.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Detalle", detalle);
                cmd.ExecuteNonQuery();
            }
        }
        private static SQLiteParameter Param(string name, object value)
        {
            return new SQLiteParameter(name, value ?? DBNull.Value);
        }
        private static object ValorDb(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? (object)DBNull.Value : valor.Trim();
        }
        private static string NormalizarSede(string sede)
        {
            return string.IsNullOrWhiteSpace(sede) ? clsDatabase.SedeCordoba : sede.Trim();
        }
        private static string NormalizarFiltro(string filtro)
        {
            return string.IsNullOrWhiteSpace(filtro) ? string.Empty : filtro.Trim();
        }
        private static string FiltroLike(string filtro)
        {
            return "%" + NormalizarFiltro(filtro) + "%";
        }
    }
}