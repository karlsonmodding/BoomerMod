using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UML;
using UnityEngine;
using UnityEngine.SceneManagement;

[assembly: MelonInfo(typeof(BoomerMod.EntryPointMelon), "BoomerMod", "1.0.0", "devilExE")]
[assembly: MelonGame("Dani", "Karlson")]
namespace BoomerMod
{
    class EntryPointMelon : MelonMod
    {
        public override void OnApplicationLateStart()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "MainMenu" && !Prefabs.Initialized)
            {
                SceneManager.LoadScene("0Tutorial");
                return;
            }
            if (scene.name == "0Tutorial" && !Prefabs.Initialized)
            {
                Prefabs.LoadPrefabs();
                return;
            }
        }
    }

    [ModInfo(GUID, "BoomerMod", "1.0.0", "devilexe")]
    class EntryPointUML : IMod
    {
        public const string GUID = "me.devilexe.boomermod";

        public static GameObject UML;
        public static BM_MonoHooks MB_Hooks;

        void IMod.Start()
        {
            CompatibilityModule.IsMelonLoader = false;
            HarmonyLib.Harmony harmony = new HarmonyLib.Harmony(GUID);
            harmony.PatchAll();
            UML = GameObject.Find("KarlsonLoader_MonoHooks");
            MB_Hooks = UML.AddComponent<BM_MonoHooks>();
            CompatibilityModule.prefabSet = new PrefabUMLCompatibility();
        }

        void IMod.FixedUpdate(float fixedDeltaTime) { }
        void IMod.OnApplicationQuit() { }
        void IMod.OnGUI() { }
        void IMod.Update(float deltaTime) { }
    }

    class CompatibilityModule
    {
        public static bool IsMelonLoader = true;
        public static IPrefabsSet prefabSet;
        public static object StartCoroutine(IEnumerator coroutine)
        {
            if(IsMelonLoader)
                return MelonCoroutines.Start(coroutine);
            return EntryPointUML.MB_Hooks.StartCoroutine(coroutine);
        }
    }

    interface IPrefabsSet
    {
        GameObject CreateBoomer();
    }
}
