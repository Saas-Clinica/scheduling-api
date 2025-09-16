namespace Scheduling.Api.Application.Common;

public interface INotifier
{
    void Add(AppError error);
    void AddRange(IEnumerable<AppError> errors);
    bool HasErrors { get; }
    IReadOnlyList<AppError> GetAll();
    void Clear();
}
