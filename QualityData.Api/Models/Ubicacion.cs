using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualityData.Api.Models
{
    [Table("Ubicacion")]
    public partial class Ubicacion
    {
        public Ubicacion()
        {
            ClienteUbicacions = new HashSet<ClienteUbicacion>();
        }

        public int UbicacionId { get; set; }

        public int TipoUbicacion { get; set; }

        [Required]
        [StringLength(500)]
        public string Direccion { get; set; }

        public int Telefono { get; set; }

        public virtual ICollection<ClienteUbicacion> ClienteUbicacions { get; set; }

        public virtual TipoUbicacion TipoUbicacion1 { get; set; }
    }
}
