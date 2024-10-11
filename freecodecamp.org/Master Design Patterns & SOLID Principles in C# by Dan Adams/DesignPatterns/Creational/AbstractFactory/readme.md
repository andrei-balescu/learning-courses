# Abstract Factory Pattern
The Abstract Factory pattern is a creational design pattern that provides an interface for creating families of related objects without specifying their concrete classes, promoting encapsulation and allowing for creation of object families that can vary independently.

**Example**: Say that you have an app for Windows and Mac. The UI componsnts - such as buttons, checkboxes and textboxes - are different for each operating systm, but each type of UI component will have the same behaviors - e.g. a checkbox will have an `onSelect` method.

So our app needs a way of knowing what the current operating system is, and then select the appropriate family of objects for that operating system.

![Abstract Factory Pattern - UML](abstract_factory_uml.svg)