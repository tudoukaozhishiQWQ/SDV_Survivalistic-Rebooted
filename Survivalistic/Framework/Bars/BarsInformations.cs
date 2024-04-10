using System;
using Microsoft.Xna.Framework;

namespace Survivalistic.Framework.Bars
{
    public class BarsInformations
    {
        public static float HungerPercentage;

        public static float ThirstPercentage;

        public static Color HungerColor = new Color(1, .7f, 0);

        public static Color ThirstColor = new Color(0, .7f, 1);

        public static void ResetStatus()
        {
            ModEntry.Data.ActualHunger = ModEntry.Data.MaxHunger;
            ModEntry.Data.ActualThirst = ModEntry.Data.MaxThirst;

            BarsUpdate.CalculatePercentage();
        }

        public static void NormalizeStatus()
        {
            if (ModEntry.Data.ActualHunger < 0) ModEntry.Data.ActualHunger = 0;
            if (ModEntry.Data.ActualThirst < 0) ModEntry.Data.ActualThirst = 0;

            if (ModEntry.Data.ActualHunger > ModEntry.Data.MaxHunger) ModEntry.Data.ActualHunger = ModEntry.Data.MaxHunger;
            if (ModEntry.Data.ActualThirst > ModEntry.Data.MaxThirst) ModEntry.Data.ActualThirst = ModEntry.Data.MaxThirst;

            BarsUpdate.CalculatePercentage();
        }

        public static Color GetOffsetHungerColor()
        {
            double maxHunger = ModEntry.Data.MaxHunger * 1.0;
            double currentHunger = ModEntry.Data.ActualHunger * 1.0;
            double offset = currentHunger / maxHunger;

            Color color = HungerColor;
            try
            {
                color = ApplyColorOffset(offset, color);
            }
            catch
            {
                NormalizeStatus();
                color = ApplyColorOffset(offset, color);
            }

            return color;
        }

        public static Color GetOffsetThirstyColor()
        {
            NormalizeStatus();

            double maxThirsty = ModEntry.Data.MaxThirst * 1.0;
            double currentThirsty = ModEntry.Data.ActualThirst * 1.0;
            double offset = currentThirsty / maxThirsty;

            Color color = ThirstColor;
            try
            {
                color = ApplyColorOffset(offset, color);
            }
            catch
            {
                NormalizeStatus();
                color = ApplyColorOffset(offset, color);
            }

            return color;
        }

        private static Color ApplyColorOffset(double offset, Color color)
        {
            color.R = Convert.ToByte(Math.Abs(offset - 1) * byte.MaxValue);
            color.G = Convert.ToByte(offset * color.G);
            color.B = Convert.ToByte(offset * color.B);

            return color;
        }
    }
}