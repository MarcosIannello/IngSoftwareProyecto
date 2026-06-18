using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_90DI.entities
{
    public class Idioma_90DI
    {
        public int IdIdioma_90DI { get; set; }
        public string NombreIdioma_90DI { get; set; } = "";
        public string CodigoIdioma_90DI { get; set; } = ""; // "es", "en" — nombre del archivo JSON
        public override string ToString() => NombreIdioma_90DI;
    }
}
