using UnityEngine;

namespace NSCharacters.Movement
{
    public class PlayerMovePattern : IMover
    {
        private bool _isMoving;
        private IMoveable _moveable;
        private DynamicJoystick _joystick;

        public PlayerMovePattern(IMoveable moveable, DynamicJoystick joystick)
        {
            _moveable = moveable;
            _joystick = joystick;
        }

        public void StartMove() =>
            _isMoving = true;

        public void StopMove() =>
            _isMoving = false;

        public void Update(float deltaTime)
        {
            if (_isMoving == false)
                return;

            _moveable.Rigidbody.velocity = new Vector3(
                _joystick.Horizontal * _moveable.Speed,
                _moveable.Rigidbody.velocity.y,
                _joystick.Vertical * _moveable.Speed); 
        }
    }
}