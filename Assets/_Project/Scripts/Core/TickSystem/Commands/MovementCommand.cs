using Eternity.Core.Level;
using Eternity.Core.Model;
using Eternity.Core.Views;
using UnityEngine;

namespace Eternity.Core.TickSystem.Commands
{
    public class MovementCommand : TickCommand
    {
        private readonly LevelMap _levelMap;
        private readonly Position _target;
        private readonly Vector2Int _targetPosition;

        public MovementCommand(LevelMap levelMap, Position target, Vector2Int targetPosition)
        {
            _levelMap = levelMap;
            _target = target;
            _targetPosition = targetPosition;
        }

        public override void Execute()
        {
            Move(_levelMap.CurrentRoom, _target, _targetPosition);
        }

        public void Move(Room room, Position target, Side side, int range = 1)
            => Move(room, target, target.Pos + side.GetDirection() * range);

        public void Move(Room room, Position target, Vector2Int targetPosition)
        {
            if (room.IsCompleted)
            {
                var exitPos = room.RoomData.GetExitPos();
                var nextRoomConnectionPos = exitPos + room.RoomData.Exit.GetDirection();

                if (target.Pos == exitPos && nextRoomConnectionPos == targetPosition)
                {
                    _levelMap.MoveToNextRoom();
                    targetPosition = _levelMap.CurrentRoom.RoomData.GetEnterPos();
                }
            }

            if (room.TryGetCell(targetPosition, out CellView targetCell))
            {
                if (targetCell.IsEmpty)
                {
                    if (room.TryGetCell(target.Pos, out CellView curCell))
                        curCell.IsEmpty = true;

                    target.ChangePos(targetPosition);
                    targetCell.IsEmpty = false;
                }
            }
        }
    }
}