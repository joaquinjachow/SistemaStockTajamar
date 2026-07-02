PRAGMA foreign_keys = ON;
CREATE TABLE IF NOT EXISTS Sedes (
    IdSede INTEGER PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL UNIQUE
);
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
);
CREATE TABLE IF NOT EXISTS StockSede (
    IdStock INTEGER PRIMARY KEY AUTOINCREMENT,
    IdProducto INTEGER NOT NULL,
    IdSede INTEGER NOT NULL,
    Cantidad INTEGER NOT NULL DEFAULT 0 CHECK (Cantidad >= 0),
    StockMinimo INTEGER NOT NULL DEFAULT 0 CHECK (StockMinimo >= 0),
    UNIQUE (IdProducto, IdSede),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto),
    FOREIGN KEY (IdSede) REFERENCES Sedes(IdSede)
);
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
);
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
);
CREATE INDEX IF NOT EXISTS IX_Productos_Rubro ON Productos(Rubro, Activo);
CREATE INDEX IF NOT EXISTS IX_StockSede_Sede ON StockSede(IdSede);
CREATE INDEX IF NOT EXISTS IX_Movimientos_ProductoFecha ON Movimientos(IdProducto, Fecha);
