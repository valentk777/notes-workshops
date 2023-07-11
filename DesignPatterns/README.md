# Workshop – DesignPatterns
* Duration: X days
* Level: Medium
* Language: English
* Speaker: [Valentinas Kaminskas](https://github.com/valentk777)

# Introduction
During this workshop we will focus to the Gangs of Four (GoF) Design Patterns. We use C# for code parts.

GoF Design Patterns are divided into three categories:
1. Creational: The design patterns that deal with the creation of an object.
2. Structural: The design patterns in this category deals with the class structure such as Inheritance and Composition
3. Behavioral: This type of design patterns provide solution for the better interaction between objects, how to provide lose coupling, and flexibility to extend easily in future.

There are 5 design patterns in the creational design patterns category.
| Pattern Name     | Description |
|------------------|:-------------------------------------------------------------------------------------------------|
| [Singleton](#singleton-pattern)        | The singleton pattern restricts the initialization of a class to ensure that only one instance of the class can be created. |
| [Factory](#factory-pattern)          | The factory pattern takes out the responsibility of instantiating a object from the class to a Factory class. |
| [Abstract Factory](#abstract-factory-pattern) | Provide an interface for creating families of related or dependent objects without specifying their concrete classes. Allows us to create a Factory for factory classes. |
| [Builder](#builder-pattern)          | Creating an object step by step and a method to finally get the object instance. |
| [Prototype](#prototype-pattern)        | Creating a new object instance from another similar instance and then modify according to our requirements. |


## Structural Design Patterns
There are 7 structural design patterns defined in the Gangs of Four design patterns book.
| Pattern Name | Description |
|--------------|:-------------------------------------------------------------------------------------------------|
| [Adapter](#adapter-pattern)      | Provides an interface between two unrelated entities so that they can work together. |
| [Composite](#composite-pattern)    | Used when we have to implement a part-whole hierarchy. For example, a diagram made of other pieces such as circle, square, triangle, etc. |
| [Proxy](#proxy-pattern)        | Provide a surrogate or placeholder for another object to control access to it. |
| [Flyweight](#flyweight-pattern)    | Caching and reusing object instances, used with immutable objects. For example, string pool. |
| [Facade](#facade-pattern)       | Creating a wrapper interfaces on top of existing interfaces to help client applications. |
| [Bridge](#bridge-pattern)       | The bridge design pattern is used to decouple the interfaces from implementation and hiding the implementation details from the client program. |
| [Decorator](#decorator-pattern)    | The decorator design pattern is used to modify the functionality of an object at runtime. |

## Behavioral Design Patterns
There are 11 behavioral design patterns defined in the GoF design patterns.
| Pattern Name            | Description |
|-------------------------|:-------------------------------------------------------------------------------------------------|
| [Template Method](#template-method-pattern)         | used to create a template method stub and defer some of the steps of implementation to the subclasses. |
| [Mediator](#mediator-pattern)                | used to provide a centralized communication medium between different objects in a system. |
| [Chain of Responsibility](#chain-of-responsibility-pattern) | used to achieve loose coupling in software design where a request from the client is passed to a chain of objects to process them. |
| [Observer](#observer-pattern)                | useful when you are interested in the state of an object and want to get notified whenever there is any change. |
| [Strategy](#strategy-pattern)                | Strategy pattern is used when we have multiple algorithm for a specific task and client decides the actual implementation to be used at runtime. |
| [Command](#command-pattern)                 | Command Pattern is used to implement lose coupling in a request-response model. |
| [State](#state-pattern)                   | State design pattern is used when an Object change it’s behavior based on it’s internal state. |
| [Visitor](#visitor-pattern)                 | Visitor pattern is used when we have to perform an operation on a group of similar kind of Objects. |
| [Interpreter](#interpreter-pattern)             | defines a grammatical representation for a language and provides an interpreter to deal with this grammar. |
| [Iterator](#iterator-pattern)                | used to provide a standard way to traverse through a group of Objects. |
| [Memento](#memento-pattern)                 | The memento design pattern is used when we want to save the state of an object so that we can restore later on. |




# Day 1

## Preparation
* Dotnet or Python env.

## Workshop notes
* General review of design patterns

## Creational Design Patterns
There are 5 design patterns in the creational design patterns category.
| Pattern Name     | Description |
|------------------|:-------------------------------------------------------------------------------------------------|
| [Singleton](#singleton-pattern)        | The singleton pattern restricts the initialization of a class to ensure that only one instance of the class can be created. |
| [Factory](#factory-pattern)          | The factory pattern takes out the responsibility of instantiating a object from the class to a Factory class. |
| [Abstract Factory](#abstract-factory-pattern) | Provide an interface for creating families of related or dependent objects without specifying their concrete classes. Allows us to create a Factory for factory classes. |
| [Builder](#builder-pattern)          | Creating an object step by step and a method to finally get the object instance. |
| [Prototype](#prototype-pattern)        | Create a new object instance from another similar instance and then modify according to our requirements. |

## Review them all in detail

### Singleton Pattern
   - Confidence: 4/5 [Remove]
   - LearningOrder: 1
   - Comment: Ensures that only one instance of a class exists throughout the application.
   - Best Use Case: When you need to have a single, shared instance of a class.
   - Antipattern: Overuse of the singleton pattern can lead to tight coupling and make testing and maintenance difficult.
   
   C#
   ```csharp
   public class Singleton
   {
       private static Singleton instance;
   
       private Singleton() { }
   
       public static Singleton Instance
       {
           get
           {
               if (instance == null)
                   instance = new Singleton();
   
               return instance;
           }
       }
   }
   ```

   Python
    
   ```python
    class Singleton:
        _instance = None

        def __new__(cls, *args, **kwargs):
            if not cls._instance:
                cls._instance = super().__new__(cls, *args, **kwargs)
            return cls._instance
   ```

### Factory Pattern
   - Confidence: 3/5 [Remove]
   - LearningOrder: 2
   - Comment: Encapsulates object creation logic and provides a centralized factory class to create objects.
   - Best Use Case: When you want to delegate the responsibility of object creation to a separate class.
   - Antipattern: When the factory class becomes too complex or violates the Single Responsibility Principle, it's called a "God object."

   C#
   ```csharp
   public abstract class Product
   {
       public abstract void Use();
   }
   
   public class ConcreteProductA : Product
   {
       public override void Use()
       {
           Console.WriteLine("Using ConcreteProductA");
       }
   }
   
   public class ConcreteProductB : Product
   {
       public override void Use()
       {
           Console.WriteLine("Using ConcreteProductB");
       }
   }
   
   public class ProductFactory
   {
       public Product CreateProduct(string type)
       {
           switch (type)
           {
               case "A":
                   return new ConcreteProductA();
               case "B":
                   return new ConcreteProductB();
               default:
                   throw new ArgumentException("Invalid product type.");
           }
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class Product(ABC):
        @abstractmethod
        def use(self):
            pass

    class ConcreteProductA(Product):
        def use(self):
            print("Using ConcreteProductA")

    class ConcreteProductB(Product):
        def use(self):
            print("Using ConcreteProductB")

    class Creator(ABC):
        @abstractmethod
        def create_product(self) -> Product:
            pass

        def use_product(self):
            product = self.create_product()
            product.use()

    class ConcreteCreatorA(Creator):
        def create_product(self) -> Product:
            return ConcreteProductA()

    class ConcreteCreatorB(Creator):
        def create_product(self) -> Product:
            return ConcreteProductB()
   ```

### Abstract Factory Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 21
   - Comment: Provides an interface for creating families of related or dependent objects without specifying their concrete classes.
   - Best Use Case: When you need to create families of objects that are designed to work together and ensure their compatibility.
   - Antipattern: Creating a bloated abstract factory interface with too many methods or incorporating unrelated product families.

   C#
   ```csharp
   public interface IProductA
   {
       void UseProductA();
   }
   
   public interface IProductB
   {
       void UseProductB();
   }
   
   public interface IAbstractFactory
   {
       IProductA CreateProductA();
       IProductB CreateProductB();
   }
   
   public class ConcreteProductA1 : IProductA
   {
       public void UseProductA()
       {
           Console.WriteLine("Using ConcreteProductA1");
       }
   }
   
   public class ConcreteProductA2 : IProductA
   {
       public void UseProductA()
       {
           Console.WriteLine("Using ConcreteProductA2");
       }
   }
   
   public class ConcreteProductB1 : IProductB
   {
       public void UseProductB()
       {
           Console.WriteLine("Using ConcreteProductB1");
       }
   }
   
   public class ConcreteProductB2 : IProductB
   {
       public void UseProductB()
       {
           Console.WriteLine("Using ConcreteProductB2");
       }
   }
   
   public class ConcreteFactory1 : IAbstractFactory
   {
       public IProductA CreateProductA()
       {
           return new ConcreteProductA1();
       }
   
       public IProductB CreateProductB()
       {
           return new ConcreteProductB1();
       }
   }
   
   public class ConcreteFactory2 : IAbstractFactory
   {
       public IProductA CreateProductA()
       {
           return new ConcreteProductA2();
       }
   
       public IProductB CreateProductB()
       {
           return new ConcreteProductB2();
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class AbstractProductA(ABC):
        @abstractmethod
        def use(self):
            pass

    class AbstractProductB(ABC):
        @abstractmethod
        def interact(self, productA):
            pass

    class ConcreteProductA1(AbstractProductA):
        def use(self):
            print("Using ConcreteProductA1")

    class ConcreteProductA2(AbstractProductA):
        def use(self):
            print("Using ConcreteProductA2")

    class ConcreteProductB1(AbstractProductB):
        def interact(self, productA):
            print("Interacting with ConcreteProductB1 and", end=" ")
            productA.use()

    class ConcreteProductB2(AbstractProductB):
        def interact(self, productA):
            print("Interacting with ConcreteProductB2 and", end=" ")
            productA.use()

    class AbstractFactory(ABC):
        @abstractmethod
        def create_productA(self) -> AbstractProductA:
            pass

        @abstractmethod
        def create_productB(self) -> AbstractProductB:
            pass

    class ConcreteFactory1(AbstractFactory):
        def create_productA(self) -> AbstractProductA:
            return ConcreteProductA1()

        def create_productB(self) -> AbstractProductB:
            return ConcreteProductB1()

    class ConcreteFactory2(AbstractFactory):
        def create_productA(self) -> AbstractProductA:
            return ConcreteProductA2()

        def create_productB(self) -> AbstractProductB:
            return ConcreteProductB2()

   ```

### Builder Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 12
   - Comment: Separates the construction of a complex object from its representation, allowing the same construction process to create different representations.
   - Best Use Case: When you want to create complex objects step by step or when the object creation process involves several optional parameters.
   - Antipattern: Violating the Single Responsibility Principle by putting too much logic into the builder class.

   C#
   ```csharp
   public class Product
   {
       public string PartA { get; set; }
       public string PartB { get; set; }
       public string PartC { get; set; }
   }
   
   public abstract class Builder
   {
       public abstract void BuildPartA();
       public abstract void BuildPartB();
       public abstract void BuildPartC();
       public abstract Product GetProduct();
   }
   
   public class ConcreteBuilder : Builder
   {
       private Product product = new Product();
   
       public override void BuildPartA()
       {
           product.PartA = "Part A";
       }
   
       public override void BuildPartB()
       {
           product.PartB = "Part B";
       }
   
       public override void BuildPartC()
       {
           product.PartC = "Part C";
       }
   
       public override Product GetProduct()
       {
           return product;
       }
   }
   
   public class Director
   {
       public void Construct(Builder builder)
       {
           builder.BuildPartA();
           builder.BuildPartB();
           builder.BuildPartC();
       }
   }
   ```

   Python
    
   ```python
    class Product:
        def __init__(self):
            self.partA = None
            self.partB = None
            self.partC = None

    class Builder(ABC):
        @abstractmethod
        def build_partA(self):
            pass

        @abstractmethod
        def build_partB(self):
            pass

        @abstractmethod
        def build_partC(self):
            pass

        @abstractmethod
        def get_product(self) -> Product:
            pass

    class ConcreteBuilder(Builder):
        def __init__(self):
            self.product = Product()

        def build_partA(self):
            self.product.partA = "Part A"

        def build_partB(self):
            self.product.partB = "Part B"

        def build_partC(self):
            self.product.partC = "Part C"

        def get_product(self) -> Product:
            return self.product

    class Director:
        def construct(self, builder: Builder):
            builder.build_partA()
            builder.build_partB()
            builder.build_partC()

   ```

### Prototype Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 22
   - Comment: Creates new objects by cloning existing objects, allowing you to create new instances without knowing their specific classes.
   - Best Use Case: When creating new instances is costly or complex, and you want to avoid the overhead of initializing objects from scratch.
   - Antipattern: Modifying the cloned object directly without following the proper cloning process can lead to unexpected behavior.

   C#
   ```csharp
   public abstract class Prototype
   {
       public abstract Prototype Clone();
   }
   
   public class ConcretePrototype : Prototype
   {
       private string data;
   
       public ConcretePrototype(string data)
       {
           this.data = data;
       }
   
       public override Prototype Clone()
       {
           return new ConcretePrototype(data);
       }
   }
   
   public class Client
   {
       public Prototype Operation(Prototype prototype)
       {
           return prototype.Clone();
       }
   }
   ```

   Python
    
   ```python
    from copy import deepcopy

    class Prototype(ABC):
        @abstractmethod
        def clone(self):
            pass

    class ConcretePrototype(Prototype):
        def __init__(self, data):
            self.data = data

        def clone(self):
            return deepcopy(self)

    class Client:
        def operation(self, prototype: Prototype):
            cloned_prototype = prototype.clone()
            # Use the cloned_prototype
   ```


## Structural Design Patterns
There are 7 structural design patterns defined in the Gangs of Four design patterns book.
| Pattern Name | Description |
|--------------|:-------------------------------------------------------------------------------------------------|
| [Adapter](#adapter-pattern)      | Provides an interface between two unrelated entities so that they can work together. |
| [Composite](#composite-pattern)    | Used when we have to implement a part-whole hierarchy. For example, a diagram made of other pieces such as circle, square, triangle, etc. |
| [Proxy](#proxy-pattern)        | Provide a surrogate or placeholder for another object to control access to it. |
| [Flyweight](#flyweight-pattern)    | Caching and reusing object instances, used with immutable objects. For example, string pool. |
| [Facade](#facade-pattern)       | Creating a wrapper interfaces on top of existing interfaces to help client applications. |
| [Bridge](#bridge-pattern)       | The bridge design pattern is used to decouple the interfaces from implementation and hiding the implementation details from the client program. |
| [Decorator](#decorator-pattern)    | The decorator design pattern is used to modify the functionality of an object at runtime. |

## Review them all in details

### Adapter Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 6
   - Comment: Converts the interface of a class into another interface that clients expect, allowing classes with incompatible interfaces to work together.
   - Best Use Case: When you want to reuse an existing class but its interface does not match the required interface.
   - Antipattern: Creating unnecessary complexity by overusing adapters and introducing too many layers of abstraction.

   C#
   ```csharp
   public interface ITarget
   {
       void Request();
   }
   
   public class Adaptee
   {
       public void SpecificRequest()
       {
           Console.WriteLine("Adaptee specific request.");
       }
   }
   
   public class Adapter : ITarget
   {
       private Adaptee adaptee;
   
       public Adapter(Adaptee adaptee)
       {
           this.adaptee = adaptee;
       }
   
       public void Request()
       {
           adaptee.SpecificRequest();
       }
   }
   ```

   Python
    
   ```python
    class Target:
        def request(self):
            print("Target request")

    class Adaptee:
        def specific_request(self):
            print("Adaptee specific request")

    class Adapter(Target):
        def __init__(self, adaptee: Adaptee):
            self.adaptee = adaptee

        def request(self):
            self.adaptee.specific_request()
   ```

### Composite Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 7
   - Comment: Allows you to treat a group of objects as a single object, creating a tree-like structure of objects.
   - Best Use Case: When you want to represent part-whole hierarchies of objects and treat individual objects and compositions uniformly.
   - Antipattern: Overcomplicating the composite structure with excessive levels of nesting or adding unnecessary functionality to leaf nodes.

   C#
   ```csharp
   public abstract class Component
   {
       public abstract void Operation();
   }
   
   public class Leaf : Component
   {
       public override void Operation()
       {
           Console.WriteLine("Leaf operation.");
       }
   }
   
   public class Composite : Component
   {
       private List<Component> children = new List<Component>();
   
       public void Add(Component component)
       {
           children.Add(component);
       }
   
       public void Remove(Component component)
       {
           children.Remove(component);
       }
   
       public override void Operation()
       {
           foreach (var child in children)
           {
               child.Operation();
           }
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class Component(ABC):
        @abstractmethod
        def operation(self):
            pass

    class Leaf(Component):
        def operation(self):
            print("Leaf operation")

    class Composite(Component):
        def __init__(self):
            self.children = []

        def add(self, component: Component):
            self.children.append(component)

        def remove(self, component: Component):
            self.children.remove(component)

        def operation(self):
            for child in self.children:
                child.operation()
   ```

### Proxy Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 11
   - Comment: Provides a surrogate or placeholder for another object, controlling access to it and adding additional functionality.
   - Best Use Case: When you want to control access to an object, add extra behavior before or after accessing it, or provide a lightweight representation of a heavy object.
   - Antipattern: Creating a bloated proxy that performs extensive processing or has complex interactions with the real object, which can degrade performance.

   C#
   ```csharp
   public interface ISubject
   {
       void Request();
   }
   
   public class RealSubject : ISubject
   {
       public void Request()
       {
           Console.WriteLine("RealSubject request.");
       }
   }
   
   public class Proxy : ISubject
   {
       private RealSubject realSubject;
   
       public void Request()
       {
           if (realSubject == null)
           {
               realSubject = new RealSubject();
           }
   
           // Perform additional operations before or after delegating to the real subject
           Console.WriteLine("Proxy request.");
   
           realSubject.Request();
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class Subject(ABC):
        @abstractmethod
        def request(self):
            pass

    class RealSubject(Subject):
        def request(self):
            print("RealSubject request")

    class Proxy(Subject):
        def __init__(self):
            self.real_subject = None

        def request(self):
            if not self.real_subject:
                self.real_subject = RealSubject()
            print("Proxy request")
            self.real_subject.request()
   ```

### Flyweight Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 15
   - Comment: Reduces the memory footprint of objects by sharing common state between multiple objects.
   - Best Use Case: When you have a large number of similar objects that can share common state to conserve memory.
   - Antipattern: Overusing the flyweight pattern and attempting to share too much state, which can lead to excessive complexity or unintended side effects.

   C#
   ```csharp
   public class Flyweight
   {
       private string sharedState;
   
       public Flyweight(string sharedState)
       {
           this.sharedState = sharedState;
       }
   
       public void Operation(string uniqueState)
       {
           Console.WriteLine("Flyweight with shared state {0} and unique state {1}.", sharedState, uniqueState);
       }
   }
   
   public class FlyweightFactory
   {
       private Dictionary<string, Flyweight> flyweights = new Dictionary<string, Flyweight>();
   
       public Flyweight GetFlyweight(string key)
       {
           if (!flyweights.ContainsKey(key))
           {
               flyweights[key] = new Flyweight(key);
           }
   
           return flyweights[key];
       }
   }
   ```

   Python
    
   ```python
    class Flyweight:
        def __init__(self, shared_state):
            self.shared_state = shared_state

        def operation(self, unique_state):
            print(f"Flyweight with shared state {self.shared_state} and unique state {unique_state}")

    class FlyweightFactory:
        def __init__(self):
            self.flyweights = {}

        def get_flyweight(self, key):
            if key not in self.flyweights:
                self.flyweights[key] = Flyweight(key)
            return self.flyweights[key]
   ```

### Facade Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 23
   - Comment: Provides a simplified interface to a complex subsystem, making it easier to use and understand.
   - Best Use Case: When you want to provide a higher-level interface that hides the complexities and dependencies of a subsystem.
   - Antipattern: Creating a facade that becomes too bloated or violates the Single Responsibility Principle by taking on too many responsibilities.

   C#
   ```csharp
   public class SubsystemA
   {
       public void OperationA()
       {
           Console.WriteLine("SubsystemA operation.");
       }
   }
   
   public class SubsystemB
   {
       public void OperationB()
       {
           Console.WriteLine("SubsystemB operation.");
       }
   }
   
   public class Facade
   {
       private SubsystemA subsystemA;
       private SubsystemB subsystemB;
   
       public Facade()
       {
           subsystemA = new SubsystemA();
           subsystemB = new SubsystemB();
       }
   
       public void Operation()
       {
           subsystemA.OperationA();
           subsystemB.OperationB();
       }
   }
   ```

   Python
    
   ```python
    class SubsystemA:
        def operationA(self):
            print("Subsystem A operation")

    class SubsystemB:
        def operationB(self):
            print("Subsystem B operation")

    class Facade:
        def __init__(self):
            self.subsystemA = SubsystemA()
            self.subsystemB = SubsystemB()

        def operation(self):
            self.subsystemA.operationA()
            self.subsystemB.operationB()
   ```

### Bridge Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 13
   - Comment: Decouples an abstraction from its implementation, allowing them to vary independently.
   - Best Use Case: When you have a set of abstractions and multiple implementations, and you want to be able to switch and combine them dynamically.
   - Antipattern: Overcomplicating the bridge by adding unnecessary complexity or creating a large number of abstraction and implementation classes.

   C#
   ```csharp
   public interface IImplementation
   {
       void OperationImplementation();
   }
   
   public class ConcreteImplementationA : IImplementation
   {
       public void OperationImplementation()
       {
           Console.WriteLine("Concrete Implementation A");
       }
   }
   
   public class ConcreteImplementationB : IImplementation
   {
       public void OperationImplementation()
       {
           Console.WriteLine("Concrete Implementation B");
       }
   }
   
   public abstract class Abstraction
   {
       protected IImplementation implementation;
   
       public Abstraction(IImplementation implementation)
       {
           this.implementation = implementation;
       }
   
       public abstract void Operation();
   }
   
   public class RefinedAbstraction : Abstraction
   {
       public RefinedAbstraction(IImplementation implementation) : base(implementation)
       {
       }
   
       public override void Operation()
       {
           Console.WriteLine("Refined Abstraction");
           implementation.OperationImplementation();
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class Implementor(ABC):
        @abstractmethod
        def operation_implementation(self):
            pass

    class ConcreteImplementorA(Implementor):
        def operation_implementation(self):
            print("Concrete Implementor A")

    class ConcreteImplementorB(Implementor):
        def operation_implementation(self):
            print("Concrete Implementor B")

    class Abstraction:
        def __init__(self, implementor: Implementor):
            self.implementor = implementor

        def operation(self):
            self.implementor.operation_implementation()
   ```

### Decorator Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 5
   - Comment: Allows behavior to be added to an object dynamically by wrapping it with other objects at runtime.
   - Best Use Case: When you need to add additional responsibilities to an object dynamically without affecting other objects.
   - Antipattern: Overusing decorators can lead to a complex object hierarchy and result in a lot of small, tightly coupled classes.

   C#
   ```csharp
   public abstract class Component
   {
       public abstract void Operation();
   }
   
   public class ConcreteComponent : Component
   {
       public override void Operation()
       {
           Console.WriteLine("ConcreteComponent operation.");
       }
   }
   
   public abstract class Decorator : Component
   {
       protected Component component;
   
       public void SetComponent(Component component)
       {
           this.component = component;
       }
   
       public override void Operation()
       {
           if (component != null)
               component.Operation();
       }
   }
   
   public class ConcreteDecoratorA : Decorator
   {
       public override void Operation()
       {
           base.Operation();
           Console.WriteLine("Added behavior from ConcreteDecoratorA.");
       }
   }
   
   public class ConcreteDecoratorB : Decorator
   {
       public override void Operation()
       {
           base.Operation();
           Console.WriteLine("Added behavior from ConcreteDecoratorB.");
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class Component(ABC):
        @abstractmethod
        def operation(self):
            pass

    class ConcreteComponent(Component):
        def operation(self):
            print("Concrete Component")

    class Decorator(Component):
        def __init__(self, component: Component):
            self.component = component

        def operation(self):
            self.component.operation()

    class ConcreteDecoratorA(Decorator):
        def operation(self):
            super().operation()
            print("Concrete Decorator A")

    class ConcreteDecoratorB(Decorator):
        def operation(self):
            super().operation()
            print("Concrete Decorator B")
   ```

## Behavioral Design Patterns
There are 11 behavioral design patterns defined in the GoF design patterns.
| Pattern Name            | Description |
|-------------------------|:-------------------------------------------------------------------------------------------------|
| [Template Method](#template-method-pattern)         | used to create a template method stub and defer some of the steps of implementation to the subclasses. |
| [Mediator](#mediator-pattern)                | used to provide a centralized communication medium between different objects in a system. |
| [Chain of Responsibility](#chain-of-responsibility-pattern) | used to achieve loose coupling in software design where a request from the client is passed to a chain of objects to process them. |
| [Observer](#observer-pattern)                | useful when you are interested in the state of an object and want to get notified whenever there is any change. |
| [Strategy](#strategy-pattern)                | Strategy pattern is used when we have multiple algorithm for a specific task and client decides the actual implementation to be used at runtime. |
| [Command](#command-pattern)                 | Command Pattern is used to implement lose coupling in a request-response model. |
| [State](#state-pattern)                   | State design pattern is used when an Object change it’s behavior based on it’s internal state. |
| [Visitor](#visitor-pattern)                 | Visitor pattern is used when we have to perform an operation on a group of similar kind of Objects. |
| [Interpreter](#interpreter-pattern)             | defines a grammatical representation for a language and provides an interpreter to deal with this grammar. |
| [Iterator](#iterator-pattern)                | used to provide a standard way to traverse through a group of Objects. |
| [Memento](#memento-pattern)                 | The memento design pattern is used when we want to save the state of an object so that we can restore later on. |

## Review them all in details

### Template Method Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 8
   - Comment: Defines the skeleton of an algorithm in a base class and lets subclasses redefine certain steps of the algorithm without changing its structure.
   - Best Use Case: When you have a common algorithm with some variations that can be implemented by subclasses.
   - Antipattern: Creating a template method that is too rigid and difficult to extend or modify.

   C#
   ```csharp
   public abstract class AbstractClass
   {
       public void TemplateMethod()
       {
           Step1();
           Step2();
           Step3();
       }
   
       protected abstract void Step1();
   
       protected abstract void Step2();
   
       protected abstract void Step3();
   }
   
   public class ConcreteClass : AbstractClass
   {
       protected override void Step1()
       {
           Console.WriteLine("Step 1");
       }
   
       protected override void Step2()
       {
           Console.WriteLine("Step 2");
       }
   
       protected override void Step3()
       {
           Console.WriteLine("Step 3");
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class AbstractClass(ABC):
        @abstractmethod
        def primitive_operation1(self):
            pass

        @abstractmethod
        def primitive_operation2(self):
            pass

        def template_method(self):
            self.primitive_operation1()
            self.primitive_operation2()

    class ConcreteClass(AbstractClass):
        def primitive_operation1(self):
            print("Concrete Class: Primitive Operation 1")

        def primitive_operation2(self):
            print("Concrete Class: Primitive Operation 2")

   ```

### Mediator Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 17
   - Comment: Defines an object that encapsulates the communication and coordination between other objects, promoting loose coupling between them.
   - Best Use Case: When you have a set of objects that need to communicate and interact with each other without explicitly referring to one another.
   - Antipattern: Creating a mediator that becomes too complex and handles excessive logic or dependencies, violating the Single Responsibility Principle.

   C#
   ```csharp
   public interface IMediator
   {
       void SendMessage(string message, Colleague colleague);
   }
   
   public abstract class Colleague
   {
       protected IMediator mediator;
   
       public Colleague(IMediator mediator)
       {
           this.mediator = mediator;
       }
   
       public abstract void ReceiveMessage(string message);
   }
   
   public class ConcreteColleagueA : Colleague
   {
       public ConcreteColleagueA(IMediator mediator) : base(mediator)
       {
       }
   
       public override void ReceiveMessage(string message)
       {
           Console.WriteLine("Colleague A received: " + message);
       }
   
       public void Send(string message)
       {
           mediator.SendMessage(message, this);
       }
   }
   
   public class ConcreteColleagueB : Colleague
   {
       public ConcreteColleagueB(IMediator mediator) : base(mediator)
       {
       }
   
       public override void ReceiveMessage(string message)
       {
           Console.WriteLine("Colleague B received: " + message);
       }
   
       public void Send(string message)
       {
           mediator.SendMessage(message, this);
       }
   }
   
   public class ConcreteMediator : IMediator
   {
       private ConcreteColleagueA colleagueA;
       private ConcreteColleagueB colleagueB;
   
       public void SetColleagueA(ConcreteColleagueA colleagueA)
       {
           this.colleagueA = colleagueA;
       }
   
       public void SetColleagueB(ConcreteColleagueB colleagueB)
       {
           this.colleagueB = colleagueB;
       }
   
       public void SendMessage(string message, Colleague colleague)
       {
           if (colleague == colleagueA)
           {
               colleagueB.ReceiveMessage(message);
           }
           else if (colleague == colleagueB)
           {
               colleagueA.ReceiveMessage(message);
           }
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class Mediator(ABC):
        @abstractmethod
        def send_message(self, message, colleague):
            pass

    class Colleague(ABC):
        def __init__(self, mediator):
            self.mediator = mediator

        @abstractmethod
        def receive_message(self, message):
            pass

    class ConcreteColleagueA(Colleague):
        def receive_message(self, message):
            print("Colleague A received:", message)

        def send_message(self, message):
            self.mediator.send_message(message, self)

    class ConcreteColleagueB(Colleague):
        def receive_message(self, message):
            print("Colleague B received:", message)

        def send_message(self, message):
            self.mediator.send_message(message, self)

    class ConcreteMediator(Mediator):
        def __init__(self):
            self.colleagueA = None
            self.colleagueB = None

        def set_colleagueA(self, colleagueA):
            self.colleagueA = colleagueA

        def set_colleagueB(self, colleagueB):
            self.colleagueB = colleagueB

        def send_message(self, message, colleague):
            if colleague == self.colleagueA:
                self.colleagueB.receive_message(message)
            elif colleague == self.colleagueB:
                self.colleagueA.receive_message(message)

   ```

### Chain of Responsibility Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 14
   - Comment: Allows an object to pass a request along a chain of potential handlers until the request is handled or reaches the end of the chain.
   - Best Use Case: When you have a set of objects that can handle a request, and you want to decouple the sender of the request from the receiver objects.
   - Antipattern: Creating a chain with no clear termination condition or having overlapping or redundant responsibilities between handlers.

   C#
   ```csharp
   public abstract class Handler
   {
       protected Handler successor;
   
       public void SetSuccessor(Handler successor)
       {
           this.successor = successor;
       }
   
       public abstract void HandleRequest(int request);
   }
   
   public class ConcreteHandlerA : Handler
   {
       public override void HandleRequest(int request)
       {
           if (request >= 0 && request < 10)
           {
               Console.WriteLine("Handled by ConcreteHandlerA");
           }
           else if (successor != null)
           {
               successor.HandleRequest(request);
           }
       }
   }
   
   public class ConcreteHandlerB : Handler
   {
       public override void HandleRequest(int request)
       {
           if (request >= 10 && request < 20)
           {
               Console.WriteLine("Handled by ConcreteHandlerB");
           }
           else if (successor != null)
           {
               successor.HandleRequest(request);
           }
       }
   }
   
   public class ConcreteHandlerC : Handler
   {
       public override void HandleRequest(int request)
       {
           if (request >= 20 && request < 30)
           {
               Console.WriteLine("Handled by ConcreteHandlerC");
           }
           else if (successor != null)
           {
               successor.HandleRequest(request);
           }
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class Handler(ABC):
        def __init__(self, successor=None):
            self.successor = successor

        @abstractmethod
        def handle_request(self, request):
            pass

    class ConcreteHandlerA(Handler):
        def handle_request(self, request):
            if 0 <= request < 10:
                print("Handled by ConcreteHandlerA")
            elif self.successor:
                self.successor.handle_request(request)

    class ConcreteHandlerB(Handler):
        def handle_request(self, request):
            if 10 <= request < 20:
                print("Handled by ConcreteHandlerB")
            elif self.successor:
                self.successor.handle_request(request)

    class ConcreteHandlerC(Handler):
        def handle_request(self, request):
            if 20 <= request < 30:
                print("Handled by ConcreteHandlerC")
            elif self.successor:
                self.successor.handle_request(request)
   ```

### Observer Pattern:  
   - Confidence: X/5 [Remove]
   - LearningOrder: 3
   - Comment: Defines a one-to-many dependency between objects, so that when one object changes its state, all its dependents are notified and updated automatically.
   - Best Use Case: When you need to maintain consistency between related objects without tight coupling.
   - Antipattern: Overusing the observer pattern can lead to performance issues and make code harder to understand.

   C#
   ```csharp
   public interface IObserver
   {
       void Update();
   }
   
   public class ConcreteObserver : IObserver
   {
       public void Update()
       {
           Console.WriteLine("Observer received an update.");
       }
   }
   
   public interface ISubject
   {
       void Attach(IObserver observer);
       void Detach(IObserver observer);
       void Notify();
   }
   
   public class ConcreteSubject : ISubject
   {
       private List<IObserver> observers = new List<IObserver>();
   
       public void Attach(IObserver observer)
       {
           observers.Add(observer);
       }
   
       public void Detach(IObserver observer)
       {
           observers.Remove(observer);
       }
   
       public void Notify()
       {
           foreach (var observer in observers)
           {
               observer.Update();
           }
       }
   }
   ```

   Python
    
   ```python
    class Observer:
        def update(self, subject):
            pass

    class ConcreteObserverA(Observer):
        def update(self, subject):
            print("Concrete Observer A received update from subject")

    class ConcreteObserverB(Observer):
        def update(self, subject):
            print("Concrete Observer B received update from subject")

    class Subject:
        def __init__(self):
            self.observers = []

        def attach(self, observer):
            self.observers.append(observer)

        def detach(self, observer):
            self.observers.remove(observer)

        def notify(self):
            for observer in self.observers:
                observer.update(self)

   ```

### Strategy Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 4
   - Comment: Defines a family of algorithms, encapsulates each one, and makes them interchangeable. It lets the algorithm vary independently from the clients that use it.
   - Best Use Case: When you need to dynamically select an algorithm at runtime or when you have multiple variants of an algorithm.
   - Antipattern: Overusing the strategy pattern can lead to excessive complexity if there are too many strategies.

   C#
   ```csharp
   public interface IStrategy
   {
       void Execute();
   }
   
   public class ConcreteStrategyA : IStrategy
   {
       public void Execute()
       {
           Console.WriteLine("Executing strategy A.");
       }
   }
   
   public class ConcreteStrategyB : IStrategy
   {
       public void Execute()
       {
           Console.WriteLine("Executing strategy B.");
       }
   }
   
   public class Context
   {
       private IStrategy strategy;
   
       public Context(IStrategy strategy)
       {
           this.strategy = strategy;
       }
   
       public void ExecuteStrategy()
       {
           strategy.Execute();
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class Strategy(ABC):
        @abstractmethod
        def execute(self):
            pass

    class ConcreteStrategyA(Strategy):
        def execute(self):
            print("Concrete Strategy A")

    class ConcreteStrategyB(Strategy):
        def execute(self):
            print("Concrete Strategy B")

    class Context:
        def __init__(self, strategy: Strategy):
            self.strategy = strategy

        def execute_strategy(self):
            self.strategy.execute()

   ```

### Command Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 18
   - Comment: Encapsulates a request as an object, allowing you to parameterize clients with different requests, queue or log requests, and support undoable operations.
   - Best Use Case: When you want to decouple the requester of an action from the object that performs the action.
   - Antipattern: Creating overly complex command objects or commands that have a direct dependency on the receiver, violating the principle of loose coupling.

   C#
   ```csharp
   public interface ICommand
   {
       void Execute();
   }
   
   public class Receiver
   {
       public void Action()
       {
           Console.WriteLine("Receiver action.");
       }
   }
   
   public class ConcreteCommand : ICommand
   {
       private Receiver receiver;
   
       public ConcreteCommand(Receiver receiver)
       {
           this.receiver = receiver;
       }
   
       public void Execute()
       {
           receiver.Action();
       }
   }
   
   public class Invoker
   {
       private ICommand command;
   
       public void SetCommand(ICommand command)
       {
           this.command = command;
       }
   
       public void ExecuteCommand()
       {
           command.Execute();
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class Command(ABC):
        @abstractmethod
        def execute(self):
            pass

    class Receiver:
        def action(self):
            print("Receiver action")

    class ConcreteCommand(Command):
        def __init__(self, receiver: Receiver):
            self.receiver = receiver

        def execute(self):
            self.receiver.action()

    class Invoker:
        def __init__(self):
            self.command = None

        def set_command(self, command: Command):
            self.command = command

        def execute_command(self):
            self.command.execute()

   ```

### State Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 10
   - Comment: Allows an object to alter its behavior when its internal state changes, encapsulating the state-specific logic into separate classes.
   - Best Use Case: When an object's behavior needs to change dynamically based on its internal state.
   - Antipattern: Having excessive duplication of state-specific code across different concrete state classes.

   C#
   ```csharp
   public interface IState
   {
       void HandleState();
   }
   
   public class ConcreteStateA : IState
   {
       public void HandleState()
       {
           Console.WriteLine("Concrete State A");
       }
   }
   
   public class ConcreteStateB : IState
   {
       public void HandleState()
       {
           Console.WriteLine("Concrete State B");
       }
   }
   
   public class Context
   {
       private IState currentState;
   
       public void SetState(IState state)
       {
           currentState = state;
       }
   
       public void Request()
       {
           currentState.HandleState();
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class State(ABC):
        @abstractmethod
        def handle_state(self):
            pass

    class ConcreteStateA(State):
        def handle_state(self):
            print("Concrete State A")

    class ConcreteStateB(State):
        def handle_state(self):
            print("Concrete State B")

    class Context:
        def __init__(self, state: State):
            self.state = state

        def set_state(self, state: State):
            self.state = state

        def request(self):
            self.state.handle_state()

   ```

### Visitor Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 20
   - Comment: Defines a new operation to be performed on elements of an object structure without changing their classes.
   - Best Use Case: When you have a set of objects with different types and want to perform different operations on them without modifying their classes.
   - Antipattern: Adding new concrete elements frequently, which requires modifying the visitor interface and all concrete visitors.

   C#
   ```csharp
   public interface IVisitor
   {
       void Visit(ElementA element);
       void Visit(ElementB element);
   }
   
   public abstract class Element
   {
       public abstract void Accept(IVisitor visitor);
   }
   
   public class ElementA : Element
   {
       public override void Accept(IVisitor visitor)
       {
           visitor.Visit(this);
       }
   }
   
   public class ElementB : Element
   {
       public override void Accept(IVisitor visitor)
       {
           visitor.Visit(this);
       }
   }
   
   public class ConcreteVisitor : IVisitor
   {
       public void Visit(ElementA element)
       {
           Console.WriteLine("Visitor visits Element A");
       }
   
       public void Visit(ElementB element)
       {
           Console.WriteLine("Visitor visits Element B");
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class Element(ABC):
        @abstractmethod
        def accept(self, visitor):
            pass

    class ConcreteElementA(Element):
        def accept(self, visitor):
            visitor.visit_concrete_elementA(self)

    class ConcreteElementB(Element):
        def accept(self, visitor):
            visitor.visit_concrete_elementB(self)

    class Visitor(ABC):
        @abstractmethod
        def visit_concrete_elementA(self, elementA):
            pass

        @abstractmethod
        def visit_concrete_elementB(self, elementB):
            pass

    class ConcreteVisitor(Visitor):
        def visit_concrete_elementA(self, elementA):
            print("Visitor: Visiting ConcreteElementA")

        def visit_concrete_elementB(self, elementB):
            print("Visitor: Visiting ConcreteElementB")

   ```

### Interpreter Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 16
   - Comment: Defines a representation of a grammar or language and provides a way to evaluate or interpret sentences in that language.
   - Best Use Case: When you need to define a language or grammar and perform operations or evaluations based on that language.
   - Antipattern: Creating complex or unmanageable grammars, or having an excessive number of different non-terminal expressions.

   C#
   ```csharp
   public abstract class AbstractExpression
   {
       public abstract void Interpret(Context context);
   }
   
   public class TerminalExpression : AbstractExpression
   {
       public override void Interpret(Context context)
       {
           Console.WriteLine("Terminal Expression");
       }
   }
   
   public class NonTerminalExpression : AbstractExpression
   {
       public override void Interpret(Context context)
       {
           Console.WriteLine("Non-Terminal Expression");
       }
   }
   
   public class Context
   {
       // Context-specific data or variables
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class Expression(ABC):
        @abstractmethod
        def interpret(self, context):
            pass

    class TerminalExpression(Expression):
        def interpret(self, context):
            print("Terminal Expression")

    class NonTerminalExpression(Expression):
        def interpret(self, context):
            print("Non-Terminal Expression")

    class Context:
        pass
   ```

### Iterator Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 9
   - Comment: Provides a way to access elements of an aggregate object sequentially without exposing its underlying structure.
   - Best Use Case: When you want to provide a standardized way to traverse and access elements of a collection.
   - Antipattern: Modifying the collection while iterating over it, which can lead to unpredictable behavior or exceptions.

   C#
   ```csharp
   public interface IIterator
   {
       bool HasNext();
       object Next();
   }
   
   public interface IAggregate
   {
       IIterator CreateIterator();
   }
   
   public class ConcreteIterator : IIterator
   {
       private readonly List<object> collection;
       private int position = 0;
   
       public ConcreteIterator(List<object> collection)
       {
           this.collection = collection;
       }
   
       public bool HasNext()
       {
           return position < collection.Count;
       }
   
       public object Next()
       {
           if (HasNext())
           {
               object item = collection[position];
               position++;
               return item;
           }
   
           return null;
       }
   }
   
   public class ConcreteAggregate : IAggregate
   {
       private readonly List<object> collection = new List<object>();
   
       public void Add(object item)
       {
           collection.Add(item);
       }
   
       public IIterator CreateIterator()
       {
           return new ConcreteIterator(collection);
       }
   }
   ```

   Python
    
   ```python
    from abc import ABC, abstractmethod

    class Iterator(ABC):
        @abstractmethod
        def has_next(self):
            pass

        @abstractmethod
        def next(self):
            pass

    class Aggregate(ABC):
        @abstractmethod
        def create_iterator(self) -> Iterator:
            pass

    class ConcreteIterator(Iterator):
        def __init__(self, collection):
            self.collection = collection
            self.position = 0

        def has_next(self):
            return self.position < len(self.collection)

        def next(self):
            if self.has_next():
                item = self.collection[self.position]
                self.position += 1
                return item
            return None

    class ConcreteAggregate(Aggregate):
        def __init__(self):
            self.collection = []

        def add_item(self, item):
            self.collection.append(item)

        def create_iterator(self) -> Iterator:
            return ConcreteIterator(self.collection)

   ```

### Memento Pattern:
   - Confidence: X/5 [Remove]
   - LearningOrder: 17
   - Comment: Captures and externalizes an object's internal state, allowing the object to be restored to that state later.
   - Best Use Case: When you need to save and restore an object's state without exposing its internal implementation details.
   - Antipattern: Exposing the internal state of the originator object directly, violating encapsulation.

   C#
   ```csharp
   public class Memento
   {
       public string State { get; private set; }
   
       public Memento(string state)
       {
           State = state;
       }
   }
   
   public class Originator
   {
       public string State { get; set; }
   
       public Memento CreateMemento()
       {
           return new Memento(State);
       }
   
       public void RestoreMemento(Memento memento)
       {
           State = memento.State;
       }
   }
   
   public class Caretaker
   {
       public Memento Memento { get; set; }
   }
   ```

   Python
    
   ```python
    class Memento:
        def __init__(self, state):
            self.state = state

    class Originator:
        def __init__(self):
            self.state = None

        def set_state(self, state):
            self.state = state

        def create_memento(self):
            return Memento(self.state)

        def restore_memento(self, memento):
            self.state = memento.state

    class Caretaker:
        def __init__(self):
            self.memento = None

        def set_memento(self, memento):
            self.memento = memento

   ```






## Post exercises
* XXXX

### Readings:
* https://www.digitalocean.com/community/tutorials/gangs-of-four-gof-design-patterns 







# Day 2

## Preparation
* XXX

## Workshop notes
* XXX


## Post exercises
### Readings
* XXXX

### Interesting things to do:
* XXXXX
