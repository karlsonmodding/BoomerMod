using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BoomerMod
{
    class EntryPoint : MelonMod
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
}
