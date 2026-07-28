using BLL_90DI;
using DAL;
using Service_90DI;
using Services_90DI.entities;
using Services_90DI.constantes;

namespace BLL
{
    public class RolBLL_90DI
    {
        public RolesDAL_90DI _dal = new RolesDAL_90DI();
        private readonly BitacoraBLL_90DI _bitacora = new BitacoraBLL_90DI();
        private readonly IntegridadBLL_90DI _integridad = new IntegridadBLL_90DI();
        public RolBLL_90DI() { }

        public List<Patente_90DI> GetAllPatentes_90DI()
        {
            return _dal.GetAllPatentes_90DI();
        }

        public List<Familia_90DI> GetAllFamilias_90DI()
        {
            return _dal.GetAllFamilias_90DI();
        }

        public Familia_90DI? GetFamiliaCompleta_90DI(int idFamilia)
        {
            return _dal.GetFamiliaCompleta_90DI(idFamilia);
        }

        public bool CreateFamilia_90DI(Familia_90DI familia)
        {
            var result = _dal.InsertFamilia_90DI(familia);
            if (result)
            {
                _bitacora.CreateLogEvent_90DI(new Event_90DI
                {
                    Login_90DI      = SessionManager_90DI.Instancia_90DI.userActual_90DI.NombreUsuario_90DI,
                    Fecha_90DI      = DateTime.Now,
                    Hora_90DI       = DateTime.Now.TimeOfDay,
                    Modulo_90DI     = Modulos_90DI.Familias,
                    Evento_90DI     = $"Alta familia: {familia.Nombre_90DI}",
                    Criticidad_90DI = 1
                });
                _integridad.RecalcularTablas_90DI(TablasDV_90DI.Familias);
            }
            return result;
        }

        public List<Rol_90DI> GetAllRoles_90DI()         => _dal.GetAllRoles_90DI();
        public Rol_90DI? GetRolCompleto_90DI(int idRol)  => _dal.GetRolCompleto_90DI(idRol);

        // Devuelve el conjunto de NOMBRES de patente efectivos de un rol: las patentes
        // directas del rol + las heredadas de sus familias y subfamilias (recursivo).
        // Comparación case-insensitive para que matchee con los Tag del menú.
        public HashSet<string> GetPatentesEfectivas_90DI(int idRol)
        {
            var patentes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var rol = _dal.GetRolCompleto_90DI(idRol);
            if (rol == null) return patentes;

            // Patentes asignadas directamente al rol
            foreach (var p in rol.Patentes_90DI)
                patentes.Add(p.Nombre_90DI.Trim());

            // Patentes heredadas de cada familia (con recursión sobre subfamilias)
            var visitadas = new HashSet<int>();
            foreach (var f in rol.Familias_90DI)
                AgregarPatentesFamilia_90DI(f.IdFamilia_90DI, patentes, visitadas);

            return patentes;
        }

        // Suma las patentes de una familia y baja recursivamente a sus subfamilias.
        // 'visitadas' evita reprocesar / loops si hubiera familias cíclicas.
        private void AgregarPatentesFamilia_90DI(int idFamilia, HashSet<string> patentes, HashSet<int> visitadas)
        {
            if (!visitadas.Add(idFamilia)) return;

            var familia = _dal.GetFamiliaCompleta_90DI(idFamilia);
            if (familia == null) return;

            foreach (var p in familia.Patentes_90DI)
                patentes.Add(p.Nombre_90DI.Trim());

            foreach (var sub in familia.SubFamilias_90DI)
                AgregarPatentesFamilia_90DI(sub.IdFamilia_90DI, patentes, visitadas);
        }

        public bool CreateRol_90DI(Rol_90DI rol)
        {
            var result = _dal.InsertRol_90DI(rol);
            if (result)
            {
                _bitacora.CreateLogEvent_90DI(new Event_90DI
                {
                    Login_90DI      = SessionManager_90DI.Instancia_90DI.userActual_90DI.NombreUsuario_90DI,
                    Fecha_90DI      = DateTime.Now,
                    Hora_90DI       = DateTime.Now.TimeOfDay,
                    Modulo_90DI     = Modulos_90DI.Roles,
                    Evento_90DI     = $"Alta rol: {rol.Nombre_90DI}",
                    Criticidad_90DI = 1
                });
                _integridad.RecalcularTablas_90DI(TablasDV_90DI.Roles);
            }
            return result;
        }

        public bool UpdateRol_90DI(Rol_90DI rol)
        {
            var result = _dal.UpdateRol_90DI(rol);
            if (result)
            {
                _bitacora.CreateLogEvent_90DI(new Event_90DI
                {
                    Login_90DI      = SessionManager_90DI.Instancia_90DI.userActual_90DI.NombreUsuario_90DI,
                    Fecha_90DI      = DateTime.Now,
                    Hora_90DI       = DateTime.Now.TimeOfDay,
                    Modulo_90DI     = Modulos_90DI.Roles,
                    Evento_90DI     = $"Modificación rol ID {rol.IdRol_90DI}: {rol.Nombre_90DI}",
                    Criticidad_90DI = 1
                });
                _integridad.RecalcularTablas_90DI(TablasDV_90DI.Roles);
            }
            return result;
        }

        public bool FamiliaEstaEnRol_90DI(int idFamilia) => _dal.FamiliaEstaEnRol_90DI(idFamilia);

        public bool DeleteFamilia_90DI(int idFamilia, string nombreFamilia)
        {
            var familia = _dal.GetFamiliaCompleta_90DI(idFamilia);
            bool tienePatentes    = familia?.Patentes_90DI.Count > 0;
            bool tieneSubFamilias = familia?.SubFamilias_90DI.Count > 0;
            bool tieneRoles       = _dal.GetAllRoles_90DI()
                                        .Any(r => r.Familias_90DI.Any(f => f.IdFamilia_90DI == idFamilia));

            var result = _dal.DeleteFamilia_90DI(idFamilia, tienePatentes, tieneSubFamilias, tieneRoles);
            if (result)
            {
                _bitacora.CreateLogEvent_90DI(new Event_90DI
                {
                    Login_90DI      = SessionManager_90DI.Instancia_90DI.userActual_90DI.NombreUsuario_90DI,
                    Fecha_90DI      = DateTime.Now,
                    Hora_90DI       = DateTime.Now.TimeOfDay,
                    Modulo_90DI     = Modulos_90DI.Familias,
                    Evento_90DI     = $"Baja familia ID {idFamilia}: {nombreFamilia}",
                    Criticidad_90DI = 2
                });
                _integridad.RecalcularTablas_90DI(TablasDV_90DI.FamiliasConRol);
            }
            return result;
        }

        public bool DeleteRol_90DI(int idRol, string nombreRol)
        {
            var result = _dal.DeleteRol_90DI(idRol);
            if (result)
            {
                _bitacora.CreateLogEvent_90DI(new Event_90DI
                {
                    Login_90DI      = SessionManager_90DI.Instancia_90DI.userActual_90DI.NombreUsuario_90DI,
                    Fecha_90DI      = DateTime.Now,
                    Hora_90DI       = DateTime.Now.TimeOfDay,
                    Modulo_90DI     = Modulos_90DI.Roles,
                    Evento_90DI     = $"Baja rol ID {idRol}: {nombreRol}",
                    Criticidad_90DI = 2
                });
                _integridad.RecalcularTablas_90DI(TablasDV_90DI.Roles);
            }
            return result;
        }

        public bool UpdateFamilia_90DI(Familia_90DI familia)
        {
            var result = _dal.UpdateFamilia_90DI(familia);
            if (result)
            {
                _bitacora.CreateLogEvent_90DI(new Event_90DI
                {
                    Login_90DI      = SessionManager_90DI.Instancia_90DI.userActual_90DI.NombreUsuario_90DI,
                    Fecha_90DI      = DateTime.Now,
                    Hora_90DI       = DateTime.Now.TimeOfDay,
                    Modulo_90DI     = Modulos_90DI.Familias,
                    Evento_90DI     = $"Modificación familia ID {familia.IdFamilia_90DI}: {familia.Nombre_90DI}",
                    Criticidad_90DI = 1
                });
                _integridad.RecalcularTablas_90DI(TablasDV_90DI.Familias);
            }
            return result;
        }
    }
}
