// TerrainFeatures.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    // Represents a terrain feature in the world (forest, river, mountain, etc.)
    // Inherits from WorldObject so it has health and a name
    public class TerrainFeature : WorldObject
    {
        [Key] // Primary key for EF Core database
        public int Id { get; set; }

        // Type of terrain (forest, river, mountain, etc.)
        public string FeatureType { get; private set; }

        // Tracks the last resource gathered from this terrain
        private string _lastGathered = "Nothing";

        // List of possible terrain types for random generation
        private static readonly string[] TerrainTypes =
        {
            "forest",
            "mountain",
            "river",
            "desert",
            "tundra"
        };

        // Constructor that randomly selects a terrain type
        public TerrainFeature(string name, Random rng) : base(name)
        {
            FeatureType = TerrainTypes[rng.Next(TerrainTypes.Length)];
        }

        // Constructor with explicit terrain type
        public TerrainFeature(string name, string featureType) : base(name)
        {
            FeatureType = featureType.ToLower();
        }

        // Parameterless constructor required by EF Core
        public TerrainFeature() : base("Unknown") { }

        // Simulates gathering a resource from the terrain
        public string GatherFromTerrain(Random rng)
        {
            // Determines possible resources based on terrain type
            string[] possibleResources = FeatureType switch
            {
                "forest" => new[] { "wood", "apples", "deer meat" },
                "mountain" => new[] { "stone", "metal", "limestone" },
                "river" => new[] { "fish", "water" },
                "desert" => new[] { "cactus", "sand" },
                "tundra" => new[] { "fox meat", "owl meat" },
                _ => new[] { "nothing" }
            };

            // Picks a random resource to gather
            string gathered = possibleResources[rng.Next(possibleResources.Length)];

            // Updates last gathered resource
            _lastGathered = gathered;

            // Returns the gathered resource
            return gathered;
        }

        // Returns a string representing the terrain's current status
        public override string GetStatus()
        {
            return $"{Name} (Terrain: {FeatureType}, Health: {Health}, Last Gathered: {_lastGathered})";
        }
    }
}