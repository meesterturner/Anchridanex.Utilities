# Anchridanex.Utilities #
A collection of semi-useful classes I've written primarily for my own use and entertainment.

## Coroutines ##
This is a system designed to replicate functionality similar to coroutines in Unity. The setup and operation is slightly different in practice to Unity, but this is to enable it to work with standard C#.

### Example ###
```csharp
public class Demo
{
    Coroutine co;

    private void StartCoroutine()
    {
        co = Coroutine.CreateAndStart(CoroutineFunction)
    }

    private void Update() // In context, this would be similar to Unity's Update method
    {
        co?.Execute(); // This will only execute the coroutine if the wait condition is met
    }

    private IEnumerator CoroutineFunction(Coroutine c)
    {
        Debug.WriteLine("Started");

        for (int i = 1; i <= 10; i++)
        {
            Debug.WriteLine(i);
            yield return new WaitForSeconds(1, c);
        }
    }
}
```

### Coroutine Class Public Methods ###
```static void CreateAndStart(Func<Coroutine, IEnumerator> coroutine)``` - Creates a new instance of the ```Coroutine``` class, and starts the specified coroutine function. The function must return ```IEnumerator``` and have a ```Coroutine``` parameter.

```void Start(Func<Coroutine, IEnumerator> coroutine)``` - Starts the specified coroutine function. The function must return ```IEnumerator``` and have a ```Coroutine``` parameter.

```void Execute()``` - Attempts to execute the previously specified coroutine function, provided that the yield condition has been met and has not been stopped.

```void Stop()``` - Stops execution of the current coroutine.

### Yield Conditions ###
All the conditions for yielding derive from ```YieldConditionBase```. The included conditions are:

```WaitForExecutions(int executions, Coroutine coroutine)``` - Waits for the ```Execute()``` method to be called the specified number of times before releasing.

```WaitForSeconds(float seconds, Coroutine coroutine)``` - Waits for the specified amount of seconds before releasing.

```WaitUntilTime(DateTime time, Coroutine coroutine)``` - Waits until the specified date and time has been met before releasing. Parameter ```time``` must be in the future.

## Logging ##
This is a simple logging framework to enable logging capabilities to other systems. The actual logging implementation needs to be a class derived from ```LoggerBase```. 

### Example ###
```csharp
// Example utilising the Godot Engine
public class GodotLog : LoggerBase
{
    public override void WriteToLogWorker(LogSeverity sev, string message)
    {
        if (sev == LogSeverity.Error)
            GD.PrintErr(message);
        else
            GD.Print(message);
    }
}

public class Game
{
    public void Start()
    {
        LogEngine.Logger = new GodotLog();
        LogEngine.Logger.Severities = LogEngine.Logger.AllSeverities;
        LogEngine.LogInformation("Godot Log Started");
        LogEngine.LogError("We have found an error!");
    }
}
```

### LogEngine ###
```static LogSeverity DefaultSeverity { get; set; }``` - Specifies the default severity when one is not specified. Default is ```Debug```.



```static void Log(LogSeverity sev, string message)``` - Adds a log entry of the given severity, with the provided message. This is also called via the following methods:

* ```static void LogInformation(string message)```
* ```static void LogWarning(string message)```
* ```static void LogError(string message)```
* ```static void LogDebug(string message)```
* ```static void Log(string message)```

Note that the overload for ```Log(string message)``` will add a log entry using the default severity.

```static void LogFunctionCall()``` - This would be useful in logging the exact method or function name generating the log event.

### LoggerBase ###
```List<LogSeverity> Severities { get; set; }``` - Specifies the levels you wish to log. The default in the base class is a blank list, therefore no events would be logged. This can be overridden in the derived class.

```bool WriteToDisk { get; set; }``` - If ```true```, will also write the log event to disk if desired. Default is ```false```. Additionally, the log folder must be set in the constructor. The log files's name inside the folder will contain the date and time the instance of the logging class was started, for example ```Log_2025-03-01_120102.txt```.

```List<LogSeverity> AllSeverities { get; }``` - Returns a list of all possible severity levels.

```List<LogSeverity> NoSeverities { get; }``` - Returns an empty list of severities. To be used for convenience with the ```Severities``` property.

```void WriteToLog(LogSeverity sev, string message)``` - Writes the event to the log. This is not inteded to be used directly, but can be. Should be used through the static functions provided in ```LogEngine```.

## Maps ##
This is a collection of classes designed to be able to track two-dimensional grid maps, for both traditional grids using rectangular cells, and hexagonal grids.

### HexagonMap (extends MapBase) ###
Implementation of a hexagon-shaped 2D map. Hexagons are rotated to have a flat top. Along the top row, odd numbered columns (remembering 1 is the first column) are positioned lower, and even numbered columns are higher.

### HexagonMapFlatSides (extends MapBase) ###
As HexagonMap, but hexagons are rotated so that the left and right sides are flat. 

### IMapCell ###
This interface is to be used to implement the class which will hold the cell-specific data. 

### MapAdjacentDirection ###
Enum used by the MapBase class to determine the relative location of neighbouring cells.

### MapVector2 ###
Contains X and Y coordinates for cells. This has been implemented so as to standardise coordinate types which are different between different systems, for example: the built in .net Point class, and Vector2 types in engines such as Unity and Godot. 

### MapBase ###
The abstract base class for all maps. When referring to grid positions, the top-left corner is position 1, 1. Size of map is to be specified in the constructor, for example:

```MapConcreteClass map = new MapConcreteClass(15, 20);```
would produce a map of type MapConcreteClass, of size 15 cells wide and 20 cells high. (This assumes that MapConcreteClass is derived from MapBase)

#### Public Properties ####
```int Width { get; init; }``` - Width of grid as number of cells

```int Height { get; init; }``` - Height of grid as number of cells

#### Public Methods ####
```void AddCell(T c)``` - Adds the cell data instance of T to the available cells.

```T? GetCell(MapVector2 position)``` - Returns the cell data instance located at the given position. If no defined cell data exists, null is returned.

```abstract MapVector2? GetAdjacentCoordinate(MapVector2 position, MapAdjacentDirection direction)``` - Implemented by the derived class to return the MapVector2 position of the neighbouring cell in the given direction, starting from the given position.

```T? GetAdjacentCell(T cell, MapAdjacentDirection direction)``` - Returns the neighbouring cell to the given cell in the specified direction. Returns null if there is no neighbouring cell.

```List<T> GetAdjacentCells(T cell)``` - Returns all the adjacent defined cells to the cell specified. Null positions are ignored.

```List<MapVector2> GetAdjacentCoordinates(MapVector2 position)``` - Returns a list of valid neighbouring coordinates to the given position.

```List<T> GetAllNonNullCells()``` - Returns all defined cell data instances previously added

### RectangleMap (extends MapBase) ###
Map consisting of traditional four-sided (square or rectangle) cells.

## Other Classes ##
### LruCache ###
Provides a simple Last Recently Used style cache for function input and output. Default capacity is 100 entries, but this can be changed in the constructor.

```csharp
// TKey could be considered to be the input for the function being cached
// TValue is the output of the function
LruCache<TKey, TValue> cache = new();
```

#### Example Usage ####
```csharp
public int Test()
{
    // Cache all the answers for the formula where the starting
    // values are 1 to 100 inclusive
    LruCache<int, int> cache = new();
    for (int i = 1; i <= 100; i++)
    {
        cache[i] = Answer(i);
    }

    // Return the cached result for the 79th iteration (result is 158)
    return cache[79];  
}

private int Answer(int i)
{
    return i + 79;
}
```

#### Public Methods ####
```get[TKey key]``` - Returns the value for the specified key. If the value does not exist in the cache, the default for the type is returned. In many cases, ```TryGetValue()``` may be more appropriate to use.

```set[TKey key, TValue value]``` - Add the given key/value pair to the cache. The oldest cached value will be pushed out if adding this value exceeds the capacity. Same as calling ```Add()```.

```void Add(TKey key, TValue? value)``` - Add the given key/value pair to the cache. The oldest cached value will be pushed out if adding this value exceeds the capacity.

```bool TryGetValue(TKey key, out TValue? value)``` - Attempt to retrieve the value for the given key. Value is returned via the output parameter ```value```. Function returns true if the value is contained in the cache, otherwise false.

### Pairing ###
Creates a pair of objects to be able to be easily compared. As this class is generic, objects must be of same type. The following code sets out an example which will be used for demonstrating method usage:

```csharp
Car car1 = new Car("Seat", "Ibiza");
Car car2 = new Car("Peugeot", "2008");
Car car3 = new Car("BMW", "3-Series");

Pairing<Car> carPair = new();
carPair.First = car1;
carPair.Second = car2;
```

#### Public Properties ####
```T? First { get; set; }``` - First item of pair

```T? Second { get; set; }``` - Second item of pair

#### Public Methods ####
```bool MatchesInExactOrder(T? one, T? two)``` - Checks to see if the two objects specified are both instances in the pair. ```one``` must match the first item, ```two``` must match the second item.

```csharp
bool match;
match = MatchesInExactOrder(car1, car2); // Returns true
match = MatchesInExactOrder(car2, car1); // Returns false (wrong order)
match = MatchesInExactOrder(car1, car3); // Returns false (second item not in pair)
```

```bool MatchesInEitherOrder(T? one, T? two)``` - Checks to see if the two objects specified are both instances in the pair. It does not matter which order they are specified in the pairing or in the parameters, as long as both objects are present in the pair.

```csharp
bool match;
match = MatchesInEitherOrder(car1, car2); // Returns true
match = MatchesInEitherOrder(car2, car1); // Returns true
match = MatchesInEitherOrder(car1, car3); // Returns false (second item not in pair)
```

```bool Contains(T? either)``` - Checks to see if the object specified in the parameter ```either``` matches at least one of the first or second item in the pair.
```csharp
bool match;
match = Contains(car1); // Returns true
match = Contains(car2); // Returns true
match = Contains(car3); // Returns false
```

```bool DoesNotContain(T? neither)``` - Checks to see if the object specified in ```neither``` does not exist in the pair.
```csharp
bool match;
match = DoesNotContain(car1); // Returns false
match = DoesNotContain(car2); // Returns false
match = DoesNotContain(car3); // Returns true
```

### RngUtil ###
Some miscellaneous randomisation functions. Prior to v1.2.0, all methods in RngUtil were static. This has now been changed to use all instance methods.

#### Public Methods ####
```void SetSeed(int seed)``` - Re-instances the random class with a new seed.

```int Next(int minValue, int maxValueExclusive)``` - Returns the next randomised integer that is >= ```minValue```, and < ```maxValueExclusive```.

```int NextInclusive(int minValue, int maxValueInclusive)``` - Returns the next randomised integer that is >= ```minValue```, and <= ```maxValueInclusive```.

```T? RandomItemFrom<T>(List<T> items)``` - Returns a random item from the given list. Returns null if ```items``` is null or contains zero items.

```T? RandomItemFrom<T>(List<T> items, bool removeFromList)``` - Returns a random item from the given list, and also removes the item from the list if removeFromList is true. Returns null if ```items``` is null or contains zero items.

```T? RandomItemFrom<T>(List<T> items, bool removeFromList, out int index)``` - As above, but also outputs the original index of the item from the list (which is before removal, if removeFromList is true). index returns -1 if the list is null or contains zero items.

```T? RandomItemFrom<T>() where T : Enum``` - Returns a random enum from the specified enum type.

### Singleton ###
Helper class to remove boilerplate code for classes intended to be used as singletons.

#### Example Usage ####
```csharp
public class MySingleton : Singleton<MySingleton>
{
    public int Value { get; set; }
    public string Name { get; set; }
}

public class Test
{
    public void TestMethod()
    {
        MySingleton.Instance.Value = 123;
        MySingleton.Name.Value = "Brian Griffin";
    }

    public void TestInstancing()
    {
        // Below will result in an InvalidOperationException
        MySingleton inst = new MySingleton(); 
    }
}
```