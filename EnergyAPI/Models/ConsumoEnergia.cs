using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyAPI.Models
{
    [Table("Consumos")]
    public class ConsumoEnergia
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Range(0.1, 100000)]
        public double ConsumoKwh { get; set; }

        public DateTime DataHora { get; set; }
    }
}
