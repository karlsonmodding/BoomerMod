using System;
using UML;
using UnityEngine;
using HarmonyLib;

namespace BoomerMod
{
    [ModInfo(GUID, "BoomerMod", "devilExE", "1.0.0")]
    class EntryPoint : IMod
    {
        public const string GUID = "me.devilexe.boomermod";

        public static GameObject UML;
        public static BM_MonoHooks MB_Hooks;

        void IMod.Start()
        {
            Harmony harmony = new Harmony(GUID);
            harmony.PatchAll();
            UML = GameObject.Find("KarlsonLoader_MonoHooks");
            MB_Hooks = UML.AddComponent<BM_MonoHooks>();
        }


        void IMod.OnGUI() { }
        void IMod.FixedUpdate(float fixedDeltaTime) { }
        void IMod.OnApplicationQuit() { }
        void IMod.Update(float deltaTime) { }
    }
}
