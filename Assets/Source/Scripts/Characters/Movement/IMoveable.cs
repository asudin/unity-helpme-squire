using UnityEngine;

namespace NSCharacters.Movement
{
    public interface IMoveable
    {
        Transform Transform { get; }
        Rigidbody Rigidbody { get; }
        float Speed { get; }
    }
}