using System.Collections.Generic;

namespace Services_90DI
{
    // Observer: la implementa cada form que se tiene que traducir.
    public interface IObserver_90DI
    {
        void UpdateLanguage_90DI(Dictionary<string, string> traducciones);
    }
}
