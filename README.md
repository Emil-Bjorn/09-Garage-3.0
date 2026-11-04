## Changes from 2.0
- Change design of program to more closely follow an MVC design allowing for better separation of concerns. This includes new UI and controller classes. 
- Add interfaces to Garage and Vehicle classes, as well as interfaces for new UI, Controller, and GarageHandler classes.
- Garage is now a generic class that implements IEnumerable meaning foreach and LINQ can be called on it directly.
- Add feature that allows user to list vehicles by type (via LINQ)

## Todo
- Due to a lack of time I have added only one unit test. More extensive unit testing would be nice to have.
- AddVehicle function in controller is still very large. Need to break it down into smaller helper functions. 
