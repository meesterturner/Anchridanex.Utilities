# Anchridanex.Utilities #
A collection of semi-useful classes I've written primarily for my own use and entertainment.

## Coroutines ##

## Logging ##

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
would produce a map of type MapConcreteClass, of size 15 cells wide and 20 cells high.

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

### Pairing ###
Creates a pair of objects to be able to be easily compared. As this class is generic, objects must be of same type. The following code sets out an example which will be used for demonstrating method usage:

```
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

```
bool match;
match = MatchesInExactOrder(car1, car2); // Returns true
match = MatchesInExactOrder(car2, car1); // Returns false (wrong order)
match = MatchesInExactOrder(car1, car3); // Returns false (second item not in pair)
```

```bool MatchesInEitherOrder(T? one, T? two)``` - Checks to see if the two objects specified are both instances in the pair. It does not matter which order they are specified in the pairing or in the parameters, as long as both objects are present in the pair.

```
bool match;
match = MatchesInEitherOrder(car1, car2); // Returns true
match = MatchesInEitherOrder(car2, car1); // Returns true
match = MatchesInEitherOrder(car1, car3); // Returns false (second item not in pair)
```

```bool Contains(T? either)``` - Checks to see if the object specified in the parameter "either" matches at least one of the first or second item in the pair.
```
bool match;
match = Contains(car1); // Returns true
match = Contains(car2); // Returns true
match = Contains(car3); // Returns false
```

```bool DoesNotContain(T? neither)``` - Checks to see if the object specified in "neither" does not exist in the pair.
```
bool match;
match = DoesNotContain(car1); // Returns false
match = DoesNotContain(car2); // Returns false
match = DoesNotContain(car3); // Returns true
```

### RngUtil ###
Some miscellaneous randomisation functions, using a static ```Random``` class.

#### Public Methods ####
```static void SetSeed(int seed)``` - Re-instances the static random class with a new seed.

```static int Next(int minValue, int maxValueExclusive)``` - Returns the next randomised integer that is >= minValue, and < maxValueExclusive.

```NextInclusive(int minValue, int maxValueInclusive)``` - Returns the next randomised integer that is >= minValue, and <= maxValueInclusive.

```static T? RandomItemFrom<T>(List<T> items)``` - Returns a random item from the given list.

```static T? RandomItemFrom<T>(List<T> items, bool removeFromList)``` - Returns a random item from the given list, and also removes the item from the list if removeFromList is true.

```static T? RandomItemFrom<T>(List<T> items, bool removeFromList, out int index)``` - As above, but also outputs the original index of the item from the list (which is before removal, if removeFromList is true). index returns -1 if the list is null or contains zero items.

```static T? RandomItemFrom<T>() where T : Enum``` - Returns a random enum from the specified enum type.

### Singleton ###
Helper class to remove boilerplate code for classes intended to be used as singletons.

#### Example Usage ####
```
public class MySingleton : Singleton<MySingleton>
{
    public int Value { get; set; }
    public string Name {get; set; }
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