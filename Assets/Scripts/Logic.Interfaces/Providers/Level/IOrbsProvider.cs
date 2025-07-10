using System.Collections.Generic;
using Logic.Interfaces.Unity;

namespace Logic.Interfaces.Providers.Level
{
    public interface IOrbsProvider
    {
        List<IOrb> Orbs { get; }

        void AddOrb(IOrb orb);
        void RemoveOrb(IOrb orb);
    }
}