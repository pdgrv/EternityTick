using Eternity.Core.Views;
using UnityEngine;

namespace Eternity.Core.Config
{
    [CreateAssetMenu(fileName = nameof(CharacterInfo), menuName = "ER/Characters" + nameof(CharacterInfo))]
    public class CharacterInfo : ScriptableObject
    {
        [SerializeField] private CharacterConfig config;
        [SerializeField] private CharacterView template;

        public CharacterConfig Config => config;
        public CharacterView Template => template;
    }
    
    [System.Serializable]
    public class CharacterConfig
    {
        public int Damage;
        public int MaxHealth;
    }
}