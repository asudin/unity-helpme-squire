namespace NSInteractable
{
    public interface  IInteractable
    {
        InteractableStatus Status { get; }

        void UpdateStatus(InteractableStatus newStatus);
    }
}
