# Singletons
A lightweight singleton system for small-scale projects:
The SingletonBase base class ensures that only a single instance of the singleton exists.
GlobalBootstrapManager handles the creation of global singletons regardless of the starting scene and applies the DontDestroyOnLoad modifier to them.
All other singletons remain local and are destroyed along with the current scene.
This approach mimics a DI (Dependency Injection) LifetimeScope.
There is no game logic in this project; it serves purely as an example of a singleton system.
