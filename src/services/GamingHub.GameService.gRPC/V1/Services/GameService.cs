using AutoMapper;
using GamingHub.GameService.gRPC.V1.Stores;
using Grpc.Core;

namespace GamingHub.GameService.gRPC.V1.Services;

public class GameService : V1.GameService.GameServiceBase
{
    private readonly GameStore _gameStore;
    private readonly IMapper _mapper;
    private readonly ILogger<GameService> _logger;

    /// <inheritdoc />
    public GameService(
        GameStore gameStore,
        IMapper mapper,
        ILogger<GameService> logger)
    {
        _gameStore = gameStore;
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc />
    public override async Task<GamesResponse> GetGames(GetGamesRequest request, ServerCallContext context)
    {
        try
        {
            var games = await _gameStore.GetGamesSync();
            var gamesMap = _mapper.Map<IList<GameStore.GameStoreItem>, IList<GameResponse>>(games);
            return await Task.FromResult(new GamesResponse()
            {
                Items = { gamesMap }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error when getting games.");
            throw;
        }
    }

    /// <inheritdoc />
    public override async Task<GameResponse> GetGameById(GetGameByIdRequest request, ServerCallContext context)
    {
        try
        {
            var games = await _gameStore.GetGamesSync();
            var game = games.FirstOrDefault(x => x.GameId == new Guid(request.GameId));

            return await Task.FromResult(_mapper.Map<GameStore.GameStoreItem, GameResponse>(game));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error when getting a game with ID: {request.GameId}");
            throw;
        }
    }

    /// <inheritdoc />
    public override async Task<GameResponse> CreateGame(CreateGameRequest request, ServerCallContext context)
    {
        try
        {
            var newGame = _mapper.Map<CreateGameRequest, GameStore.GameStoreItem>(
                request,
                opt => opt.AfterMap((_, dest) =>
                {
                    dest.GameId = Guid.NewGuid();
                    dest.CreatedAt = DateTime.UtcNow;
                    dest.Status = GameStore.GameStatus.Active;
                }));

            await _gameStore.AddGameAsync(newGame);

            return await Task.FromResult(_mapper.Map<GameStore.GameStoreItem, GameResponse>(newGame));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when creating a new game.");
            throw;
        }
    }

    /// <inheritdoc />
    public override async Task<GameResponse> UpdateGame(UpdateGameRequest request, ServerCallContext context)
    {
        try
        {
            var games = await _gameStore.GetGamesSync();
            var game = games.FirstOrDefault(x => x.GameId == new Guid(request.GameId));

            if (game == null)
            {
                return null;
            }

            game.Name = request.Name;
            game.Description = request.Description;

            return await Task.FromResult(_mapper.Map<GameStore.GameStoreItem, GameResponse>(game));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error when deleting a game with ID: {request.GameId}");
            throw;
        }
    }

    /// <inheritdoc />
    public override async Task<GameResponse> DeleteGame(DeleteGameRequest request, ServerCallContext context)
    {
        try
        {
            var games = await _gameStore.GetGamesSync();
            var game = games.FirstOrDefault(x => x.GameId == new Guid(request.GameId));

            if (game == null)
            {
                return null;
            }

            game.Status = GameStore.GameStatus.Deleted;

            return await Task.FromResult(_mapper.Map<GameStore.GameStoreItem, GameResponse>(game));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error when updating a game with ID: {request.GameId}");
            throw;
        }
    }
}