namespace Scheduling.Api.Application.Common;

public sealed class Notifier : INotifier
{
    private readonly List<AppError> _errors = new();

    public void Add(AppError error) => _errors.Add(error);

    public void AddRange(IEnumerable<AppError> errors) => _errors.AddRange(errors);

    public bool HasErrors => _errors.Count > 0;

    public IReadOnlyList<AppError> GetAll() => _errors;

    public void Clear() => _errors.Clear();
}
