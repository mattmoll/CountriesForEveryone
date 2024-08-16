using Microsoft.Extensions.Primitives;
using System.Net;
using System.Text.RegularExpressions;

namespace CountriesForEveryone.Server.Sanitization
{
    public class InputSanitizationMiddleware
    {
        private readonly RequestDelegate _next;

        public InputSanitizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.HasFormContentType && context.Request.Form != null)
            {
                var sanitizedForm = new FormCollection(context.Request.Form
                    .ToDictionary(
                        f => f.Key,
                        f => new StringValues(f.Value.Select(v => SanitizeInput(v ?? "")).ToArray())));

                context.Request.Form = sanitizedForm;
            }

            await _next(context);
        }

        private string SanitizeInput(string input)
        {
            // HTML Encode to prevent XSS and basic checks to avoid SQL Injection,
            // even though we don't currently use any dynamic query and probably wont ever for this API.
            var encodedInput = WebUtility.HtmlEncode(input);

            string[] sqlKeywords = { "SELECT", "INSERT", "DELETE", "UPDATE", "MERGE", "DROP", "EXECUTE", "--", ";" };
            foreach (var keyword in sqlKeywords)
            {
                encodedInput = Regex.Replace(encodedInput, $"\\b{keyword}\\b", "", RegexOptions.IgnoreCase);
            }

            return encodedInput;
        }
    }
}
