-- ============================================================================
-- Setup de la patente BACKUP_BD (servicio de Backup/Restore).
-- Ejecutar UNA vez sobre la base IngSoftware90DI.
--
-- 1) Da de alta la patente BACKUP_BD (si no existe).
-- 2) La asigna al rol de administrador (ajustar el nombre del rol si difiere).
-- 3) Recalcula los dígitos verificadores para que la verificación de integridad
--    en el login no marque las tablas modificadas como corruptas.
-- ============================================================================

USE IngSoftware90DI;
GO

-- 1) Alta de la patente ------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Patente_90DI WHERE Nombre_90DI = 'BACKUP_BD')
BEGIN
    INSERT INTO Patente_90DI (Nombre_90DI, Descripcion_90DI, FechaAlta_90DI)
    VALUES ('BACKUP_BD', 'Generar backup de la base de datos', GETDATE());
END
GO

-- 2) Asignación al rol Admin -------------------------------------------------
--    Ajustar 'Admin' por el Nombre_90DI real del rol administrador.
DECLARE @idRol      INT = (SELECT IdRol_90DI     FROM Rol_90DI     WHERE Nombre_90DI = 'Admin');
DECLARE @idPatente  INT = (SELECT IdPatente_90DI FROM Patente_90DI WHERE Nombre_90DI = 'BACKUP_BD');

IF @idRol IS NOT NULL AND @idPatente IS NOT NULL
   AND NOT EXISTS (SELECT 1 FROM Rol_Patente_90DI WHERE IdRol_90DI = @idRol AND IdPatente_90DI = @idPatente)
BEGIN
    INSERT INTO Rol_Patente_90DI (IdRol_90DI, IdPatente_90DI) VALUES (@idRol, @idPatente);
END
GO

-- 3) Recalcular dígitos verificadores ----------------------------------------
EXEC sp_RecalcularTodo_90DI;
GO
