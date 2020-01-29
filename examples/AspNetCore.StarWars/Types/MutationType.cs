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
            descriptor.Field(t => t.CreateReview(default, default, default))
                .Type<NonNullType<ReviewType>>()
                //.Ignore
                .Argument("episode", a => a
                    .Type<NonNullType<EpisodeType>>()
                    .Ignore(this.ContextData["schemaName"] == "schema2")
                    )

                .Argument("review", a => a.Type<NonNullType<ReviewInputType>>());
        }
    }
    //public class HiddenDirective
    //: DirectiveType
    //{
    //    protected override void Configure(IDirectiveTypeDescriptor descriptor)
    //    {
    //        descriptor.Use(n => c =>
    //        {
    //            c.re
    //            return n(c);
    //        });
    //    }
    //    protected override void Configure(IDirectiveDescriptor descriptor)
    //    {
    //        descriptor.Middleware<HiddenDirectiverMiddleware>();
    //    }
    //}

    //public class HiddenDirectiverMiddleware
    //    : IResolverMiddleware
    //{
    //    public MyValidatorMiddleware(MyDependency foo)
    //    {

    //    }

    //}

}
