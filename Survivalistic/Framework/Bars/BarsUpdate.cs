namespace Survivalistic_Rebooted.Framework.Bars
{
    public static class BarsUpdate
    {
        public static void UpdateBarsInformation()
        {
            if (ModEntry.Data.ActualHunger > 0) ModEntry.Data.ActualHunger -= BarsDatabase.HungerVelocity;
            else ModEntry.Data.ActualHunger = 0;

            if (ModEntry.Data.ActualThirst > 0) ModEntry.Data.ActualThirst -= BarsDatabase.ThirstVelocity;
            else ModEntry.Data.ActualThirst = 0;
        }

        public static void CalculatePercentage()
        {
            BarsInformations.HungerPercentage = (ModEntry.Data.ActualHunger / ModEntry.Data.MaxHunger) * 168;
            BarsInformations.ThirstPercentage = (ModEntry.Data.ActualThirst / ModEntry.Data.MaxThirst) * 168;
        }
    }
}
