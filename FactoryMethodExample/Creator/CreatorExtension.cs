internal class CreatorExtension : IAlias
{
    private readonly Action<string> registerDelegate;

    public CreatorExtension(Action<string> registerDelegate)
    {
        this.registerDelegate = registerDelegate;
    }

    // Extension: RegisterProductType(someType).As("someType alias");
    public void As(string alias)
    {
        registerDelegate(alias);
    }
}