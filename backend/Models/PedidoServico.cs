namespace Mudanca.Models
{
    public class PedidoServico
    {
        public int CodigoPedido { get; set; }
        public string NomeServico { get; set; } = string.Empty;
        public decimal TempoDuracao { get; set; }
        public DateTime? DataFim { get; set; }
        public decimal? PesoCarga { get; set; }
        public decimal Preco { get; set; }
    }
}
