using System.Globalization;
using AutoMapper;
using gRPC.UserService.V1.Stores;

namespace gRPC.UserService.V1.Mappings;

/// <inheritdoc cref="Profile"/>
public class UserProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserProfile"/> class.
    /// </summary>
    public UserProfile()
    {
        CreateMap<UserStore.UserStoreItem, UserResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId.ToString()))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.ToString(CultureInfo.InvariantCulture)));
    }
}