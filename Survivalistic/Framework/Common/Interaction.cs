﻿using System;
using System.Collections.Generic;
using System.Linq;
using StardewModdingAPI;
using StardewValley;
using Survivalistic.Framework.Bars;
using Survivalistic.Framework.Databases;

namespace Survivalistic.Framework.Common
{
    public class Interaction
    {
        private static bool already_eating = false;
        private static bool already_using_tool = false;

        private static string item_eated_name;
        private static string tool_used_name;

        public static void EatingCheck()
        {
            if (!Context.IsWorldReady) return;

            if (Game1.player.isEating)
            {
                item_eated_name = Game1.player.itemToEat.Name;
                already_eating = true;
            }
            else
            {
                if (already_eating)
                {
                    already_eating = false;
                    IncreaseStatus(item_eated_name);
                }
            }
        }

        public static void UsingToolCheck()
        {
            if (!Context.IsWorldReady) return;

            if (Game1.player.UsingTool)
            {
                tool_used_name = Game1.player.CurrentTool.BaseName;
                already_using_tool = true;
            }
            else
            {
                if (already_using_tool)
                {
                    already_using_tool = false;
                    ModEntry.instance.Monitor.Log($"Start decrease, tool: {tool_used_name}", LogLevel.Info);
                    DecreaseStatus(tool_used_name);
                }
            }
        }

        private static void IncreaseStatus(string food_eated)
        {
            if (Foods.GetFoodDatabase().TryGetValue(food_eated, out string food_status_string))
            {
                List<string> food_status = food_status_string.Split('/').ToList();

                float last_hunger = ModEntry.data.actual_hunger;
                float last_thirst = ModEntry.data.actual_thirst;

                if (ModEntry.data.actual_hunger < ModEntry.data.max_hunger) ModEntry.data.actual_hunger += Int32.Parse(food_status[0]);
                if (ModEntry.data.actual_thirst < ModEntry.data.max_thirst) ModEntry.data.actual_thirst += Int32.Parse(food_status[1]);

                BarsInformations.NormalizeStatus();

                float hunger_diff = ModEntry.data.actual_hunger - last_hunger;
                float thirst_diff = ModEntry.data.actual_thirst - last_thirst;

                if (hunger_diff > 0) Game1.addHUDMessage(new HUDMessage($"Hunger + {hunger_diff}", 4));
                if (thirst_diff > 0) Game1.addHUDMessage(new HUDMessage($"Thirst + {thirst_diff}", 4));
            }
        }

        private static void DecreaseStatus(string tool_used)
        {
            if (Tools.GetToolDatabase().TryGetValue(tool_used, out string tool_status_string))
            {
                List<string> tool_status = tool_status_string.Split('/').ToList();

                if (ModEntry.data.actual_hunger >= 0) ModEntry.data.actual_hunger -= float.Parse(tool_status[0]);
                if (ModEntry.data.actual_thirst >= 0) ModEntry.data.actual_thirst -= float.Parse(tool_status[1]);

                BarsInformations.NormalizeStatus();
            }
        }
    }
}
