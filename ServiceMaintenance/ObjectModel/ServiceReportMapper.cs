using AutoMapper;
using ServiceMaintenance.Models;

namespace ServiceMaintenance.ObjectModel
{
    public class ServiceReportMapper : Profile
    {
        public ServiceReportMapper()
        {
            CreateMap<ServiceReportData, EditServiceReport>();
              /*  .ForMember(dest => dest.ConfirmEmail,
                           opt => opt.MapFrom(src => src.Email));*/
            CreateMap<EditServiceReport, ServiceReportData>();
        }
    }
}
