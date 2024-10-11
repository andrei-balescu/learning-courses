# Command Pattern
The Command Pattern is a behavioral pattern that encapsulates the request as an object, allowing you to paramaterize clients with queues. It enables you to decouple the sender from the receiver, providing flexibility in the executing of commands and supporting undoable operations.

The Command Pattern is also commonly used in UI frameworks, especially for handling user interactions with buttons or menu items. Each button or menu item can be associated with a specific command object, allowing the framework to execute the appropriate action when the user interacts with the UI element. This decouples the UI components from the actual operations they perform, providing flexibility and maintainability in UI development. Additionally the Command Pattern facilitates features such as undo/redo functionality and event logging in UI applications.

The framework would be code that you couldn't edit, i.e. some UI package, and the app is the part that you create. You would create concrete commands that extend the `Command` interface found in the UI package. Your concrete commands keep reference to a class that contains the business logic, such as `UserService` which can perform operations such as adding, updating, deleting users etc.

We could also add an `IUndoableCommand` with the additional `Unexecute()` method which can provide undo / redo functionality, i.e. with an `ItalicsTextCommand` class.

**Undo operations with Command vs. Memento Patterns**
The differenca between Memento and Command patterns is that the Memento stores snapshots of an objects' state which can get expensive over time. With the Command Pattern each command knows how to undo itself.

**When to use the Command Pattern**
Use the Command PAttern when you want to implement reversible operations. The command pattern is most popular for implementing undo / redo as it uses less memory than the Memento Pattern which has to backup the whole state of the object.

The Command pattern is great when you want to queue operations or schedule their execution, as command objects can be serialized and stored in databases or sent over networks, then, at a later time, converted to objects and executed.

**Pros and cons**
- satisfies the Single Responsibility Principle: classes than invoke operations are decoupled from the classes that execute these operations.
- satisfies the Open / Closed Principle: new commands can be added to the codebase without having to modify existing code.
- (con) code may become more complex when you're adding a new layer between senders and receivers.

![Command Pattern - UML](command_uml.svg)
---
![Command Pattern - UML](command_uml2.svg)
