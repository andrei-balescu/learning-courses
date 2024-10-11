# Interpreter Pattern
The Interpreter pattern defines ways to represent and evaluate sentences ain a language by using an abstract class for expressions, which concrete subclasses implement to interpret specific parts of the language's grammar.

**Example use cases**:
- Parsing and executing SQL queries, where the interpreter pattern helps parse the query and execute it against a database.
- Calculators or scientific software that interpret and evaluate complex mathematical formulas entered by users.
- Web frameworks that render HTML templates with embedded expressions or directives - i.e. templates (eg. `{{ variable }}` in Django or `<% expression %>` in JSP)

So let's say that we need to build a calculator app that takes some user input string and calculates the result. User input: `1 + 2 * 3` The interpreter will parse this input or "expression" into an Abstract Syntax Tree (AST) or "Expression tree".

**Components of the interpreter pattern**
- Abstract Expression: Establishes an interface for all the expressions within the language
- Terminal expression: Represents the fundamental components of the language, such as numbers or variables.
- Non-terminal Expression: Represents more complex expressions that are composed of other expressions using operators or functions.
- Interpreter: Implements the logic for interpretation and determines how to evaluate different types of expressions.

![Interpreter Pattern - UML](interpreter_uml.svg)