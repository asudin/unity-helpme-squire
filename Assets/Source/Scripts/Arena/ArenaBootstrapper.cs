using NSCharacters;
using NSCharacters.PlayerInput;
using NSFactories;
using UnityEngine;

namespace NSArena
{
    public class ArenaBootstrapper : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private FiresFactoryData _firesFactoryData;

        private PlayerMovementInput _playerMovementInput;
        private FiresFactory _firesFactory;
        private float _easyDifficultSpawnTimer = 6f;


        private void Awake()
        {
            _playerMovementInput = _player.GetComponent<PlayerMovementInput>();
            _playerMovementInput.UpdateGameState(ArenaState.Paused);
            _firesFactory = new FiresFactory(_firesFactoryData, _easyDifficultSpawnTimer);
        }

        private void Start()
        {
            _firesFactory.Initialize();
        }
    }
}