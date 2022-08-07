namespace RNGesus;

/// <summary>
/// Defines the drop chance for an item
/// </summary>
public interface IDroppable : IRarity
{
    /// <summary>
    /// The chance of this item to drop. 0,0 - 100.0. the higher, the more likely to be dopped.
    /// A drop chance of 100 will categorize the item for the fallback pool (common item).
    /// </summary>
    public double DropChance { get; set; }
}