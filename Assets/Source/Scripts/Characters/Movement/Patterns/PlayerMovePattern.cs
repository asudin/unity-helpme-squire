using UnityEngine;

namespace NSCharacters.Movement
{
    public class PlayerMovePattern : IMover
    {
        private bool _isMoving;
        private IMoveable _moveable;
        private DynamicJoystick _joystick;
        private float _speed;

        public PlayerMovePattern(IMoveable moveable, PlayerMovementProperties properties)
        {
            _moveable = moveable;
            _joystick = properties.Joystick;
            _speed = properties.Speed;
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
                _joystick.Horizontal * _speed,
                _moveable.Rigidbody.velocity.y,
                _joystick.Vertical * _speed); 
        }
    }
}