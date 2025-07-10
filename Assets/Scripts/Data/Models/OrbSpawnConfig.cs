using System.Collections.Generic;
using System.Linq;

namespace Data.Models
{
    public class OrbSpawnConfig
    {
        public IReadOnlyCollection<OrbSpawnParameters> OrbSpawnParameters { get; }

        public OrbSpawnConfig(params OrbSpawnParameters[] orbSpawnParameters)
        {
            OrbSpawnParameters = orbSpawnParameters.OrderBy(o => o.Chance).ToArray();
        }
    }
}