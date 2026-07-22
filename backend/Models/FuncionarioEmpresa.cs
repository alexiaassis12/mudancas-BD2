namespace Mudanca.Models
{
    public class FuncionarioEmpresa
    {
        public string CpfFunc { get; set; } = string.Empty;
        public int IdEmpresa { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string? TelefoneEmpresa { get; set; }
    }
}
