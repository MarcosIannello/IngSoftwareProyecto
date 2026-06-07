using System.Data;
using Microsoft.Data.SqlClient;
using NLog;
using Services_90DI.entities;

namespace DAL
{
    public class RolesDAL_90DI
    {
        private readonly ConexionSql_90DI _conexion = new ConexionSql_90DI();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public RolesDAL_90DI() { }

        private void LogError(string method, Exception ex)
        {
            _logger.Error(ex);
            Console.WriteLine($"[DAL][{method}] ERROR: {ex.Message}");
        }

        // ── Gets ────────────────────────────────────────────────────────────

        public List<Rol_90DI> GetAllRoles_90DI()
        {
            var result = new List<Rol_90DI>();
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetAllRoles_90DI";

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    result.Add(MapRol(reader));
            }
            catch (Exception ex) { LogError(nameof(GetAllRoles_90DI), ex); }
            return result;
        }

        public Rol_90DI? GetRolCompleto_90DI(int idRol)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetRolCompleto_90DI";
                cmd.Parameters.AddWithValue("@IdRol", idRol);

                using var reader = cmd.ExecuteReader();

                if (!reader.Read()) return null;
                var rol = MapRol(reader);

                reader.NextResult();
                while (reader.Read())
                    rol.Familias.Add(MapFamilia(reader));

                reader.NextResult();
                while (reader.Read())
                    rol.Patentes.Add(MapPatente(reader));

                return rol;
            }
            catch (Exception ex) { LogError(nameof(GetRolCompleto_90DI), ex); return null; }
        }

        public Familia_90DI? GetFamiliaCompleta_90DI(int idFamilia)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetFamiliaCompleta_90DI";
                cmd.Parameters.AddWithValue("@IdFamilia", idFamilia);

                using var reader = cmd.ExecuteReader();

                if (!reader.Read()) return null;
                var familia = MapFamilia(reader);

                reader.NextResult();
                while (reader.Read())
                    familia.Patentes.Add(MapPatente(reader));

                reader.NextResult();
                while (reader.Read())
                    familia.SubFamilias.Add(MapFamilia(reader));

                return familia;
            }
            catch (Exception ex) { LogError(nameof(GetFamiliaCompleta_90DI), ex); return null; }
        }

        public List<Familia_90DI> GetAllFamilias_90DI()
        {
            var result = new List<Familia_90DI>();
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandText =
                    """
                    SELECT IdFamilia_90DI, Nombre_90DI, Descripcion_90DI,
                           Activo_90DI, FechaAlta_90DI
                    FROM Familia_90DI
                    WHERE Activo_90DI = 1
                    ORDER BY Nombre_90DI
                    """;
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    result.Add(MapFamilia(reader));
            }
            catch (Exception ex) { LogError(nameof(GetAllFamilias_90DI), ex); }
            return result;
        }

        public List<Patente_90DI> GetAllPatentes_90DI()
        {
            var result = new List<Patente_90DI>();
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetAllPatentes_90DI";

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    result.Add(MapPatente(reader));
            }
            catch (Exception ex) { LogError(nameof(GetAllPatentes_90DI), ex); }
            return result;
        }

        public bool HasPatente_90DI(int idRol, string nombrePatente)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_HasPatente_90DI";
                cmd.Parameters.AddWithValue("@IdRol",         idRol);
                cmd.Parameters.AddWithValue("@NombrePatente", nombrePatente);

                var result = cmd.ExecuteScalar();
                return result is bool b && b;
            }
            catch (Exception ex) { LogError(nameof(HasPatente_90DI), ex); return false; }
        }

        // ── Inserts / Updates / Deletes ────────────────────────────────────────

        public bool InsertRol_90DI(Rol_90DI rol)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();

                // 1 — insertar rol y obtener ID generado
                cmd.CommandText =
                    """
                    INSERT INTO Rol_90DI (Nombre_90DI, Descripcion_90DI, Activo_90DI, FechaAlta_90DI)
                    VALUES (@nombre, @descripcion, 1, GETDATE());
                    SELECT SCOPE_IDENTITY();
                    """;
                cmd.Parameters.AddWithValue("@nombre",      rol.Nombre_90DI);
                cmd.Parameters.AddWithValue("@descripcion", rol.Descripcion_90DI);

                var scalar = cmd.ExecuteScalar();
                if (scalar == null) return false;
                int idRol = Convert.ToInt32(scalar);

                // 2 — insertar patentes sueltas
                foreach (var p in rol.Patentes)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText =
                        """
                        IF NOT EXISTS (SELECT 1 FROM Rol_Patente_90DI WHERE IdRol_90DI = @idRol AND IdPatente_90DI = @idPatente)
                            INSERT INTO Rol_Patente_90DI (IdRol_90DI, IdPatente_90DI) VALUES (@idRol, @idPatente)
                        """;
                    cmd.Parameters.AddWithValue("@idRol",     idRol);
                    cmd.Parameters.AddWithValue("@idPatente", p.IdPatente_90DI);
                    cmd.ExecuteNonQuery();
                }

                // 3 — insertar familias
                foreach (var f in rol.Familias)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText =
                        """
                        IF NOT EXISTS (SELECT 1 FROM Rol_Familia_90DI WHERE IdRol_90DI = @idRol AND IdFamilia_90DI = @idFamilia)
                            INSERT INTO Rol_Familia_90DI (IdRol_90DI, IdFamilia_90DI) VALUES (@idRol, @idFamilia)
                        """;
                    cmd.Parameters.AddWithValue("@idRol",     idRol);
                    cmd.Parameters.AddWithValue("@idFamilia", f.IdFamilia_90DI);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex) { LogError(nameof(InsertRol_90DI), ex); return false; }
        }

        public bool InsertPatente_90DI(Patente_90DI patente)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandText =
                    """
                    INSERT INTO Patente_90DI (Nombre_90DI, Descripcion_90DI, Activo_90DI, FechaAlta_90DI)
                    VALUES (@nombre, @descripcion, 1, GETDATE())
                    """;
                cmd.Parameters.AddWithValue("@nombre",      patente.Nombre_90DI);
                cmd.Parameters.AddWithValue("@descripcion", patente.Descripcion_90DI);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex) { LogError(nameof(InsertPatente_90DI), ex); return false; }
        }

        public bool InsertFamilia_90DI(Familia_90DI familia)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();

                cmd.CommandText =
                    """
                    INSERT INTO Familia_90DI (Nombre_90DI, Descripcion_90DI, Activo_90DI, FechaAlta_90DI)
                    VALUES (@nombre, @descripcion, 1, GETDATE());
                    SELECT SCOPE_IDENTITY();
                    """;
                cmd.Parameters.AddWithValue("@nombre",      familia.Nombre_90DI);
                cmd.Parameters.AddWithValue("@descripcion", familia.Descripcion_90DI);

                var scalar = cmd.ExecuteScalar();
                if (scalar == null) return false;
                int idFamilia = Convert.ToInt32(scalar);

                foreach (var patente in familia.Patentes)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText =
                        """
                        IF NOT EXISTS (SELECT 1 FROM Familia_Patente_90DI WHERE IdFamilia_90DI = @idFamilia AND IdPatente_90DI = @idPatente)
                            INSERT INTO Familia_Patente_90DI (IdFamilia_90DI, IdPatente_90DI) VALUES (@idFamilia, @idPatente)
                        """;
                    cmd.Parameters.AddWithValue("@idFamilia", idFamilia);
                    cmd.Parameters.AddWithValue("@idPatente", patente.IdPatente_90DI);
                    cmd.ExecuteNonQuery();
                }

                foreach (var hija in familia.SubFamilias)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText =
                        """
                        IF NOT EXISTS (SELECT 1 FROM Familia_Familia_90DI WHERE IdFamiliaPadre_90DI = @idPadre AND IdFamiliaHija_90DI = @idHija)
                            INSERT INTO Familia_Familia_90DI (IdFamiliaPadre_90DI, IdFamiliaHija_90DI) VALUES (@idPadre, @idHija)
                        """;
                    cmd.Parameters.AddWithValue("@idPadre", idFamilia);
                    cmd.Parameters.AddWithValue("@idHija",  hija.IdFamilia_90DI);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex) { LogError(nameof(InsertFamilia_90DI), ex); return false; }
        }

        public bool DeleteFamilia_90DI(int idFamilia)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_DeleteFamilia_90DI";
                cmd.Parameters.AddWithValue("@IdFamilia", idFamilia);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) { LogError(nameof(DeleteFamilia_90DI), ex); return false; }
        }


        public bool UpdateRol_90DI(Rol_90DI rol)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();

                cmd.CommandText =
                    """
                    UPDATE Rol_90DI
                    SET Nombre_90DI      = @nombre,
                        Descripcion_90DI = @descripcion
                    WHERE IdRol_90DI = @id
                    """;
                cmd.Parameters.AddWithValue("@nombre",      rol.Nombre_90DI);
                cmd.Parameters.AddWithValue("@descripcion", rol.Descripcion_90DI);
                cmd.Parameters.AddWithValue("@id",          rol.IdRol_90DI);
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE FROM Rol_Patente_90DI WHERE IdRol_90DI = @id";
                cmd.Parameters.AddWithValue("@id", rol.IdRol_90DI);
                cmd.ExecuteNonQuery();

                foreach (var p in rol.Patentes)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO Rol_Patente_90DI (IdRol_90DI, IdPatente_90DI) VALUES (@idRol, @idPatente)";
                    cmd.Parameters.AddWithValue("@idRol",     rol.IdRol_90DI);
                    cmd.Parameters.AddWithValue("@idPatente", p.IdPatente_90DI);
                    cmd.ExecuteNonQuery();
                }

                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE FROM Rol_Familia_90DI WHERE IdRol_90DI = @id";
                cmd.Parameters.AddWithValue("@id", rol.IdRol_90DI);
                cmd.ExecuteNonQuery();

                foreach (var f in rol.Familias)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO Rol_Familia_90DI (IdRol_90DI, IdFamilia_90DI) VALUES (@idRol, @idFamilia)";
                    cmd.Parameters.AddWithValue("@idRol",     rol.IdRol_90DI);
                    cmd.Parameters.AddWithValue("@idFamilia", f.IdFamilia_90DI);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex) { LogError(nameof(UpdateRol_90DI), ex); return false; }
        }

        public bool UpdateFamilia_90DI(Familia_90DI familia)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();

                cmd.CommandText =
                    """
                    UPDATE Familia_90DI
                    SET Nombre_90DI      = @nombre,
                        Descripcion_90DI = @descripcion
                    WHERE IdFamilia_90DI = @id
                    """;
                cmd.Parameters.AddWithValue("@nombre",      familia.Nombre_90DI);
                cmd.Parameters.AddWithValue("@descripcion", familia.Descripcion_90DI);
                cmd.Parameters.AddWithValue("@id",          familia.IdFamilia_90DI);
                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE FROM Familia_Patente_90DI WHERE IdFamilia_90DI = @id";
                cmd.Parameters.AddWithValue("@id", familia.IdFamilia_90DI);
                cmd.ExecuteNonQuery();

                foreach (var patente in familia.Patentes)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText =
                        """
                        INSERT INTO Familia_Patente_90DI (IdFamilia_90DI, IdPatente_90DI)
                        VALUES (@idFamilia, @idPatente)
                        """;
                    cmd.Parameters.AddWithValue("@idFamilia", familia.IdFamilia_90DI);
                    cmd.Parameters.AddWithValue("@idPatente", patente.IdPatente_90DI);
                    cmd.ExecuteNonQuery();
                }

                cmd.Parameters.Clear();
                cmd.CommandText = "DELETE FROM Familia_Familia_90DI WHERE IdFamiliaPadre_90DI = @id";
                cmd.Parameters.AddWithValue("@id", familia.IdFamilia_90DI);
                cmd.ExecuteNonQuery();

                foreach (var hija in familia.SubFamilias)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText =
                        """
                        IF NOT EXISTS (SELECT 1 FROM Familia_Familia_90DI WHERE IdFamiliaPadre_90DI = @idPadre AND IdFamiliaHija_90DI = @idHija)
                            INSERT INTO Familia_Familia_90DI (IdFamiliaPadre_90DI, IdFamiliaHija_90DI) VALUES (@idPadre, @idHija)
                        """;
                    cmd.Parameters.AddWithValue("@idPadre", familia.IdFamilia_90DI);
                    cmd.Parameters.AddWithValue("@idHija",  hija.IdFamilia_90DI);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex) { LogError(nameof(UpdateFamilia_90DI), ex); return false; }
        }

        public bool AsignarPatenteARol_90DI(int idRol, int idPatente)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandText =
                    """
                    IF NOT EXISTS (SELECT 1 FROM Rol_Patente_90DI WHERE IdRol_90DI = @idRol AND IdPatente_90DI = @idPatente)
                        INSERT INTO Rol_Patente_90DI (IdRol_90DI, IdPatente_90DI) VALUES (@idRol, @idPatente)
                    """;
                cmd.Parameters.AddWithValue("@idRol",     idRol);
                cmd.Parameters.AddWithValue("@idPatente", idPatente);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) { LogError(nameof(AsignarPatenteARol_90DI), ex); return false; }
        }

        public bool AsignarFamiliaARol_90DI(int idRol, int idFamilia)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandText =
                    """
                    IF NOT EXISTS (SELECT 1 FROM Rol_Familia_90DI WHERE IdRol_90DI = @idRol AND IdFamilia_90DI = @idFamilia)
                        INSERT INTO Rol_Familia_90DI (IdRol_90DI, IdFamilia_90DI) VALUES (@idRol, @idFamilia)
                    """;
                cmd.Parameters.AddWithValue("@idRol",     idRol);
                cmd.Parameters.AddWithValue("@idFamilia", idFamilia);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) { LogError(nameof(AsignarFamiliaARol_90DI), ex); return false; }
        }

        public bool AsignarPatenteAFamilia_90DI(int idFamilia, int idPatente)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandText =
                    """
                    IF NOT EXISTS (SELECT 1 FROM Familia_Patente_90DI WHERE IdFamilia_90DI = @idFamilia AND IdPatente_90DI = @idPatente)
                        INSERT INTO Familia_Patente_90DI (IdFamilia_90DI, IdPatente_90DI) VALUES (@idFamilia, @idPatente)
                    """;
                cmd.Parameters.AddWithValue("@idFamilia", idFamilia);
                cmd.Parameters.AddWithValue("@idPatente", idPatente);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) { LogError(nameof(AsignarPatenteAFamilia_90DI), ex); return false; }
        }

        public bool RemoverPatenteDeRol_90DI(int idRol, int idPatente)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM Rol_Patente_90DI WHERE IdRol_90DI = @idRol AND IdPatente_90DI = @idPatente";
                cmd.Parameters.AddWithValue("@idRol",     idRol);
                cmd.Parameters.AddWithValue("@idPatente", idPatente);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex) { LogError(nameof(RemoverPatenteDeRol_90DI), ex); return false; }
        }

        public bool RemoverFamiliaDeRol_90DI(int idRol, int idFamilia)
        {
            try
            {
                using var con = _conexion.GetConnection();
                con.Open();
                using var cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM Rol_Familia_90DI WHERE IdRol_90DI = @idRol AND IdFamilia_90DI = @idFamilia";
                cmd.Parameters.AddWithValue("@idRol",     idRol);
                cmd.Parameters.AddWithValue("@idFamilia", idFamilia);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex) { LogError(nameof(RemoverFamiliaDeRol_90DI), ex); return false; }
        }

        // ── Mappers privados ───────────────────────────────────────────────────

        private static Rol_90DI MapRol(SqlDataReader r) => new()
        {
            IdRol_90DI       = r.GetInt32(0),
            Nombre_90DI      = r.IsDBNull(1) ? "" : r.GetString(1),
            Descripcion_90DI = r.IsDBNull(2) ? "" : r.GetString(2),
            Activo_90DI      = !r.IsDBNull(3) && r.GetBoolean(3),
            FechaAlta_90DI   = r.IsDBNull(4) ? DateTime.MinValue : r.GetDateTime(4)
        };

        private static Familia_90DI MapFamilia(SqlDataReader r) => new()
        {
            IdFamilia_90DI   = r.GetInt32(0),
            Nombre_90DI      = r.IsDBNull(1) ? "" : r.GetString(1),
            Descripcion_90DI = r.IsDBNull(2) ? "" : r.GetString(2),
            Activo_90DI      = !r.IsDBNull(3) && r.GetBoolean(3),
            FechaAlta_90DI   = r.IsDBNull(4) ? DateTime.MinValue : r.GetDateTime(4)
        };

        private static Patente_90DI MapPatente(SqlDataReader r) => new()
        {
            IdPatente_90DI   = r.GetInt32(0),
            Nombre_90DI      = r.IsDBNull(1) ? "" : r.GetString(1),
            Descripcion_90DI = r.IsDBNull(2) ? "" : r.GetString(2),
            Activo_90DI      = !r.IsDBNull(3) && r.GetBoolean(3),
            FechaAlta_90DI   = r.IsDBNull(4) ? DateTime.MinValue : r.GetDateTime(4)
        };
    }
}
