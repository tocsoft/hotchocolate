namespace HotChocolate.Contracts
{
    public interface INamedSchema
    {
        public string Name { get; }
        public ISchema Schema { get; }
    }
}
