# RnJesus
![](md_images/nugeticon.png)

RnJesus is a small pseudo random drop generator.

It allows you to define a pool of items (class instances), each with a given rarity and drop chance and then pick one at random.

## How does it work?

RnJesus tries to pick an item from the pool with a rarity >= 1. If no drop was generated it will pick one item with rarity=0 at random, ignoring it's drop chance (Fallback).

Let's assume you have the following rarities[^1]:

| Rarity | Integer value |
| ------ | ------------- |
| Common | 0             |
| Rare   | 1             |
| Epic   | 2             |

[^1]: RnJesus uses byte values for the rarity. The higher the number, the more rare the item. This way you can define up to 256 rarities 😲



For each rarity type there are X items, each with a specific drop chance:

| Item name  | Rarity | Drop chance |
| ---------- | ------ | ----------- |
| Sword      | Common | ignored     |
| Axe        | Common | ignored     |
| Rare Sword | Rare   | 50          |
| Rare Axe   | Rare   | 25          |
| Epic Sword | Epic   | 50          |
| Epic Axe   | Epic   | 25          |

RnJesus will now check the drops in the following order:

1. Check if next drop should be Epic

2. If so, check each Epic item's drop chance against RNG => If lucky, return item

3. If nothing found, check if next drop should be Rare

4. If so, check each Rare item's drop chance against RNG => If lucky, return item

5. No special drop done, so choose one Common item (fallback) and return it. RnJesus will not look at it's dropchance and relies purely on `System.Runtime.Random` 

   

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

Create an instance of RnJesus and assign the item pool:

```csharp
RnJesus<Item> rng = new(items, new Random());
```

Drop a new item:

```csharp
var droppedItem = rng.Next();
```

That's it 🙃 See the ConsoleTest Project in the repository for a more complete example.



## Luck

The `RnJesus.Next()` method takes an optional `double` parameter called `luck`. It adds to each item's drop chance. This is an easy way to increase the chance of dropping items with a higher rarity:

```csharp
var droppedItem = rng.Next(10d);
```



