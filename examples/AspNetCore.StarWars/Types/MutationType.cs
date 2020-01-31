using System;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using StarWars.Models;

namespace StarWars.Types
{
    public class MutationType
        : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            var field = descriptor.Field(t => t.CreateReview(default, default, default))
                .Type<NonNullType<ReviewType>>();

            field.Argument("review", a => a.Type<NonNullType<ReviewInputType>>());

            if (this.ContextData["schemaName"] == "schema2")
            {
                field.HiddenArgument("episode");
            }
            else
            {
                field.Argument("episode", a => a.Type<NonNullType<EpisodeType>>());
            }
        }
    }

    public static class HiddenArgumentExtensions
    {
        public static IObjectFieldDescriptor HiddenArgument(this IObjectFieldDescriptor descriptor, NameString argumentName, Action<IArgumentDescriptor> argumentDescriptor = null)
        {
            descriptor.Argument(argumentName, (a) =>
            {
                argumentDescriptor?.Invoke(a);
            }).Extend()
            .OnBeforeCompletion((c, d) =>
            {
                var matching = d.Arguments.Where(x => x.Name == argumentName).ToList();

                foreach (var i in matching)
                {
                    d.Arguments.Remove(i);
                }
            });

            descriptor.Use(n => c =>
            {
                c.OverrideArgument(argumentName, Episode.Jedi);
                return n(c);
            });

            return descriptor;
        }
    }
}
