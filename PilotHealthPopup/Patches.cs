using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using BattleTech;
using Harmony;
using JetBrains.Annotations;

namespace PilotHealthPopup
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [HarmonyPatch(typeof(Pilot), "InjurePilot")]
    public static class Pilot_InjurePilot_Patch
    {
        public static void Postfix(Pilot __instance)
        {
            if (__instance.Injuries == __instance.TotalHealth)
                return;

            if (__instance.Injuries == 0)
                return;

            __instance.ParentActor?.Combat.MessageCenter.PublishMessage(new AddSequenceToStackMessage(
                new ShowActorInfoSequence(__instance.ParentActor, $"HEALTH: {__instance.TotalHealth - __instance.Injuries}/{__instance.TotalHealth}", 
                    FloatieMessage.MessageNature.Debuff, true)));
        }
    }

    public static class Patches
    {
        public static void Init()
        {
            var harmony = HarmonyInstance.Create("io.github.mpstark.EnemyPilotHealthPopup");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
