public abstract class Creator
{
    private readonly IDictionary<string, Type> productTypes;
    protected delegate IProduct ProductCreator();

    protected Creator()
    {
        productTypes = new Dictionary<string, Type>();
    }

    public IProduct CreateProduct(Type productType)
    {
        if (String.IsNullOrEmpty(productType.FullName))
        {
            throw new ArgumentException("Product type name cannot be null or empty.");
        }

        if (!IsRegistered(productType))
        {
            throw new ProductTypeNotFoundException($"Product type {productType.FullName} is not registered in this creator.");
        }

        return GetCreator(productType.FullName!)();
    }

    public virtual IProduct CreateProduct(string alias)
    {
        return GetCreator(alias)();
    }

    protected ProductCreator GetCreator(string alias)
    {
        if (!IsRegistered(alias))
        {
            throw new ProductTypeNotFoundException($"No product type registered under the alias: {alias}");
        }

        Type? t = productTypes[alias];

        ArgumentNullException.ThrowIfNull(t);

        Type productType = t;

        // Creator delegate
        return new ProductCreator(() =>
        {
            object? o = Activator.CreateInstance(productType);
            ArgumentNullException.ThrowIfNull(o);

            return (IProduct)o;
        });
    }

    private bool IsRegistered(string alias)
    {
        return productTypes.ContainsKey(alias);
    }

    private bool IsRegistered(string alias, Type type)
    {
        bool aliasRegistered = IsRegistered(alias);
        Type? t = aliasRegistered ? productTypes[alias] : null;

        return aliasRegistered && type.Equals(t);
    }

    private bool IsRegistered(Type type)
    {
        if (String.IsNullOrEmpty(type.FullName))
        {
            throw new ArgumentException("Product type name cannot be null or empty.");
        }

        return productTypes.ContainsKey(type.FullName);
    }

    // Register Product type with alias
    private IAlias RegisterProductType(Type type, string alias)
    {
        if (String.IsNullOrEmpty(type.FullName))
        {
            throw new ProductTypeRegisterException($"Product type name cannot be null or empty.");
        }

        if (String.IsNullOrEmpty(alias))
        {
            throw new ProductTypeRegisterException($"Alias for the product type {type.FullName} cannot be empty.");
        }

        if (IsRegistered(alias) && !IsRegistered(alias, type))
        {
            throw new ProductTypeRegisterException($"Alias \"{alias}\" are already used for another product type.");
        }

        if (!IsRegistered(alias, type))
        {
            productTypes.Add(alias, type);
        }

        return new CreatorExtension(
            (alias) => RegisterProductType(type, alias)
        );
    }

    // Just register Product Type as its own type.FullName
    protected IAlias RegisterProductType(Type type)
    {
        if (String.IsNullOrEmpty(type.FullName))
        {
            throw new ProductTypeRegisterException($"Product type name cannot be null or empty.");
        }

        return RegisterProductType(type, type.FullName);
    }

    public override string ToString()
    {
        return productTypes
            .Select(kv => $"{kv.Value.FullName} as \"{kv.Key}\"")
            .Aggregate("Creator for product types:\n", (acc, val) => $"{acc}{val}\n");
    }
}
