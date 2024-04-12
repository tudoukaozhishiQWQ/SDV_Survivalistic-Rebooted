using StardewModdingAPI;
using StardewValley;
using Survivalistic_Rebooted.Framework.Common;
using System.Collections.Generic;

namespace Survivalistic_Rebooted.Models
{
    public static class SurvivalisticBuffs
    {
        internal static List<Buff> SurvivalisticBuffsTemplates { get; }

        static SurvivalisticBuffs()
        {
            SurvivalisticBuffsTemplates = new List<Buff>
            {
                // Fulness Buff.
                new(string.Format(Names.BuffIDTemplate, Names.FullnessBuff), "food",
                    duration: Buff.ENDLESS,
                    effects: new StardewValley.Buffs.BuffEffects()
                    {
                        Defense = { 2 }
                    })
                {
                    description = ModEntry.Instance.Helper.Translation.Get("buff.fullness.description"),
                    displaySource = ModEntry.Instance.Helper.Translation.Get("buff.fullness.source"),
                    iconTexture = Textures.BuffSprites,
                    iconSheetIndex = 28,
                },
                // Hydrated Buff.
                new(string.Format(Names.BuffIDTemplate, Names.HydratedBuff), "food",
                    duration: Buff.ENDLESS,
                    effects: new StardewValley.Buffs.BuffEffects()
                    {
                        MaxStamina = { 25 }
                    })
                {
                    description = ModEntry.Instance.Helper.Translation.Get("buff.hydrated.description"),
                    displaySource = ModEntry.Instance.Helper.Translation.Get("buff.hydrated.source"),
                    iconTexture = Textures.BuffSprites,
                    iconSheetIndex = 19
                },
                // Hunger DeBuff.
                new(string.Format(Names.BuffIDTemplate, Names.HungerDeBuff), "food",
                    duration: Buff.ENDLESS,
                    effects: new StardewValley.Buffs.BuffEffects()
                    {
                        Defense = { -2 },
                        Attack = { -2 }
                    },
                    isDebuff: true)
                {
                    description = ModEntry.Instance.Helper.Translation.Get("hunger-warning"),
                    displaySource = ModEntry.Instance.Helper.Translation.Get("hunger-source"),
                    iconTexture = Textures.BuffSprites,
                    iconSheetIndex = 6
                },
                // Thirst DeBuff.
                new(string.Format(Names.BuffIDTemplate, Names.ThirstDeBuff), "food",
                    duration: Buff.ENDLESS, 
                    effects: new StardewValley.Buffs.BuffEffects()
                    {
                        MaxStamina = { -30 },
                        Speed = { -1 }
                    },
                    isDebuff: true)
                {
                    description = ModEntry.Instance.Helper.Translation.Get("thirsty-warning"),
                    displaySource = ModEntry.Instance.Helper.Translation.Get("thirsty-source"),
                    iconTexture = Textures.BuffSprites,
                    iconSheetIndex = 7
                },
                // Faint DeBuff.
                new(string.Format(Names.BuffIDTemplate, Names.FaintDeBuff), "food",
                    duration: Buff.ENDLESS,
                    effects: new StardewValley.Buffs.BuffEffects()
                    {
                        Speed = { -2 },
                        Attack = { -3 },
                        Defense = { -3 }
                    },
                    isDebuff: true)
                {
                    description = ModEntry.Instance.Helper.Translation.Get("pass-out"),
                    displaySource = ModEntry.Instance.Helper.Translation.Get("pass-out-source"),
                    iconTexture = Textures.BuffSprites,
                    iconSheetIndex = 26
                }
            };
        }

        public static Buff GetBuffCopyByID(string id)
        {
            var result = SurvivalisticBuffsTemplates.Find(buff => buff.id == id);
            if (result == null)
            {
                ModEntry.Instance.Monitor.Log($"Unknown buff id sent to 'GetBuffCopyByID' function. Value: {id}.", LogLevel.Error);
                return result;
            }

            return new Buff(id: result.id, source: result.source, effects: result.effects)
            {
                displayName = result.displayName,
                displaySource = result.displaySource,
                description = result.description,
                iconTexture = result.iconTexture,
                iconSheetIndex = result.iconSheetIndex,
                millisecondsDuration = result.millisecondsDuration
            };
        }

        internal static class Names
        {
            public const string BuffIDTemplate = "SURV.{0}";

            public const string FullnessBuff = "Fulness";

            public const string HydratedBuff = "Hydrated";

            /// <summary>
            /// It looks kind weird, but VS 2022 spell checker can be really annoying, so to ignore it, I made a name like this.
            /// </summary>
            public const string HungerDeBuff = "Hunger";

            public const string ThirstDeBuff = "Thirst";

            public const string FaintDeBuff = "Faint";
        }

        public enum Codes
        {
            FullnessBuff = 0,

            HydratedBuff = 1,

            /// <summary>
            /// It looks kind weird, but VS 2022 spell checker can be really annoying, so to ignore it, I made a name like this.
            /// </summary>
            HungerDeBuff = 2,

            ThirstDeBuff = 3,

            FaintDeBuff = 4,
        }
    }
}
