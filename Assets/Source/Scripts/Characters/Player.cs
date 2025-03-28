using NSCharacters.Movement;
using UnityEngine;

namespace NSCharacters
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IMoveable
    {
        public Transform Transform => this.transform;
        public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
    }
}