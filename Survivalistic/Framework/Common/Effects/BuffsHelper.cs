using static Survivalistic_Rebooted.Models.SurvivalisticBuffs;

namespace Survivalistic_Rebooted.Framework.Common.Effects
{
    public static class BuffsHelper
    {
        public static string GetBuffNameByCode(Codes buff)
        {
            return buff switch
            {
                Codes.FullnessBuff => Names.FullnessBuff,
                Codes.HydratedBuff => Names.HydratedBuff,
                Codes.HungerDeBuff => Names.HungerDeBuff,
                Codes.ThirstDeBuff => Names.ThirstDeBuff,
                _ => Names.FaintDeBuff,
            };
        }

        public static string GetBuffIDByCode(Codes buff)
        {
            return buff switch
            {
                Codes.FullnessBuff => string.Format(Names.BuffIDTemplate, Names.FullnessBuff),
                Codes.HydratedBuff => string.Format(Names.BuffIDTemplate, Names.HydratedBuff),
                Codes.HungerDeBuff => string.Format(Names.BuffIDTemplate, Names.HungerDeBuff),
                Codes.ThirstDeBuff => string.Format(Names.BuffIDTemplate, Names.ThirstDeBuff),
                _ => string.Format(Names.BuffIDTemplate, Names.FaintDeBuff),
            };
        }
    }
}
