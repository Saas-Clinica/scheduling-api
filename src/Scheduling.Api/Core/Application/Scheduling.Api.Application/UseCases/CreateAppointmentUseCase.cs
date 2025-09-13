using Enums;
using Models.Commands;
using Models.Results;

namespace UseCases;

public class CreateAppointmentUseCase : ICreateAppointmentUseCase
{
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
