# Iterator Pattern
The Iterator Pattern provides a way of iterating over an object without having to expose the object's internal structure., which may change in the future. Changing the internals of an object should not affect its consumers.

**Example**: Let's create a shoppiing list where we can add and remove items.

**When to use the Iterator Pattern**
Employ the iterator when your collection posseses a complex data structure or when the data structure is likely to change, so the clients can iterate over the collection without knowledge of the data structure.

_NOTE_: The .net `IEnumerable` implements the iterator pattern

**Pros and cons**
- satisfies the Single Responsability Principle: traversal logic is separated into external classes
- satisfies the Open / Closed Principle: You can create new collections and new iterators without breaking the code that uses them
- (con) can be over-engineering if the app only works with simple collections.

![Iterator Pattern - UML](iterator_uml.svg)