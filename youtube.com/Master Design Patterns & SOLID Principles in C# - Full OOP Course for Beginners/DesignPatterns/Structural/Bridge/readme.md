# Bridge Pattern
The Bridge pattern is a design pattern that separates a large class or a set of related classes in two separate hierarchies that can evolve independently.

**Example**: Say that we have a remote for controlling a radio. There are multiple brands of remote and there are different brands of radio. 

We have a hierarchy that is growing in two dimensions: type of remote (abstract) and brand (concrete). This can be split into tow separate hierarchies that can grow independently. These separate hierarchies are connected (bridged).