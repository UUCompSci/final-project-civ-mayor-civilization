using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Models;
using Final_Project_Civ_Mayor_Civilization.Data;

// This is the main entry point for the application
class Program
{
    static void Main()
    {
        // Random number generator for game logic
        var rng = new Random();

        // Controls the main game loop
        var running = true;

        // Initializes database context
        using var context = new WorldDbContext();

        // Ensures database and tables exist
        context.Database.EnsureCreated();

        // Loads existing world objects from database
        List<Building> buildings = context.Buildings.ToList();
        List<Resource> resources = context.Resources.ToList();
        List<TerrainFeature> terrains = context.TerrainFeatures.ToList();
        List<Unit> units = context.Units.ToList();

        // Combines into a single world object list
        List<WorldObject> objects = new();
        objects.AddRange(buildings);
        objects.AddRange(resources);
        objects.AddRange(terrains);
        objects.AddRange(units);

        // If world is empty (first run), initialize starting objects
        if (objects.Count == 0)
        {
            // Creates starting buildings
            objects.Add(new TownCenter());
            objects.Add(new House());

            // Creates starting units
            objects.Add(new Villager());
            objects.Add(new Scout());

            // Creates starting resource nodes
            objects.Add(new Resource("Tree", "wood", 50));
            objects.Add(new Resource("Stone Quarry", "stone", 30));

            // Creates random terrains
            objects.Add(new TerrainFeature("Terrain A", rng));
            objects.Add(new TerrainFeature("Terrain B", rng));
            objects.Add(new TerrainFeature("Terrain C", rng));
        }

        Console.WriteLine("=== Civilization-Style World Simulator ===");

        // Main program/game loop
        while (running)
        {
            // Display menu
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1) Add Villager");
            Console.WriteLine("2) Add Scout");
            Console.WriteLine("3) Add Barbarian");
            Console.WriteLine("4) View World Status");
            Console.WriteLine("5) Next Turn");
            Console.WriteLine("6) Save Game");
            Console.WriteLine("7) Load Game");
            Console.WriteLine("8) Exit");
            Console.Write("Choose: ");

            // Reads the user choice
            string input = Console.ReadLine() ?? "";
            Console.WriteLine();

            switch (input)
            {
                case "1":
                    objects.Add(new Villager());
                    Console.WriteLine("Villager added.");
                    break;

                case "2":
                    objects.Add(new Scout());
                    Console.WriteLine("Scout added.");
                    break;

                case "3":
                    objects.Add(new Barbarian());
                    Console.WriteLine("Barbarian added!");
                    break;

                case "4":
                    // Display status of every world object
                    foreach (var obj in objects)
                        Console.WriteLine(obj.GetStatus());
                    break;

                case "5":
                    // Simulate one world tick/update
                    List<Unit> producedUnits = new();

                    foreach (var obj in objects)
                    {
                        obj.Update(rng);

                        // Allow buildings to produce units
                        if (obj is Building building)
                        {
                            var unit = building.ProduceUnit();
                            if (unit != null)
                            {
                                producedUnits.Add(unit);
                                Console.WriteLine($"{unit.Name} produced by {building.Name}!");
                            }
                        }

                        // Allowunits to act
                        if (obj is Unit unitObj)
                            unitObj.PerformTask(objects, rng);
                    }

                    // Adds newly created units to world
                    objects.AddRange(producedUnits);

                    // Removes dead objects from world
                    objects.RemoveAll(o => o.IsDead);

                    Console.WriteLine("World Updated.");
                    break;

                case "6":
                    // Save game state to database
                    SaveGame(context, objects);
                    Console.WriteLine("Game saved!");
                    break;

                case "7":
                    // Reload game state from database
                    objects = LoadGame(context);
                    Console.WriteLine("Game loaded!");
                    break;

                case "8":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    // Saves current world state to database
    private static void SaveGame(WorldDbContext context, List<WorldObject> objects)
    {
        // Clear old data
        context.Buildings.RemoveRange(context.Buildings);
        context.Resources.RemoveRange(context.Resources);
        context.TerrainFeatures.RemoveRange(context.TerrainFeatures);
        context.Units.RemoveRange(context.Units);

        // Add current state
        context.Buildings.AddRange(objects.OfType<Building>());
        context.Resources.AddRange(objects.OfType<Resource>());
        context.TerrainFeatures.AddRange(objects.OfType<TerrainFeature>());
        context.Units.AddRange(objects.OfType<Unit>());

        // Commit changes
        context.SaveChanges();
    }

    // Loads world state from database
    private static List<WorldObject> LoadGame(WorldDbContext context)
    {
        List<WorldObject> objects = new();
        objects.AddRange(context.Buildings.ToList());
        objects.AddRange(context.Resources.ToList());
        objects.AddRange(context.TerrainFeatures.ToList());
        objects.AddRange(context.Units.ToList());
        return objects;
    }
}






