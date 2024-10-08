# Strategy Pattern
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