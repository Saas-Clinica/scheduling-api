using Scheduling.Api.Domain.Enums;

namespace Scheduling.Api.Dtos.v1.Appointment;

public record CreateAppointmentResponseDto
{
    public Guid Id { get; set; }
    public AppointmentStatusEnum Status { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}
