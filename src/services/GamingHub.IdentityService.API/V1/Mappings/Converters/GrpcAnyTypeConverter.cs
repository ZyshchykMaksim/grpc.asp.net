using System.Collections.Concurrent;
using System.Reflection;
using AutoMapper;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Type = System.Type;

namespace GamingHub.IdentityService.API.V1.Mappings.Converters;

public class GrpcAnyTypeConverter : IValueConverter<object?, Any?>, IValueConverter<Any?, object?>
{
    private static readonly ConcurrentDictionary<string, Type> ProtobufTypes = new();
    
    static GrpcAnyTypeConverter()
    {
        var protoMessageTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(IMessage).IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var type in protoMessageTypes)
        {
            var descriptor = ((IMessage)Activator.CreateInstance(type)!).Descriptor;
            ProtobufTypes.TryAdd(descriptor.FullName, type);
        }
    }
    
    public Any? Convert(object? sourceMember, ResolutionContext context)
    {
        return sourceMember switch
        {
            null => new Any(),
            IMessage message => Any.Pack(message),
            _ => throw new InvalidOperationException($"Cannot pack type {sourceMember.GetType()} into Any.")
        };
    }

    public object? Convert(Any? sourceMember, ResolutionContext context)
    {
        if (sourceMember == null || string.IsNullOrWhiteSpace(sourceMember.TypeUrl))
        {
            return null;
        }

        if (ProtobufTypes.TryGetValue(sourceMember.TypeUrl.Split('/').Last(), out var targetType))
        {
            var unpackMethod = typeof(Any).GetMethod(nameof(Any.Unpack))!.MakeGenericMethod(targetType);
            return unpackMethod.Invoke(sourceMember, null);
        }

        throw new InvalidOperationException($"Unknown Any type: {sourceMember.TypeUrl}");
    }
}