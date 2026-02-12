using ECommercePedidos.Application.UseCases.AtualizarPedido;
using ECommercePedidos.Application.UseCases.CriarPedido;
using ECommercePedidos.Application.UseCases.DeletarPedido;
using ECommercePedidos.Application.UseCases.ObterPedidoPorId;
using ECommercePedidos.Application.UseCases.ObterTodosPedidos;
using ECommercePedidos.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ECommercePedidos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController(
        ICriarPedidoUseCase criarPedidoUseCase,
        IObterPedidoPorIdUseCase obterPedidoPorIdUseCase,
        IObterTodosPedidosUseCase obterTodosPedidosUseCase,
        IAtualizarPedidoUseCase atualizarPedidoUseCase,
        IDeletarPedidoUseCase deletarPedidoUseCase)
        : ControllerBase
    {
        private readonly ICriarPedidoUseCase _criarPedidoUseCase = criarPedidoUseCase;
        private readonly IObterPedidoPorIdUseCase obterPedidoPorIdUseCase = obterPedidoPorIdUseCase;
        private readonly IObterTodosPedidosUseCase obterTodosPedidosUseCase = obterTodosPedidosUseCase;
        private readonly IAtualizarPedidoUseCase _atualizarPedidoUseCase = atualizarPedidoUseCase;
        private readonly IDeletarPedidoUseCase _deletarPedidoUseCase = deletarPedidoUseCase;

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

        [HttpGet]
        public async Task<IActionResult> ObterTodosPedidos(
            [FromQuery] PedidoStatus? status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                var pedidos = await obterTodosPedidosUseCase.ExecutarAsync(page, pageSize, status);

                return Ok(pedidos);
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

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletarPedido([FromQuery] Guid id)
        {
            try
            {
                var resultado = await _deletarPedidoUseCase.ExecutarAsync(id);

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
