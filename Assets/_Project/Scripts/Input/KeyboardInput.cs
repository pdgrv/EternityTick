using System;
using UnityEngine;

namespace Eternity.Input
{
    public class KeyboardInput : MonoBehaviour, ICharacterInput
    {
        public event Action<Vector2Int> AxisInput;
        public event Action<int> UseSlot;
        public event Action Interact;
        public event Action Cancel;

        private void Update()
        {
            HandleAxisInput();
            HandleActionsInput();
        }

        private void HandleAxisInput()
        {
            int x = Mathf.RoundToInt(UnityEngine.Input.GetAxisRaw("Horizontal"));
            int y = Mathf.RoundToInt(UnityEngine.Input.GetAxisRaw("Vertical"));

            if (x != 0 || y != 0)
            {
                AxisInput?.Invoke(new Vector2Int(x, y));
            }
        }

        private void HandleActionsInput()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
                UseSlot?.Invoke(0);
            else if (UnityEngine.Input.GetKeyDown(KeyCode.W))
                UseSlot?.Invoke(1);
            else if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                Interact?.Invoke();
            else if (UnityEngine.Input.GetKeyDown(KeyCode.R))
                Cancel?.Invoke();
        }
    }
}