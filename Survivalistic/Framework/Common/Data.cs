using System.Collections.Generic;

namespace Survivalistic.Framework.Common
{
    public class Data
    {
        public float ActualHunger { get; set; } = 100;
        public float ActualThirst { get; set; } = 100;

        public float MaxHunger { get; set; } = 100;
        public float MaxThirst { get; set; } = 100;

        public float InitialHunger { get; set; } = 100;
        public float InitialThirst { get; set; } = 100;

        public int ActualDay { get; set; } = 0;
        public int ActualSeason { get; set; } = 0;
        public int ActualYear { get; set; } = 0;
        public int ActualTick { get; set; } = 0;

        public static Dictionary<string, string> FoodDatabase { get; set; }
    }
}
