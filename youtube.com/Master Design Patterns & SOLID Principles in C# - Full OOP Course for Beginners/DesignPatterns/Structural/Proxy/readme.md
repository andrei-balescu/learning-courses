# Proxy Pattern
The Proxy pattern is a structural design pattern that provides a proxy, or agent, object to control access to another object, allowing for additional functionality such as caching, logging, lazy loading or access control, without changing the client's code.

**Example**: Let's say we have an application that fetches a list of YouTube videos from YouTube's API, and displays them in a list. In our application we are using a 3rd-party YouTube package to handle fetching YouTube videos from the API, then rendering the videos on screen with the video controls.