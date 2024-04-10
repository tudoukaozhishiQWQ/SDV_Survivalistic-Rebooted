using System;
using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI;
using StardewValley;
using Survivalistic.Framework.Bars;
using Survivalistic.Framework.Common.Affection;
using Survivalistic.Framework.Databases;

namespace Survivalistic.Framework.Common
{
    public static class Interaction
    {
        private static bool AlreadyEating;

        private static bool AlreadyUsingTool;

        private static string ItemEatenName;

        private static string ToolUsedName;

        private static bool GettingTickInformation = true;

        public static void EatingCheck()
        {
            if (!Context.IsWorldReady) return;

            if (Game1.player.isEating)
            {
                ItemEatenName = Game1.player.itemToEat.Name;
                AlreadyEating = true;
            }
            else
            {
                if (AlreadyEating)
                {
                    AlreadyEating = false;
                    IncreaseStatus(ItemEatenName, Game1.player.itemToEat.staminaRecoveredOnConsumption());
                }
            }
        }

        public static void UsingToolCheck()
        {
            if (!Context.IsWorldReady) return;

            if (Game1.player.UsingTool)
            {
                ToolUsedName = Game1.player.CurrentTool.BaseName;
                AlreadyUsingTool = true;
            }
            else
            {
                if (AlreadyUsingTool)
                {
                    AlreadyUsingTool = false;
                    DecreaseStatus(ToolUsedName);
                }
            }
        }

        private static void IncreaseStatus(string foodEaten, int recover)
        {
            float lastHunger = ModEntry.Data.ActualHunger;
            float lastThirst = ModEntry.Data.ActualThirst;

            if (Foods.FoodDatabase.TryGetValue(foodEaten, out string food_status_string))
            {
                List<string> food_status = food_status_string.Split('/').ToList();

                if (ModEntry.Data.ActualHunger < ModEntry.Data.MaxHunger) ModEntry.Data.ActualHunger += Int32.Parse(food_status[0]);
                if (ModEntry.Data.ActualThirst < ModEntry.Data.MaxThirst) ModEntry.Data.ActualThirst += Int32.Parse(food_status[1]);

                BarsInformations.NormalizeStatus();

                float hungerDiff = ModEntry.Data.ActualHunger - lastHunger;
                float thirstDiff = ModEntry.Data.ActualThirst - lastThirst;

                if (hungerDiff > 0) Game1.addHUDMessage(new HUDMessage(string.Format(ModEntry.Instance.Helper.Translation.Get("info-fullness"), (int)hungerDiff), 4));
                if (thirstDiff > 0) Game1.addHUDMessage(new HUDMessage(string.Format(ModEntry.Instance.Helper.Translation.Get("info-thirsty"), (int)thirstDiff), 4));
            }

            else if (ModEntry.Config.NonSupportedFood)
            {
                if (ModEntry.Data.ActualHunger < ModEntry.Data.MaxHunger) ModEntry.Data.ActualHunger += recover * new Random().Next(1, 3);
                if (ModEntry.Data.ActualThirst < ModEntry.Data.MaxThirst) ModEntry.Data.ActualThirst += recover * new Random().Next(1, 3);

                BarsInformations.NormalizeStatus();

                float hunger_diff = ModEntry.Data.ActualHunger - lastHunger;
                float thirst_diff = ModEntry.Data.ActualThirst - lastThirst;

                if (hunger_diff > 0) Game1.addHUDMessage(new HUDMessage(string.Format(ModEntry.Instance.Helper.Translation.Get("info-fullness"), (int)hunger_diff), 4));
                if (thirst_diff > 0) Game1.addHUDMessage(new HUDMessage(string.Format(ModEntry.Instance.Helper.Translation.Get("info-thirsty"), (int)thirst_diff), 4));
            }

            if (!Benefits.VerifyBenefits())
                Penalty.VerifyPenalty();
        }

        private static void DecreaseStatus(string tool_used)
        {
            if (Tools.GetToolDatabase().TryGetValue(tool_used, out string toolStatusString))
            {
                List<string> toolStatus = toolStatusString.Split('/').ToList();

                if (ModEntry.Data.ActualHunger >= 0) 
                    ModEntry.Data.ActualHunger -= float.Parse(toolStatus[0]) * (BarsDatabase.ToolUseMultiplier * ModEntry.Config.HungerActionMultiplier);

                if (ModEntry.Data.ActualThirst >= 0) 
                    ModEntry.Data.ActualThirst -= float.Parse(toolStatus[1]) * (BarsDatabase.ToolUseMultiplier * ModEntry.Config.ThirstActionMultiplier);

                if (!Benefits.VerifyBenefits())
                    Penalty.VerifyPenalty();

                BarsInformations.NormalizeStatus();
                BarsWarnings.VerifyStatus();
            }
        }

        public static void Awake()
        {
            ModEntry.Data.InitialHunger = ModEntry.Data.ActualHunger;
            ModEntry.Data.InitialThirst = ModEntry.Data.ActualThirst;
            ModEntry.Data.ActualDay = Game1.Date.DayOfMonth;
            ModEntry.Data.ActualSeason = Game1.Date.SeasonIndex;
            ModEntry.Data.ActualYear = Game1.Date.Year;
        }

        public static void ReceiveAwakeInfo()
        {
            if (Game1.IsMultiplayer)
            {
                if (ModEntry.Data.ActualDay != Game1.Date.DayOfMonth ||
                        ModEntry.Data.ActualSeason != Game1.Date.SeasonIndex ||
                        ModEntry.Data.ActualYear != Game1.Date.Year ||
                        ModEntry.Data.ActualTick < Game1.ticks)
                {
                    ModEntry.Data.ActualHunger = ModEntry.Data.InitialHunger;
                    ModEntry.Data.ActualThirst = ModEntry.Data.InitialThirst;
                }
            }
            else
            {
                ModEntry.Data.ActualHunger = ModEntry.Data.InitialHunger;
                ModEntry.Data.ActualThirst = ModEntry.Data.InitialThirst;
            }
            GettingTickInformation = false;
        }

        public static void UpdateTickInformation()
        {
            if (!GettingTickInformation)
            {
                ModEntry.Data.ActualTick = Game1.ticks;
            }
        }
    }
}
