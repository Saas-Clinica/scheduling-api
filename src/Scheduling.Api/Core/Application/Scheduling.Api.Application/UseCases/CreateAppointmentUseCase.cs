using Scheduling.Api.Application.Base;
using Scheduling.Api.Application.Common;
using Scheduling.Api.Application.Models.Commands;
using Scheduling.Api.Application.Models.Results;
using Scheduling.Api.Application.Ports;
using Scheduling.Api.Domain.Enums;

namespace Scheduling.Api.Application.UseCases;

public class CreateAppointmentUseCase : UseCaseBase, ICreateAppointmentUseCase
{
    public CreateAppointmentUseCase(
        INotifier notifier) : base(notifier)
    {
        
    }
    public Task<CreateAppointmentResult> ExecuteAsync(CreateAppointmentCommand command, CancellationToken cancellationToken)
    {
        return Task.FromResult(new CreateAppointmentResult
        {
            Id = Guid.NewGuid(),
            Status = AppointmentStatusEnum.PENDING,
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow
        });
    }
}
