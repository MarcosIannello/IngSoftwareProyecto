using DAL;
using Services_90DI.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RolBLL_90DI
    {
        public RolesDAL_90DI _dal = new RolesDAL_90DI();
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
            return _dal.InsertFamilia_90DI(familia);
        }

        //!TODO: Generar Evengto de bitacora 
    }
}
