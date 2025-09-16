using FluentValidation;
using Scheduling.Api.Dtos.v1.Appointment;

namespace Scheduling.Api.Validators.v1.Appointment;

public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentRequestDto>
{
    public CreateAppointmentValidator()
    {
        RuleFor(x => x.ProcedureId)
            .NotEmpty()
            .NotNull()
            .WithMessage("deve ser informado");

        RuleFor(x => x.PatientId)
            .NotEmpty()
            .NotNull()
            .WithMessage("deve ser informado");

        RuleFor(x => x.StartsAt)
            .NotEmpty()
            .NotNull()
            .WithMessage("deve ser informado");

        When(x => x.StartsAt != DateTime.MinValue, () =>
        {
            RuleFor(x => x.StartsAt)
                .Must(date => date > DateTime.UtcNow)
                .WithMessage("deve ser uma data futura");
        });

        RuleFor(x => x.EndsAt)
            .NotEmpty()
            .NotNull()
            .WithMessage("deve ser informado");

        When(x => x.EndsAt != DateTime.MinValue, () =>
        {
            RuleFor(x => x.EndsAt)
            .Must(date => date > DateTime.MinValue)
            .WithMessage("deve ser uma data futura");

            RuleFor(x => x.EndsAt)
            .GreaterThan(x => x.StartsAt)
            .WithMessage("deve ser maior que StartsAt");
        });

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .WithMessage("deve ter no máximo 500 caracteres");
    }
}
