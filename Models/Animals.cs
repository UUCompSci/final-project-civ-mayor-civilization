
using System;

public class Animal : WorldObject
{
    public int Hunger { get; private set; }

    public Animal(string name) : base(name)
    {
        Hunger = 0;
    }

    public override void Update(Random rng)
    {
        base.Update(rng);
        Hunger += rng.Next(1, 6);

        if (Hunger > 80)
            Health -= 5;
    }

    public void Eat(Plant plant)
    {
        if (!plant.IsDead)
        {
            Hunger -= 20;
            plant.TakeDamage(30);
        }
    }

    public override string GetStatus()
    {
        return $"{Name} (Health: {Health}, Hunger: {Hunger})";
    }
}