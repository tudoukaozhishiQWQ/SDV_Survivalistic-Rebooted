﻿namespace Survivalistic.Models
{
    public class Config
    {
        // MULTIPLIERS
        public float ThirstMultiplier { get; set; } = 0.5f;
        public float HungerMultiplier { get; set; } = 1f;

        public float HungerActionMultiplier { get; set; } = 1f;

        public float ThirstActionMultiplier { get; set; } = 1f;

        // BARS PIVOT
        public string BarsPosition { get; set; } = "bottom-right";

        // CUSTOM BARS AXIS (use "custom" in the pivot to use this)
        public int BarsCustomX { get; set; } = 0;
        public int BarsCustomY { get; set; } = 0;

        public bool NonSupportedFood { get; set; }

        public bool DecreaseValuesAfterSleep { get; set; }

        public int FoodDecreaseAfterSleep { get; set; }

        public int ThirstDecreaseAfterSleep { get; set; }
    }
}
