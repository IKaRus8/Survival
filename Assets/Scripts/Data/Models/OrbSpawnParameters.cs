namespace Data.Models
{
    public class OrbSpawnParameters
    {
        public string Id { get; }
        public float Chance { get; }
        
        public OrbSpawnParameters(string id, float chance)
        {
            Id = id;
            Chance = chance;
        }
    }
}