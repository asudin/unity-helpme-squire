using NSCharacters;
using NSCharacters.PlayerInput;
using NSFactories;
using UnityEngine;

namespace NSArena
{
    public class ArenaBootstrapper : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private FireFactoryData _firesFactoryData;
        [SerializeField] private PotionFactoryData _potionFactoryData;

        private PlayerMovementInput _playerMovementInput;
        private FireFactory _firesFactory;
        private PotionFactory _potionFactory;
        private float _easyDifficultSpawnTimer = 6f;


        private void Awake()
        {
            _playerMovementInput = _player.GetComponent<PlayerMovementInput>();
            _playerMovementInput.UpdateGameState(ArenaState.Paused);
            _firesFactory = new FireFactory(_firesFactoryData, _easyDifficultSpawnTimer);
            _potionFactory = new PotionFactory(_potionFactoryData, _easyDifficultSpawnTimer);
        }

        private void Start()
        {
            _firesFactory.Initialize();
            _potionFactory.Initialize();
        }
    }
}