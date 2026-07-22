namespace Mudanca.Models
{
    public class TransporteAcrescimo
    {
        public string NomeServico { get; set; } = string.Empty;
        public decimal PesoLimite { get; set; }
        public decimal PercentualAcrescimo { get; set; }
    }
}
