using System.Reflection;
using Harmony;

// ReSharper disable UnusedMember.Global

namespace PilotHealthPopup
{
    public static class Main
    {
        public static void Init()
        {
            var harmony = HarmonyInstance.Create("io.github.mpstark.PilotHealthPopup");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
