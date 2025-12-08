using System;

namespace MyProject.Models
{
    // This is the base class for all objects in the world
    // Resources, Units, Buildings, and Terrain inherit from this
    public abstract class WorldObject
    {
        // Name of the object
        public string Name { get; protected set; }

        // Object Health
        public int Health { get; protected set; }

        // Max Health of object
        public int MaxHealth { get; protected set; }

        // Determines whether the object has died/been destroyed
        public bool IsDead => Health <= 0;

        // Constructor sets initial name and health
        public WorldObject(string name, int health = 100)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
        }

        // Called every game tick/turn to update the object
        public virtual void Update(Random rng)
        {
            // Passive regeneration logic
            if (Health < MaxHealth && rng.NextDouble() < 0.05)
            {
                Health += 1;
            }
        }

        // Applies damage to the object
        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Console.WriteLine($"{Name} has been destroyed.");
            }
        }

        // Returns the object's status as a string
        public virtual string GetStatus()
        {
            return $"{Name} | HP: {Health}/{MaxHealth}";
        }
    }
}



