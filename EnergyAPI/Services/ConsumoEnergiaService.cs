
using EnergyAPI.Data;
using EnergyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyAPI.Services
{
    public class ConsumoEnergiaService
    {
        private readonly EnergyContext _context;

        public ConsumoEnergiaService(EnergyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConsumoEnergia>> GetConsumosAsync(int page, int pageSize)
        {
            return await _context.Consumos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<ConsumoEnergia?> GetByIdAsync(int id)
        {
            return await _context.Consumos.FindAsync(id);
        }

        public async Task<ConsumoEnergia> CreateAsync(ConsumoEnergia consumo)
        {
            _context.Consumos.Add(consumo);
            await _context.SaveChangesAsync();
            return consumo;
        }

        public async Task<bool> UpdateAsync(int id, ConsumoEnergia consumo)
        {
            var existing = await _context.Consumos.FindAsync(id);
            if (existing == null) return false;

            existing.Nome = consumo.Nome;
            existing.ConsumoKwh = consumo.ConsumoKwh;
            existing.DataHora = consumo.DataHora;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Consumos.FindAsync(id);
            if (existing == null) return false;

            _context.Consumos.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
