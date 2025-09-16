using Microsoft.AspNetCore.Mvc;
using Scheduling.Api.Application.Common;

namespace Scheduling.Api.Presenters;

public static class NotifierPresenter
{
    public static IActionResult ToActionResult(INotifier notifier)
    {
        var errors = notifier.GetAll();

        var status = errors.Any(e => e.Type is ErrorType.Unauthorized) ? StatusCodes.Status401Unauthorized :
                     errors.Any(e => e.Type is ErrorType.Forbidden) ? StatusCodes.Status403Forbidden :
                     errors.Any(e => e.Type is ErrorType.NotFound) ? StatusCodes.Status404NotFound :
                     errors.Any(e => e.Type is ErrorType.Conflict) ? StatusCodes.Status409Conflict :
                     errors.All(e => e.Type is ErrorType.Validation) ? StatusCodes.Status422UnprocessableEntity :
                                                                          StatusCodes.Status400BadRequest;

        var problem = new ProblemDetails
        {
            Title = "Request Failed",
            Status = status,
            Detail = "One or more errors occurred. See extensions['errors'].",
        };

        problem.Extensions["errors"] = errors
            .GroupBy(e => e.Code)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Message).ToArray()
            );

        return new ObjectResult(problem) { StatusCode = status };
    }
}
