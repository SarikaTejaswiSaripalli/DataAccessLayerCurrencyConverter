using System.Globalization;
namespace BusinessLogicLayerCurrencyConverter.Exceptions
{
    public class InformationNotAvailableException : Exception
    {
        public InformationNotAvailableException(string message) : base(message)
        {
        }
        public InformationNotAvailableException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
