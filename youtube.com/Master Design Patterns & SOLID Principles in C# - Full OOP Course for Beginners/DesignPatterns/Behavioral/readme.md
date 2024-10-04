# Behavioral Design Patterns
Behavioral Design Patterns focus on how objects interact with each other and how they communicate to acomplish specific tasks. These patterns address communication, responsibility, and algorithmic issues in object-oriented software design. They help defining clear and efficient communication between objects and classes.

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

**Example**: Let's consider an application that stores videos. Before storing the video it needs to be compressed using a specific compression algorithm, such as MOV or MP4, then, if necessary, apply an overlay to the video, such as black and white or blur.

**State Pattern vs. Strategy Pattern**
The two patterns are similar in practice. Differences are:
- states store a reference to the context object that contains them.
- states are allowed to replace themselves
- strategies only handle a single specific task while states provide the underlying implementation for everything the context object does.

**When to use the Strategy Pattern**
A good rule of thumb is when you have a class with a large number of conditional statements that switch between variants of the same algorithm. The logic can be extracted into different classes that implement the same interface.

**Pros and cons**
- satisfies the Open / Closed principle: Add new strategies without modifying the context.
- can swap algorithms used inside an object at runtime;
- (con) clients have to be aware of the different algorithms and use them appropriately

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

**When to use the Command Pattern**
Use the Command PAttern when you want to implement reversible operations. The command pattern is most popular for implementing undo / redo as it uses less memory than the Memento Pattern which has to backup the whole state of the object.

The Command pattern is great when you want to queue operations or schedule their execution, as command objects can be serialized and stored in databases or sent over networks, then, at a later time, converted to objects and executed.

**Pros and cons**
- satisfies the Single Responsibility Principle: classes than invoke operations are decoupled from the classes that execute these operations.
- satisfies the Open / Closed Principle: new commands can be added to the codebase without having to modify existing code.
- (con) code may become more complex when you're adding a new layer between senders and receivers.

## Template Method Pattern
The Template Method Pattern allows you to define a template method, or skeleton for an operation. The specific steps can then be implemented by subclasses.

**Example**: Suppose we're designing some software that will be installed on a machine that makes hot beverages. At the beginning we just had tea and coffee. After some feedback rom customers we decided to add some more beverages such as camomile tea.

**Template Method vs. Strategy Pattern**
Template Method Pattern:
- use the Template Method Pattern when you have an algorithm with a fixed structure but with certain steps that need do be customized or implemented differently by subclasses.
- this pattern is ideal when you want to define a common algorithm skeleton (template method) in a base class and allow classes to selectively override specific steps to provide their own implementation.
- the Template Method Pattern is suitable when the overall algorithm structure remains consistent, but specific parts of the algorithm can vary based on different contexts or requirements.

Strategy pattern
- use the sstrategy pattern when you want to create a family of interchangeable algorithms or behaviors and encapsulate each algorithm in its own class.
- this pattern is ideal when you need to dynamically select and switch between different algorythms at runtime depending on the situation or context.
- the Strategy Pattern is suitable when you want to decouple the client code from specific algorythm implementations allowing greater flexibility and extensibility.

**When to use the Template Method Pattern**
Use the Template Method Pattern when you want to allow clients to implement only particular steps in an algorithm, and not the whole algorithm. It's a good pattern to use when you have a bunch of classes with the same logic, or algorithm but with differences in a few steps. So if the algorithm changes, it only has to be modified in a single place - the base class.

**Pros and cons**
- reduces code duplication
- clients only allowed to modify certain steps in an algorithm, reducing the risk of breaking clients if the algorithm changes.
- (con) some clients may be limited by the provided template
- (con) template methods can be hard to maintain if they have a lot of steps

## Observer Pattern
The Observer Pattern involves an object, known as the subject maintaining a list of its dependent objects, called observers and notifying them automatically of any state changes.

The Observer pattern is also know as a publPublish and Subscribe (pub and sub). It publishes changes in its states and it's subscribers (observers) subscribe to those events.

## Mediator Pattern
The Mediator Pattern defined an object (the Mediator) that describes how a set of objects interact with each other, therefore reducing lots of chaotic dependencies between those objects.

**Example**: Let's say we have a blog that lists all of your posts and you can select a post and then edit that post's title.

## Chain of Responsibility Pattern
The Chain of Responsibility Pattern allows building a chain of objects to handle a request. A request passes through the chain of handlers, each capable of either handling the request or passing it to the next handler in the chain.

**Example**: Let's say that we hae a web page which contains some information that only admins of the website can access, such as a page that allows an admin to manage the website's users.

Say that a user makes a request to a website's server, but before returning the web page the user's data must ba validated (e.g trim the whitespaces arount uthe user entered data), authenticate the user (e.g. check that ther username and password are correct) and then log some information to the server about this request. If any of these steps fail then "access denied" is returned to the user.

## Visitor Pattern
The visitor Pattern separates the algorithms or behaviors from the objects on which they operate.

**Example**: Say that you are a developer for a marketing agency, thas different types of clients:
- Restaurants
- Law firms
- Retailers