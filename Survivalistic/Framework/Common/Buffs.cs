using StardewModdingAPI;
using StardewValley;
using System.Linq;

namespace Survivalistic_Rebooted.Framework.Common
{
    public static class Buffs
    {
        public const string FullnessBuffName = "Fullness";

        public const string HydratedBuffName = "Hydrated";

        public const string HungerBuffName = "Hunger";

        public const string ThirstyBuffName = "Thirsty";

        public const string FaintingBuffName = "Fainting";

        public static void SetBuff(string name)
        {
            switch (name)
            {
                case FullnessBuffName:
                    Buff fullnessBuff = Game1.player.buffs.AppliedBuffs.Values.FirstOrDefault(i => i.source == "SURV_Fullness");

                    if (fullnessBuff == null || true)
                    {
                        fullnessBuff = new Buff("SURV_Fullness", "SURV_Fullness",
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
                        };

                        Game1.player.buffs.Apply(fullnessBuff);
                    }

                    break;

                case HydratedBuffName:
                    Buff hydratedBuff = Game1.player.buffs.AppliedBuffs.Values.FirstOrDefault(i => i.source == "SURV_Hydrated");

                    if (hydratedBuff == null)
                    {
                        hydratedBuff = new Buff("SURV_Hydrated", "SURV_Hydrated",
                                                duration: Buff.ENDLESS,
                                                effects: new StardewValley.Buffs.BuffEffects()
                                                {
                                                    MaxStamina = { 25 }
                                                })
                        {
                            description = ModEntry.Instance.Helper.Translation.Get("buff.hydrated.description"),
                            displaySource = ModEntry.Instance.Helper.Translation.Get("buff.hydrated.source"),
                            iconTexture = Textures.BuffSprites,
                            iconSheetIndex = 19,
                            millisecondsDuration = 60 * 1000
                        };

                        Game1.player.buffs.Apply(hydratedBuff);
                    }

                    break;

                case HungerBuffName:
                    Buff hungerBuff = Game1.player.buffs.AppliedBuffs.Values.FirstOrDefault(i => i.source == "SURV_Hunger");

                    if (hungerBuff == null)
                    {
                        hungerBuff = new Buff("SURV_Hunger", "SURV_Hunger",
                                                duration: Buff.ENDLESS, isDebuff: true,
                                                effects: new StardewValley.Buffs.BuffEffects()
                                                {
                                                    Defense = { -2 },
                                                    Attack = { -2 }
                                                })
                        {
                            description = ModEntry.Instance.Helper.Translation.Get("hunger-warning"),
                            displaySource = ModEntry.Instance.Helper.Translation.Get("hunger-source"),
                            iconTexture = Textures.BuffSprites,
                            iconSheetIndex = 6,
                            millisecondsDuration = 60 * 1000
                        };

                        Game1.player.buffs.Apply(hungerBuff);
                    }

                    break;

                case ThirstyBuffName:
                    Buff thirstyBuff = Game1.player.buffs.AppliedBuffs.Values.FirstOrDefault(i => i.source == "SURV_Thirsty");

                    if (thirstyBuff == null)
                    {
                        thirstyBuff = new Buff("SURV_Thirsty", "SURV_Thirsty",
                                                duration: Buff.ENDLESS, isDebuff: true,
                                                effects: new StardewValley.Buffs.BuffEffects()
                                                {
                                                    MaxStamina = { -30 },
                                                    Speed = { -1 }
                                                })
                        {
                            description = ModEntry.Instance.Helper.Translation.Get("thirsty-warning"),
                            displaySource = ModEntry.Instance.Helper.Translation.Get("thirsty-source"),
                            iconTexture = Textures.BuffSprites,
                            iconSheetIndex = 7,
                            millisecondsDuration = 60 * 1000
                        };

                        Game1.player.buffs.Apply(thirstyBuff);
                    }

                    break;

                case FaintingBuffName:
                    Buff faintingBuff = Game1.player.buffs.AppliedBuffs.Values.FirstOrDefault(i => i.source == "SURV_Fainting");

                    if (faintingBuff == null)
                    {
                        faintingBuff = new Buff("SURV_Fainting", "SURV_Fainting",
                                                duration: Buff.ENDLESS,
                                                effects: new StardewValley.Buffs.BuffEffects()
                                                {
                                                    Speed = { -2 },
                                                    Attack = { -3 },
                                                    Defense = { -3 }
                                                })
                        {
                            description = ModEntry.Instance.Helper.Translation.Get("pass-out"),
                            displaySource = ModEntry.Instance.Helper.Translation.Get("pass-out-source"),
                            iconTexture = Textures.BuffSprites,
                            iconSheetIndex = 26,
                            millisecondsDuration = 60 * 1000
                        };

                        Game1.player.buffs.Apply(faintingBuff);
                    }

                    break;

                default:
                    ModEntry.Instance.Monitor.Log($"Unknown buff name send to 'SetBuff' function. Value: {name}.", LogLevel.Error);

                    break;
            }
        }

        public static void RemoveBuff(string name)
        {
            switch (name)
            {
                case FullnessBuffName:
                    Game1.player.buffs.Remove("SURV_Fullness");
                    break;

                case HydratedBuffName:
                    Game1.player.buffs.Remove("SURV_Hydrated");
                    break;

                case HungerBuffName:
                    Game1.player.buffs.Remove("SURV_Hunger");
                    break;

                case ThirstyBuffName:
                    Game1.player.buffs.Remove("SURV_Thirsty");
                    break;

                case FaintingBuffName:
                    Game1.player.buffs.Remove("SURV_Fainting");
                    break;

                default:
                    ModEntry.Instance.Monitor.Log($"Unknown value send to 'RemoveBuff' function. Value: {name}.", LogLevel.Error);
                    break;
            }
        }

        public static void CallUpdateSettingBuff(string buffName, bool remove = false)
        {
            if (!remove && !Game1.player.hasBuff($"SURV_{buffName}"))
                SetBuff(buffName);
            else if (remove && Game1.player.hasBuff($"SURV_{buffName}"))
                RemoveBuff(buffName);
        }
    }
}
