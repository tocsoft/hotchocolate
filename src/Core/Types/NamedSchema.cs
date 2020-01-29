using System;
using System.Collections.Generic;
using System.Text;
using HotChocolate.Contracts;

namespace HotChocolate
{
    public class NamedSchema : INamedSchema
    {
        public NamedSchema(string name, ISchema schema)
        {
            Name = name;
            Schema = schema;
        }

        public string Name { get; }

        public ISchema Schema { get; }
    }
}
