﻿using System;
using System.Collections.Generic;
using System.Linq;
using Eternity.Core.Config;
using Eternity.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;
using RangeInt = Eternity.Utils.RangeInt;

namespace Eternity.Core.Level
{
    public class LevelMap : MonoBehaviour
    {
        public event Action<Room> RoomChanged; 

        [SerializeField] private LevelsInfo levels;

        private List<Room> _rooms = new();
        public IReadOnlyList<Room> Rooms => _rooms;

        public Room CurrentRoom => Rooms[_currentPlayerRoom];

        private int _currentPlayerRoom = 0;

        public void MoveToNextRoom()
        {
            if (CurrentRoom.RoomData.RoomType is RoomType.End)
            {
                Debug.Log("Complete Level!");
                PersistantData.Level++;
                
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
                return;
            }
            
            _currentPlayerRoom++;
            RoomChanged?.Invoke(CurrentRoom);
            
            Debug.Log($"Change room to {_currentPlayerRoom}");
        }
        
        public void CreateLevel(CoreContext ctx)
        {
            var levelData = levels.GetLevelData(ctx.CurrentLevel);

            CreateRoom(ctx, levelData, null, RoomType.Root);

            for (int i = 1; i < levelData.RoomCount - 1; i++)
            {
                var prevRoom = _rooms[i - 1];
                CreateRoom(ctx, levelData, prevRoom, RoomType.EnemyEncounter);
            }

            CreateRoom(ctx, levelData, _rooms[^1], RoomType.End);
        }

        private void CreateRoom(CoreContext ctx, LevelData levelData, Room prevRoom, RoomType roomType)
        {
            var w = GetRandomOddNumber(levelData.RoomWidthRange, ctx.Rnd);
            var h = GetRandomOddNumber(levelData.RoomHeightRange, ctx.Rnd);

            var centerPos = prevRoom == null
                ? Vector2Int.zero
                : prevRoom.RoomData.GetExitPos() + prevRoom.RoomData.Exit.GetDirection() *
                new Vector2Int(w / 2 + Constants.FieldConnectionLength + 1,
                    h / 2 + Constants.FieldConnectionLength + 1);

            Side enterSide = prevRoom == null ? Side.Down : prevRoom.RoomData.Exit.Invert();
            
            var exitPool = new HashSet<Side> { Side.Left, Side.Top, Side.Right };
            exitPool.Remove(enterSide);

            RoomData roomData =
                new RoomData(roomType, centerPos, w, h, enterSide, exitPool.ToArray().GetRandom(ctx.Rnd),
                    levelData.CellViewReferences);

            var container = new GameObject($"Room_{_rooms.Count + 1}").transform;
            container.SetParent(transform);
            
            var room = Room.GenerateCellsField(roomData, ctx.Rnd, container);
            _rooms.Add(room);
        }

        private static int GetRandomOddNumber(RangeInt range, Random rnd)
        {
            int number = rnd.Next(range.Min, range.Max);
            if (number % 2 == 0)
                number--;
            return number;
        }
    }
}