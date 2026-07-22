namespace Mudanca.Models
{
    public class Cliente
    {
        public int CodigoCliente { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Rg { get; set; } = string.Empty;
        public string NomeCompleto { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
    }
}
