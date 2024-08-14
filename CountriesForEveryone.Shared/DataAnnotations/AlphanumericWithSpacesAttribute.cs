using System.ComponentModel.DataAnnotations;

public class AlphanumericWithSpacesAttribute : RegularExpressionAttribute
{
    private static readonly string pattern = @"^[a-zA-Z0-9\s]*$";

    public AlphanumericWithSpacesAttribute()
        : base(pattern)
    {
        ErrorMessage = "Only alphanumeric characters and spaces are allowed.";
    }
}
