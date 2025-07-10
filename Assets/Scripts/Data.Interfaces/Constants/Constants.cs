namespace Data.Interfaces.Constants
{
    public static class Constants
    {
        public static class Scenes
        {
            public const string MenuScene = "MenuScene";
            public const string SurvivalLevelScene = "SurvivalLevelScene";
            public const string DefendLevelScene = "DefendLevelScene";
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
                public const string DefendHero = "hero_defend";
            }
        }

        public static class Attack
        {
            public const string SimpleMelee = "melee_simple";
            public const string BigMelee = "melee_big";
            public const string SimpleRange = "range_simple";
            public const string LongRange = "range_long";
        }
        
        public static class Vfx
        {
            public const string ElectroHit = "electro_hit";
        }

        public static class Settings
        {
            public static class Spawn
            {
                public const string LevelSpawnSettings = "level_spawn_settings";
                public const string DefendSpawnSettings = "defend_spawn_settings";
            }
        }
        
        public static class Orbs
        {
            public const string ExpOrb = "orb.exp";
            public const string HealOrb = "orb.heal";
        }
    }
}