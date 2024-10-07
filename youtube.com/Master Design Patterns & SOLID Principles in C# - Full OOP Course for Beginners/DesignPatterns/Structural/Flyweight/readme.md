# Flyweight Pattern
The Flyweight pattern is a structural design pattern that aims to memorize memory usage by sharing common state between multiple objects allowing efficient handling of large numbers light objects with shared characteristics.

**Example**: Sey that we have a farming game that includes multiple types of crops, such as potatoes, carrots and wheat. Each crop is represented by a crop object, that includes itd x and y coordinates, the type of crop and an icon. 

While the item's position (x, y) - (extrinsic state -> can change within the object's lifecycle - dynamic) is specific to every object, the crop type and icon (intrinsic state - constant values) can be shared among objects. Also, the icons can take up a lot of memory if duplicated within each object.

An object that only contains an intrinsic state is called a _Flyweight_