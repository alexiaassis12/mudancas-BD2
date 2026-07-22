namespace Mudanca.Models
{
    public class Oferecem
    {
        public int IdEmpresa { get; set; }
        public int IdCidade { get; set; }
        public string NomeServico { get; set; } = string.Empty;
        public decimal PrecoHora { get; set; }
    }
}
