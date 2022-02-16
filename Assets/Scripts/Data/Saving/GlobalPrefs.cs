using UnityEngine;

namespace Data.Saving
{
    public static class GlobalPrefs
    {
        public static int BestScore {
            get => PlayerPrefs.GetInt("BestScore", 0);
            set => PlayerPrefs.SetInt("BestScore", value);
        }
        
        public static float SoundValue {
            get => PlayerPrefs.GetFloat("SoundValue", 1f);
            set => PlayerPrefs.SetFloat("SoundValue", value);
        }
        
        public static float MusicValue {
            get => PlayerPrefs.GetFloat("MusicValue", 1f);
            set => PlayerPrefs.SetFloat("MusicValue", value);
        }
    }
}