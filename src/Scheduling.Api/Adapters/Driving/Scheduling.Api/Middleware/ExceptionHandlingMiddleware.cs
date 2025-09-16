using System.Diagnostics;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace Scheduling.Api.Middleware;
public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext ctx, RequestDelegate next)
    {
        try
        {
            await next(ctx);
        }
        catch (OperationCanceledException) when (!ctx.RequestAborted.IsCancellationRequested)
        {
            await WriteProblemAsync(ctx, StatusCodes.Status400BadRequest, "Operation cancelled", detail: null);
        }
        catch (OperationCanceledException)
        {
            await WriteProblemAsync(ctx, 499, "Client Closed Request");
        }
        catch (ValidationException vex)
        {
            var errors = vex.Errors
                .GroupBy(e => e.PropertyName ?? "validation")
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            await WriteProblemAsync(
                ctx,
                StatusCodes.Status422UnprocessableEntity,
                title: "Validation failed",
                detail: "One or more validation errors occurred.",
                extensions: new() { ["errors"] = errors }
            );
        }
        catch (UnauthorizedAccessException uex)
        {
            _logger.LogWarning(uex, "Unauthorized");
            await WriteProblemAsync(ctx, StatusCodes.Status401Unauthorized, "Unauthorized");
        }
        catch (KeyNotFoundException kex)
        {
            _logger.LogInformation(kex, "Not found");
            await WriteProblemAsync(ctx, StatusCodes.Status404NotFound, "Resource not found");
        }
        catch (ConflictException cex) // se tiver uma exceção custom
        {
            _logger.LogInformation(cex, "Conflict");
            await WriteProblemAsync(ctx, StatusCodes.Status409Conflict, "Conflict", cex.Message);
        }
        catch (DomainException dex) // se tiver base de exceção de domínio
        {
            _logger.LogWarning(dex, "Domain error");
            await WriteProblemAsync(ctx, StatusCodes.Status400BadRequest, "Business rule violated", dex.Message,
                extensions: new() { ["code"] = dex.Code });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await WriteProblemAsync(ctx, StatusCodes.Status500InternalServerError, "Unexpected error");
        }
    }

    private static async Task WriteProblemAsync(
        HttpContext ctx,
        int statusCode,
        string title,
        string? detail = null,
        Dictionary<string, object?>? extensions = null)
    {
        if (ctx.Response.HasStarted) return;

        var traceId = Activity.Current?.Id ?? ctx.TraceIdentifier;

        var problem = new ProblemDetails
        {
            Title = title,
            Status = statusCode,
            Detail = detail,
            Instance = ctx.Request.Path,
        };

        problem.Extensions["traceId"] = traceId;
        if (extensions is not null)
        {
            foreach (var kv in extensions)
                problem.Extensions[kv.Key] = kv.Value;
        }

        ctx.Response.Clear();
        ctx.Response.ContentType = "application/problem+json";
        ctx.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(problem, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        });

        await ctx.Response.WriteAsync(json);
    }
}

public class DomainException : Exception
{
    public string Code { get; }
    public DomainException(string code, string message) : base(message) => Code = code;
}
public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}

