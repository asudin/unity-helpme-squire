using NSCharacters.Movement;
using NSHandlers;
using UnityEngine;

namespace NSCharacters.PlayerInput
{
    public abstract class PlayerMovementInputBase : MonoBehaviour
    {
        [SerializeField] protected PlayerMovementProperties _properties;

        private IPlayerMovementHandler _playerMovementHandler;
        private ArenaState _currentState;

        protected void Awake()
        {
            _playerMovementHandler = CreateMovementHandler();
        }

        private void FixedUpdate()
        {
            if (_currentState == ArenaState.Paused)
                return;

            var isScreenTouched = IsPlayerScreenTouching();
    
            if (isScreenTouched)
            {
                _playerMovementHandler.Move();
            }
            else
            {
                _playerMovementHandler.Stop();
            }

            _playerMovementHandler.Update(Time.deltaTime);
        }

        protected abstract IPlayerMovementHandler CreateMovementHandler();

        protected abstract bool IsPlayerScreenTouching();

        public void UpdateGameState(ArenaState state) =>
            _currentState = state;
    }
}
