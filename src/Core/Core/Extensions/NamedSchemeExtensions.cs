using HotChocolate.Contracts;

namespace HotChocolate
{
    internal static class NamedSchemeExtensions
    {
        public static INamedSchema WithName(this ISchema schema, string name)
        {
            return new NamedSchema(name, schema);
        }
    }
}
