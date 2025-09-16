using Scheduling.Api.Dtos.v1.Appointment;

namespace Scheduling.Api.AppServices.Ports;

public interface IAppointmentAppService
{
    Task<CreateAppointmentResponseDto> CreateAsync(CreateAppointmentRequestDto requestDto, CancellationToken cancellationToken);
}
