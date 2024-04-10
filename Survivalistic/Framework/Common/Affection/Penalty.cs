﻿using StardewModdingAPI;
using StardewValley;
using Survivalistic_Rebooted.Framework.Bars;
using Survivalistic_Rebooted.Framework.Networking;

namespace Survivalistic_Rebooted.Framework.Common.Affection
{
    public static class Penalty
    {
        private static bool _alreadyCheckedFaint;

        public static void VerifyPenalty()
        {
            if (!Context.IsWorldReady) return;

            if (ModEntry.Data.ActualHunger <= 15 && ModEntry.Data.ActualHunger > 0) BarsDatabase.ToolUseMultiplier = 1.5f;
            else if (ModEntry.Data.ActualHunger <= 0) BarsDatabase.ToolUseMultiplier = 2.5f;
            else BarsDatabase.ToolUseMultiplier = 1;

            if (ModEntry.Data.ActualThirst <= 15 && ModEntry.Data.ActualThirst > 0) BarsDatabase.ToolUseMultiplier = 1.5f;
            else if (ModEntry.Data.ActualThirst <= 0) BarsDatabase.ToolUseMultiplier = 2.5f;
            else BarsDatabase.ToolUseMultiplier = 1;

            CheckValuesAndDealDamageIfReady();
        }

        public static void CheckValuesAndDealDamageIfReady()
        {
            if (!Context.IsWorldReady) return;
            bool applyingHealthDamage = false;

            if (ModEntry.Data.ActualHunger <= 10)
            {
                if (Game1.player.stamina > 0)
                {
                    Game1.player.stamina -= 15;
                }
                else
                {
                    Game1.player.health -= 10;
                    applyingHealthDamage = true;
                }

                Buffs.CallUpdateSettingBuff(Buffs.HungerBuffName);
            }
            else
            {
                Buffs.CallUpdateSettingBuff(Buffs.HungerBuffName, true);
            }

            if (ModEntry.Data.ActualThirst <= 10)
            {
                if (ModEntry.Data.ActualThirst <= 0)
                {
                    if (Game1.player.stamina > 0)
                    {
                        Game1.player.stamina -= 15;
                    }
                    else
                    {
                        Game1.player.health -= 10;
                        applyingHealthDamage = true;
                    }
                }

                Buffs.CallUpdateSettingBuff(Buffs.ThirstyBuffName);
            }
            else
            {
                Buffs.CallUpdateSettingBuff(Buffs.ThirstyBuffName, true);
            }


            if (applyingHealthDamage)
            {
                Game1.player.checkForExhaustion(Game1.player.Stamina);
                Buffs.CallUpdateSettingBuff(Buffs.FaintingBuffName);
            }
            else
            {
                Buffs.CallUpdateSettingBuff(Buffs.FaintingBuffName, true);
            }
        }

        public static void VerifyPassOut()
        {
            if (Game1.player.health <= 0)
            {
                if (!_alreadyCheckedFaint)
                {
                    ModEntry.Data.ActualHunger = ModEntry.Data.MaxHunger / 3;
                    ModEntry.Data.ActualThirst = ModEntry.Data.MaxThirst / 2;

                    NetController.Sync();

                    _alreadyCheckedFaint = true;
                }
            }
            else
            {
                _alreadyCheckedFaint = false;
            }
        }
    }
}
