using System;
using UnityEngine;

namespace NSFires
{
    public class Fire : MonoBehaviour, IFire
    {
        public FireStatus Status { get; private set; } = FireStatus.Inactive;
        public event Action<Fire> OnFireDeactivated;

        public void UpdateStatus(FireStatus newStatus)
        {
            Status = newStatus;

            if (newStatus == FireStatus.Inactive)
            {
                OnFireDeactivated?.Invoke(this);
            }
        }
    }

    [Serializable]
    public enum FireStatus
    {
        Active,
        Inactive
    }
}
