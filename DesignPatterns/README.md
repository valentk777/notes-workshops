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
| [Prototype](#prototype-pattern)        | Creating a new object instance from another similar instance and then modify according to our requirements. |

## Review them all in details

### Singleton Pattern
   - Confidence: 4/5 [Remove]
   - Comment: Ensures that only one instance of a class exists throughout the application.
   - Best Use Case: When you need to have a single, shared instance of a class.
   - Antipattern: Overuse of the singleton pattern can lead to tight coupling and make testing and maintenance difficult.
   
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

### Factory Pattern
   - Confidence: 3/5
   - Comment: Encapsulates object creation logic and provides a centralized factory class to create objects.
   - Best Use Case: When you want to delegate the responsibility of object creation to a separate class.
   - Antipattern: When the factory class becomes too complex or violates the Single Responsibility Principle, it's called a "God object."

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

### Abstract Factory Pattern:
   - Comment: Provides an interface for creating families of related or dependent objects without specifying their concrete classes.
   - Best Use Case: When you need to create families of objects that are designed to work together and ensure their compatibility.
   - Antipattern: Creating a bloated abstract factory interface with too many methods or incorporating unrelated product families.

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

### Builder Pattern:
   - Comment: Separates the construction of a complex object from its representation, allowing the same construction process to create different representations.
   - Best Use Case: When you want to create complex objects step by step or when the object creation process involves several optional parameters.
   - Antipattern: Violating the Single Responsibility Principle by putting too much logic into the builder class.

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

### Prototype Pattern:
   - Comment: Creates new objects by cloning existing objects, allowing you to create new instances without knowing their specific classes.
   - Best Use Case: When creating new instances is costly or complex, and you want to avoid the overhead of initializing objects from scratch.
   - Antipattern: Modifying the cloned object directly without following the proper cloning process can lead to unexpected behavior.

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
   - Comment: Converts the interface of a class into another interface that clients expect, allowing classes with incompatible interfaces to work together.
   - Best Use Case: When you want to reuse an existing class but its interface does not match the required interface.
   - Antipattern: Creating unnecessary complexity by overusing adapters and introducing too many layers of abstraction.

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

### Composite Pattern:
   - Comment: Allows you to treat a group of objects as a single object, creating a tree-like structure of objects.
   - Best Use Case: When you want to represent part-whole hierarchies of objects and treat individual objects and compositions uniformly.
   - Antipattern: Overcomplicating the composite structure with excessive levels of nesting or adding unnecessary functionality to leaf nodes.

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

### Proxy Pattern:
   - Comment: Provides a surrogate or placeholder for another object, controlling access to it and adding additional functionality.
   - Best Use Case: When you want to control access to an object, add extra behavior before or after accessing it, or provide a lightweight representation of a heavy object.
   - Antipattern: Creating a bloated proxy that performs extensive processing or has complex interactions with the real object, which can degrade performance.

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

### Flyweight Pattern:
   - Comment: Reduces the memory footprint of objects by sharing common state between multiple objects.
   - Best Use Case: When you have a large number of similar objects that can share common state to conserve memory.
   - Antipattern: Overusing the flyweight pattern and attempting to share too much state, which can lead to excessive complexity or unintended side effects.

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

### Facade Pattern:
   - Comment: Provides a simplified interface to a complex subsystem, making it easier to use and understand.
   - Best Use Case: When you want to provide a higher-level interface that hides the complexities and dependencies of a subsystem.
   - Antipattern: Creating a facade that becomes too bloated or violates the Single Responsibility Principle by taking on too many responsibilities.

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

### Bridge Pattern:
   - Comment: Decouples an abstraction from its implementation, allowing them to vary independently.
   - Best Use Case: When you have a set of abstractions and multiple implementations, and you want to be able to switch and combine them dynamically.
   - Antipattern: Overcomplicating the bridge by adding unnecessary complexity or creating a large number of abstraction and implementation classes.

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

### Decorator Pattern:
   - Comment: Allows behavior to be added to an object dynamically by wrapping it with other objects at runtime.
   - Best Use Case: When you need to add additional responsibilities to an object dynamically without affecting other objects.
   - Antipattern: Overusing decorators can lead to a complex object hierarchy and result in a lot of small, tightly coupled classes.

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
   - Comment: Defines the skeleton of an algorithm in a base class and lets subclasses redefine certain steps of the algorithm without changing its structure.
   - Best Use Case: When you have a common algorithm with some variations that can be implemented by subclasses.
   - Antipattern: Creating a template method that is too rigid and difficult to extend or modify.

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

### Mediator Pattern:
   - Comment: Defines an object that encapsulates the communication and coordination between other objects, promoting loose coupling between them.
   - Best Use Case: When you have a set of objects that need to communicate and interact with each other without explicitly referring to one another.
   - Antipattern: Creating a mediator that becomes too complex and handles excessive logic or dependencies, violating the Single Responsibility Principle.

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

### Chain of Responsibility Pattern:
   - Comment: Allows an object to pass a request along a chain of potential handlers until the request is handled or reaches the end of the chain.
   - Best Use Case: When you have a set of objects that can handle a request, and you want to decouple the sender of the request from the receiver objects.
   - Antipattern: Creating a chain with no clear termination condition or having overlapping or redundant responsibilities between handlers.

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

### Observer Pattern:  
   - Confidence: 3/5
   - Comment: Defines a one-to-many dependency between objects, so that when one object changes its state, all its dependents are notified and updated automatically.
   - Best Use Case: When you need to maintain consistency between related objects without tight coupling.
   - Antipattern: Overusing the observer pattern can lead to performance issues and make code harder to understand.

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

### Strategy Pattern:
   - Comment: Defines a family of algorithms, encapsulates each one, and makes them interchangeable. It lets the algorithm vary independently from the clients that use it.
   - Best Use Case: When you need to dynamically select an algorithm at runtime or when you have multiple variants of an algorithm.
   - Antipattern: Overusing the strategy pattern can lead to excessive complexity if there are too many strategies.

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

### Command Pattern:
   - Comment: Encapsulates a request as an object, allowing you to parameterize clients with different requests, queue or log requests, and support undoable operations.
   - Best Use Case: When you want to decouple the requester of an action from the object that performs the action.
   - Antipattern: Creating overly complex command objects or commands that have a direct dependency on the receiver, violating the principle of loose coupling.

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

### State Pattern:
   - Comment: Allows an object to alter its behavior when its internal state changes, encapsulating the state-specific logic into separate classes.
   - Best Use Case: When an object's behavior needs to change dynamically based on its internal state.
   - Antipattern: Having excessive duplication of state-specific code across different concrete state classes.

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

### Visitor Pattern:
   - Comment: Defines a new operation to be performed on elements of an object structure without changing their classes.
   - Best Use Case: When you have a set of objects with different types and want to perform different operations on them without modifying their classes.
   - Antipattern: Adding new concrete elements frequently, which requires modifying the visitor interface and all concrete visitors.

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

### Interpreter Pattern:
   - Comment: Defines a representation of a grammar or language and provides a way to evaluate or interpret sentences in that language.
   - Best Use Case: When you need to define a language or grammar and perform operations or evaluations based on that language.
   - Antipattern: Creating complex or unmanageable grammars, or having an excessive number of different non-terminal expressions.

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

### Iterator Pattern:
   - Comment: Provides a way to access elements of an aggregate object sequentially without exposing its underlying structure.
   - Best Use Case: When you want to provide a standardized way to traverse and access elements of a collection.
   - Antipattern: Modifying the collection while iterating over it, which can lead to unpredictable behavior or exceptions.

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

### Memento Pattern:
   - Comment: Captures and externalizes an object's internal state, allowing the object to be restored to that state later.
   - Best Use Case: When you need to save and restore an object's state without exposing its internal implementation details.
   - Antipattern: Exposing the internal state of the originator object directly, violating encapsulation.

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
