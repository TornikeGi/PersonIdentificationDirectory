using Autofac;
using FluentValidation;
using MediatR;

namespace PersonIdentificationDirectory.Utility.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILifetimeScope _scope;

        public ValidationBehavior(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(request.GetType());
            dynamic validator = _scope.ResolveOptional(validatorType);

            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
            }

            return await next();
        }
    }
}
