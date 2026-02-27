CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;
CREATE TABLE `Clientes` (
    `ClienteId` int NOT NULL AUTO_INCREMENT,
    `Nombre` longtext NOT NULL,
    `Apellido` longtext NOT NULL,
    `Email` longtext NOT NULL,
    `Telefono` longtext NOT NULL,
    `Cedula` longtext NOT NULL,
    `Direccion` longtext NOT NULL,
    `Genero` longtext NOT NULL,
    PRIMARY KEY (`ClienteId`)
);

CREATE TABLE `Productos` (
    `ProductoId` int NOT NULL AUTO_INCREMENT,
    `Nombre` longtext NOT NULL,
    `Codigo` longtext NOT NULL,
    `Talla` longtext NOT NULL,
    `Color` longtext NOT NULL,
    `Precio` decimal(18,2) NOT NULL,
    `PrecioDescuento` decimal(18,2) NULL,
    `Foto` longtext NOT NULL,
    PRIMARY KEY (`ProductoId`)
);

CREATE TABLE `Carritos` (
    `CarritoId` int NOT NULL AUTO_INCREMENT,
    `ClienteId` int NOT NULL,
    `FechaCreacion` datetime(6) NOT NULL,
    `Estado` longtext NOT NULL,
    PRIMARY KEY (`CarritoId`),
    CONSTRAINT `FK_Carritos_Clientes_ClienteId` FOREIGN KEY (`ClienteId`) REFERENCES `Clientes` (`ClienteId`) ON DELETE CASCADE
);

CREATE TABLE `CarritoProductos` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `CarritoId` int NOT NULL,
    `ProductoId` int NOT NULL,
    `Cantidad` int NOT NULL,
    `PrecioUnitario` decimal(18,2) NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CarritoProductos_Carritos_CarritoId` FOREIGN KEY (`CarritoId`) REFERENCES `Carritos` (`CarritoId`) ON DELETE CASCADE,
    CONSTRAINT `FK_CarritoProductos_Productos_ProductoId` FOREIGN KEY (`ProductoId`) REFERENCES `Productos` (`ProductoId`) ON DELETE CASCADE
);

CREATE TABLE `Compras` (
    `CompraId` int NOT NULL AUTO_INCREMENT,
    `ClienteId` int NOT NULL,
    `CarritoId` int NOT NULL,
    `FechaCompra` datetime(6) NOT NULL,
    `Subtotal` decimal(18,2) NOT NULL,
    PRIMARY KEY (`CompraId`),
    CONSTRAINT `FK_Compras_Carritos_CarritoId` FOREIGN KEY (`CarritoId`) REFERENCES `Carritos` (`CarritoId`) ON DELETE CASCADE,
    CONSTRAINT `FK_Compras_Clientes_ClienteId` FOREIGN KEY (`ClienteId`) REFERENCES `Clientes` (`ClienteId`) ON DELETE CASCADE
);

CREATE INDEX `IX_CarritoProductos_CarritoId` ON `CarritoProductos` (`CarritoId`);

CREATE INDEX `IX_CarritoProductos_ProductoId` ON `CarritoProductos` (`ProductoId`);

CREATE INDEX `IX_Carritos_ClienteId` ON `Carritos` (`ClienteId`);

CREATE INDEX `IX_Compras_CarritoId` ON `Compras` (`CarritoId`);

CREATE INDEX `IX_Compras_ClienteId` ON `Compras` (`ClienteId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260227182009_NuevaMigracion', '10.0.3');

COMMIT;

