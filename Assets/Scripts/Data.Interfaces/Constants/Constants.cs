namespace Data.Interfaces.Constants
{
    public static class Constants
    {
        public static class Scenes
        {
            public const string MenuScene = "MenuScene";
            public const string LevelScene = "LevelScene";
        }
        
        public static class Enemy
        {
            public static class Id
            {
                public const string SimpleEnemy = "enemy_simple";
                public const string NavEnemy = "enemy_nav";
            }
        }

        public static class Hero
        {
            public static class Id
            {
                public const string SimpleHero = "hero_simple";
            }
        }

        public static class Attack
        {
            public const string SimpleMelee = "melee_simple";
            public const string SimpleRange = "range_simple";
        }
        
        public static class Vfx
        {
            public const string ElectroHit = "electro_hit";
        }
    }
}