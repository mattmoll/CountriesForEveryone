namespace CountriesForEveryone.Core.Exceptions
{
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException(string message) : base(message)
        {
        }

        public InvalidParameterException() : base()
        {
        }

        public InvalidParameterException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}