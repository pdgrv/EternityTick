using UnityEngine;

namespace Eternity.Core
{
    public class PersistantData
    {
        public static int Level
        {
            get => PlayerPrefs.GetInt(nameof(Level), 1);
            set => PlayerPrefs.SetInt(nameof(Level), value);
        }
    }
}