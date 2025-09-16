using Scheduling.Api.Application.Common;

namespace Scheduling.Api.Application.Base;

public abstract class UseCaseBase
{
    protected readonly INotifier Notifier;

    protected UseCaseBase(INotifier notifier)
    {
        Notifier = notifier;
    }
}
