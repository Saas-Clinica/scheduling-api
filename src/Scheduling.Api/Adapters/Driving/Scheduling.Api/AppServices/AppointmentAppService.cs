using AutoMapper;
using FluentValidation;
using Scheduling.Api.Application.Common;
using Scheduling.Api.Application.Models.Commands;
using Scheduling.Api.Application.Ports;
using Scheduling.Api.AppServices.Ports;
using Scheduling.Api.Dtos.v1.Appointment;
using Scheduling.Api.Extensions;

namespace Scheduling.Api.AppServices;

public class AppointmentAppService(
        IValidator<CreateAppointmentRequestDto> _validator,
        ICreateAppointmentUseCase _useCase,
        IMapper _mapper,
        INotifier _notifier) : IAppointmentAppService
{


    public async Task<CreateAppointmentResponseDto> CreateAsync(CreateAppointmentRequestDto requestDto, CancellationToken cancellationToken)
    {
        var validation = await _validator.ValidateAsync(requestDto, cancellationToken);

        if (!validation.IsValid)
        {
            _notifier.AddRange(validation.ToErrors());
            return default!;
        }

        var command = _mapper.Map<CreateAppointmentCommand>(requestDto);

        var result = await _useCase.ExecuteAsync(command, cancellationToken);

        return _mapper.Map<CreateAppointmentResponseDto>(result);
    }
}
