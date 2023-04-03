using System;
using UnityEngine;

namespace Eternity.Input
{
    public interface ICharacterInput
    {
        public event Action<Vector2Int> AxisInput;
        public event Action<int> UseSlot;
        public event Action Interact;
        public event Action Cancel;
    }
}