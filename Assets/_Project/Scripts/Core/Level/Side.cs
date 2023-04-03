using System;
using UnityEngine;

namespace Eternity.Core.Level
{
    public enum Side
    {
        Top,
        Right,
        Left,
        Down,
        TopRight,
        TopLeft,
        DownRight,
        DownLeft
    }

    public static class SideHelper
    {
        public static Vector2Int GetDirection(this Side side)
        {
            return side switch
            {
                Side.Top => Vector2Int.up,
                Side.Right => Vector2Int.right,
                Side.Down => Vector2Int.down,
                Side.Left => Vector2Int.left,
                Side.TopRight => Vector2Int.up + Vector2Int.right,
                Side.TopLeft => Vector2Int.up + Vector2Int.left,
                Side.DownRight => Vector2Int.down + Vector2Int.right,
                Side.DownLeft => Vector2Int.down + Vector2Int.left,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
        }

        public static Vector2Int GetDirectionInverted(this Side side) => side.Invert().GetDirection();

        public static Side Invert(this Side side)
        {
            return side switch
            {
                Side.Top => Side.Down,
                Side.Right => Side.Left,
                Side.Down => Side.Top,
                Side.Left => Side.Right,
                Side.TopRight => Side.DownLeft,
                Side.TopLeft => Side.DownRight,
                Side.DownRight => Side.TopLeft,
                Side.DownLeft => Side.TopRight,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
        }
    }
}