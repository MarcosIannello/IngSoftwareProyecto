using System.Collections.Generic;

namespace Services_90DI
{
    // Observer: cada formulario que quiera traducirse implementa esta interfaz.
    public interface IObserver_90DI
    {
        void UpdateLanguage_90DI(Dictionary<string, string> traducciones);
    }
}
