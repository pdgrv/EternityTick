using Eternity.Core.Levels;
using UnityEngine;

namespace Eternity.Core
{
    public class CoreEntryPoint : MonoBehaviour
    {
        [SerializeField] private Level level;

        private void Awake()
        {
            CoreContext ctx = new CoreContext(PersistantData.Level);
        
            level.CreateLevel(ctx);
        }
    }
}