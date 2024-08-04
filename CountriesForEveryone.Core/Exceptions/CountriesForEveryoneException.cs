namespace CountriesForEveryone.Core.Exceptions
{
    public class CountriesForEveryoneException : Exception
    {
        public CountriesForEveryoneException(string message) : base(message)
        {
        }

        public CountriesForEveryoneException() : base()
        {
        }

        public CountriesForEveryoneException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}