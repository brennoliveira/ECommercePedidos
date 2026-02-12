using ECommercePedidos.Application.UseCases.CriarPedido;
using Microsoft.AspNetCore.Mvc;

namespace ECommercePedidos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController(ICriarPedidoUseCase criarPedidoUseCase) : ControllerBase
    {
        private readonly ICriarPedidoUseCase _criarPedidoUseCase = criarPedidoUseCase;

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] CriarPedidoInput input)
        {
            try
            {
                var pedidoId = await _criarPedidoUseCase.ExecutarAsync(input);
                return CreatedAtAction(nameof(CriarPedido), new { id = pedidoId }, new { id = pedidoId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
