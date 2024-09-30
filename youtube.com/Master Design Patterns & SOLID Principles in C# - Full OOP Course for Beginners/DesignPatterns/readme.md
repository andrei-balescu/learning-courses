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

## Strategy Pattern
The strategy pattern is used to pass different apgorythms or behaviors to an object.

**Example**: Let's consider an application that stores videos. Before storing the video it needs to be compressed using a specific compression algorythm, such as MOV or MP4, then, if necessary, apply an overlay to the video, such as black and white or blur.

**State Pattern vs. Strategy Pattern**
The two patterns are similar in practice. Differences are:
- states store a reference to the context object that contains them (<span style="color:red">!?</span>).
- states are allowed to replace themselves
- strategies only handle a single specific task while states provide the underlying implementation for everything the context object does.

**When to use the Strategy Pattern**
A good rule of thumb is when you have a class with a large number of conditional statements that switch between variants of the same algorythm. The logic can be extracted into different classes that implement the same interface.

**Pros and cons**
- satisfies the Open / Closed principle: Add new strategies without modifying the context.
- can swap algorythms used inside an object at runtime;
- (con) clients have to be aware of the different algorythms and use them appropriately
- (con) if you only have a few algorythms that rarely change, then using the Strategy Pattern can be over-engineering.