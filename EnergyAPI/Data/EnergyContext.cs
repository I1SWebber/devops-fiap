using EnergyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EnergyAPI.Data
{
    public class EnergyContext : DbContext
    {
        public EnergyContext(DbContextOptions<EnergyContext> options) : base(options) { }

        public DbSet<ConsumoEnergia> Consumos { get; set; }
    }
}
