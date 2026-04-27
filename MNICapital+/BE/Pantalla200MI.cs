using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Pantalla200MI
    {
        public int IdPantalla_200MI {  get; set; }
        public string Nombre_200MI { get; set; } = "";
        public string NombreForm_200MI { get; set; } = "";
        public int? IdPadre_200MI { get; set; }
        public int Orden_200MI { get; set; }

        public bool EsAccion_200MI { get; set; }

        public List<Pantalla200MI> Hijos { get; set; } = new List<Pantalla200MI>();

    }
}
