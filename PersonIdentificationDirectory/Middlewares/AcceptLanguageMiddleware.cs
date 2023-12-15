using PersonIdentificationDirectory.API.Commons;
using System.Globalization;

namespace PersonIdentificationDirectory.API.Middlewares
{
    public class AcceptLanguageMiddleware
    {
        private readonly RequestDelegate _next;

        public AcceptLanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var language = GetLanguage(context);

            if (!string.IsNullOrEmpty(language))
            {
                CultureInfo.CurrentCulture = new CultureInfo(language);
                CultureInfo.CurrentUICulture = new CultureInfo(language);
            }

            await _next(context);
        }

        public string GetLanguage(HttpContext context)
        {
            var language = context.Request.Headers["Accept-Language"].FirstOrDefault();

            if (language == null)
                return LanguageConstants.En;

            string[] parts = language.Split(',');

            if (parts.Length > 0)
                return parts[0].Trim();

            return language;
        }
    }
}
