# RNGesus
[![Nuget](https://img.shields.io/nuget/v/RNGesus)](https://www.nuget.org/packages/RNGesus/) [![Nuget](https://img.shields.io/nuget/dt/RNGesus)](https://www.nuget.org/packages/RNGesus/)

![](RNGesus/nugeticon_small.png)

RNGesus is a small pseudo random drop generator.

It allows you to define a pool of items (class instances), each with a given rarity and drop chance and then pick one at random.

## How does it work?

RNGesus tries to pick an item from the pool with a rarity >= 1. If no drop was generated it will pick one item with rarity=0 at random, ignoring it's drop chance (Fallback).

Let's assume you have the following rarities[^1]:

| Rarity | Integer value |
|--------|---------------|
| Common | 0             |
| Rare   | 1             |
| Epic   | 2             |

[^1]: RNGesus uses byte values for the rarity. The higher the number, the more rare the item. This way you can define up to 256 rarities 😲



For each rarity type there are X items, each with a specific drop chance:

| Item name  | Rarity | Drop chance |
|------------|--------|-------------|
| Sword      | Common | ignored     |
| Axe        | Common | ignored     |
| Rare Sword | Rare   | 50          |
| Rare Axe   | Rare   | 25          |
| Epic Sword | Epic   | 50          |
| Epic Axe   | Epic   | 25          |

RNGesus will now check the drops in the following order:

1. Check if next drop should be Epic
2. If so, check each Epic item's drop chance against RNG => If lucky, return item
3. If nothing found, check if next drop should be Rare
4. If so, check each Rare item's drop chance against RNG => If lucky, return item
5. No special drop done, so choose one Common item (fallback) and return it. RNGesus will not look at it's dropchance and relies purely on `System.Runtime.Random` 

## Usage

Create your Items you want to drop and implement the `IDroppable` interface, e.g:

```csharp
public class Item : IDroppable
{
    public double DropChance { get; set;}
    public byte Rarity { get; set; }

    public string Name { get; set; } = string.Empty;
}
```

Create a list of some items:

```csharp
List<Item> items = new List<Item>()
{
    new Item() { Name = "Common I", Rarity = 0 }, //Common
    new Item() { Name = "Common II", Rarity = 0 },//Common
    new Item() { Name = "Common III", Rarity = 0 },//Common
    new Item() { Name = "Rare I", Rarity = 1, DropChance = 40 },
    new Item() { Name = "Rare II", Rarity = 1, DropChance = 20 },
    new Item() { Name = "Epic I", Rarity = 2, DropChance = 35},
    new Item() { Name = "Epic II", Rarity = 2, DropChance = 20},
    new Item() { Name = "Legendary I", Rarity = 3, DropChance = 10},
    new Item() { Name = "Legendary II", Rarity = 3, DropChance = 0.01},
}
```

Create an instance of RNGesus and assign the item pool:

```csharp
RNGesus<Item> rng = new(items, new Random());
```

Drop a new item:

```csharp
var droppedItem = rng.Next();
```

That's it 🙃 See the ConsoleTest Project in the repository for a more complete example.

## Rarity Factors

RNGesus generates a default factor for each rarity there is. 
This factor is the overall chance of items with this rarity get dropped.

You can overwrite the factors like this:

```
rngesus.RarityFactors[0] = 20;
rngesus.RarityFactors[1] = 10;
rngesus.RarityFactors[2] = 0.01;
//...
```

The default values are `50d / x` where x is the rarity. Rarity 0 defaults to 100d and should not be changed, otherwise you may run into a NullReferenceException.

| Rarity | Default factor |
|--------|----------------|
| 0      | 100            | 
| 1      | 50             | 
| 2      | 25             | 
| 3      | 16.666         | 
| 4      | 12.5           | 
| ...    | ...            | 

## Luck

The `RNGesus.Next()` method takes an optional `double` parameter called `luck`. It adds to each item's drop chance. This is an easy way to increase the chance of dropping items with a higher rarity:

```csharp
var droppedItem = rng.Next(10d);
```

## Example with100,000 drops

The ConsoleTest programm generates 100,000 drops and these are the results of one run with the following rarity factors:

- Common => 100
- Uncommon => 80
- Rare => 35
- Epic => 10
- Legendary => 0.01

| Rarity    | Drop Count |
|-----------|------------|
| Common    | 39781      |
| Uncommon  | 47643      |
| Rare      | 10179      |
| Epic      | 2392       |
| Legendary | 5          |


| Item          | Drop Count    |
|---------------|---------------|
| Common I      | 8146          |
| Common II     | 7879          |
| Common III    | 7898          |
| Common IV     | 7896          |
| Common V      | 7962          |
| Uncommon I    | 26410         |
| Uncommon II   | 17618         |
| Uncommon III  | 3615          |
| Rare I        | 6329          |
| Rare II       | 2932          |
| Rare III      | 918           |
| Epic I        | 1456          |
| Epic II       | 715           |
| Epic III      | 220           |
| Epic IV       | 1             |
| Legendary I   | 3             |
| Legendary II  | 2             |
| Legendary III | 0             |
| Legendary IV  | 0             |

