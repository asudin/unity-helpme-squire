using NSCharacters;
using NSCharacters.PlayerInput;
using NSFactories;
using NSInteractable;
using NSInteractable.Consumables;
using NSInteractable.Fires;
using UnityEngine;

namespace NSArena
{
    public class ArenaBootstrapper : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private InteractableFactoryData _firesFactoryData;
        [SerializeField] private InteractableFactoryData _potionFactoryData;

        private PlayerMovementInput _playerMovementInput;
        private InteractableFactory<Fire> _firesFactory;
        private InteractableFactory<Potion> _potionFactory;

        private void Awake()
        {
            _playerMovementInput = _player.GetComponent<PlayerMovementInput>();
            _playerMovementInput.GetGameState(ArenaState.Paused);
            _firesFactory = new InteractableFactory<Fire>(_firesFactoryData);
            _potionFactory = new InteractableFactory<Potion>(_potionFactoryData);
        }

        private void Start()
        {
            SpawnInteractables(_firesFactory);
            SpawnInteractables(_potionFactory);
        }

        private void SpawnInteractables<T>(IFactory<T> factory) where T : MonoBehaviour, IInteractable =>
             factory.ActivateItems();
    }
}