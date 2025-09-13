using Models.Commands;
using Models.Results;

namespace UseCases;

public interface ICreateAppointmentUseCase
{
    Task<CreateAppointmentResult> ExecuteAsync(CreateAppointmentCommand command, CancellationToken cancellationToken);
}
