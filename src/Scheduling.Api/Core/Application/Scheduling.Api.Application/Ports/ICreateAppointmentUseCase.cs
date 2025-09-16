using Scheduling.Api.Application.Models.Commands;
using Scheduling.Api.Application.Models.Results;

namespace Scheduling.Api.Application.Ports;

public interface ICreateAppointmentUseCase
{
    Task<CreateAppointmentResult> ExecuteAsync(CreateAppointmentCommand command, CancellationToken cancellationToken);
}
