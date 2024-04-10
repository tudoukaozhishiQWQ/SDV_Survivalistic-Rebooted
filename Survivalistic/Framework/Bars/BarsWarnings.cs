﻿using StardewModdingAPI;
using StardewValley;

namespace Survivalistic.Framework.Bars
{
    public static class BarsWarnings
    {
        private static bool isHungerWarningActive;
        private static bool isThirstWarningActive;

        public static void VerifyStatus()
        {
            if (!Context.IsWorldReady) return;

            if (ModEntry.Data.ActualHunger <= 15)
            {
                if (!isHungerWarningActive)
                {
                    isHungerWarningActive = true;
                    Game1.addHUDMessage(new HUDMessage(ModEntry.Instance.Helper.Translation.Get("hunger-warning"), 2));
                }
            }
            else isHungerWarningActive = false;

            if (ModEntry.Data.ActualThirst <= 15)
            {
                if (!isThirstWarningActive)
                {
                    isThirstWarningActive = true;
                    Game1.addHUDMessage(new HUDMessage(ModEntry.Instance.Helper.Translation.Get("thirsty-warning"), 2));
                }
            }
            else isThirstWarningActive = false;
        }
    }
}
