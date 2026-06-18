using Services_90DI.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_90DI
{
    public interface IPublisher_90DI
    {
       public abstract void AddObserver_90DI(IObserver_90DI observer);

        public abstract void RemoveAllObservers_90DI();

        public abstract void NotifyAllObservers_90DI(Idioma_90DI idioma);
    }
}
