using Newtonsoft.Json;
using JsonSubTypes;
namespace OAS.Jala.API;

public static class JsonSubtypesSerialization
{
    public static JsonConverter SubTypesConverter;
    static JsonSubtypesSerialization()
    {
        SubTypesConverter = JsonSubtypesConverterBuilder
            .Of(typeof(Person), "_type")
            .RegisterSubtype<User>(Descriminator.User)
            .RegisterSubtype<Manager>(Descriminator.Manager).Build();
    }
}