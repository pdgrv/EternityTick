using System.Collections.Generic;
using Eternity.Core.Levels.Data;
using Eternity.Utils;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace Eternity.Core.Levels
{
    public enum RoomType
    {
        Root,
        End,
        EnemyEncounter
    }

    public class Room
    {
        public readonly RoomData RoomData;

        private readonly Dictionary<Vector2Int, CellView> _cells;

        private Room(RoomData roomData, Dictionary<Vector2Int, CellView> cells)
        {
            RoomData = roomData;
            _cells = cells;
        }

        public bool TryGetCell(Vector2Int pos, out CellView cellView) => _cells.TryGetValue(pos, out cellView);

        public static Room CreateRoomField(RoomData data, Random rnd, Transform container)
        {
            Dictionary<Vector2Int, CellView> cells = new();

            var aW = data.Width / 2;
            var aH = data.Height / 2;
            for (int i = -aW; i <= aW; i++)
            {
                for (int j = -aH; j <= aH; j++)
                {
                    var worldPos = data.CenterPos + new Vector2Int(i, j);
                    CreateAndAddCell(worldPos, data.CellReferences.GetEmptyCellView(rnd));
                }
            }

            if (data.RoomType is not RoomType.End)
            {
                var exitPos = data.GetExitPos();
                var exitDir = data.Exit.GetDirection();
                for (int i = 1; i <= Constants.Field.ConnectionLength; i++)
                {
                    var pos = exitPos + exitDir * i;
                    CreateAndAddCell(pos,data.CellReferences.ConnectionTemplate);
                }
            }

            return new Room(data, cells);

            void CreateAndAddCell(Vector2Int pos, CellView template)
            {
                float rndAngle90 = 90f * rnd.Next(1, 5);

                CellView cell = Object.Instantiate(template, pos.ToV3(),
                    Quaternion.Euler(0, 0, rndAngle90), container);
                cells.Add(pos, cell);
            }
        }
    }

    public class RoomData
    {
        public readonly RoomType RoomType;
        public readonly int Width;
        public readonly int Height;
        public readonly Vector2Int CenterPos;
        public readonly Side Enter;
        public readonly Side Exit;
        public readonly CellViewReferences CellReferences;

        public RoomData(RoomType roomType, Vector2Int centerPos, int width, int height, Side enter,
            Side exit, CellViewReferences cellReferences)
        {
            RoomType = roomType;
            CenterPos = centerPos;
            Width = width;
            Height = height;
            Enter = enter;
            Exit = exit;
            CellReferences = cellReferences;
        }

        public Vector2Int GetExitPos() => CenterPos + Exit.GetDirection() * new Vector2Int(Width / 2, Height / 2);
        public Vector2Int GetStartPos() => CenterPos + Enter.GetDirection() * new Vector2Int(Width / 2, Height / 2);
    }
}