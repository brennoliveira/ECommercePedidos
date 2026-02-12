using ECommercePedidos.Application.UseCases.AtualizarPedido;
using ECommercePedidos.Application.UseCases.CriarPedido;
using ECommercePedidos.Application.UseCases.ObterPedidoPorId;
using Microsoft.AspNetCore.Mvc;

namespace ECommercePedidos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController(
        ICriarPedidoUseCase criarPedidoUseCase,
        IObterPedidoPorIdUseCase obterPedidoPorIdUseCase,
        IAtualizarPedidoUseCase atualizarPedidoUseCase)
        : ControllerBase
    {
        private readonly ICriarPedidoUseCase _criarPedidoUseCase = criarPedidoUseCase;
        private readonly IObterPedidoPorIdUseCase obterPedidoPorIdUseCase = obterPedidoPorIdUseCase;
        private readonly IAtualizarPedidoUseCase _atualizarPedidoUseCase = atualizarPedidoUseCase;

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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPedidoPorId([FromQuery] Guid id)
        {
            try
            {
                var pedido = await obterPedidoPorIdUseCase.ExecutarAsync(id);

                if (pedido == null)
                    return NotFound();

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> AtualizarPedido([FromQuery] Guid id, [FromBody] AtualizarPedidoInput input)
        {
            try
            {
                var resultado = await _atualizarPedidoUseCase.ExecutarAsync(id, input);
                if (!resultado)
                    return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
