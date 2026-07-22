namespace Mudanca.DTOs
{
    public class PedidoServicoCreateDto
    {
        public int CodigoPedido { get; set; }
        public string NomeServico { get; set; } = string.Empty;
        public decimal TempoDuracao { get; set; }
        public decimal? PesoCarga { get; set; } // Opcional, usado se for Transporte[cite: 3]
    }
}
