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