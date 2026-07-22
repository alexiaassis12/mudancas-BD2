namespace Mudanca.DTOs
{
    public class EmpresaRankingDto
    {
        public int IdEmpresa { get; set; }
        public string NomeEmpresa { get; set; } = string.Empty;
        public int TotalServicosSolicitados { get; set; }
        public decimal ValoresGanhos { get; set; }
    }
}
