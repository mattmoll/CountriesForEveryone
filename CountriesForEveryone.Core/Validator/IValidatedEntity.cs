namespace CountriesForEveryone.Core.Validator
{
    public interface IValidatedEntity
    {
        public List<ValidationError> Errors { get; set; }
        public bool HasErrors { get; }
        public string ErrorMessages { get; }

        public void AddError(string errorMessage);
    }

    public interface IValidatedEntity<TData> : IValidatedEntity
    {
        TData Data { get; set; }
    }
}