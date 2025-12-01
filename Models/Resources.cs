using System;

public class Resource : WorldObject
{
    public string ResourceType { get; private set; }
    public int Quantity { get; private set; }

    public Resource(string name, string resourceType, int quantity)
        : base(name)
    {
        ResourceType = resourceType;
        Quantity = quantity;
    }

    /// The player or unit gathers a specified amount.
    /// If not enough remains, they gather what's left.
    public int Gather(int amount)
    {
        int gatheredAmount = Math.Min(amount, Quantity);
        Quantity -= gatheredAmount;
        return gatheredAmount;
    }


    /// This is the regeneration system.
    /// Wood regrows slowly; stone/metal hardly regrow; food grows fast.

    public override void Update(Random rng)
    {
        base.Update(rng);

        // Regeneration behavior by type
        switch (ResourceType.ToLower())
        {
            case "wood":
                if (rng.NextDouble() < 0.15)   // 15% chance
                    Quantity += rng.Next(1, 4);
                break;

            case "food":
                if (rng.NextDouble() < 0.30)   // 30% chance
                    Quantity += rng.Next(2, 6);
                break;

            case "stone":
            case "metal":
                // Rare regeneration
                if (rng.NextDouble() < 0.05)   // 5% chance
                    Quantity += 1;
                break;
        }
    }

    public override string GetStatus()
    {
        return $"{Name} - {ResourceType} | Qty: {Quantity}, Health: {Health}";
    }
}
