using UnityEngine;

namespace NSInteractable.Consumables
{
    public class Potion : MonoBehaviour, IInteractable
    {
        public InteractableStatus Status { get; private set; } = InteractableStatus.Inactive;

        public void UpdateStatus(InteractableStatus newStatus)
        {
            Status = newStatus;
        }
    }
}
