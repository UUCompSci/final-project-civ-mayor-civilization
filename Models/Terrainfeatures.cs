// Terrain features like forests, mountains, rivers, deserts, etc.
using System; 
system.Collections.Generic; 
public class TerrainFeature : WorldObject
{
    public string FeatureType { get; private set; }

    public TerrainFeature(string name, string featureType) : base(name)
    {
        FeatureType = featureType;
    }

    public override void Update(Random rng)
    {
        base.Update(rng);
        // Terrain features might have specific behaviors in future
    }

    public override string GetStatus()
    {
        return $"{Name} (Type: {FeatureType}, Health: {Health})";
    }
}