# Design Patterns

There are three main groups of design patterns:
- Creational: the different ways to create objects
- Structural: the relationships between those objects
- Behavioral: the interaction or communication between those objects

# Behavioral Design Patterns
Behavioral Design Patterns focus on how objects interact with each other and how they communicate to acomplish specific tasks. These patterns address communication, responsibility, and algorythmic issues in object-oriented software design. They help defining clear and efficient communication between objects and classes.

These patterns help in making design more flexible, extensible and maintainable by promoting better communication and separation of concerns between objects and classes in the system. Each pattern addresses specific design issues and provides a standardised solution to common problems encountered in software development.

## Memento Pattern
The memento pattern is used to restore an object to a previous state.

**Example**": A common use case for the Memento Pattern is when implementing an undo feature. For example most text editors have undo features where you can undo actions by pressing Ctrl + Z.

**When to use the Memento Pattern**
The memento pattern can be used when you want to produce snapshots of an object's state to be able to restore an object to a previous state. It is commonly used for implementing the undo feature and so provides a common solution that a team of developers can quickly understand and get on the same page with.

**Pros and cons of the Memento Pattern**
- You can simplify the originator's code by letting the caretaker maintain the history of the originator's state. satisfying the Single Responsibility Principle
- The app might consume a lot of memory if lots of mementos are created.

## State Pattern
The state pattern allows an object to behave differremtly depending on the state that it is in.

**Example**: Say you're writing a blog post using the popular content management system, Wordpress. The document or post can be in one of these 3 states:
1. Draft
2. Moderation (under review by admin)
3. Published
There are three types of user roles:
1. Reader
2. Editor
3. Admin
Only admins can publish documents.

The State Pattern suggests that we should create state classes for each possible state and extract state specific logic into these classes.

The `Document` (class) will store a reference to one of these states to represent the state that it is currently in. Then the `Document`, instead of implementing state specific behavior by itself, it delegates the state specific work to the state object that it has reference to.

**When to use the State Pattern**
When you have a class that behaves differently depending on state and you have a large number of conditions.

**Pros and cons of the state pattern**
- improves readability and simplicity of the context class
- satisfies the Single Responsability principle by delegating state logic
- satisfies the Open / Closed principle as new states can be added without modifying existing ones
- (con) can be overkill for simple state logic or if the logic rarely changes
- (con) _WARNING_: risk of exposing aspects of the internal logic of the context class publicly: use dependency inversion

## Strategy Pattern
The strategy pattern is used to pass different apgorythms or behaviors to an object.

**Example**: Let's consider an application that stores videos. Before storing the video it needs to be compressed using a specific compression algorythm, such as MOV or MP4, then, if necessary, apply an overlay to the video, such as black and white or blur.

**State Pattern vs. Strategy Pattern**
The two patterns are similar in practice. Differences are:
- states store a reference to the context object that contains them.
- states are allowed to replace themselves
- strategies only handle a single specific task while states provide the underlying implementation for everything the context object does.

**When to use the Strategy Pattern**
A good rule of thumb is when you have a class with a large number of conditional statements that switch between variants of the same algorythm. The logic can be extracted into different classes that implement the same interface.

**Pros and cons**
- satisfies the Open / Closed principle: Add new strategies without modifying the context.
- can swap algorythms used inside an object at runtime;
- (con) clients have to be aware of the different algorythms and use them appropriately

## Iterator Pattern
The Iterator Pattern provides a way of iterating over an object without having to expose the object's internal structure., which may change in the future. Changing the internals of an object should not affect its consumers.

**Example**: Let's create a shoppiing list where we can add and remove items.

**When to use the Iterator Pattern**
Employ the iterator when your collection posseses a complex data structure or when the data structure is likely to change, so the clients can iterate over the collection without knowledge of the data structure.

_NOTE_: The .net `IEnumerable` implements the iterator pattern

**Pros and cons**
- satisfies the Single Responsability Principle: traversal logic is separated into external classes
- satisfies the Open / Closed Principle: You can create new collections and new iterators without breaking the code that uses them
- (con) can be over-engineering if the app only works with simple collections.

## Command Pattern
The Command Pattern is a behavioral pattern that encapsulates the request as an object, allowing you to paramaterize clients with queues. It enables you to decouple the sender from the receiver, providing flexibility in the executing of commands and supporting undoable operations.

The Command Pattern is also commonly used in UI frameworks, especially for handling user interactions with buttons or menu items. Each button or menu item can be associated with a specific command object, allowing the framework to execute the appropriate action when the user interacts with the UI element. This decouples the UI components from the actual operations they perform, providing flexibility and maintainability in UI development. Additionally the Command Pattern facilitates features such as undo/redo functionality and event logging in UI applications.

The framework would be code that you couldn't edit, i.e. some UI package, and the app is the part that you create. You would create concrete commands that extend the `Command` interface found in the UI package. Your concrete commands keep reference to a class that contains the business logic, such as `UserService` which can perform operations such as adding, updating, deleting users etc.

We could also add an `IUndoableCommand` with the additional `Unexecute()` method which can provide undo / redo functionality, i.e. with an `ItalicsTextCommand` class.

**Undo operations with Command vs. Memento Patterns**
The differenca between Memento and Command patterns is that the Memento stores snapshots of an objects' state which can get expensive over time. With the Command Pattern each command knows how to undo itself.