namespace Scheduling.Api.Application.Common;

public sealed record AppError(string Code, string Message, ErrorType Type);
