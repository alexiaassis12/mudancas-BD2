namespace Mudanca.DTOs
{
    public class FuncionarioCreateDto
    {
        public string CpfFunc { get; set; } = string.Empty;
        public string RgFunc { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string TipoFunc { get; set; } = string.Empty; // Ex: 'MOTORISTA', 'AJUDANTE'
        public decimal Salario { get; set; }
    }
}
