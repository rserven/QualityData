using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualityData.Api.Models
{
    [Table("TipoUbicacion")]
    public partial class TipoUbicacion
    {
        public TipoUbicacion()
        {
            Ubicacions = new HashSet<Ubicacion>();
        }

        public int TipoUbicacionId { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        public virtual ICollection<Ubicacion> Ubicacions { get; set; }
    }
}
