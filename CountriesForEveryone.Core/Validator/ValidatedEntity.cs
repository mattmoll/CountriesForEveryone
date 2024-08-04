namespace CountriesForEveryone.Core.Validator
{
    public class ValidatedEntity : IValidatedEntity
    {
        public ValidatedEntity()
        {
        }

        public ValidatedEntity(IValidatedEntity validatedEntity)
        {
            Errors = validatedEntity.Errors;
        }

        public static ValidatedEntity Error(string error)
        {
            return new ValidatedEntity
            {
                Errors = new()
                {
                    new()
                    {
                        Message = error
                    }
                }
            };
        }

        public static ValidatedEntity<TData> WithErrors<TData>(List<ValidationError> errors)
        {
            return new ValidatedEntity<TData>
            {
                Errors = errors
            };
        }

        public static ValidatedEntity<TData> Error<TData>(string error)
        {
            return new ValidatedEntity<TData>
            {
                Errors = new()
                {
                    new()
                    {
                        Message = error
                    }
                }
            };
        }

        public List<ValidationError> Errors { get; set; } = new();

        public bool HasErrors => Errors.Any();

        public string ErrorMessages => string.Join(',', Errors.Select(e => e.Message));

        public void AddError(string errorMessage)
        {
            Errors.Add(new ValidationError
            {
                Message = errorMessage
            });
        }
    }

    public class ValidatedEntity<TData> : ValidatedEntity, IValidatedEntity<TData>
    {
        public ValidatedEntity() : base()
        {
        }

        public ValidatedEntity(IValidatedEntity validatedEntity) : base(validatedEntity)
        {
        }

        public TData Data { get; set; }
    }
}
