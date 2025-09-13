using Enums;

namespace Models.Results;

public record CreateAppointmentResult
{
    public Guid Id { get; set; }
    public AppointmentStatusEnum Status { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}
