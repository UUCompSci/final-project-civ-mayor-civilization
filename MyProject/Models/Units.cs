// Units.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    // Enum representing the possible tasks a unit can perform
    public enum CurrentTasks
    {
        Gather,
        Scouting,
        Attack,
        Building,
        Nothing
    }

    // Base class for all units in the game
    public class Unit : WorldObject
    {
        [Key] // Primary key for EF Core database
        public int Id { get; set; }

        // Unit's offensive and defensive stats
        public int AttackPower { get; protected set; }
        public int DefensePower { get; protected set; }

        // Current task assigned to the unit
        public CurrentTasks CTasks { get; set; }

        // Tracks the last terrain type the unit gathered from
        public string LastGatheredTerrain { get; private set; } = "None";

        // Tracks the last resource the unit gathered
        public string LastGatheredResource { get; private set; } = "None";

        // Constructor with name and optional health
        public Unit(string name, int health = 100) : base(name, health)
        {
            CTasks = CurrentTasks.Nothing;
        }

        // EF core parameterless constructor
        public Unit() : base("Unknown") { }

        // Executes the unit's current task
        public virtual void PerformTask(List<WorldObject> worldObjects, Random rng)
        {
            switch (CTasks)
            {
                case CurrentTasks.Gather:
                    // Finds all terrain features in the world
                    var terrains = worldObjects.FindAll(o => o is TerrainFeature);

                    if (terrains.Count > 0)
                    {
                        // Picks a random terrain and gather a resource
                        var terrain = (TerrainFeature)terrains[rng.Next(terrains.Count)];
                        string resource = terrain.GatherFromTerrain(rng);

                        // Updates tracking info
                        LastGatheredTerrain = terrain.FeatureType;
                        LastGatheredResource = resource;

                        // Prints result
                        Console.WriteLine($"{Name} gathered {resource} from {terrain.FeatureType}.");
                    }
                    break;

                case CurrentTasks.Scouting:
                    // Prints scouting activity
                    Console.WriteLine($"{Name} is scouting...");
                    break;

                case CurrentTasks.Attack:
                    // find the target to attack
                    var target = worldObjects.Find(o => (o is Unit || o is Building) && !o.IsDead);

                    if (target != null)
                    {
                        if (target is Unit u)
                        {
                            int dmg = Math.Max(0, AttackPower - u.DefensePower);
                            u.TakeDamage(dmg);
                            Console.WriteLine($"{Name} attacks {u.Name} for {dmg} damage!");
                        }
                        else if (target is Building b)
                        {
                            int dmg = Math.Max(1, AttackPower - b.DefensePower);
                            b.TakeDamage(dmg);
                            Console.WriteLine($"{Name} attacks {b.Name} for {dmg} damage!");
                        }
                    }
                    break;
            }
        }

        // Returns the unit's current status
        public override string GetStatus()
        {
            return $"{Name} | HP:{Health} | Task:{CTasks} | Terrain:{LastGatheredTerrain} | Resource:{LastGatheredResource}";
        }

        // Apply incoming damage to the unit
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                Console.WriteLine($"{Name} has died.");
        }

        // Called each game tick to update the unit
        public override void Update(Random rng)
        {
            base.Update(rng); // Call base regeneration logic
        }
    }

    // Gathering Unit
    public class Villager : Unit
    {
        public Villager() : base("Villager", 100)
        {
            AttackPower = 5;
            DefensePower = 5;
            CTasks = CurrentTasks.Gather;
        }
    }

    // Scouting Unit
    public class Scout : Unit
    {
        public Scout() : base("Scout", 80)
        {
            AttackPower = 2;
            DefensePower = 2;
            CTasks = CurrentTasks.Scouting;
        }
    }

    // Hostile Unit
    public class Barbarian : Unit
    {
        public Barbarian() : base("Barbarian", 120)
        {
            AttackPower = 15;
            DefensePower = 5;
            CTasks = CurrentTasks.Attack;
        }
    }
}



