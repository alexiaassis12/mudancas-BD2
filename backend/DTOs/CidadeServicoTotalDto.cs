namespace Mudanca.DTOs
{
    public class CidadeServicoTotalDto
    {
        public int IdCidade { get; set; }
        public string NomeCidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public int TotalServicos { get; set; }
    }
}
