namespace Mudanca.DTOs
{
    public class CidadeFaturamentoDto
    {
        public int IdCidade { get; set; }
        public string NomeCidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public decimal ValorInvestido { get; set; }
    }
}
