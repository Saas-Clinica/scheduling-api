using Scheduling.Api.Application.Common;

namespace Scheduling.Api.Application.Helpers;

public static class Errors
{
    public static AppError Validation(string code, string message) =>
        new(code, message, ErrorType.Validation);

    public static AppError Business(string code, string message) =>
        new(code, message, ErrorType.Business);

    public static AppError NotFound(string code, string message) =>
        new(code, message, ErrorType.NotFound);

    public static AppError Conflict(string code, string message) =>
        new(code, message, ErrorType.Conflict);

    public static AppError Infra(string code, string message) =>
        new(code, message, ErrorType.Infrastructure);

    public static AppError Unauthorized(string message = "Unauthorized") =>
        new("auth.unauthorized", message, ErrorType.Unauthorized);

    public static AppError Forbidden(string message = "Forbidden") =>
        new("auth.forbidden", message, ErrorType.Forbidden);
}
