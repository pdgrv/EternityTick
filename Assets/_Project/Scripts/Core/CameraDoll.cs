using Eternity.Core.Level;
using Eternity.Utils;
using UnityEngine;

namespace Eternity.Core
{
    public class CameraDoll : MonoBehaviour
    {
        [SerializeField] private float changeRoomMovingTime;
        
        private LevelMap _levelMap;
        
        public void Init(LevelMap levelMap)
        {
            _levelMap = levelMap;
            
            levelMap.RoomChanged += OnRoomChanged;
        }

        private Coroutine _moveCoroutine;
        
        private void OnRoomChanged(Room targetRoom)
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
                _moveCoroutine = null;
            }
            
            _moveCoroutine = StartCoroutine(transform.LerpMoving(targetRoom.RoomData.CenterPos.ToV3(), changeRoomMovingTime));
        }

        private void OnDestroy()
        {
            _levelMap.RoomChanged -= OnRoomChanged;
        }
    }
}