using DAL;
using Services_90DI;

namespace BLL_90DI
{
    public class BitacoraBLL_90DI
    {
        private readonly BitacoraDAL_90DI _dal = new BitacoraDAL_90DI();

        public BitacoraBLL_90DI() { }

        public bool CreateLogEvent_90DI(Event_90DI logEvent_90DI)
        {
            return _dal.CreateLogEvent_90DI(logEvent_90DI);
        }

        public List<Event_90DI> GetLogEvent_90DI()
        {
            return _dal.GetLogEvent_90DI();
        }
    }
}
