namespace Mudanca.DTOs
{
    public class PedidoCreateDto
    {
        public int IdEmpresa { get; set; }
        public int CodigoCliente { get; set; }
        public int? IdCidadeOrigem { get; set; }
        public string? EnderecoOrigem { get; set; }
        public int? IdCidadeDestino { get; set; }
        public string? EnderecoDestino { get; set; }
        public DateTime DataSolicitacao { get; set; }
    }
}
