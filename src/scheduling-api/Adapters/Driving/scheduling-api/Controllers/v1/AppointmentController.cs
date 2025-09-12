using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Agendamentos de procedimentos")]
public class AppointmentController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [SwaggerOperation(
        Summary = "Cria um novo agendamento de procedimento",
        Description = "Cria um novo agendamento de procedimento para um paciente em uma data e hora específicas."
    )]

    public Task<IActionResult> CreateAppointment(
        
        )
    {
         return Task.FromResult<IActionResult>(Ok());
    }


    //TODO: POST   /api/appointments          -> criar
    //TODO: GET    /api/appointments
    //TODO: GET    /api/appointments/{id}     -> detalhe
    //TODO: PUT    /api/appointments/{id}     -> atualizar (ex.: reagendar)
    //TODO: DELETE /api/appointments/{id}     -> cancelar

    //TODO: POST   /api/appointments/{id}/confirm  
    //TODO: POST   /api/appointments/{id}/cancel
    //TODO: POST   /api/appointments/{id}/reschedule
}
