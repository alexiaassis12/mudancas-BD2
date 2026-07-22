namespace Mudanca.Models
{
    public class Funcionario
    {
        public string CpfFunc { get; set; } = string.Empty;
        public string RgFunc { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string TipoFunc { get; set; } = string.Empty;
        public decimal Salario { get; set; }
    }
}
