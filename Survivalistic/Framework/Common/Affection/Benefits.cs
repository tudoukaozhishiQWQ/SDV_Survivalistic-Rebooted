using StardewModdingAPI;

namespace Survivalistic_Rebooted.Framework.Common.Affection
{
    public static class Benefits
    {
        public static bool VerifyBenefits()
        {
            if (!Context.IsWorldReady)
                return false;

            var hungerSate = ProceedHungerCheck();
            var thirstSate = ProceedThirstCheck();

            return hungerSate && thirstSate;
        }

        private static bool ProceedHungerCheck()
        {
            var result = true;
            if (ModEntry.Data.ActualHunger >= 80)
            {
                Buffs.CallUpdateSettingBuff(Buffs.FullnessBuffName);
            }
            else
            {
                result = ModEntry.Data.ActualHunger > 30;
                Buffs.CallUpdateSettingBuff(Buffs.FullnessBuffName, true);
            }
            
            return result;
        }

        private static bool ProceedThirstCheck()
        {
            var result = true;
            if (ModEntry.Data.ActualThirst >= 80)
            {
                Buffs.CallUpdateSettingBuff(Buffs.HydratedBuffName);
            }
            else
            {
                result = ModEntry.Data.ActualThirst > 30;
                Buffs.CallUpdateSettingBuff(Buffs.HydratedBuffName, true);
            }

            return result;
        }
    }
}
