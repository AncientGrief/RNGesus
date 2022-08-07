namespace RNGesus;

/// <summary>
/// A random drop genrator based on a set of items categorized by rarity and their individual drop chances
/// </summary>
/// <typeparam name="T">Type of item</typeparam>
public class RNGesus<T> where T : IDroppable
{
    private readonly IList<T> items;
    private readonly IList<T> commonItems;
    private readonly Random rng;

    /// <summary>
    /// RNGesus calculated factors for each rarity found in the Droppable pool. Can be altered.
    /// </summary>
    public Dictionary<byte, double> RarityFactors { get; set; }

    /// <summary>
    /// Generates a new instance of RNGesus
    /// </summary>
    /// <param name="droppables">The object pool of all droppable items</param>
    /// <param name="rng">Random Number Generator instance</param>
    /// <exception cref="ArgumentException">If not at least 1 object with rarity=0 is given</exception>
    public RNGesus(IEnumerable<T> droppables, Random rng)
    {
        this.rng = rng;

        items = droppables.ToList();
        
        RarityFactors = items.GroupBy(x => x.Rarity).Select(g => g.Key)
            .ToDictionary(x => x, x => x == 0 ? 100d : 50d / x);
        
        commonItems = items.Where(x => x.Rarity == 0).ToList();

        if (commonItems.Count == 0)
            throw new ArgumentException(
                "No droppable found with Rarity=0. Please ensure there is at least one object with Rarity 0 to be used as a fallback.");

    }

    /// <summary>
    /// Return the next random item. Rarities are checked in reverse order. The highest rarity gets checked first!
    /// If nothing could be found, an item with Raritiy=0 will be choosen at random, ignoring the DropChance.
    /// </summary>
    /// <param name="luck">Optional luck, which will add to the drop chance for each rarity</param>
    /// <returns>A random item</returns>
    public T Next(double? luck = 0d)
    {
        //Try dropping a non common item. Rarity = 0 = 100d is common, so skip it
        foreach (KeyValuePair<byte, double> rarity in RarityFactors.Where(x => x.Key < 100d).OrderBy(x => x.Key))
        {
            if (!(rarity.Value + luck > rng.NextDouble() * 100))
                continue;

            foreach (T item in items.Where(x => x.Rarity == rarity.Key))
            {
                if (item.DropChance > rng.NextDouble() * 100)
                {
                    return item;
                }
            }
        }

        //If no rare item was found, drop a common one
        return items.Where(x => x.Rarity == 0).ElementAt(rng.Next(0, commonItems.Count));
    }
}
