# Singleton Pattern

The singleton pattern is a creational design pattern that ensures a class has only onre instance and provides a global point of access to that instance. The single instances is commonly used for managing shared resources, configuration settings, or logging functionality within an application.

A common of the Singleton pattern is to use a single global instance of a database object throughout an application. This means that all clients that need to connect to a database will retrieve the same database object, And not be creating new, separate ones. The database object is only created once, the first time it is needed, and then all other clients that need to connect and query the database will use this same object.

This provides the following benefits:
- **Resource Efficiency**: Database connections and resources are tipically limited and can be difficult to establish. By using a single instance of a database object you minimixe the overhead of creating and managing multiple connections, optimizing resource utilization.
- **Consistency and State MAnagement**: Having a single database instance ensures consistent state management and transaction handling accross different parts of the application. Changes made to the database state are visible universally within the application, avoiding inconsistencies that could arise from multiple database instances.
- **Simplified Configuration and Management**: With a singleton database instance, configuration and settings such as connection parameters, credentials and initialization logic are centralized and managed in one place. This simplifiez application setup and management.
- **Performance Optimization**: By reusing a single database instance you can optimize query performance and reduce latency associated with establishing new connections or reinitializing database resources.

The Singleton pattern is great for storing app configuration settings, logging configuration, session information, authentication tokens - and making this information available globally via a single instance, ensuring that it is the same throughout the app.

**Example**: Say that we need to keep an `AppSettings` object that stores global variables such as the name of the app, the database configurations (e.g. the database we are using, username, password) and logger settings (e.g. the filer location of our log file, the format - e.g. text vs JSON). We need to create only one instance of this object throughout our app to ensure that it only needs to be configured once in one place, and to ensure consistancy throughout the app.

![Singleton Pattern - UML](singleton_uml.svg)