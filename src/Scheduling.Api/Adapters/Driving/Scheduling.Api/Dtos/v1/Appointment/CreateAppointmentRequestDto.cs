namespace Scheduling.Api.Dtos.v1.Appointment;

public record CreateAppointmentRequestDto
{
    public Guid ProfessionalId { get; init; }
    public Guid PatientId { get; init; }
    public Guid ProcedureId { get; init; }
    //public string ProcedureName { get; init; } = string.Empty;
    public DateTime StartsAt { get; init; }
    public DateTime EndsAt { get; init; }
    public string Notes { get; init; } = string.Empty;
}
