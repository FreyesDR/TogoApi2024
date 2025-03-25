using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using XDev_UnitWork.DTO;
using XDev_TogoApi.Code;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using XDev_Model.Entities;
using XDev_UnitWork.Interfaces;
using XDev_AvaLinkAIO;
using XDev_RazorTemplate.Views;
using Razor.Templating.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PortalWeb.EndPoints
{
    public static class SecurityEP
    {
        private static readonly EmailAddressAttribute _emailAddressAttribute = new();

        public static IEndpointConventionBuilder MapSecurity<TUser>(this IEndpointRouteBuilder endpoints) where TUser : ApplicationUser, new()
        {
            // We'll figure out a unique endpoint name based on the final route pattern during endpoint generation.
            string? confirmEmailEndpointName = null;

            var routeGroup = endpoints.MapGroup("");

            var timeProvider = endpoints.ServiceProvider.GetRequiredService<TimeProvider>();
            var bearerTokenOptions = endpoints.ServiceProvider.GetRequiredService<IOptionsMonitor<BearerTokenOptions>>();
            //var emailSender = endpoints.ServiceProvider.GetRequiredService<IEmailSender<TUser>>();
            var emailSender = endpoints.ServiceProvider.GetRequiredService<IEmailSenderService>();
            var linkGenerator = endpoints.ServiceProvider.GetRequiredService<LinkGenerator>();

            // Get Token
            routeGroup.MapPost("/token", async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, BadRequest<ExceptionReturnDTO>>>
            ([FromBody] LoginRequest login, [FromServices] IServiceProvider sp) =>
            {
                var signInManager = sp.GetRequiredService<SignInManager<TUser>>();

                //var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
                //var isPersistent = (useCookies == true) && (useSessionCookies != true);
                //signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;
                signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;

                var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: true);

                if (result.RequiresTwoFactor)
                {
                    if (!string.IsNullOrEmpty(login.TwoFactorCode))
                    {
                        result = await signInManager.TwoFactorAuthenticatorSignInAsync(login.TwoFactorCode, false, rememberClient: false);
                    }
                    else if (!string.IsNullOrEmpty(login.TwoFactorRecoveryCode))
                    {
                        result = await signInManager.TwoFactorRecoveryCodeSignInAsync(login.TwoFactorRecoveryCode);
                    }
                }

                

                if (!result.Succeeded)
                {           
                    string rtmsg = "Login failed";

                    if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains("es"))
                        rtmsg = "Error de inicio de sesion";


                    return TypedResults.BadRequest(new ExceptionReturnDTO { Message = rtmsg, StatusCode = StatusCodes.Status401Unauthorized.ToString() });
                }

                // The signInManager already produced the needed response in the form of a cookie or bearer token.
                return TypedResults.Empty;
            }).AddEndpointFilter<ValidationFilter<LoginRequest>>();

            // Refresh Toekn
            routeGroup.MapPost("/refresh", async Task<Results<Ok<AccessTokenResponse>, UnauthorizedHttpResult, SignInHttpResult, ChallengeHttpResult>>
            ([FromBody] RefreshRequest refreshRequest, [FromServices] IServiceProvider sp) =>
            {
                var signInManager = sp.GetRequiredService<SignInManager<TUser>>();
                var refreshTokenProtector = bearerTokenOptions.Get(IdentityConstants.BearerScheme).RefreshTokenProtector;
                var refreshTicket = refreshTokenProtector.Unprotect(refreshRequest.RefreshToken);

                // Reject the /refresh attempt with a 401 if the token expired or the security stamp validation fails
                if (refreshTicket?.Properties?.ExpiresUtc is not { } expiresUtc ||
                    timeProvider.GetUtcNow() >= expiresUtc ||
                    await signInManager.ValidateSecurityStampAsync(refreshTicket.Principal) is not TUser user)

                {
                    return TypedResults.Challenge();
                }

                var newPrincipal = await signInManager.CreateUserPrincipalAsync(user);
                return TypedResults.SignIn(newPrincipal, authenticationScheme: IdentityConstants.BearerScheme);
            });

            // Registro de usuario
            //routeGroup.MapPost("/register", async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>>
            //([FromBody] RegisterDTO registration, HttpContext context, [FromServices] IServiceProvider sp) =>
            //{
            //    var userManager = sp.GetRequiredService<UserManager<TUser>>();

            //    if (!userManager.SupportsUserEmail)
            //    {
            //        throw new NotSupportedException($"{nameof(MapSecurity)} requires a user store with email support.");
            //    }

            //    var userStore = sp.GetRequiredService<IUserStore<TUser>>();
            //    var emailStore = (IUserEmailStore<TUser>)userStore;
            //    var email = registration.Email;

            //    if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
            //    {
            //        return CreateValidationProblem(IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email)));
            //    }

            //    var user = new TUser();
            //    await userStore.SetUserNameAsync(user, email, CancellationToken.None);
            //    await emailStore.SetEmailAsync(user, email, CancellationToken.None);

            //    // Custom fields
            //    user.Name = registration.Name;
                
            //    var result = await userManager.CreateAsync(user, registration.Password);

            //    if (!result.Succeeded)
            //    {
            //        return CreateValidationProblem(result);
            //    }

            //    await SendConfirmationEmailAsync(user, userManager, context, email);
            //    return TypedResults.Ok();
            //}).AddEndpointFilter<ValidationFilter<RegisterDTO>>(); ;
         
            // Confirmación Email
            //routeGroup.MapGet("/confirmEmail", async Task<Results<ContentHttpResult, UnauthorizedHttpResult>>
            //([FromQuery] string userId, [FromQuery] string code, [FromServices] IServiceProvider sp) =>
            //{
            //    var userManager = sp.GetRequiredService<UserManager<TUser>>();
            //    if (await userManager.FindByIdAsync(userId) is not { } user)
            //    {
            //        // We could respond with a 404 instead of a 401 like Identity UI, but that feels like unnecessary information.
            //        return TypedResults.Unauthorized();
            //    }

            //    try
            //    {
            //        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            //    }
            //    catch (FormatException)
            //    {
            //        return TypedResults.Unauthorized();
            //    }

            //    IdentityResult result;

            //    //if (string.IsNullOrEmpty(changedEmail))
            //    //{
            //    result = await userManager.ConfirmEmailAsync(user, code);
            //    //}
            //    //else
            //    //{
            //    //    // As with Identity UI, email and user name are one and the same. So when we update the email,
            //    //    // we need to update the user name.
            //    //    result = await userManager.ChangeEmailAsync(user, changedEmail, code);

            //    //    if (result.Succeeded)
            //    //    {
            //    //        result = await userManager.SetUserNameAsync(user, changedEmail);
            //    //    }
            //    //}

            //    if (!result.Succeeded)
            //    {
            //        return TypedResults.Unauthorized();
            //    }

            //    return TypedResults.Text("Thank you for confirming your email.");
            //}).Add(endpointBuilder =>
            //{
            //    var finalPattern = ((RouteEndpointBuilder)endpointBuilder).RoutePattern.RawText;
            //    confirmEmailEndpointName = $"{nameof(MapSecurity)}-{finalPattern}";
            //    endpointBuilder.Metadata.Add(new EndpointNameMetadata(confirmEmailEndpointName));
            //});

            async Task SendConfirmationEmailAsync(TUser user, UserManager<TUser> userManager, HttpContext context, string email, bool isChange = false)
            {
                if (confirmEmailEndpointName is null)
                {
                    throw new NotSupportedException("No email confirmation endpoint was registered!");
                }

                var code = isChange
                    ? await userManager.GenerateChangeEmailTokenAsync(user, email)
                    : await userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var userId = await userManager.GetUserIdAsync(user);
                var routeValues = new RouteValueDictionary()
                {
                    ["userId"] = userId,
                    ["code"] = code,
                };

                if (isChange)
                {
                    // This is validated by the /confirmEmail endpoint on change.
                    routeValues.Add("changedEmail", email);
                }

                var confirmEmailUrl = linkGenerator.GetUriByName(context, confirmEmailEndpointName, routeValues)
                    ?? throw new NotSupportedException($"Could not find endpoint named '{confirmEmailEndpointName}'.");

                //await emailSender.SendConfirmationLinkAsync(user, email, HtmlEncoder.Default.Encode(confirmEmailUrl));
            }

            // Reenviar confirmación
            //routeGroup.MapPost("/resendConfirmationEmail", async Task<Ok>
            //([FromBody] ResendConfirmationEmailRequest resendRequest, HttpContext context, [FromServices] IServiceProvider sp) =>
            //{
            //    var userManager = sp.GetRequiredService<UserManager<TUser>>();
            //    if (await userManager.FindByEmailAsync(resendRequest.Email) is not { } user)
            //    {
            //        return TypedResults.Ok();
            //    }

            //    await SendConfirmationEmailAsync(user, userManager, context, resendRequest.Email);
            //    return TypedResults.Ok();
            //});

            routeGroup.MapPost("/forgotPassword", async Task<Results<Ok, ValidationProblem>>
                ([FromBody] ForgotPasswordRequest resetRequest, [FromServices] IServiceProvider sp) =>
            {
                var cfg = AIO.GetConfig("WebConfig.enc");

                var userManager = sp.GetRequiredService<UserManager<TUser>>();
                var user = await userManager.FindByEmailAsync(resetRequest.Email);

                if (user is not null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var code = await userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var url = string.Format("{0}/public/resetPassword?email={1}&code={2}", cfg.FrontEndUrl, resetRequest.Email, HtmlEncoder.Default.Encode(code));

                    ForgotPassword dto = new ForgotPassword
                    {
                        Name = user.Name,
                        Url = url,
                    };

                    var body = await RazorTemplateEngine.RenderAsync("~/Views/ForgotPassword.cshtml", dto);

                    await emailSender.SendEmailAsync(user.Email!,"Restablecer Contraseña",body);

                    //await emailSender.SendPasswordResetCodeAsync(user, resetRequest.Email, HtmlEncoder.Default.Encode(code));
                }

                // Don't reveal that the user does not exist or is not confirmed, so don't return a 200 if we would have
                // returned a 400 for an invalid code given a valid user email.
                return TypedResults.Ok();
            });

            routeGroup.MapPost("/resetPassword", async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>>
                ([FromBody] ResetPasswordRequest resetRequest, [FromServices] IServiceProvider sp) =>
            {
                var userManager = sp.GetRequiredService<UserManager<TUser>>();

                var user = await userManager.FindByEmailAsync(resetRequest.Email);

                if (user is null || !(await userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed, so don't return a 200 if we would have
                    // returned a 400 for an invalid code given a valid user email.
                    return CreateValidationProblem(IdentityResult.Failed(userManager.ErrorDescriber.InvalidToken()));
                }

                IdentityResult result;
                try
                {
                    var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetRequest.ResetCode));
                    result = await userManager.ResetPasswordAsync(user, code, resetRequest.NewPassword);
                }
                catch (FormatException)
                {
                    result = IdentityResult.Failed(userManager.ErrorDescriber.InvalidToken());
                }

                if (!result.Succeeded)
                {
                    return CreateValidationProblem(result);
                }

                return TypedResults.Ok();
            });

            return new IdentityEndpointsConventionBuilder(routeGroup);
        }



        private sealed class IdentityEndpointsConventionBuilder(RouteGroupBuilder inner) : IEndpointConventionBuilder
        {
            private IEndpointConventionBuilder InnerAsConventionBuilder => inner;

            public void Add(Action<EndpointBuilder> convention) => InnerAsConventionBuilder.Add(convention);
            public void Finally(Action<EndpointBuilder> finallyConvention) => InnerAsConventionBuilder.Finally(finallyConvention);
        }

        private static BadRequest<ExceptionReturnDTO> CreateValidationProblem(IdentityResult result)
        {
            // We expect a single error code and description in the normal case.
            // This could be golfed with GroupBy and ToDictionary, but perf! :P
            Debug.Assert(!result.Succeeded);
            var errorDictionary = new Dictionary<string, string[]>(1);

            foreach (var error in result.Errors)
            {
                string[] newDescriptions;

                if (errorDictionary.TryGetValue(error.Code, out var descriptions))
                {
                    newDescriptions = new string[descriptions.Length + 1];
                    Array.Copy(descriptions, newDescriptions, descriptions.Length);
                    newDescriptions[descriptions.Length] = error.Description;
                }
                else
                {
                    newDescriptions = [error.Description];
                }

                errorDictionary[error.Code] = newDescriptions;
            }

            string rtmsg = "Request failed";

            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains("es"))
                rtmsg = "Solicitud Incorrecta";

            return TypedResults.BadRequest(new ExceptionReturnDTO { Message = rtmsg, StatusCode = StatusCodes.Status400BadRequest.ToString(),Errors = errorDictionary.Select(k => string.Format("{0}:{1}",k.Key, k.Value.FirstOrDefault())).ToArray() });
        }
    }
}
