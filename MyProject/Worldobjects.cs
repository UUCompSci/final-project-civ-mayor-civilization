// Worldobjects.cs
using System;

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

    public virtual void Update(Random rng)
    {
        // Base behavior: small health decay
        Health -= rng.Next(0, 3);
    }

    public virtual string GetStatus()
    {
        return $"{Name} (Health: {Health})";
    }

    public bool IsDead => Health <= 0;
}