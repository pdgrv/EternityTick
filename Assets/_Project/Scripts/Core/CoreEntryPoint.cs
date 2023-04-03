using System.Collections.Generic;
using Eternity.Core.Level;
using Eternity.Core.TickSystem;
using Eternity.Core.Views;
using Eternity.Input;
using UnityEngine;
using CharacterInfo = Eternity.Core.Config.CharacterInfo;

namespace Eternity.Core
{
    public class CoreEntryPoint : MonoBehaviour
    {
        [SerializeField] private LevelMap levelMap;
        [SerializeField] private CharacterInfo playerInfo;

        [SerializeField] private TickOrchestrationSystem tickSystem;
        
        [Header("I/O")]
        [SerializeField] private KeyboardInput kbInput;
        [SerializeField] private CameraDoll camera;

        private void Awake()
        {
            CoreContext ctx = new CoreContext(PersistantData.Level);

            levelMap.CreateLevel(ctx);
            
            camera.Init(levelMap);

            var player = CharacterView.Create(playerInfo, levelMap.CurrentRoom.RoomData.CenterPos, kbInput, levelMap);

            List<ITickEntity> orderedEntities = new List<ITickEntity>
            {
                player
            };

            tickSystem.Init(orderedEntities);
            tickSystem.StartTickProcess(ctx.CurrentLevel);
        }
    }
}