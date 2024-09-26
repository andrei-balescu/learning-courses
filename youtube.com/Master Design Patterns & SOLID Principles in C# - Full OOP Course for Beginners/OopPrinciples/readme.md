# OOP Principles

Initial set-up (for future reference): Add a library using `dotnet add package Microsoft.Extensions.Logging`

# Encapsulation
Bundling data, attributes, fields, methods into a single class and limiting direct access to the data.

# Abstraction
Reduce complexity by hiding unnecesary details.

# Inheritance
Create new classes (subclasses or derived classes) based on existing classes (superclasses or base classes). Subclasses inherit properties and behavior from their superclasses and can also add new features or override existing ones. Inheritance is often described in terms of an "is-a" relationship.

# Polymorphism
The ability of an object to take many forms.

# Coupling
Degree of dependency between classes. Tight coupling means that classes are interdependent making it hard to modify one without the other.

# Composition
Composition envolves creating complex objects by combining simpler objects or components. In composition oblects are assembled together to form larger structures, with each component object maintaining its state and behavior. Composition is ofthen described in terms of a "has-a" relationship.

# Composition vs. Inheritance
## When to use composition
- When you need more flexibility in constructing objects by assembling smaller, reusable components.
- When there is no clear "is-a" relationship between classes and a "has-a" relationship is more appropriate.
- When you want to avoid the limitation of inheritance, such as tight coupling and the fragile classes problem.

## When to use inheritance
- When there is a clear "is-a" relationship between classes, and subclass objects can be treated as instances of their superclass.
- When you want to promote code reuse by inheriting properties and behaviors from existing classes.
- When you want to leverage polymorphism to allow objects of different subclasses to be treated uniformly through their common superclass interface.

# Fragile base class problem

The fragile base class problem is a software design issue that arises in object oriented programming when changes made to a base class can inadvertently break the functionality of derived classes. This problem occurs due to the tight coupling between base and derived classes in their inheritance hierarchies.

1. Inheritance Coupling: Inheritance creates a strong coupling between the base class (superclass) and derived classes (subclasses). Any change to the base class can potentially affect the behavior of all derived classes.
2. Limited Extensibility: The fragile base class problem limits the extensibility of software systems, as modifications to the base class become increasingly risky and costly over time. Developers may avoid making necessary changes due to fear of breaking existing functionality -- brittle software.

Mitigation strategies: to mitigate the fragile base class problem, software developers can use design principles such as Open/Closed Principle (OCP) And Dependency Inversion Principle (DIP) as well as design patterns like Composition over Inheritance. These approaches promote loose coupling, encapsulation and modular design, reducing the impact of changes in base classes.