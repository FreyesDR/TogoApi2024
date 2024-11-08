using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace XDev_UnitWork.Custom
{
    public class CustomIdentityError : IdentityErrorDescriber
    {
        public override IdentityError ConcurrencyFailure()
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains("es"))
                return new IdentityError
                {
                    Code = nameof(ConcurrencyFailure),
                    Description = "Error de concurrencia optimista, el objeto ha sido modificado por otro usuario."
                };

            return base.ConcurrencyFailure();
        }

        public override IdentityError InvalidUserName(string userName)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains("es"))
                return new IdentityError
                {
                    Code = nameof(InvalidUserName),
                    Description = string.Format("El nombre de usuario '{0}' no es válido, solo puede contener letras o dígitos.", userName)
                };

            return base.InvalidUserName(userName);
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains("es"))
                return new IdentityError
                {
                    Code = nameof(PasswordRequiresNonAlphanumeric),
                    Description = "Las contraseñas deben tener al menos un carácter no alfanumérico."
                };

            return base.PasswordRequiresNonAlphanumeric();
        }

        public override IdentityError PasswordRequiresDigit()
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains("es"))
                return new IdentityError
                {
                    Code = nameof(PasswordRequiresDigit),
                    Description = "Las contraseñas deben tener al menos un dígito ('0'-'9')."
                };

            return base.PasswordRequiresDigit();
        }

        public override IdentityError PasswordRequiresUpper()
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains("es"))
                return new IdentityError
                {
                    Code = nameof(PasswordRequiresUpper),
                    Description = "Las contraseñas deben tener al menos una mayúscula ('A'-'Z')."
                };

            return base.PasswordRequiresUpper();
        }

        public override IdentityError PasswordRequiresLower()
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains("es"))
                return new IdentityError
                {
                    Code = nameof(PasswordRequiresLower),
                    Description = "Las contraseñas deben tener al menos una minúscula ('a'-'z')."
                };

            return base.PasswordRequiresLower();
        }

        public override IdentityError PasswordTooShort(int length)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains("es"))
                return new IdentityError
                {
                    Code = nameof(PasswordTooShort),
                    Description = "Las contraseñas deben tener al menos 6 caracteres."
                };

            return base.PasswordTooShort(length);
        }

        public override IdentityError DuplicateEmail(string email)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains("es"))
                return new IdentityError
                {
                    Code = nameof(DuplicateEmail),
                    Description = $"El correo electrónico '{email}' ya está en uso."
                };

            return base.DuplicateEmail(email);
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains("es"))
                return new IdentityError
                {
                    Code = nameof(DuplicateUserName),
                    Description = $"El nombre de usuario '{userName}' ya está en uso."
                };

            return base.DuplicateUserName(userName);
        }

        public override IdentityError InvalidToken()
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName.Contains("es"))
                return new IdentityError
                {
                    Code = nameof(InvalidToken),
                    Description = "Token incorrecto"
                };
            return base.InvalidToken();
        }
    }
}
