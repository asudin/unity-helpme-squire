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
        [SerializeField] private InteractableObjectFactoryData _firesFactoryData;
        [SerializeField] private InteractableObjectFactoryData _potionFactoryData;

        private PlayerMovementInput _playerMovementInput;
        private InteractableObjectFactory<Fire> _firesFactory;
        private InteractableObjectFactory<Potion> _potionFactory;

        private void Awake()
        {
            _playerMovementInput = _player.GetComponent<PlayerMovementInput>();
            _playerMovementInput.UpdateGameState(ArenaState.Paused);
            _firesFactory = new InteractableObjectFactory<Fire>(_firesFactoryData);
            _potionFactory = new InteractableObjectFactory<Potion>(_potionFactoryData);
        }

        private void Start()
        {
            SpawnInteractables(_firesFactoryData, _firesFactory);
            SpawnInteractables(_potionFactoryData, _potionFactory);
        }

        private void SpawnInteractables<T>(InteractableObjectFactoryData config, IFactory<T> factory) where T : MonoBehaviour, IInteractable
        {
            foreach (var point in config.SpawnPoints)
            {
                if (factory.Pool.TryGetPooledObject(out var interactable))
                {
                    factory.ActivateItem(interactable, point.position, point.rotation);
                }
            }
        }
    }
}