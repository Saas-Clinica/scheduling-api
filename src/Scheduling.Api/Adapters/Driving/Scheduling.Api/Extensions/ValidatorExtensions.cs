using FluentValidation.Results;
using Scheduling.Api.Application.Common;
using Scheduling.Api.Application.Helpers;

namespace Scheduling.Api.Extensions;

public static class ValidatorExtensions
{
    public static IEnumerable<AppError> ToErrors(this ValidationResult result) =>
    result.Errors.Select(e => Errors.Validation(
            code: string.IsNullOrWhiteSpace(e.PropertyName) ? "validation" : $"validation.{e.PropertyName}",
            message: e.ErrorMessage
        ));
}
