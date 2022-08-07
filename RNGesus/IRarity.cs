namespace RNGesus;

/// <summary>
/// Interface defining the rarity/category of an item
/// </summary>
public interface IRarity
{
    /// <summary>
    /// The item's rarity, the higher the value, the more rare it is.
    /// Objects with a rarity of 0 will be used as a fallback (common item)
    /// </summary>
    public byte Rarity { get; set;}
}  


