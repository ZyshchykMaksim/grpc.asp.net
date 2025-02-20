using AutoMapper;
using GamingHub.UserService.gRPC.V1.Stores;
using Grpc.Core;

namespace GamingHub.UserService.gRPC.V1.Services;

public class UserService : V1.UserService.UserServiceBase
{
    private readonly UserStore _userStore;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;

    /// <inheritdoc />
    public UserService(
        UserStore userStore,
        IMapper mapper,
        ILogger<UserService> logger)
    {
        _userStore = userStore;
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc />
    public override async Task<UserResponse> GetUserById(GetUserByIdRequest request, ServerCallContext context)
    {
        try
        {
            var users = await _userStore.GetUsersSync();
            var user = users.FirstOrDefault(x => x.UserId == new Guid(request.UserId));

            return await Task.FromResult(_mapper.Map<UserStore.UserStoreItem, UserResponse>(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when getting a user with ID: {UserId}", request.UserId);
            throw;
        }
    }

    /// <inheritdoc />
    public override async Task<UserResponse> GetUserByPhoneNumber(GetUserByPhoneNumberRequest request,
        ServerCallContext context)
    {
        try
        {
            var users = await _userStore.GetUsersSync();
            var user = users.FirstOrDefault(x => x.PhoneNumber.Contains(
                request.PhoneNumber,
                StringComparison.CurrentCultureIgnoreCase));

            return await Task.FromResult(_mapper.Map<UserStore.UserStoreItem, UserResponse>(user));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when getting a user with phone number: {PhoneNumber}", request.PhoneNumber);
            throw;
        }
    }
}