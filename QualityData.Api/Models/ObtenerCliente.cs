using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualityData.Api.Models
{
    public partial class ObtenerCliente
    {
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClienteId { get; set; }

        [Column(Order = 1)]
        [StringLength(150)]
        public string Nombre { get; set; }

        [Column(Order = 2)]
        [StringLength(150)]
        public string Apellido { get; set; }

        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Documento { get; set; }

        [Column(Order = 4)]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Column(Order = 5)]
        [StringLength(500)]
        public string Direccion { get; set; }

        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Telefono { get; set; }
    }
}
