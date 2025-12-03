using System;

public class TerrainFeature : WorldObject
{
    public string FeatureType { get; private set; }

    public TerrainFeature(string name, string featureType)
        : base(name)
    {
        FeatureType = featureType;
    }

    public override void Update(Random rng)
    {
        base.Update(rng);

        // add light features to terrain behavior
        switch (FeatureType.ToLower())
        {
            case "forest":
                // Forest gradually produces more wood "potential"
                if (rng.NextDouble() < 0.20)
                    Health += rng.Next(1, 4); // Forest thickens
                break;

            case "mountain":
                // Mountains are stable but can erode slightly due to weathering.
                if (rng.NextDouble() < 0.10)
                    Health -= rng.Next(0, 2);
                break;

            case "river":
                // A river's health fluctuates with seasonal changes.
                if (rng.NextDouble() < 0.30)
                    Health += rng.Next(-2, 3); // Can go up or down
                break;

            case "desert":
                // Deserts slowly expand, lowering health of the tile
                if (rng.NextDouble() < 0.15)
                    Health -= rng.Next(1, 3);
                break;

            case "tundra":
                // Cold regions slowly degrade
                if (rng.NextDouble() < 0.20)
                    Health -= rng.Next(1, 4);
                break;
        }
    }

    public override string GetStatus()
    {
        return $"{Name} (Terrain: {FeatureType}, Health: {Health})";
    }
}

