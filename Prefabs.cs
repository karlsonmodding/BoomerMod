using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoomerMod
{
    public class Prefabs
    {

        public static bool Initialized { get; private set; } = false;

        public static void LoadPrefabs()
        {
            if (Initialized)
                return;
            CompatibilityModule.prefabSet = new PrefabCompatibility();
            foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {
                if (go.name == "Boomer")
                {
                    boomer = UnityEngine.Object.Instantiate(go);
                    boomer.name = "BoomerMod-Instance Boomer";
                    UnityEngine.Object.DontDestroyOnLoad(boomer);
                    boomer.SetActive(false);
                    continue;
                }   
            }
            SceneManager.LoadScene("MainMenu");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Initialized = true;
        }

        private static GameObject boomer;
        public static GameObject CreateBoomer()
        {
            GameObject go = UnityEngine.Object.Instantiate(boomer);
            go.name = "Boomer";
            go.SetActive(true);
            return go;
        }
    }

    public class PrefabCompatibility : IPrefabsSet
    {
        public GameObject CreateBoomer()
        {
            return Prefabs.CreateBoomer();
        }
    }

    public class PrefabUMLCompatibility : IPrefabsSet
    {
        public GameObject CreateBoomer()
        {
            return KarlsonLoader.Prefabs.NewBoomer();
        }
    }

}
