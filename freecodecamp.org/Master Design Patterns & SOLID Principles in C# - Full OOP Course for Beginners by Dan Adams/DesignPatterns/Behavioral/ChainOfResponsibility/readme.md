## Chain of Responsibility Pattern
The Chain of Responsibility Pattern allows building a chain of objects to handle a request. A request passes through the chain of handlers, each capable of either handling the request or passing it to the next handler in the chain.

**Example**: Let's say that we hae a web page which contains some information that only admins of the website can access, such as a page that allows an admin to manage the website's users.

Say that a user makes a request to a website's server, but before returning the web page the user's data must ba validated (e.g trim the whitespaces arount uthe user entered data), authenticate the user (e.g. check that ther username and password are correct) and then log some information to the server about this request. If any of these steps fail then "access denied" is returned to the user.