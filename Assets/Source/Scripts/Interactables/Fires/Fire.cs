using UnityEngine;

namespace NSInteractable.Fires
{
    public class Fire : MonoBehaviour, IInteractable
    {
        public InteractableStatus Status { get; private set; } = InteractableStatus.Inactive;

        public void UpdateStatus(InteractableStatus newStatus)
        {
            Status = newStatus;
        }
    }
}
