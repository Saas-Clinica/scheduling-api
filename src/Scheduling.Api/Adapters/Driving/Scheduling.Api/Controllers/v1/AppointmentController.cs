using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Api.Application.Common;
using Scheduling.Api.AppServices.Ports;
using Scheduling.Api.Dtos.v1.Appointment;
using Scheduling.Api.Presenters;
using Swashbuckle.AspNetCore.Annotations;

namespace Scheduling.Api.Controllers.v1;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[SwaggerTag("Agendamentos de procedimentos")]

public class AppointmentController(IAppointmentAppService service, INotifier notifier) : ControllerBase
{
    #region CREATE APPOINTMENT

    [HttpPost]
    [ProducesResponseType(typeof(CreateAppointmentResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [SwaggerOperation(
        Summary = "Cria um novo agendamento de procedimento",
        Description = "Cria um novo agendamento de procedimento para um paciente em uma data e hora específicas."
    )]

    public async Task<IActionResult> CreateAppointment(
        [FromBody] CreateAppointmentRequestDto request,
        CancellationToken cancellationToken)
    {

        var result = await service.CreateAsync(request, cancellationToken);

        if (notifier.HasErrors)
            return NotifierPresenter.ToActionResult(notifier);

        return Created(string.Empty, result);
    }

    #endregion
    
    //TODO: POST   /api/appointments          -> criar EM_ANDAMENTO
    //TODO: AUTHENTICATE

  
    //TODO: GET    /api/appointments
    //TODO: GET    /api/appointments/{id}     -> detalhe
    //TODO: PUT    /api/appointments/{id}     -> atualizar (ex.: reagendar)
    //TODO: DELETE /api/appointments/{id}     -> cancelar

    //TODO: POST   /api/appointments/{id}/confirm  
    //TODO: POST   /api/appointments/{id}/cancel
    //TODO: POST   /api/appointments/{id}/reschedule
}
