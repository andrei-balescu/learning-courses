# Memento Pattern
The memento pattern is used to restore an object to a previous state.

**Example**": A common use case for the Memento Pattern is when implementing an undo feature. For example most text editors have undo features where you can undo actions by pressing Ctrl + Z.

**When to use the Memento Pattern**
The memento pattern can be used when you want to produce snapshots of an object's state to be able to restore an object to a previous state. It is commonly used for implementing the undo feature and so provides a common solution that a team of developers can quickly understand and get on the same page with.

**Pros and cons of the Memento Pattern**
- You can simplify the originator's code by letting the caretaker maintain the history of the originator's state. satisfying the Single Responsibility Principle
- The app might consume a lot of memory if lots of mementos are created.