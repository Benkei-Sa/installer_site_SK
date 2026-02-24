using Microsoft.AspNetCore.Identity;

namespace installer_site_SK.Infrastructure;

public class RussianIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() =>
        new() { Code = nameof(DefaultError), Description = "Произошла неизвестная ошибка." };

    public override IdentityError ConcurrencyFailure() =>
        new() { Code = nameof(ConcurrencyFailure), Description = "Данные были изменены. Обновите страницу и попробуйте снова." };

    public override IdentityError DuplicateEmail(string email) =>
        new() { Code = nameof(DuplicateEmail), Description = "Пользователь с такой почтой уже существует." };

    public override IdentityError DuplicateUserName(string userName) =>
        new() { Code = nameof(DuplicateUserName), Description = "Пользователь с таким логином уже существует." };

    public override IdentityError InvalidEmail(string? email) =>
        new() { Code = nameof(InvalidEmail), Description = "Некорректный адрес электронной почты." };

    public override IdentityError InvalidUserName(string? userName) =>
        new() { Code = nameof(InvalidUserName), Description = "Некорректный логин пользователя." };

    public override IdentityError PasswordTooShort(int length) =>
        new() { Code = nameof(PasswordTooShort), Description = $"Пароль должен быть не короче {length} символов." };

    public override IdentityError PasswordRequiresDigit() =>
        new() { Code = nameof(PasswordRequiresDigit), Description = "Пароль должен содержать хотя бы одну цифру (0–9)." };

    public override IdentityError PasswordRequiresLower() =>
        new() { Code = nameof(PasswordRequiresLower), Description = "Пароль должен содержать хотя бы одну строчную букву (a–z)." };

    public override IdentityError PasswordRequiresUpper() =>
        new() { Code = nameof(PasswordRequiresUpper), Description = "Пароль должен содержать хотя бы одну заглавную букву (A–Z)." };

    public override IdentityError PasswordRequiresNonAlphanumeric() =>
        new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Пароль должен содержать хотя бы один спецсимвол." };

    public override IdentityError PasswordMismatch() =>
        new() { Code = nameof(PasswordMismatch), Description = "Неверный текущий пароль." };

    public override IdentityError InvalidToken() =>
        new() { Code = nameof(InvalidToken), Description = "Неверный или просроченный токен." };

    public override IdentityError LoginAlreadyAssociated() =>
        new() { Code = nameof(LoginAlreadyAssociated), Description = "Этот способ входа уже привязан к другому пользователю." };

    public override IdentityError InvalidRoleName(string? role) =>
        new() { Code = nameof(InvalidRoleName), Description = "Некорректное название роли." };

    public override IdentityError DuplicateRoleName(string role) =>
        new() { Code = nameof(DuplicateRoleName), Description = "Такая роль уже существует." };

    public override IdentityError UserAlreadyHasPassword() =>
        new() { Code = nameof(UserAlreadyHasPassword), Description = "У пользователя уже задан пароль." };

    public override IdentityError UserLockoutNotEnabled() =>
        new() { Code = nameof(UserLockoutNotEnabled), Description = "Блокировка для этого пользователя отключена." };

    public override IdentityError UserAlreadyInRole(string role) =>
        new() { Code = nameof(UserAlreadyInRole), Description = "Пользователь уже состоит в этой роли." };

    public override IdentityError UserNotInRole(string role) =>
        new() { Code = nameof(UserNotInRole), Description = "Пользователь не состоит в этой роли." };

    public override IdentityError RecoveryCodeRedemptionFailed() =>
        new() { Code = nameof(RecoveryCodeRedemptionFailed), Description = "Не удалось применить резервный код." };

    // 2FA / подтверждения
    //public override IdentityError InvalidAuthenticatorCode() =>
    //    new() { Code = nameof(InvalidAuthenticatorCode), Description = "Неверный код аутентификатора." };

    //public override IdentityError InvalidTwoFactorToken() =>
    //    new() { Code = nameof(InvalidTwoFactorToken), Description = "Неверный код двухфакторной аутентификации." };

    // Парольная политика (доп. правила)
    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) =>
        new() { Code = nameof(PasswordRequiresUniqueChars), Description = $"Пароль должен содержать минимум {uniqueChars} уникальных символов." };
}