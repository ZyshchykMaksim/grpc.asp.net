using AutoMapper;
using GamingHub.Service.Shared.gRPC;
using GamingHub.Service.Shared.Models;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace GamingHub.UserService.gRPC.V1.Mappings.Converters;

public class GrpcDataConverter<T> :
    ITypeConverter<ResponseDataModel<T>, ResponseDataModel>,
    ITypeConverter<ResponseDataModel, ResponseDataModel<T>> where T : class, IMessage, new()
{
    /// <inheritdoc />
    public ResponseDataModel Convert(ResponseDataModel<T> source, ResponseDataModel destination,
        ResolutionContext context)
    {
        return new ResponseDataModel
        {
            ServerTime = Timestamp.FromDateTime(source.ServerTime.ToUniversalTime()),
            Error = context.Mapper.Map<Service.Shared.gRPC.ResponseErrorModel>(source.Error),
            Data = source.Data is IMessage message
                ? Any.Pack(message)
                : new Any()
        };
    }

    /// <inheritdoc />
    public ResponseDataModel<T> Convert(ResponseDataModel source, ResponseDataModel<T> destination,
        ResolutionContext context)
    {
        return new ResponseDataModel<T>
        {
            ServerTime = source.ServerTime.ToDateTime(),
            Error = context.Mapper.Map<Service.Shared.Models.ResponseErrorModel>(source.Error),
            Data = source.Data.TypeUrl != ""
                ? source.Data.Unpack<T>()
                : null
        };
    }
}