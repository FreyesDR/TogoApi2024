using FluentValidation;
using XDev_UnitWork.DTO;

namespace XDev_TogoApi.Code
{
    public class ValidationFilter<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            ExceptionReturnDTO response = new ExceptionReturnDTO();

            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
            if (validator is null)
                return await next(context);

            var dto = context.Arguments.OfType<T>().FirstOrDefault();
            if (dto is null)
            {
                response.Message = "Entidad a validar no existe";
                return TypedResults.BadRequest(response);
            }

            var resultValidator = await validator.ValidateAsync(dto);

            if (!resultValidator.IsValid)
            {
                response.Message = "Error validación";
                foreach (var error in resultValidator.Errors)
                {
                    response.Errors.Add($"[{error.PropertyName}] {error.ErrorMessage}");
                }

                return TypedResults.BadRequest(response);
            }


            return await next(context);
        }
    }
}
