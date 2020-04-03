namespace MFR.Core.Service.Implementation
{
    public class ValueAddedTaxService : IValueAddedTaxService
    {
        private readonly decimal rate = 0.075m;

        public decimal CalculateVat(decimal totalAmount) => totalAmount * rate;
    }
}
