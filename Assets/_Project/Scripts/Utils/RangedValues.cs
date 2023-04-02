using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Eternity.Utils
{
    [Serializable]
    public struct RangeFloat
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        public float RandomValue => Random.Range(_min, _max);
        public float AverageValue => (_min + _max) / 2;
        public float Min => _min;
        public float Max => _max;

        public RangeFloat(float min, float max)
        {
            _min = min;
            _max = max;
        }
        
        public bool IsInRange(float value) => value >= _min && value <= _max;
    }
    
    [Serializable]
    public struct RangeInt
    {
        [SerializeField] private int _min;
        [SerializeField] private int _max;

        public int RandomValue => Random.Range(_min, _max);
        public int AverageValue => (_min + _max)  / 2;
        public int Min => _min;
        public int Max => _max;

        public RangeInt(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public bool IsInRange(int value) => value >= _min && value <= _max;
    }
}