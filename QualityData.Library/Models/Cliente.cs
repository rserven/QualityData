using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualityData.Library.Models
{
    [Table("Cliente")]
    public partial class Cliente
    {
        public Cliente()
        {
            ClienteUbicacions = new HashSet<ClienteUbicacion>();
        }

        public int ClienteId { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(150)]
        public string Apellido { get; set; }

        public int Documento { get; set; }

        public virtual ICollection<ClienteUbicacion> ClienteUbicacions { get; set; }
    }
}
