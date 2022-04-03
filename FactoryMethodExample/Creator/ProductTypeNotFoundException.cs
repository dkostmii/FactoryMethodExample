internal class ProductTypeNotFoundException : Exception
{
    public ProductTypeNotFoundException() : base() { }
    public ProductTypeNotFoundException(string message) : base(message) { }
}