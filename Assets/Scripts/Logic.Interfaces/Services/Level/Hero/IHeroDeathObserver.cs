using System;

namespace Logic.Interfaces.Services.Level.Hero
{
    public interface IHeroDeathObserver
    {
        event Action HeroDie;
    }
}