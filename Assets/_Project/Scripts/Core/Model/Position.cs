using System;
using UnityEngine;

namespace Eternity.Core.Model
{
    public class Position
    {
        public event Action<Vector2Int> PosChanged;

        public Vector2Int Pos { get; private set; }

        public Position(Vector2Int pos)
        {
            Pos = pos;
        }

        public void ChangePos(Vector2Int targetPos)
        {
            if (Pos == targetPos)
                return;

            Pos = targetPos;
            PosChanged?.Invoke(Pos);
        }
    }
}