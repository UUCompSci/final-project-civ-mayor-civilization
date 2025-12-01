
// Each type should have unique behaviors in Update and interactions with other types
// Implement saving/loading for all new types in WorldPersistence 
// Units are the things that can move and perform actions. Units make buildings. Buildings make Units. 
// Units have tasks that they need to do. Buildings have 1 unit per turn depending on if you can afford it. 
// Need enemy units such as barbarians that attack your units and buildings. 



// Removed the plants and animals and added buildings, units, resources, and terrain features.

using System;
using System.Collections.Generic;

public abstract class WorldObject
{
    public int Id { get; set; }
    public string Name { get; protected set; }

    protected int Health;

    public WorldObject(string name, int health = 100)
    {
        Name = name;
        Health = health;
    }

    /// This is the base update logic that is shared by all world objects.
    /// The override in subclasses for custom behavior like the units moving. 
    public virtual void Update(Random rng)
    {
        // This is simple decay / passage-of-time effect
        Health -= rng.Next(0, 3);
    }

    public virtual string GetStatus()
    {
        return $"{Name} (Health: {Health})";
    }

    public bool IsDead => Health <= 0;
}


class Program 
{
    static void Main()
    {
        var rng = new Random();
        var running = true;

        // This is the active world memory
        List<WorldObject> objects = new();

        Console.WriteLine("=== Civilization-Style World Simulator ===");

        while (running)
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1) Add Unit");
            Console.WriteLine("2) Add Building");
            Console.WriteLine("3) Add Resource");
            Console.WriteLine("4) Add Terrain Feature");
            Console.WriteLine("5) Update World");
            Console.WriteLine("6) View World Status");
            Console.WriteLine("7) Save World");
            Console.WriteLine("8) Load World");
            Console.WriteLine("9) Exit");
            Console.Write("Choose: ");

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1":
                    objects.Add(new Unit("Unit", 100));
                    Console.WriteLine("Unit added.");
                    break;

                case "2":
                    objects.Add(new Building("Building", 200));
                    Console.WriteLine("Building added.");
                    break;

                case "3":
                    objects.Add(new Resource("Resource Node", 50));
                    Console.WriteLine("Resource added.");
                    break;

                case "4":
                    objects.Add(new TerrainFeature("Terrain Feature", 150));
                    Console.WriteLine("Terrain Feature added.");
                    break;

                case "5":
                    foreach (var obj in objects)
                        obj.Update(rng);
                    Console.WriteLine("World updated.");
                    break;

                case "6":
                    foreach (var obj in objects)
                        Console.WriteLine(obj.GetStatus());
                    break;

                case "7":
                    WorldPersistence.SaveWorld(objects);
                    break;

                case "8":
                    objects = WorldPersistence.LoadWorld();
                    break;

                case "9":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
