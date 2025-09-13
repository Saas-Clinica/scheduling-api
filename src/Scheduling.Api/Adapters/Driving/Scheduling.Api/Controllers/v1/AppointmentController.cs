using Asp.Versioning;
using Dtos.v1.Appointment;
using Microsoft.AspNetCore.Mvc;
using Models.Commands;
using Swashbuckle.AspNetCore.Annotations;
using UseCases;

namespace Controllers.v1;

[ApiController]
//[ApiVersion("1.0")]
//[Route("api/v{version:apiVersion}/[controller]")]
[Route("api/v1/[controller]")]
[SwaggerTag("Agendamentos de procedimentos")]
public class AppointmentController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateAppointmentResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [SwaggerOperation(
        Summary = "Cria um novo agendamento de procedimento",
        Description = "Cria um novo agendamento de procedimento para um paciente em uma data e hora específicas."
    )]

    public Task<IActionResult> CreateAppointment(
        [FromBody] CreateAppointmentRequestDto request,
        [FromServices] ICreateAppointmentUseCase useCase,
        CancellationToken cancellationToken = default
        )
    {
        //var command = mapper.Map<CreateAppointmentCommand>(request);

        //var result = useCase.ExecuteAsync(command, cancellationToken);

        return Task.FromResult<IActionResult>(Ok());
    }

    //TODO: CRIAR VALIDATOR REQUEST
    //TODO: CRIAR MAPPER (AUTOMAPPER 14.0.0)
    //TODO: ATIVAR VERSION API (configs no program)
    //TODO: CRIAR BASE RETURN ERROR

    //TODO: POST   /api/appointments          -> criar EM_ANDAMENTO
    //TODO: GET    /api/appointments
    //TODO: GET    /api/appointments/{id}     -> detalhe
    //TODO: PUT    /api/appointments/{id}     -> atualizar (ex.: reagendar)
    //TODO: DELETE /api/appointments/{id}     -> cancelar

    //TODO: POST   /api/appointments/{id}/confirm  
    //TODO: POST   /api/appointments/{id}/cancel
    //TODO: POST   /api/appointments/{id}/reschedule
}
