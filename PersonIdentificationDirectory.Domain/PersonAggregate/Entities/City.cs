namespace PersonIdentificationDirectory.Domain.PersonAggregate.Entities
{
    public static class City
    {
        public static Dictionary<int, string> Cities = new()
        {
            { 1, "Tbilisi" },
            { 2, "Batumi" },
            { 3, "Kutaisi" },
        };

        public static bool Exists(int id)
        {
            return Cities.ContainsKey(id);
        }
    }
}
