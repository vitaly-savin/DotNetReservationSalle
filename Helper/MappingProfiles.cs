using AutoMapper;
using backend.Dto;
using backend.Models;

namespace backend.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TypeActivite, TypeActiviteDto>();
            CreateMap<TypeActiviteDto, TypeActivite>();
            CreateMap<SalleLaboratoire, SalleLaboratoireDto>();
            CreateMap<SalleLaboratoireDto, SalleLaboratoire>();
            CreateMap<SalleLaboratoireTypeActivite, SalleLaboratoireTypeActiviteDto>();
            CreateMap<SalleLaboratoireTypeActiviteDto, SalleLaboratoireTypeActivite>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, Reservation>();
            CreateMap<Invitation, InvitationDto>();
            CreateMap<InvitationDto, Invitation>();
        }
    }
}
