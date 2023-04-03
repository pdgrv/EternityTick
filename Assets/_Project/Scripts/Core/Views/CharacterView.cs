using Eternity.Core.Level;
using Eternity.Core.Model;
using Eternity.Core.TickSystem;
using Eternity.Core.TickSystem.Commands;
using Eternity.Input;
using Eternity.Utils;
using UnityEngine;
using CharacterInfo = Eternity.Core.Config.CharacterInfo;

namespace Eternity.Core.Views
{
    public abstract class CharacterView : MonoBehaviour, ITickEntity
    {
        [SerializeField] private float moveDuration = 0.5f;
        
        public TickCommand CurrentCommand { get; private set; }
        public ICharacterInput Input { get; private set; }

        public Health Health { get; private set; }
        public Position Pos { get; private set; }

        private LevelMap _levelMap;

        public static CharacterView Create(CharacterInfo characterInfo, Vector2Int startPosition, ICharacterInput input,
            LevelMap level)
        {
            Health health = new Health(characterInfo.Config.MaxHealth);
            Position pos = new Position(startPosition);

            var instance = Instantiate(characterInfo.Template, startPosition.ToV3(), Quaternion.identity);
            instance.Initialize(health, pos, input, level);
            return instance;
        }

        public void Initialize(Health health, Position pos, ICharacterInput input, LevelMap levelMap)
        {
            Health = health;
            Pos = pos;
            Input = input;

            _levelMap = levelMap;

            Input.AxisInput += OnAxisInput;
            Input.Cancel += OnCancel;

            Health.HealthChanged += OnHealthChanged;
            Health.Die += OnDie;
            Pos.PosChanged += OnPosChanged;
        }

        public void ClearCommand()
        {
            if (CurrentCommand != null)
                CurrentCommand = null;
        }

        private void OnCancel()
        {
            ClearCommand();
        }

        private void OnAxisInput(Vector2Int direction)
        {
            if (CurrentCommand == null || CurrentCommand is MovementCommand)
                CurrentCommand = new MovementCommand(_levelMap, Pos, Pos.Pos + direction);
        }

        private Coroutine _movingCoroutine;
        private void OnPosChanged(Vector2Int targetPos)
        {
            if (_movingCoroutine != null)
            {
                StopCoroutine(_movingCoroutine);
                _movingCoroutine = null;
            }

            _movingCoroutine = StartCoroutine(transform.LerpMoving(targetPos.ToV3(), moveDuration));
        }

        private void OnHealthChanged(int value)
        { }

        private void OnDie()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Health.HealthChanged -= OnHealthChanged;
            Health.Die -= OnDie;
            Pos.PosChanged -= OnPosChanged;

            Input.AxisInput -= OnAxisInput;
            Input.Cancel -= OnCancel;
        }
    }
}