using System;
using System.Collections.Generic;
using System.Text;

namespace HotChocolate.Contracts
{
    public interface INamedSchemaProvider
    {
        ISchema GetSchema(string name);
    }
}
