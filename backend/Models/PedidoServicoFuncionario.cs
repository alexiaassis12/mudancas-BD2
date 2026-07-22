namespace Mudanca.Models
{
    public class PedidoServicoFuncionario
    {
        public int CodigoPedido { get; set; }
        public string NomeServico { get; set; } = string.Empty;
        public string CpfFunc { get; set; } = string.Empty;
    }
}
