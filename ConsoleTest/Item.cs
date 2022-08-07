namespace RNGesus;

public class Item : IDroppable
{
    public double DropChance { get; set;}
    public byte Rarity { get; set; }

    public string Name { get; private init; } = string.Empty;

    public static List<Item> GetItems()
    {
        return new List<Item>()
        {
            new Item() { Name = "Common I", Rarity = (byte)ERarity.Common },
            new Item() { Name = "Common II", Rarity = (byte)ERarity.Common },
            new Item() { Name = "Common III", Rarity = (byte)ERarity.Common },
            new Item() { Name = "Common IV", Rarity = (byte)ERarity.Common },
            new Item() { Name = "Common V", Rarity = (byte)ERarity.Common },

            new Item() { Name = "Uncommon I", Rarity = (byte)ERarity.Uncommon, DropChance = 33},
            new Item() { Name = "Uncommon II", Rarity = (byte)ERarity.Uncommon, DropChance = 33 },
            new Item() { Name = "Uncommon III", Rarity = (byte)ERarity.Uncommon, DropChance = 10 },

            new Item() { Name = "Rare I", Rarity = (byte)ERarity.Rare, DropChance = 35},
            new Item() { Name = "Rare II", Rarity = (byte)ERarity.Rare, DropChance = 25 },
            new Item() { Name = "Rare III", Rarity = (byte)ERarity.Rare, DropChance = 10 },
    
            new Item() { Name = "Epic I", Rarity = (byte)ERarity.Epic, DropChance = 35},
            new Item() { Name = "Epic II", Rarity = (byte)ERarity.Epic, DropChance = 25 },
            new Item() { Name = "Epic III", Rarity = (byte)ERarity.Epic, DropChance = 10 },
            new Item() { Name = "Epic IV", Rarity = (byte)ERarity.Epic, DropChance = 0.1 },
    
            new Item() { Name = "Legendary I", Rarity = (byte)ERarity.Legendary, DropChance = 50},
            new Item() { Name = "Legendary II", Rarity = (byte)ERarity.Legendary, DropChance = 33 },
            new Item() { Name = "Legendary III", Rarity = (byte)ERarity.Legendary, DropChance = 10 },
            new Item() { Name = "Legendary IV", Rarity = (byte)ERarity.Legendary, DropChance = 0.1 },
        };
    }
}