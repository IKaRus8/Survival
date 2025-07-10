using Data.Interfaces;
using Data.Models;
using Logic.Interfaces.Providers.Level;

namespace Logic.Providers.Level
{
    public class OrbSpawnConfigProvider : IOrbSpawnConfigProvider
    {
        public OrbSpawnConfig Config { get; }

        public OrbSpawnConfigProvider(IGameSettings gameSettings)
        {
            Config = gameSettings.OrbSpawnConfig;
        }
    }
}