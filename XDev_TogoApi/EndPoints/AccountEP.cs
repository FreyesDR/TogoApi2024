using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class AccountEP
    {
        public static RouteGroupBuilder MapAccount(this RouteGroupBuilder builder)
        {
            builder.MapPost("/changepassword", ChangePassword);
            builder.MapGet("/", GetUser);
            return builder;
        }

        private static async Task<Results<Ok<UserInfoDTO>, BadRequest<ExceptionReturnDTO>>> GetUser(IHttpContextAccessor contextAccessor, IAccountBL accountBL)
        {
            return TypedResults.Ok(await accountBL.UserInfo());
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> ChangePassword(ChangePasswordDTO dto, IAccountBL accountBL)
        {
            var result = await accountBL.ChangePassword(dto);

            if (!result.Succeeded)
            {
                var returnex = new ExceptionReturnDTO();

                returnex.Errors = result.Errors.Select(x => x.Description).ToList();

                return TypedResults.BadRequest(returnex);
            }

            return TypedResults.Ok();
        }
    }
}
