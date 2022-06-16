using System;
using System.Collections.Generic;
using System.Text;

namespace GaleriaArtesMVVM.Models
{
    internal class Arte
    {
        public string Titulo { get; set; }
        public string Foto { get; set; } = "https://cdn-icons-png.flaticon.com/512/739/739249.png";

        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public string Artista { get; set; }
    }
}
