using System.Globalization;
using AutoMapper;
using GamingHub.GameService.gRPC.V1.Stores;

namespace GamingHub.GameService.gRPC.V1.Mappings;

/// <inheritdoc cref="Profile"/>
public class GameProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameProfile"/> class.
    /// </summary>
    public GameProfile()
    {
        CreateMap<GameStore.GameStoreItem, GameResponse>()
            .ForMember(dest => dest.GameId, opt => opt.MapFrom(src => src.GameId.ToString()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString(CultureInfo.InvariantCulture)));
        CreateMap<CreateGameRequest, GameStore.GameStoreItem>();
    }
}