using AutoMapper;
using Scheduling.Api.Application.Models.Commands;
using Scheduling.Api.Application.Models.Results;
using Scheduling.Api.Dtos.v1.Appointment;

namespace Scheduling.Api.Mappers.v1;

public class DtoMapperProfile : Profile
{
    public DtoMapperProfile()
    {
        CreateMap<CreateAppointmentRequestDto, CreateAppointmentCommand>();
        CreateMap<CreateAppointmentResult, CreateAppointmentResponseDto>();
    }
}
