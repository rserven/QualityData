using System.ComponentModel.DataAnnotations.Schema;

namespace QualityData.Api.Models
{
    [Table("ClienteUbicacion")]
    public partial class ClienteUbicacion
    {
        public int ClienteUbicacionId { get; set; }

        public int ClienteId { get; set; }

        public int UbicacionId { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Ubicacion Ubicacion { get; set; }
    }
}
