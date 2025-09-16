namespace Scheduling.Api.Application.Common;

public enum ErrorType
{
    Validation = 1,
    Business = 2,
    NotFound = 3,
    Conflict = 4,
    Infrastructure = 5,
    Unauthorized = 6,
    Forbidden = 7
}
