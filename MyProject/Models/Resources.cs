using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    // Represents a resource node in the world. Units can gather from these.
    public class Resource : WorldObject
    {
        [Key] // EF Core primary key
        public int Id { get; set; }

        // Type of resource stored in this object
        public string ResourceType { get; private set; }

        // Current quantity available
        public int Quantity { get; private set; }

        // Constructor sets the name, type, and starting quantity
        public Resource(string name, string resourceType, int quantity) : base(name)
        {
            ResourceType = resourceType.ToLower();
            Quantity = quantity;
        }

        // Parameterless constructor required by EF Core
        public Resource() : base("Unknown") { }

        // Returns true if the resource has been fully depleted
        public bool IsDepleted()
        {
            return Quantity <= 0;
        }

        // Allows a unit to gather some amount of this resource
        public int Gather(int amount)
        {
            if (IsDepleted()) return 0;
            int taken = Math.Min(amount, Quantity);
            Quantity -= taken;
            return taken;
        }

        // Simulates passive resource regeneration each world update
        public override void Update(Random rng)
        {
            base.Update(rng);
            switch (ResourceType)
            {
                case "wood":
                case "apples":
                case "deer meat":
                case "fish":
                case "fox meat":
                case "owl meat":
                    if (rng.NextDouble() < 0.30)
                        Quantity += rng.Next(1, 5);
                    break;
                case "stone":
                case "metal":
                case "limestone":
                case "sand":
                    if (rng.NextDouble() < 0.10)
                        Quantity += rng.Next(1, 3);
                    break;
                case "cactus":
                case "water":
                    if (rng.NextDouble() < 0.20)
                        Quantity += rng.Next(1, 4);
                    break;
            }
        }

        // Returns a readable status string for display
        public override string GetStatus()
        {
            return $"{Name} - {ResourceType} | Qty: {Quantity}, Health: {Health}";
        }
    }
}



