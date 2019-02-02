using BattleTech;
using Harmony;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace PilotHealthPopup.Patches
{
    [HarmonyPatch(typeof(Pilot), "InjurePilot")]
    public static class Pilot_InjurePilot_Patch
    {
        public static void Postfix(Pilot __instance)
        {
            if (__instance.Injuries == __instance.TotalHealth || __instance.Injuries == 0)
                return;

            __instance.ParentActor?.Combat.MessageCenter.PublishMessage(new AddSequenceToStackMessage(
                new ShowActorInfoSequence(__instance.ParentActor,
                    $"HEALTH: {__instance.TotalHealth - __instance.Injuries}/{__instance.TotalHealth}",
                    FloatieMessage.MessageNature.Debuff, true)));
        }
    }
}
