using System.Collections.Generic;
using System.Linq;

namespace CountriesForEveryone.Adapter.Models
{
    public class ValidatedDto
    {
        public List<ValidationErrorDto> Errors { get; set; } = new();

        public bool HasErrors => Errors.Any();
        public bool Success => !HasErrors;

    }

    public class ValidatedDto<TDto> : ValidatedDto
    {
        public TDto Data { get; set; }
    }
}
