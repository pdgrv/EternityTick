using System;
using System.Collections;
using System.Collections.Generic;
using Eternity.Core.Config;
using UnityEngine;

namespace Eternity.Core.TickSystem
{
    public class TickOrchestrationSystem : MonoBehaviour
    {
        public event Action<float> TickStartWithTime;

        private const float IncreasingPercentPerLevel = 0.1f;

        private Coroutine _tickCoroutine;
        private IReadOnlyList<ITickEntity> _orderedTickEntities;

        public void Init(IReadOnlyList<ITickEntity> orderedEntities)
        {
            _orderedTickEntities = orderedEntities;
        }

        public void StartTickProcess(int level)
        {
            StopTickProcess();

            _tickCoroutine = StartCoroutine(TickProcess(level));
        }

        public void StopTickProcess()
        {
            if (_tickCoroutine != null)
            {
                StopCoroutine(_tickCoroutine);
                _tickCoroutine = null;
            }
        }

        private IEnumerator TickProcess(int level)
        {
            float tickTime = Mathf.Max(Constants.BaseTimePerTick * (1f - IncreasingPercentPerLevel * (level - 1)),
                Constants.MinTimePerTick);

            while (true)
            {
                TickStartWithTime?.Invoke(tickTime);
                Debug.Log($"TickStartWithTime {tickTime}");
                yield return new WaitForSeconds(tickTime);
                
                foreach (var tickEntity in _orderedTickEntities)
                {
                    tickEntity.CurrentCommand?.Execute();
                }
            }
        }
    }
}