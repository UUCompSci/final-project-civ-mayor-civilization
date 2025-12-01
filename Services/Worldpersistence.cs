public static class WorldPersistence
{
    public static void SaveWorld(List<WorldObject> objects)
    {
        using var db = new WorldDbContext();

        db.Animals.RemoveRange(db.Animals);
        db.Plants.RemoveRange(db.Plants);

        foreach (var obj in objects)
        {
            if (obj is Animal a) db.Animals.Add(a);
            else if (obj is Plant p) db.Plants.Add(p);
        }

        db.SaveChanges();
        Console.WriteLine("World saved.");
    }

    public static List<WorldObject> LoadWorld()
    {
        using var db = new WorldDbContext();
        var list = new List<WorldObject>();

        list.AddRange(db.Animals);
        list.AddRange(db.Plants);

        Console.WriteLine("World loaded.");
        return list;
    }
}
// need to change the world persistence to include all the new objects and get rid of the plants and animals.