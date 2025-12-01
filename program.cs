
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Random rng = new Random();

    static List<WorldObject> world = new()
    {
        new Animal("Wolf"),
        new Animal("Rabbit"),
        new Plant("Berry Bush"),
        new Plant("Grass Patch")
    };

    static void Main()
    {
        Console.WriteLine("=== WORLD SIMULATOR ===");

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Run simulation tick");
            Console.WriteLine("2. List world objects");
            Console.WriteLine("3. Make animals eat plants");
            Console.WriteLine("4. Save world");
            Console.WriteLine("5. Load world");
            Console.WriteLine("0. Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1": SimulationTick(); break;
                case "2": ListObjects(); break;
                case "3": AnimalEat(); break;
                case "4": WorldPersistence.SaveWorld(world); break;
                case "5": world = WorldPersistence.LoadWorld(); break;
                case "0": return;
            }
        }
    }

    static void SimulationTick()
    {
        foreach (var obj in world)
            obj.Update(rng);

        world = world.Where(o => !o.IsDead).ToList();

        Console.WriteLine("Tick complete.");
    }

    static void ListObjects()
    {
        foreach (var obj in world)
            Console.WriteLine(obj.GetStatus());
    }

    static void AnimalEat()
    {
        var animals = world.OfType<Animal>().ToList();
        var plants = world.OfType<Plant>().Where(p => !p.IsDead).ToList();

        if (!animals.Any() || !plants.Any())
        {
            Console.WriteLine("No interactions possible.");
            return;
        }

        foreach (var animal in animals)
        {
            var target = plants[rng.Next(plants.Count)];
            animal.Eat(target);
            Console.WriteLine($"{animal.Name} ate {target.Name}");
        }
    }
}