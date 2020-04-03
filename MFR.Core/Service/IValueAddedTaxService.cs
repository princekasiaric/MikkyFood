namespace MFR.Core.Service
{
    public interface IValueAddedTaxService
    {
        decimal CalculateVat(decimal totalAmount);
    }
}
