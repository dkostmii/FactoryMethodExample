const string option1String = "// option 1: default creator";
const string option2String = "// option 2: Register single product";
const string option3String = "// option 3: Register a dictionary of Products to alias in Creator";


// option 1: default creator
Creator defaultCreator = new ConcreteCreator();

Console.WriteLine(option1String);
defaultCreator.CreateProduct("aaaaa").Operation();
defaultCreator.CreateProduct("bbbbb").Operation();

Console.WriteLine();


// option 2: Register single product
Creator singleProductCreator = new ConcreteCreator(typeof(ConcreteProductA), "Product A");

Console.WriteLine(option2String);
singleProductCreator.CreateProduct("Product A").Operation();

Console.WriteLine();


// Will throw a ProductTypeNotFoundException
// singleProductCreator.CreateProduct("Product B").Operation();


// option 3: Register a dictionary of Products to alias in Creator
Creator dictOfProductsCreator = new ConcreteCreator(
    new Dictionary<string, Type>
    {
        { "Product A", typeof(ConcreteProductA) },
        { "Product B", typeof(ConcreteProductB) },
        { "A", typeof(ConcreteProductA) },
        { "B", typeof(ConcreteProductB) },
    }
);

Console.WriteLine(option3String);
dictOfProductsCreator.CreateProduct("Product A").Operation();
dictOfProductsCreator.CreateProduct("Product B").Operation();
dictOfProductsCreator.CreateProduct("A").Operation();
dictOfProductsCreator.CreateProduct("B").Operation();

Console.WriteLine();


// Will throw a ProductTypeNotFoundException
// dictOfProductsCreator.CreateProduct("C").Operation();
