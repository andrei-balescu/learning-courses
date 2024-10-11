# Template Method Pattern
The Template Method Pattern allows you to define a template method, or skeleton for an operation. The specific steps can then be implemented by subclasses.

**Example**: Suppose we're designing some software that will be installed on a machine that makes hot beverages. At the beginning we just had tea and coffee. After some feedback rom customers we decided to add some more beverages such as camomile tea.

**Template Method vs. Strategy Pattern**
Template Method Pattern:
- use the Template Method Pattern when you have an algorithm with a fixed structure but with certain steps that need do be customized or implemented differently by subclasses.
- this pattern is ideal when you want to define a common algorithm skeleton (template method) in a base class and allow classes to selectively override specific steps to provide their own implementation.
- the Template Method Pattern is suitable when the overall algorithm structure remains consistent, but specific parts of the algorithm can vary based on different contexts or requirements.

Strategy pattern
- use the strategy pattern when you want to create a family of interchangeable algorithms or behaviors and encapsulate each algorithm in its own class.
- this pattern is ideal when you need to dynamically select and switch between different algorythms at runtime depending on the situation or context.
- the Strategy Pattern is suitable when you want to decouple the client code from specific algorythm implementations allowing greater flexibility and extensibility.

**When to use the Template Method Pattern**
Use the Template Method Pattern when you want to allow clients to implement only particular steps in an algorithm, and not the whole algorithm. It's a good pattern to use when you have a bunch of classes with the same logic, or algorithm but with differences in a few steps. So if the algorithm changes, it only has to be modified in a single place - the base class.

**Pros and cons**
- reduces code duplication
- clients only allowed to modify certain steps in an algorithm, reducing the risk of breaking clients if the algorithm changes.
- (con) some clients may be limited by the provided template
- (con) template methods can be hard to maintain if they have a lot of steps

![Template Method - UML](template_method_uml.svg)