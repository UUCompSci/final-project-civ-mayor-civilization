// Resources are items that can be gathered in the world like wood, stone, and food.
using System;  
system.Collections.Generic; 
public class Resource : WorldObject
{
    public string ResourceType { get; private set; }
    public int Quantity { get; private set; }
    public Resource(string name, string resourceType, int quantity) : base(name)
    {
        ResourceType = resourceType;
        Quantity = quantity;
    }  
    public void Gather(int amount)
    {
        if (amount <= Quantity)
        {
            Quantity -= amount;
        }
        else
        {
            Quantity = 0;
        }
    }
    public override void Update(Random rng)
    {
        base.Update(rng);
        // Resources might regenerate over time
        if (rng.NextDouble() < 0.1) // 10% chance to    regenerate
        {
            Quantity += rng.Next(1, 6); // Regenerate 1 to 5 units
        }         
    } 
}
    // may not need the random rng stuff
    // decide on resource depletion/regeneration mechanics if need be. 

