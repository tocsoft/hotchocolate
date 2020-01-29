using System;
using System.Collections.Generic;
using System.Linq;
using HotChocolate.Contracts;

namespace HotChocolate
{
    public class NamedSchemaProvider : INamedSchemaProvider
    {
        private readonly Func<IEnumerable<INamedSchema>> schemasLoader;

        public NamedSchemaProvider(Func<IEnumerable<INamedSchema>> schemasLoader)
        {
            this.schemasLoader = schemasLoader;
        }

        public ISchema GetSchema(string name)
        {
            return schemasLoader.Invoke().LastOrDefault(x => x.Name == name)?.Schema;
        }
    }
}
