using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Disco
    {
        public int Id { get; set; }
        [DisplayName ("Título")]
        public string Titulo { get; set; }
        [DisplayName("Fecha de Lanzamiento")]

        public DateTime FechaLanzamiento { get; set; }
        [DisplayName("Cantidad de Canciones")]

        public int CantidadCanciones { get; set; }
        public string Imagen { get; set; }
        public Estilo Estilo { get; set; }
        [DisplayName("2° Estilo")]

        public Estilo SegundoEstilo { get; set; }
        [DisplayName("Tipo de Edición")]

        public TipoEdicion TipoEdicion { get; set; }

    }
}
