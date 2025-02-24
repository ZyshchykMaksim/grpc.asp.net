using AutoMapper;
using GamingHub.Service.Shared.gRPC;
using GamingHub.Service.Shared.Models;
using GamingHub.UserService.gRPC.V1.Mappings.Converters;

namespace GamingHub.UserService.gRPC.V1.Mappings;

/// <summary>
/// Profile with <see cref="CommonProfile"/> automapper mappings.
/// </summary>
public class CommonProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommonProfile"/> class.
    /// </summary>
    public CommonProfile()
    {
        CreateMap<string, DateTime>().ConvertUsing(s => DateTime.Parse(s));
        CreateMap<string, DateTime?>().ConvertUsing(s => s != null ? DateTime.Parse(s) : null);

        CreateMap<Service.Shared.Models.ResponseErrorModel, Service.Shared.gRPC.ResponseErrorModel>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.ErrorCode))
            .ForMember(dest => dest.Details, opt => opt.ConvertUsing(new GrpcAnyTypeConverter()!, src => src.Details));

        CreateMap<Service.Shared.gRPC.ResponseErrorModel, Service.Shared.Models.ResponseErrorModel>()
            .ForMember(dest => dest.ErrorCode, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Details, opt => opt.ConvertUsing(new GrpcAnyTypeConverter(), src => src.Details));
        
        CreateMap(typeof(ResponseDataModel<>), typeof(ResponseDataModel)).ConvertUsing(typeof(GrpcDataConverter<>));
        CreateMap(typeof(ResponseDataModel), typeof(ResponseDataModel<>)).ConvertUsing(typeof(GrpcDataConverter<>));
    }
}