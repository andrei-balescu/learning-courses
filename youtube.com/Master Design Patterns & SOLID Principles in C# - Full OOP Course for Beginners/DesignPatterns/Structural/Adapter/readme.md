# Adapter Pattern
The Adapter Patttern is a structural design pattern that allows incompatible interfaces between coasses to work together by providing a wrapper that translates one interface to another.

**Example**: Say you have a video editing application that allows users to upload a video and then change the color of a video. The application provides preset colors that users can select such as "black and white" pr "midnight purple". We then need to add more colors to the app only available through a 3rd party library. However this library fololows a different interface than the one we are using in our app.

![Adapter Pattern - UML](adapter_uml.svg)