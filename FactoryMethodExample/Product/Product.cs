internal abstract class Product : IProduct
{
    public virtual void Operation()
    {
        Console.WriteLine($"{this.GetType().FullName}'s operation executed.");
    }
}