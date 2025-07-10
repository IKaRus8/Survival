using System.Collections.Generic;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Unity;

namespace Logic.Providers.Level
{
    public class OrbsProvider : IOrbsProvider
    {
        public List<IOrb> Orbs { get; }

        public OrbsProvider()
        {
            Orbs = new List<IOrb>();
        }

        public void AddOrb(IOrb orb)
        {
            Orbs.Add(orb);
        }

        public void RemoveOrb(IOrb orb)
        {
            Orbs.Remove(orb);
        }
    }
}