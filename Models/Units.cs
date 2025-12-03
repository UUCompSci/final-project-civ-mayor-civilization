// Units are the things that can move and perform actions. Units make buildings. Buildings make Units.
namespace Final_Project_Civ_Mayor_Civilization.Models
{
    public class Unit : WorldObject
    {
        public int AttackPower { get; private set; }
        public int DefensePower { get; private set; } 

        // also need hitpoints for units both enemy and allys. 
    }
} 

// You can get rid of what is here and make your own version of it i just put all this here as a placeholder. 


// economic balance. 
// make a task system for units to do. like a task state with enums. 
// state of activity for units. 
// certain units have specific activities they can do and are the only ones who can do that task 
// armies fight, workers get resources. 
// types of units, and types of buildings, and they have things that they can specifically do, nd make a task system. 

// Each type should have unique behaviors in Update and interactions with other types
// Implement saving/loading for all new types in WorldPersistence 
// Units are the things that can move and perform actions. Units make buildings. Buildings make Units. 
// Units have tasks that they need to do. Buildings have 1 unit per turn depending on if you can afford it. 
// Need enemy units such as barbarians that attack your units and buildings. 
