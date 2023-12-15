using FluentValidation;
using PersonIdentificationDirectory.Utility.Exceptions;
using System.Net;

namespace PersonIdentificationDirectory.API.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException validationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync($"Validation Error: {validationException.Message}");
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(notFoundException.ErrorCode.ToString());
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
