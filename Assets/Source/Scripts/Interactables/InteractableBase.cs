using UnityEngine;

namespace NSInteractable
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        public InteractableStatus Status { get; private set; } = InteractableStatus.Inactive;

        public void UpdateStatus(InteractableStatus newStatus)
        {
            Status = newStatus;
        }
    }
}
