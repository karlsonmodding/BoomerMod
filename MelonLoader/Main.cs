using System.Collections;
using UnityEngine;
using HarmonyLib;
using UnityEngine.SceneManagement;
using MelonLoader;
using System;

namespace BoomerMod
{
    [HarmonyPatch(typeof(PlayerMovement), "Start")]
    public class PlayerMovement_Start
    {
        public static bool Prefix(PlayerMovement __instance)
        {
            __instance.spawnWeapon = Prefabs.CreateBoomer();
            return true;
        }
    }
    
    [HarmonyPatch(typeof(Game), "Win")]
    public class Game_Win
    {
        public static bool Prefix(Game __instance)
        {
			__instance.playing = false;
			Timer.Instance.Stop();
			Time.timeScale = 0.05f;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			UIManger.Instance.WinUI(true);
			float timer = Timer.Instance.GetTimer();
			int num = int.Parse(SceneManager.GetActiveScene().name[0].ToString() ?? "");
			int num2;
			if (int.TryParse(SceneManager.GetActiveScene().name.Substring(0, 2) ?? "", out num2))
			{
				num = num2;
			}
			float num3 = CustomSave.times[num];
			if (timer < num3 || num3 == 0f)
			{
				CustomSave.times[num] = timer;
				CustomSave.Save();
			}
			MonoBehaviour.print("time has been saved as: " + Timer.Instance.GetFormattedTime(timer) + " on BoomerModSave");
			__instance.done = true;
			return false;
        }
    }

    

    [HarmonyPatch(typeof(Lobby), "Start")]
    public class Lobby_Start
    {
        public static void Postfix()
        {
            CustomSave.Load();
            MelonCoroutines.Start(MainMenu());
        }

        public static readonly string[] levelNames = new string[] { "Tutorial", "Sandbox0", "Sandbox1", "Sandbox2", "Escape0", "Escape1", "Escape2", "Escape3", "Sky0", "Sky1", "Sky2" };

        public static IEnumerator MainMenu()
        {
            yield return new WaitForEndOfFrame();
            GameObject playSection = null;
            foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {
                if (go.name == "UI" && go.transform.childCount == 5)
                {
                    playSection = go.transform.Find("Play").gameObject;
                    break;
                }
            }
            if (playSection == null)
                yield break;
            playSection.SetActive(true);
            int i = 0;
            foreach (var s in levelNames)
            {
                var parent = playSection.transform.Find(s);
                var text = UnityEngine.Object.Instantiate(parent.Find("Text (TMP) (1)").gameObject, parent);
                text.transform.position = new Vector3(7.829f, 16.419f, 191.500f);
                text.transform.localPosition = new Vector3(0f, -42.949f, 0.017f);
                text.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                text.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                var tmp = text.GetComponent<TMPro.TextMeshProUGUI>();
                tmp.text = $"<size=20>[BM] " + (CustomSave.times[i] == 0 ? "[NO RECORD]" : TimeSpan.FromSeconds(CustomSave.times[i]).ToString(@"mm\:ss\:ff"));
                tmp.fontStyle = TMPro.FontStyles.Normal;
                i++;
            }
            playSection.SetActive(false);
        }
    }
}
