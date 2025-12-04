// Building.cs
// Need houses (child class of buildings) that produce units (workers) every few turns if you can afford it. 
// cannot build units if you do not have enough population capacity. 
// buildings need hitpoints and can be attacked by enemy units.
namespace Final_Project_Civ_Mayor_Civilization.Models
{
    public enum Production
    {
        scout,
        villager,
        nothing
    }
    public class Building : WorldObject
    {
        
        public int AttackPower { get; private set; }
        public int DefensePower { get; private set; } 

        public Production Prod { get; private set; }


    }

    public class House : Building
    {
        public int AttackPower = 0;
        public int DefensePower = 100;
        public Production Prod = Production.nothing;


        public House(){}

    }

    public class TownCenter : Building
    {
        public int AttackPower = 0;
        public int DefensePower = 200;    
        public Production Prod = Production.nothing;


        public TownCenter(){}

    }

    public class Stable : Building
    {
        
        public int AttackPower = 0;
        public int DefensePower = 100;
        public Production Prod = Production.nothing;

        
        public Stable(){}

    }



} 
// // You can get rid of what is here and make your own version of it i just put all this here as a placeholder. 