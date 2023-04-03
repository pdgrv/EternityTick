using Eternity.Core.Views;
using Eternity.Utils;
using UnityEngine;
using Random = System.Random;
using RangeInt = Eternity.Utils.RangeInt;

namespace Eternity.Core.Config
{
    [CreateAssetMenu(fileName = nameof(LevelsInfo), menuName = "ER/Level" + nameof(LevelsInfo))]
    public class LevelsInfo : ScriptableObject
    {
        [SerializeField] private LevelData[] levels;

        public LevelData GetLevelData(int levelIdx)
        {
            var targetLevel = levelIdx % levels.Length;
            return levels[targetLevel];
        }
    }

    [System.Serializable]
    public class LevelData
    {
        [SerializeField] private int roomCount;
        [SerializeField] private RangeInt roomWidthRange;
        [SerializeField] private RangeInt roomHeightRange;
        [SerializeField] private CellViewReferences cellViewReferences;

        public int RoomCount => roomCount;
        public RangeInt RoomWidthRange => roomWidthRange;
        public RangeInt RoomHeightRange => roomHeightRange;
        public CellViewReferences CellViewReferences => cellViewReferences;
    }

    [System.Serializable]
    public class CellViewReferences
    {
        [SerializeField] private CellView[] emptyTemplates;
        [SerializeField] private CellView connectionTemplate;
        [SerializeField] private CellView exitTemplate;

        public CellView GetEmptyCellView(Random rnd) => emptyTemplates.GetRandom(rnd);
        public CellView ConnectionTemplate => connectionTemplate;
        public CellView ExitTemplate => exitTemplate;
    }
}