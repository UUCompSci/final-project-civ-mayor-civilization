// removed animals and plants and added buildings, units, resources, and terrain features.
using System;
using System.Collections.Generic;
using System.Linq;

public static class WorldPersistence
{
    public static void SaveWorld(List<WorldObject> objects)
    {
        using var db = new WorldDbContext();

        // Clears old world state
        db.Buildings.RemoveRange(db.Buildings);
        db.Resources.RemoveRange(db.Resources);
        db.TerrainFeatures.RemoveRange(db.TerrainFeatures);
        db.Units.RemoveRange(db.Units);

        // Inserts new objects
        foreach (var obj in objects)
        {
            switch (obj)
            {
                case Building b:
                    db.Buildings.Add(b);
                    break;

                case Resource r:
                    db.Resources.Add(r);
                    break;

                case TerrainFeature t:
                    db.TerrainFeatures.Add(t);
                    break;

                case Unit u:
                    db.Units.Add(u);
                    break;
            }
        }

        db.SaveChanges();
        Console.WriteLine("World saved.");
    }

    public static List<WorldObject> LoadWorld()
    {
        using var db = new WorldDbContext();

        var list = new List<WorldObject>();

        list.AddRange(db.Buildings);
        list.AddRange(db.Resources);
        list.AddRange(db.TerrainFeatures);
        list.AddRange(db.Units);

        Console.WriteLine("World loaded.");
        return list;
    }
}
