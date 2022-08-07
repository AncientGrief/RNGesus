using System.Text;
using RNGesus;

Console.OutputEncoding = Encoding.UTF8;

List<Item> items = Item.GetItems();
RNGesus<Item> rng = new(items, new Random());

Dictionary<Item, int> DropCounter = new ();
DropCounter = items.ToDictionary(x => x, x => 0);

// ----- Change default drop rates ----

rng.RarityFactors[(byte)ERarity.Uncommon] = 80;
rng.RarityFactors[(byte)ERarity.Rare] = 35; 
rng.RarityFactors[(byte)ERarity.Epic] = 10; 
rng.RarityFactors[(byte)ERarity.Legendary] = 0.01;

int DropAmount = 100000;
for (int i = 0; i < DropAmount; i++)
{
    DropCounter[rng.Next()]++;
}

//------ Output ----

Console.WriteLine($"{DropAmount:n0} drops");
Console.WriteLine("-------------------------------------------");
Console.WriteLine("|{0,-20}|{1,-20}|", "Rarity", "Drop Count");
Console.WriteLine("-------------------------------------------");
foreach (var item in DropCounter.GroupBy(x => x.Key.Rarity).ToDictionary(x => x.Key, x => x.Sum(x => x.Value)))
{
    Console.WriteLine("|{0,-20}|{1,-20:n0}|", Enum.Parse<ERarity>(item.Key.ToString()), item.Value);
}

Console.WriteLine("-------------------------------------------");
Console.WriteLine("|{0,-20}|{1,-20}|", "Item", "Drop Count");
Console.WriteLine("-------------------------------------------");
foreach (var item in DropCounter)
{
    Console.WriteLine("|{0,-20}|{1,-20:n0}|", item.Key.Name, item.Value);
}
Console.WriteLine("-------------------------------------------");
Console.WriteLine();
Console.WriteLine("Droprates:");
foreach (var droprates in rng.RarityFactors)
{
    Console.WriteLine($"{Enum.Parse<ERarity>(droprates.Key.ToString())} => {droprates.Value}");
}

