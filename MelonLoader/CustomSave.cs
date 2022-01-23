using UnityEngine;

namespace BoomerMod
{
    class CustomSave
    {
        public static float[] times;
        public static void Load()
        {
            if(!PlayerPrefs.HasKey("BoomerModSave"))
            {
                times = new float[11] { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
                Save();
            }
            times = SaveManager.Instance.Deserialize<float[]>(PlayerPrefs.GetString("BoomerModSave"));
        }
        public static void Save()
        {
            PlayerPrefs.SetString("BoomerModSave", SaveManager.Instance.Serialize(times));
        }
    }
}
