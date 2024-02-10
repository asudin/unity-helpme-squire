using NSCharacters;
using NSCharacters.Movement;
using UnityEngine;

namespace NSArena
{
    public class ArenaBootstrapper : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private DynamicJoystick _joystick;

        private bool _isPlayerMoving = false;

        private void Awake()
        {
            _player.SetMover(new NoMovePattern());
        }

        private void FixedUpdate()
        {
            if (Input.touchCount > 0 || Input.GetMouseButton(0))
            {
                HandlePlayerMovementStart();
            }
            else
            {
                HandlePlayerMovementStop();
            }
        }

        private void HandlePlayerMovementStart()
        {
            if (_isPlayerMoving)
                return;

            _player.SetMover(new PlayerMovePattern(_player, _joystick));
            _isPlayerMoving = true;
        }

        private void HandlePlayerMovementStop()
        {
            _player.SetMover(new NoMovePattern());
            _isPlayerMoving = false;
        }
    }
}