using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    // Base class for all buildings
    public abstract class Building : WorldObject
    {
        [Key] // EF Core primary key
        public int Id { get; set; }

        // Defensive strength of the building
        public int DefensePower { get; protected set; }

        // Tracks turns passed for unit production timing
        public int TurnsSinceLastProduce { get; set; } = 0;

        // Constructor for buildings
        protected Building(string name, int health = 300) : base(name, health)
        {
            DefensePower = 10;
        }

        // Parameterless constructor required by EF Core
        protected Building() : base("Unknown") { }

        // This override is called every turn to update building state
        public override void Update(Random rng)
        {
            base.Update(rng);
            TurnsSinceLastProduce++;
        }

        // This allows buildings to optionally produce units
        public virtual Unit? ProduceUnit()
        {
            return null; // Buildings don't produce by default
        }
    }

    // Central base building that creates villagers
    public class TownCenter : Building
    {
        public TownCenter() : base("Town Center", 500)
        {
            DefensePower = 20;
        }

        // Produces a new villager every 3 turns
        public override Unit? ProduceUnit()
        {
            if (TurnsSinceLastProduce >= 3)
            {
                TurnsSinceLastProduce = 0;
                return new Villager();
            }
            return null;
        }
    }

    // Simple housing building
    public class House : Building
    {
        public House() : base("House", 200)
        {
            DefensePower = 5;
        }
    }
}





