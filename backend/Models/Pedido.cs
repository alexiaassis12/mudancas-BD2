namespace Mudanca.Models
{
    public class Pedido
    {
        public int CodigoPedido { get; set; }
        public int IdEmpresa { get; set; }
        public int CodigoCliente { get; set; }
        public int? IdCidadeOrigem { get; set; }
        public string? EnderecoOrigem { get; set; }
        public int? IdCidadeDestino { get; set; }
        public string? EnderecoDestino { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataResolucao { get; set; }
        public string Situacao { get; set; } = string.Empty;
        public decimal PrecoTotal { get; set; }
    }
}
