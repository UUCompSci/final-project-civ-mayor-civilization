
using System;

public class Plant : WorldObject
{
    public Plant(string name) : base(name) {}

    public void TakeDamage(int amount)
    {
        Health -= amount;
    }

    public override void Update(Random rng)
    {
        // Plants grow or wilt
        Health += rng.Next(-2, 4);
    }
}