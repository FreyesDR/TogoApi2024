using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using SendGrid.Helpers.Mail;
using System.Net;

namespace XDev_UnitWork.Services
{
    public class DynamicAuthorizationRequirement: IAuthorizationRequirement
    {
    }

    public class DynamicAuthorizationHandler : AuthorizationHandler<DynamicAuthorizationRequirement>
    {
        private readonly IEndPointPolicyService endPointPolicy;
        private readonly IUserPolicyService userPolicy;

        public DynamicAuthorizationHandler(IEndPointPolicyService endPointPolicy, IUserPolicyService userPolicy)
        {
            this.endPointPolicy = endPointPolicy;
            this.userPolicy = userPolicy;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DynamicAuthorizationRequirement requirement)
        {
            var httpContext = context.Resource as HttpContext;
            var endpoint = httpContext.GetEndpoint();
            var metodoHttp = httpContext.Request.Method;
            var ruta = (endpoint as RouteEndpoint).RoutePattern.RawText; // httpContext.Request.Path.Value; //endpoint.Metadata.GetMetadata<Route>().RouteTemplate;

            if (metodoHttp == null)
            {
                context.Fail();
                return;
            }

            if (!httpContext.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            // Obtener política requerida para el endpoint
            var politicaRequerida = await endPointPolicy.GetPolicyEndpointAsync(metodoHttp, ruta);

            if (politicaRequerida == null)
            {
                context.Succeed(requirement); // Si no hay política requerida, permitir acceso
                return;
            }

            // Verificar si el usuario tiene la política
            var userHasPolicy = await userPolicy.GetUserPolicyAsync(httpContext.User.Identity.Name, politicaRequerida);

            if (userHasPolicy)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
