using NSCharacters.Movement;
using UnityEngine;

namespace NSCharacters
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IMoveable
    {
        [SerializeField] private float _speed;

        private IMover _mover;

        public Transform Transform => this.transform;
        public Rigidbody Rigidbody { get; private set; }
        public float Speed => _speed;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void Update() =>
            _mover?.Update(Time.deltaTime);

        public void SetMover(IMover mover)
        {
            _mover?.StopMove();
            _mover = mover;
            _mover.StartMove();
        }
    }
}