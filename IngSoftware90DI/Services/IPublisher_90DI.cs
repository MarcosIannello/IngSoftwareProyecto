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
        void AddObserver_90DI(IObserver_90DI observer);
        void RemoveAllObservers_90DI();
        void NotifyAllObservers_90DI(Idioma_90DI idioma);
        void Unsubscribe_90DI(IObserver_90DI observer);
    }
}
