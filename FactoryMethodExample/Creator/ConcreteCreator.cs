internal class ConcreteCreator : Creator
{
    // option 1: default creator
    public ConcreteCreator() : base()
    {
        RegisterProductType(typeof(ConcreteProductA)).As("aaaaa");
        RegisterProductType(typeof(ConcreteProductB)).As("bbbbb");
    }

    // option 2: Register single product
    public ConcreteCreator(Type type, string alias) : base()
    {
        RegisterProductType(type).As(alias);
    }

    // option 3: Register a dictionary of Products to alias in Creator
    public ConcreteCreator(IDictionary<string, Type> productTypes) : base()
    {
        if (!productTypes.Any())
        {
            throw new ArgumentException("Dictionary of Product types to alias cannot be empty.");
        }

        productTypes.ToList().ForEach(kv => RegisterProductType(kv.Value).As(kv.Key));
    }

    /* Can be overriden
    public override IProduct CreateProduct(string alias)
    {
        ProductCreator creator = GetCreator(alias);
        
        // some more logical stuff

        return creator();
    }
    */
}
