using System.Collections.Generic;

namespace Survivalistic_Rebooted.Framework.Databases
{
    public static class Tools
    {
        public static Dictionary<string, string> GetToolDatabase()
        {
            return new()
            {
                { "Axe", "0.25/0.5" },
                { "Pickaxe", "0.25/0.5" },
                { "Hoe", "0.25/0.5" },
                { "Scythe", "0.1/0.2" },
                { "Fishing Rod", "0.15/0.3" },
                { "Watering Can", "0.1/0.2" },
                { "Shears", "0.15/0.3" },
                { "Milk Pail", "0.15/0.3" }
            };
        }
    }
}
