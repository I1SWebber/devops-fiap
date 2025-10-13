
using EnergyAPI.Data;
using EnergyAPI.Models;
using EnergyAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnergyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoEnergiaController : ControllerBase
    {
        private readonly ConsumoEnergiaService _service;

        public ConsumoEnergiaController(ConsumoEnergiaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsumoEnergia>>> GetConsumos(int page = 1, int pageSize = 10)
        {
            return Ok(await _service.GetConsumosAsync(page, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoEnergia>> GetConsumoEnergia(int id)
        {
            var consumo = await _service.GetByIdAsync(id);
            if (consumo == null) return NotFound();
            return Ok(consumo);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumoEnergia>> PostConsumoEnergia(ConsumoEnergia consumo)
        {
            var created = await _service.CreateAsync(consumo);
            return CreatedAtAction(nameof(GetConsumoEnergia), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutConsumoEnergia(int id, ConsumoEnergia consumo)
        {
            var updated = await _service.UpdateAsync(id, consumo);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumoEnergia(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
