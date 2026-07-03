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
    DireccionComercial TEXT NULL,
    Telefono TEXT NULL,
    Cuit TEXT NULL,
    Email TEXT NULL,
    CondicionIva TEXT NULL,
    DireccionLegal TEXT NULL,
    Observaciones TEXT NULL,
    FechaAlta TEXT NOT NULL DEFAULT (datetime('now', 'localtime'))
);
CREATE TABLE IF NOT EXISTS Usuarios (
    IdUsuario INTEGER PRIMARY KEY AUTOINCREMENT,
    Usuario TEXT NOT NULL UNIQUE,
    ClaveHash TEXT NOT NULL,
    Rol TEXT NOT NULL DEFAULT 'Operario',
    Activo INTEGER NOT NULL DEFAULT 1 CHECK (Activo IN (0, 1)),
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
    IdUsuario INTEGER NULL,
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto),
    FOREIGN KEY (IdSede) REFERENCES Sedes(IdSede),
    FOREIGN KEY (IdSedeDestino) REFERENCES Sedes(IdSede),
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
);
INSERT OR IGNORE INTO Usuarios (Usuario, ClaveHash, Rol) VALUES
('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'Administrador'),
('operario', 'a39ba034a2e1e73308a0291a1541aeee1290ffa697bca8342799ef19a7fa8c99', 'Operario'),
('compras', 'f74d0c877bba1b76db96b606bca17cd26b5103d0fa97e86b53a9f346211bb0fe', 'Compras');
CREATE INDEX IF NOT EXISTS IX_Productos_Rubro ON Productos(Rubro, Activo);
CREATE INDEX IF NOT EXISTS IX_StockSede_Sede ON StockSede(IdSede);
CREATE INDEX IF NOT EXISTS IX_Movimientos_ProductoFecha ON Movimientos(IdProducto, Fecha);
