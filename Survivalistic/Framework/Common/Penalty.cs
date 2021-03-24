﻿using StardewModdingAPI;
using StardewValley;
using Survivalistic.Framework.Bars;

namespace Survivalistic.Framework.Common
{
    public class Penalty
    {
        public static void VerifyPenalty()
        {
            if (!Context.IsWorldReady) return;

            if (ModEntry.data.actual_hunger <= 15 || ModEntry.data.actual_hunger > 0) BarsDatabase.tool_use_multiplier = 1.5f;
            else if (ModEntry.data.actual_hunger <= 0) BarsDatabase.tool_use_multiplier = 2.5f;
            else BarsDatabase.tool_use_multiplier = 1;

            if (ModEntry.data.actual_thirst <= 15 || ModEntry.data.actual_thirst > 0) BarsDatabase.tool_use_multiplier = 1.5f;
            else if (ModEntry.data.actual_thirst <= 0) BarsDatabase.tool_use_multiplier = 2.5f;
            else BarsDatabase.tool_use_multiplier = 1;

            DealDamage();
        }

        public static void DealDamage()
        {
            if (!Context.IsWorldReady) return;
            bool _applying_health_damage = false;

            if (ModEntry.data.actual_hunger <= 0)
            {
                if (Game1.player.stamina > 0) Game1.player.stamina -= 15;
                else
                {
                    Game1.player.health -= 10;
                    _applying_health_damage = true;
                }
                Buffs.SetBuff("Hunger");
            }
            if (ModEntry.data.actual_thirst <= 0)
            {
                if (Game1.player.stamina > 0) Game1.player.stamina -= 15;
                else
                {
                    Game1.player.health -= 10;
                    _applying_health_damage = true;
                }
                Buffs.SetBuff("Thirsty");
            }

            if (_applying_health_damage)
            {
                Game1.player.checkForExhaustion(Game1.player.Stamina);
                Buffs.SetBuff("Fainting");
            }
        }
    }
}
